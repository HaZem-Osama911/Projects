@extends('layouts.app')
@section('title', 'الاختبار الموجه')
@push('styles')
<link rel="stylesheet" href="{{ asset('assets/css/exams.css') }}">
@endpush

@section('content')
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
          link: "{{ route('exam.exam3') }}"
        });
      }

      if (q2 === "yes") {
        recommendations.push({
          name: "اختبار الذكاء",
          link: "{{ route('exam.exam1') }}"
        }, {
          name: "اختبار قياس العمر العقلي",
          link: "{{ route('exam.exam2') }}"
        });
      }

      if (q3 === "yes") {
        recommendations.push({
          name: "اختبار التوحد (M-CHAT)",
          link: "{{ route('exam.exam4') }}"
        });
      }

      if (q4 === "yes") {
        recommendations.push({
          name: "اختبار نطق الحروف",
          link: "{{ route('pronunciation') }}"
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
      <p>لكن نرشحلك الاطمئنان عن طريق <a href="{{ route('exam.exam1') }}" class="result-link">اختبار الذكاء</a></p>
    `;
        }

        resultDiv.style.display = "block";
        document.getElementById("backBtn").style.display = "block";
      });

    function goBack() {
      window.location.href = "{{ route('exam') }}";
    }

  </script>
@endsection
