import whisper
import re

# تحميل نموذج Whisper
model = whisper.load_model("small")


def normalize_text(text):
    """تنظيف النص بإزالة الرموز والتشكيل وتحويله إلى أحرف صغيرة."""
    text = text.lower().strip()
    text = re.sub(r"[^\w\s]", "", text)  # إزالة الرموز غير الضرورية
    text = re.sub(r"[\u064B-\u0652]", "", text)  # إزالة التشكيل العربي
    return text


def transcribe_audio(audio_path):
    """تحويل الصوت إلى نص باستخدام Whisper."""
    try:
        result = model.transcribe(audio_path, language="ar")
        return result["text"].strip()
    except Exception as e:
        print("❌ خطأ أثناء تحليل الصوت:", str(e))
        return ""


def check_pronunciation(correct_letter, audio_path):
    """مقارنة النطق المستخرج بالحرف الصحيح."""
    spoken_text = transcribe_audio(audio_path)

    correct_letter = normalize_text(correct_letter)
    spoken_text = normalize_text(spoken_text)

    print(f"📌 الحرف الصحيح المتوقع: {correct_letter}")
    print(f"🎤 النطق المسجل: {spoken_text}")

    return spoken_text == correct_letter, spoken_text


# اختبار الكود
if __name__ == "__main__":
    correct_letter = "ر"  # الحرف الصحيح
    audio_path = "test.wav"  # المسار إلى ملف الصوت

    is_correct, spoken_text = check_pronunciation(correct_letter, audio_path)
    print(f"✅ النتيجة: {'صحيح' if is_correct else 'غير صحيح'}")
