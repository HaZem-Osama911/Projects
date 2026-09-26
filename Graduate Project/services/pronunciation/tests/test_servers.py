import importlib.util
import io
import sys
import types
import unittest
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1]


def load_server(name, server_path, dependency_name):
    dependency = types.ModuleType(dependency_name)
    dependency.check_pronunciation = lambda letter, path: {
        "is_correct": True,
        "expected": letter,
        "spoken": letter,
        "_temporary_path": path,
    }
    sys.modules[dependency_name] = dependency

    spec = importlib.util.spec_from_file_location(name, server_path)
    module = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(module)
    module.app.config.update(TESTING=True)
    return module


class PronunciationServerTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.arabic = load_server(
            "arabic_server",
            ROOT / "arabic" / "server.py",
            "speech_to_text",
        )
        cls.english = load_server(
            "english_server",
            ROOT / "english" / "server.py",
            "speech_to_text",
        )

    def test_health_endpoints_do_not_require_loading_whisper(self):
        for module in (self.arabic, self.english):
            with self.subTest(module=module.__name__):
                response = module.app.test_client().get("/health")
                self.assertEqual(200, response.status_code)
                self.assertEqual("healthy", response.get_json()["status"])
                self.assertEqual("nosniff", response.headers["X-Content-Type-Options"])
                self.assertEqual("DENY", response.headers["X-Frame-Options"])
                self.assertIn("default-src 'self'", response.headers["Content-Security-Policy"])
                self.assertIn("microphone=(self)", response.headers["Permissions-Policy"])

    def test_missing_audio_returns_validation_error_instead_of_server_error(self):
        cases = ((self.arabic, "ألف"), (self.english, "A"))

        for module, letter in cases:
            with self.subTest(module=module.__name__):
                response = module.app.test_client().post(
                    "/check_pronunciation",
                    data={"letter": letter},
                )
                self.assertEqual(400, response.status_code)

    def test_invalid_english_target_is_rejected(self):
        response = self.english.app.test_client().post(
            "/check_pronunciation",
            data={
                "letter": "../A",
                "audio": (io.BytesIO(b"audio"), "sample.wav"),
            },
        )
        self.assertEqual(422, response.status_code)

    def test_invalid_arabic_target_is_rejected(self):
        response = self.arabic.app.test_client().post(
            "/check_pronunciation",
            data={
                "letter": "غير محدد",
                "audio": (io.BytesIO(b"audio"), "sample.wav"),
            },
        )
        self.assertEqual(422, response.status_code)

    def test_upload_uses_server_generated_name_and_is_always_removed(self):
        for module, letter in ((self.arabic, "ألف"), (self.english, "A")):
            upload_dir = Path(module.app.config["UPLOAD_FOLDER"])
            before = set(upload_dir.iterdir())

            with self.subTest(module=module.__name__):
                response = module.app.test_client().post(
                    "/check_pronunciation",
                    data={
                        "letter": letter,
                        "audio": (io.BytesIO(b"audio"), "../../escape.wav"),
                    },
                    content_type="multipart/form-data",
                )
                self.assertEqual(200, response.status_code)
                temporary_path = Path(response.get_json()["_temporary_path"])
                self.assertEqual(upload_dir.resolve(), temporary_path.parent.resolve())
                self.assertEqual(32, len(temporary_path.stem))
                self.assertFalse(temporary_path.exists())
                self.assertEqual(before, set(upload_dir.iterdir()))

    def test_source_files_are_not_public(self):
        for module in (self.arabic, self.english):
            with self.subTest(module=module.__name__):
                response = module.app.test_client().get("/server.py")
                self.assertEqual(404, response.status_code)

    def test_unused_model_sources_are_not_public(self):
        cases = (
            (self.arabic, "/assets/3D%20Model/package.json"),
            (self.english, "/assets/models/face-api.js-master/package.json"),
        )

        for module, path in cases:
            with self.subTest(module=module.__name__, path=path):
                response = module.app.test_client().get(path)
                self.assertEqual(404, response.status_code)


if __name__ == "__main__":
    unittest.main()
