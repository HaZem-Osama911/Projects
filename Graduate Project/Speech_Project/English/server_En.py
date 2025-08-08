import os
import time
from flask import Flask, request, jsonify, send_from_directory
from whisper_speech_to_text_En import check_pronunciation

# Create Flask app to serve static files from the same folder
app = Flask(
    __name__,
    static_folder=".",  # Read any static file from the project folder
    static_url_path="",  # Serve these files from the root '/'
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


@app.route("/")
@app.route("/index_En.html")
def serve_index():
    return send_from_directory(".", "index_En.html")


@app.route("/test_En.html")
def serve_test():
    return send_from_directory(".", "test_En.html")


@app.route("/avatar_En.html")
def serve_avatar():
    return send_from_directory(".", "avatar_En.html")


@app.route("/check_pronunciation", methods=["POST", "OPTIONS"])
def check_pronunciation_endpoint():
    try:
        # Support preflight request
        if request.method == "OPTIONS":
            return "", 204

        # Check if letter is sent
        if "letter" not in request.form:
            return jsonify({"error": "No letter provided!"}), 400
        correct_letter = request.form["letter"]

        # Check if audio file is sent
        if "audio" not in request.files:
            return jsonify({"error": "No audio file provided!"}), 400
        audio_file = request.files["audio"]
        if audio_file.filename == "" or not allowed_file(audio_file.filename):
            return jsonify({"error": "Invalid file!"}), 400

        # Save the file temporarily
        timestamp = str(int(time.time()))
        ext = audio_file.filename.rsplit(".", 1)[1].lower()
        filename = f"{correct_letter}_{timestamp}.{ext}"
        audio_path = os.path.join(app.config["UPLOAD_FOLDER"], filename)
        audio_file.save(audio_path)

        # Process pronunciation using your function
        result = check_pronunciation(correct_letter, audio_path)
        return jsonify(result), 200

    except Exception as e:
        app.logger.error(f"Error in endpoint: {str(e)}")
        return jsonify({"error": "Internal server error"}), 500

    finally:
        # Delete the file after processing
        try:
            if os.path.exists(audio_path):
                os.remove(audio_path)
        except Exception as e:
            app.logger.error(f"Failed to delete file: {str(e)}")


@app.route("/health")
def health_check():
    return jsonify({"status": "healthy", "timestamp": time.time()}), 200


if __name__ == "__main__":
    # When running, all static files (HTML/JS) and the API will be on the same port
    app.run(host="0.0.0.0", port=5001, debug=True)
