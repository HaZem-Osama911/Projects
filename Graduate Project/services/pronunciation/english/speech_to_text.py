import re
import subprocess
import warnings
from functools import lru_cache

warnings.filterwarnings("ignore", message="FP16 is not supported on CPU")


def load_audio(audio_path):
    import imageio_ffmpeg
    import numpy as np

    command = [
        imageio_ffmpeg.get_ffmpeg_exe(),
        "-nostdin",
        "-threads",
        "0",
        "-i",
        str(audio_path),
        "-f",
        "s16le",
        "-ac",
        "1",
        "-acodec",
        "pcm_s16le",
        "-ar",
        "16000",
        "-",
    ]

    try:
        output = subprocess.run(command, capture_output=True, check=True).stdout
    except subprocess.CalledProcessError as error:
        details = error.stderr.decode(errors="replace").strip()
        raise RuntimeError(f"Unable to decode the uploaded audio: {details}") from error

    return np.frombuffer(output, np.int16).flatten().astype(np.float32) / 32768.0


@lru_cache(maxsize=1)
def get_model():
    import whisper

    return whisper.load_model("base", device="cpu", in_memory=True)


def normalize_text(text):
    text = text.strip().upper()
    text = re.sub(r"[^A-Z]", "", text)  # Remove everything except A-Z
    return text


def transcribe_audio(audio_path):
    try:
        result = get_model().transcribe(load_audio(audio_path), language="en")
        return result["text"].strip()
    except Exception as e:
        print("❌ Error in converting audio to text:", e)
        return ""


def check_pronunciation(correct_letter, audio_path):
    spoken_text = transcribe_audio(audio_path)
    expected_normalized = normalize_text(correct_letter)
    spoken_normalized = normalize_text(spoken_text)

    print(f"✅ Target pronunciation: {correct_letter}")
    print(f"🎤 Actual pronunciation: {spoken_normalized}")

    # Direct comparison between texts
    is_correct = bool(expected_normalized) and spoken_normalized == expected_normalized

    return {
        "is_correct": is_correct,
        "expected": expected_normalized,
        "spoken": spoken_normalized,
    }
