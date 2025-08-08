document.addEventListener("DOMContentLoaded", function () {
  const recordButton = document.getElementById("recordButton");
  const feedback = document.getElementById("feedback");
  let mediaRecorder;
  let audioChunks = [];

  const urlParams = new URLSearchParams(window.location.search);
  const letter = urlParams.get("letter") || "غير محدد";
  document.getElementById(
    "letterToPronounce"
  ).textContent = `الحرف المراد نطقه: ${letter}`;

  if (!letter) {
    feedback.textContent = "لم يتم تحديد الحرف!";
    return;
  }

  console.log("الحرف المختار:", letter);

  // إنشاء عناصر الصوت
  const clapSound = new Audio("assets/sounds/clap.mp3");
  const tryAgainSound = new Audio("assets/sounds/try_again.mp3");

  recordButton.addEventListener("click", async () => {
    console.log("تم الضغط على الزر");
    if (mediaRecorder && mediaRecorder.state === "recording") {
      console.log("إيقاف التسجيل");
      mediaRecorder.stop();
      recordButton.textContent = "جاري المعالجة...";
    } else {
      try {
        console.log("طلب الوصول للميكروفون");
        const stream = await navigator.mediaDevices.getUserMedia({
          audio: true,
        });
        mediaRecorder = new MediaRecorder(stream);
        mediaRecorder.start();
        recordButton.textContent = "إيقاف التسجيل";
        audioChunks = [];
        console.log("بدء التسجيل");

        mediaRecorder.addEventListener("dataavailable", (event) => {
          audioChunks.push(event.data);
          console.log("تم جمع بيانات الصوت");
        });

        mediaRecorder.addEventListener("stop", async () => {
          console.log("توقف التسجيل");
          const audioBlob = new Blob(audioChunks, { type: "audio/webm" });
          const formData = new FormData();
          formData.append("audio", audioBlob, "recording.webm");
          formData.append("letter", letter);
          console.log("إرسال البيانات للسيرفر");

          try {
            const response = await fetch(
              "http://127.0.0.1:5000/check_pronunciation",
              {
                method: "POST",
                body: formData,
                credentials: "include",
                mode: "cors",
              }
            );
            console.log("حالة الرد من السيرفر:", response.status);
            if (!response.ok) {
              throw new Error(
                `خطأ في الاستجابة: ${response.status} - ${response.statusText}`
              );
            }
            const result = await response.json();
            console.log("النتيجة:", result);

            if (result.is_correct) {
              clapSound.play();
              feedback.textContent = "تهانينا! لقد نطقت الحرف بشكل صحيح.";
              feedback.style.color = "#4caf50";
              feedback.classList.add("animate__animated", "animate__bounceIn");
            } else {
              tryAgainSound.play();
              feedback.textContent = "حاول مرة أخرى!";
              feedback.style.color = "#ff6f61";
              feedback.classList.add("animate__animated", "animate__shakeX");

              setTimeout(() => {
                window.location.href = `avatar.html?letter=${letter}`;
              }, 4000);
            }

            // إزالة الأنيميشن بعد انتهائه
            setTimeout(() => {
              feedback.classList.remove(
                "animate__animated",
                "animate__bounceIn",
                "animate__shakeX"
              );
            }, 1000);
          } catch (error) {
            // setTimeout(() => {
            //   clapSound.pause(); // إيقاف الصوت
            //   clapSound.currentTime = 0; // رجع المؤشر لبداية الصوت لو حبيت
            // }, 3000);

            console.error("خطأ في الاتصال أو المعالجة:", error.message);
            feedback.textContent = `حدث خطأ: ${error.message}. تأكد من تشغيل السيرفر.`;
            feedback.style.color = "#ff6f61";
          }

          recordButton.textContent = "بدء التسجيل";
        });
      } catch (error) {
        console.error("خطأ في الوصول للميكروفون:", error);
        feedback.textContent = "لا يمكن الوصول إلى الميكروفون.";
        feedback.style.color = "#ff6f61";
      }
    }
  });
});
