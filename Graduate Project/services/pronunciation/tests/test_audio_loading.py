import importlib.util
import math
import struct
import tempfile
import unittest
import wave
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1]


def load_module(name, path):
    spec = importlib.util.spec_from_file_location(name, path)
    module = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(module)
    return module


class AudioLoadingTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.modules = (
            load_module(
                "arabic_transcription",
                ROOT / "arabic" / "speech_to_text.py",
            ),
            load_module(
                "english_transcription",
                ROOT / "english" / "speech_to_text.py",
            ),
        )

    def test_bundled_ffmpeg_decodes_audio_without_system_ffmpeg(self):
        sample_rate = 8_000
        duration = 0.1

        with tempfile.TemporaryDirectory() as directory:
            audio_path = Path(directory) / "tone.wav"
            samples = [
                int(8_000 * math.sin(2 * math.pi * 440 * index / sample_rate))
                for index in range(int(sample_rate * duration))
            ]

            with wave.open(str(audio_path), "wb") as output:
                output.setnchannels(1)
                output.setsampwidth(2)
                output.setframerate(sample_rate)
                output.writeframes(b"".join(struct.pack("<h", sample) for sample in samples))

            for module in self.modules:
                with self.subTest(module=module.__name__):
                    decoded = module.load_audio(audio_path)
                    self.assertEqual(1_600, decoded.shape[0])
                    self.assertGreater(float(abs(decoded).max()), 0)


if __name__ == "__main__":
    unittest.main()
