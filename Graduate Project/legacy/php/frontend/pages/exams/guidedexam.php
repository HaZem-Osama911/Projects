<!DOCTYPE html>
<html lang="ar" dir="rtl">

<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>الاختبار الموجه</title>
  <link rel="stylesheet" href="../../assets/css/exam.css">
  <link rel="stylesheet" href="../../assets/css/exams.css">
  <link rel="stylesheet" href="../../assets/css/all.min.css">
  <link rel="stylesheet" href="../../assets/css/normalize.css">
  <link rel="preconnect" href="https:fonts.googleapis.com">
  <link rel="preconnect" href="https:fonts.gstatic.com" crossorigin>
  <link href="https:fonts.googleapis.com/css2?family=Cairo:wght@200..1000&display=swap" rel="stylesheet">
  <link rel="preconnect" href="https://fonts.googleapis.com">
  <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
  <link
    href="https://fonts.googleapis.com/css2?family=Lalezar&family=Work+Sans:ital,wght@0,100..900;1,100..900&display=swap"
    rel="stylesheet">
</head>
<link rel="shortcut icon" href="../../assets/image/logo.png" type="../../assets/image/x-icon">
</head>

<body>

  <div class="guided-container">
    <h1>الاختبار الموجه</h1>
    <p class="description">جاوب على ٤ أسئلة بسيطة واحنا هنرشحلك أنسب اختبار لحالة طفلك 👶</p>

    <form id="guidedForm">
      <div class="question">
        <h3>هل يعاني طفلك من صعوبة في التواصل أو الفهم؟</h3>
        <label><input type="radio" name="q1" value="yes" required> نعم</label>
        <label><input type="radio" name="q1" value="no"> لا</label>
      </div>

      <div class="question">
        <h3>هل تلاحظين تأخرًا في التطور العقلي أو الدراسي مقارنة بأقرانه؟</h3>
        <label><input type="radio" name="q2" value="yes" required> نعم</label>
        <label><input type="radio" name="q2" value="no"> لا</label>
      </div>

      <div class="question">
        <h3>هل لاحظتِ سلوكيات متكررة أو انطواء غير معتاد؟</h3>
        <label><input type="radio" name="q3" value="yes" required> نعم</label>
        <label><input type="radio" name="q3" value="no"> لا</label>
      </div>
      <div class="question">
        <h3>هل يعاني طفلك من اي مشكله في نطق الحروف؟</h3>
        <label><input type="radio" name="q4" value="yes" required> نعم</label>
        <label><input type="radio" name="q4" value="no"> لا</label>
      </div>


      <button type="submit" class="submit-btn">اعرض النتيجة</button>
    </form>

    <div id="result" class="result"></div>
    <button id="backBtn" onclick="goBack()" style="display: none;">رجوع</button>
  </div>

  <script>
    document.getElementById("guidedForm").addEventListener("submit", function (event) {
      event.preventDefault();

      const q1 = document.querySelector('input[name="q1"]:checked').value;
      const q2 = document.querySelector('input[name="q2"]:checked').value;
      const q3 = document.querySelector('input[name="q3"]:checked').value;
      const q4 = document.querySelector('input[name="q4"]:checked').value;

      let recommendations = [];

      if (q1 === "yes") {
        recommendations.push({
          name: "اختبار الفهم اللغوي",
          link: "exam3.html"
        });
      }

      if (q2 === "yes") {
        recommendations.push({
          name: "اختبار الذكاء",
          link: "exam1.html"
        }, {
          name: "اختبار قياس العمر العقلي",
          link: "exam2.html"
        });
      }

      if (q3 === "yes") {
        recommendations.push({
          name: "اختبار التوحد (M-CHAT)",
          link: "exam4.html"
        });
      }

      if (q4 === "yes") {
        recommendations.push({
          name: "اختبار نطق الحروف",
          link: "exam5.html"
        });
      }

        const resultDiv = document.getElementById("result");
        resultDiv.innerHTML = "";

        if (recommendations.length > 0) {
          const heading = document.createElement("h3");
          heading.textContent = "ننصحك بإجراء الاختبارات التالية:";
          resultDiv.appendChild(heading);

          const ul = document.createElement("ul");
          ul.classList.add("recommend-list");

          recommendations.forEach(test => {
            const li = document.createElement("li");
            const link = document.createElement("a");
            link.href = test.link;
            link.textContent = test.name;
            link.classList.add("result-link");
            li.appendChild(link);
            ul.appendChild(li);
          });

          resultDiv.appendChild(ul);
        } else {
          resultDiv.innerHTML = `
      <p>طفلك يبدو بخير حسب إجاباتك 💙</p>
      <p>لكن نرشحلك الاطمئنان عن طريق <a href="exam1.php" class="result-link">اختبار الذكاء</a></p>
    `;
        }

        resultDiv.style.display = "block";
        document.getElementById("backBtn").style.display = "block";
      });

    function goBack() {
      window.location.href = "exam.html";
    }

  </script>

</body>

</html>
