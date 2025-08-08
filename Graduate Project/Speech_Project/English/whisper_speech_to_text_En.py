import whisper
import re
import warnings

warnings.filterwarnings("ignore", message="FP16 is not supported on CPU")

model = whisper.load_model("base", device="cpu", in_memory=True)


def normalize_text(text):
    text = text.strip().upper()
    text = re.sub(r"[^A-Z]", "", text)  # Remove everything except A-Z
    return text


def transcribe_audio(audio_path):
    try:
        result = model.transcribe(audio_path, language="en")
        return result["text"].strip()
    except Exception as e:
        print("❌ Error in converting audio to text:", e)
        return ""


def check_pronunciation(correct_letter, audio_path):
    spoken_text = transcribe_audio(audio_path)
    spoken_normalized = normalize_text(spoken_text)

    print(f"✅ Target pronunciation: {correct_letter}")
    print(f"🎤 Actual pronunciation: {spoken_normalized}")

    # Direct comparison between texts
    is_correct = spoken_normalized == correct_letter

    return {
        "is_correct": is_correct,
        "expected": correct_letter,
        "spoken": spoken_normalized,
    }
