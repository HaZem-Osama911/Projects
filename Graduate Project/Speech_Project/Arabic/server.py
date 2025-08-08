import os
import time
from flask import Flask, request, jsonify
from whisper_speech_to_text import check_pronunciation

# إنشاء تطبيق Flask ليخدم الملفات الثابتة من نفس المجلد
app = Flask(
    __name__,
    static_folder=".",  # يقرأ أي ملف ثابت من مجلد المشروع نفسه
    static_url_path="",  # يخدم هذه الملفات من الجذر '/'
)

UPLOAD_FOLDER = "uploads"
ALLOWED_EXTENSIONS = {"wav", "mp3", "ogg", "flac", "m4a", "aac", "webm"}

app.config["UPLOAD_FOLDER"] = UPLOAD_FOLDER
if not os.path.exists(UPLOAD_FOLDER):
    os.makedirs(UPLOAD_FOLDER)


def allowed_file(filename):
    return "." in filename and filename.rsplit(".", 1)[1].lower() in ALLOWED_EXTENSIONS


@app.before_request
def log_request_info():
    app.logger.info(f"Received {request.method} request to {request.path}")
    app.logger.info(f"Headers: {request.headers}")
    app.logger.info(f"Form data: {request.form}")
    if request.files:
        app.logger.info(f"Files received: {list(request.files.keys())}")


@app.route("/check_pronunciation", methods=["POST", "OPTIONS"])
def check_pronunciation_endpoint():
    try:
        # دعم preflight request
        if request.method == "OPTIONS":
            return "", 204

        # التحقق من إرسال الحرف
        if "letter" not in request.form:
            return jsonify({"error": "لم يتم إرسال الحرف!"}), 400
        correct_letter = request.form["letter"]

        # التحقق من إرسال ملف الصوت
        if "audio" not in request.files:
            return jsonify({"error": "لم يتم إرسال ملف الصوت!"}), 400
        audio_file = request.files["audio"]
        if audio_file.filename == "" or not allowed_file(audio_file.filename):
            return jsonify({"error": "ملف غير صالح!"}), 400

        # حفظ الملف مؤقتًا
        timestamp = str(int(time.time()))
        ext = audio_file.filename.rsplit(".", 1)[1].lower()
        filename = f"{correct_letter}_{timestamp}.{ext}"
        audio_path = os.path.join(app.config["UPLOAD_FOLDER"], filename)
        audio_file.save(audio_path)

        # معالجة النطق باستخدام دالتك
        result = check_pronunciation(correct_letter, audio_path)
        return jsonify(result), 200

    except Exception as e:
        app.logger.error(f"Error in endpoint: {str(e)}")
        return jsonify({"error": "خطأ داخلي في الخادم"}), 500

    finally:
        # حذف الملف بعد المعالجة
        try:
            if os.path.exists(audio_path):
                os.remove(audio_path)
        except Exception as e:
            app.logger.error(f"Failed to delete file: {str(e)}")


@app.route("/health")
def health_check():
    return jsonify({"status": "healthy", "timestamp": time.time()}), 200


if __name__ == "__main__":
    # عند التشغيل، كل الملفات الثابتة (HTML/JS) وخدمة الـ API ستكون على نفس البورت
    app.run(host="0.0.0.0", port=5000, debug=True)
