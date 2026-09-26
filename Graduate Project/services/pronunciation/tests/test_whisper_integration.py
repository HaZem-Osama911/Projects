import gc
import importlib.util
import math
import os
import struct
import tempfile
import unittest
import wave
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1]
RUN_INTEGRATION = os.getenv("RUN_WHISPER_INTEGRATION") == "1"


def load_module(name, path):
    spec = importlib.util.spec_from_file_location(name, path)
    module = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(module)
    return module


@unittest.skipUnless(
    RUN_INTEGRATION,
    "Set RUN_WHISPER_INTEGRATION=1 to load the real Whisper models.",
)
class WhisperIntegrationTests(unittest.TestCase):
    def test_real_models_accept_audio_decoded_by_bundled_ffmpeg(self):
        cases = (
            (
                load_module(
                    "arabic_whisper_integration",
                    ROOT / "arabic" / "speech_to_text.py",
                ),
                "ar",
            ),
            (
                load_module(
                    "english_whisper_integration",
                    ROOT / "english" / "speech_to_text.py",
                ),
                "en",
            ),
        )

        with tempfile.TemporaryDirectory() as directory:
            audio_path = Path(directory) / "tone.wav"
            sample_rate = 16_000
            samples = [
                int(4_000 * math.sin(2 * math.pi * 440 * index / sample_rate))
                for index in range(sample_rate // 2)
            ]

            with wave.open(str(audio_path), "wb") as output:
                output.setnchannels(1)
                output.setsampwidth(2)
                output.setframerate(sample_rate)
                output.writeframes(b"".join(struct.pack("<h", sample) for sample in samples))

            for module, language in cases:
                with self.subTest(module=module.__name__):
                    audio = module.load_audio(audio_path)
                    model = module.get_model()
                    result = model.transcribe(audio, language=language, fp16=False)
                    self.assertIn("text", result)

                    module.get_model.cache_clear()
                    del model
                    gc.collect()


if __name__ == "__main__":
    unittest.main()
