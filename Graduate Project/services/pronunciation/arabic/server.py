import os
import time
from pathlib import Path
from uuid import uuid4

from flask import Flask, request, jsonify
from flask import send_from_directory
from speech_to_text import check_pronunciation

BASE_DIR = Path(__file__).resolve().parent
app = Flask(__name__, static_folder=None)

UPLOAD_FOLDER = BASE_DIR / "uploads"
ALLOWED_EXTENSIONS = {"wav", "mp3", "ogg", "flac", "m4a", "aac", "webm"}
VALID_TARGETS = {
    "ألف", "باء", "تاء", "ثاء", "جيم", "حاء", "خاء",
    "دال", "ذال", "راء", "زاي", "سين", "شين", "صاد",
    "ضاد", "طاء", "ظاء", "عين", "غين", "فاء", "قاف",
    "كاف", "لام", "ميم", "نون", "هاء", "واو", "ياء",
}
PUBLIC_FILES = {"index.html", "test.html", "avatar.html", "main.css", "test.css", "letters.js", "test.js"}
PUBLIC_ASSET_PREFIXES = ("lib/", "sounds/", "الحروف/")

app.config["UPLOAD_FOLDER"] = str(UPLOAD_FOLDER)
app.config["MAX_CONTENT_LENGTH"] = 10 * 1024 * 1024
UPLOAD_FOLDER.mkdir(parents=True, exist_ok=True)


@app.after_request
def add_security_headers(response):
    response.headers["Content-Security-Policy"] = (
        "default-src 'self'; "
        "script-src 'self' 'unsafe-inline'; "
        "style-src 'self' 'unsafe-inline'; "
        "img-src 'self' data:; "
        "media-src 'self' blob:; "
        "connect-src 'self'; "
        "font-src 'self' data:"
    )
    response.headers["X-Content-Type-Options"] = "nosniff"
    response.headers["X-Frame-Options"] = "DENY"
    response.headers["Referrer-Policy"] = "strict-origin-when-cross-origin"
    response.headers["Permissions-Policy"] = "camera=(), geolocation=(), microphone=(self)"
    return response


def allowed_file(filename):
    return "." in filename and filename.rsplit(".", 1)[1].lower() in ALLOWED_EXTENSIONS


@app.get("/")
@app.get("/index.html")
def serve_index():
    return send_from_directory(BASE_DIR, "index.html")


@app.get("/<path:filename>")
def serve_public_file(filename):
    if filename.startswith("assets/"):
        asset_path = filename.removeprefix("assets/")
        if asset_path.startswith(PUBLIC_ASSET_PREFIXES):
            return send_from_directory(BASE_DIR / "assets", asset_path)
    if filename in PUBLIC_FILES:
        return send_from_directory(BASE_DIR, filename)
    return jsonify({"error": "الملف غير موجود"}), 404


@app.route("/check_pronunciation", methods=["POST", "OPTIONS"])
def check_pronunciation_endpoint():
    audio_path = None

    try:
        if request.method == "OPTIONS":
            return "", 204

        if "letter" not in request.form:
            return jsonify({"error": "لم يتم إرسال الحرف!"}), 400
        correct_letter = request.form["letter"].strip()
        if correct_letter not in VALID_TARGETS:
            return jsonify({"error": "قيمة الحرف غير صالحة!"}), 422

        if "audio" not in request.files:
            return jsonify({"error": "لم يتم إرسال ملف الصوت!"}), 400
        audio_file = request.files["audio"]
        if audio_file.filename == "" or not allowed_file(audio_file.filename):
            return jsonify({"error": "ملف غير صالح!"}), 400

        ext = audio_file.filename.rsplit(".", 1)[1].lower()
        filename = f"{uuid4().hex}.{ext}"
        audio_path = os.path.join(app.config["UPLOAD_FOLDER"], filename)
        audio_file.save(audio_path)

        result = check_pronunciation(correct_letter, audio_path)
        return jsonify(result), 200

    except Exception as e:
        app.logger.error(f"Error in endpoint: {str(e)}")
        return jsonify({"error": "خطأ داخلي في الخادم"}), 500

    finally:
        try:
            if audio_path and os.path.exists(audio_path):
                os.remove(audio_path)
        except OSError as error:
            app.logger.error("Failed to delete temporary audio: %s", error)


@app.errorhandler(413)
def upload_too_large(_error):
    return jsonify({"error": "حجم الملف أكبر من الحد المسموح (10 ميجابايت)."}), 413


@app.route("/health")
def health_check():
    return jsonify({"status": "healthy", "timestamp": time.time()}), 200


if __name__ == "__main__":
    app.run(
        host=os.getenv("PRONUNCIATION_HOST", "127.0.0.1"),
        port=int(os.getenv("PRONUNCIATION_PORT", "5000")),
        debug=os.getenv("FLASK_DEBUG") == "1",
    )
