import whisper
import re
import warnings

warnings.filterwarnings("ignore", message="FP16 is not supported on CPU")

model = whisper.load_model("small", device="cpu", in_memory=True)


def normalize_text(text):
    text = text.strip()
    text = re.sub(r"[^\u0600-\u06FF]", "", text)  # إزالة كل ما ليس حرف عربي
    return text


def transcribe_audio(audio_path):
    try:
        result = model.transcribe(audio_path, language="ar")
        return result["text"].strip()
    except Exception as e:
        print("❌ خطأ في تحويل الصوت إلى نص:", e)
        return ""


def check_pronunciation(correct_letter, audio_path):
    spoken_text = transcribe_audio(audio_path)
    spoken_normalized = normalize_text(spoken_text)

    print(f"✅ النطق المستهدف: {correct_letter}")
    print(f"🎤 النطق الفعلي: {spoken_normalized}")

    # مقارنة مباشرة بين النصوص
    is_correct = spoken_normalized == correct_letter

    return {
        "is_correct": is_correct,
        "expected": correct_letter,
        "spoken": spoken_normalized,
    }
