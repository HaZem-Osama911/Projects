document.addEventListener("DOMContentLoaded", function () {
  const recordButton = document.getElementById("recordButton");
  const feedback = document.getElementById("feedback");
  const letterToPronounce = document.getElementById("letterToPronounce");
  let mediaRecorder;
  let audioChunks = [];

  const urlParams = new URLSearchParams(window.location.search);
  const letter = urlParams.get("letter");

  if (!letter) {
    letterToPronounce.textContent = "No letter selected!";
    feedback.textContent = "No letter selected!";
    return;
  }

  // Display the letter to pronounce
  letterToPronounce.textContent = `Letter to Pronounce: ${letter}`;

  console.log("Selected letter:", letter);

  recordButton.addEventListener("click", async () => {
    console.log("Button clicked");
    if (mediaRecorder && mediaRecorder.state === "recording") {
      console.log("Stopping recording");
      mediaRecorder.stop();
      recordButton.textContent = "Processing...";
    } else {
      try {
        console.log("Requesting microphone access");
        const stream = await navigator.mediaDevices.getUserMedia({
          audio: true,
        });
        mediaRecorder = new MediaRecorder(stream);
        mediaRecorder.start();
        recordButton.textContent = "Stop Recording";
        audioChunks = [];
        console.log("Recording started");

        mediaRecorder.addEventListener("dataavailable", (event) => {
          audioChunks.push(event.data);
          console.log("Audio data collected");
        });

        mediaRecorder.addEventListener("stop", async () => {
          console.log("Recording stopped");
          const audioBlob = new Blob(audioChunks, { type: "audio/webm" });
          const formData = new FormData();
          formData.append("audio", audioBlob, "recording.webm");
          formData.append("letter", letter);
          console.log("Sending data to server");

          try {
            const response = await fetch(
              "http://127.0.0.1:5001/check_pronunciation",
              {
                method: "POST",
                body: formData,
                credentials: "include",
                mode: "cors",
              }
            );
            console.log("Server response status:", response.status);
            if (!response.ok) {
              throw new Error(
                `Response error: ${response.status} - ${response.statusText}`
              );
            }
            const result = await response.json();
            console.log("Result:", result);

            if (result.is_correct) {
              feedback.textContent =
                "Congratulations! You pronounced the letter correctly.";
              feedback.style.color = "#4caf50";
            } else {
              feedback.textContent = "Try again!";
              feedback.style.color = "#ff6f61";
              setTimeout(() => {
                window.location.href = `avatar_En.html?letter=${letter}`;
              }, 2000);
            }
          } catch (error) {
            console.error("Error in connection or processing:", error.message);
            feedback.textContent = `Error: ${error.message}. Ensure the server is running.`;
            feedback.style.color = "#ff6f61";
          }

          recordButton.textContent = "Start Recording";
        });
      } catch (error) {
        console.error("Error accessing microphone:", error);
        feedback.textContent = "Cannot access the microphone.";
        feedback.style.color = "#ff6f61";
      }
    }
  });
});
