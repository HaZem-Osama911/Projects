@extends('layouts.app')
@section('title', 'اختبار الفهم اللغوي')
@push('styles')
<link rel="stylesheet" href="{{ asset('assets/css/exams.css') }}">
@endpush

@section('content')
<section class="exam3">
    <div class="container">
        <h1>اختبار الفهم اللغوي</h1>
        <div id="quiz-container">
            <p id="question"></p>

            <div class="progress-bar-container">
                <div class="progress-bar" id="progress-bar"></div>
            </div>

            <div id="options" class="options-grid"></div>
            <button id="next-btn" disabled>التالي</button>
        </div>

        <div id="result-container" class="hidden">
            <h2>نتيجتك</h2>
            <p id="result-text"></p>
            <button onclick="restartQuiz()">إعادة الاختبار</button>
            <button id="backBtn" onclick="goBack()">رجوع</button>
        </div>


    <audio id="correctSound" src="{{ asset('assets/sound/correct.mp3') }}"></audio>
    <audio id="wrongSound" src="{{ asset('assets/sound/wrong.mp3') }}"></audio>

    </div>
</section>
    <script>
        const allQuestions = [
            { question: "أين الفيل؟", options: [{ image: "{{ asset('assets/image/elephant.jpg') }}", correct: true }, { image: "{{ asset('assets/image/lion.jpg') }}", correct: false }, { image: "{{ asset('assets/image/cat.jpg') }}", correct: false }, { image: "{{ asset('assets/image/dog.jpg') }}", correct: false }] },
            { question: "أي صورة تُظهر طفلاً يأكل؟", options: [{ image: "{{ asset('assets/image/eating.jpg') }}", correct: true }, { image: "{{ asset('assets/image/running.jpg') }}", correct: false }, { image: "{{ asset('assets/image/sleeping.jpg') }}", correct: false }, { image: "{{ asset('assets/image/playing.jpg') }}", correct: false }] },
            { question: "أين السيارة؟", options: [{ image: "{{ asset('assets/image/car.jpg') }}", correct: true }, { image: "{{ asset('assets/image/bus.jpg') }}", correct: false }, { image: "{{ asset('assets/image/bicycle.jpg') }}", correct: false }, { image: "{{ asset('assets/image/train.jpg') }}", correct: false }] },
            { question: "أي صورة تعبر عن الطيران؟", options: [{ image: "{{ asset('assets/image/plane.jpg') }}", correct: true }, { image: "{{ asset('assets/image/boat.jpg') }}", correct: false }, { image: "{{ asset('assets/image/car.jpg') }}", correct: false }, { image: "{{ asset('assets/image/train.jpg') }}", correct: false }] },
            { question: "أين صورة السمكة؟", options: [{ image: "{{ asset('assets/image/fish.jpg') }}", correct: true }, { image: "{{ asset('assets/image/dog.jpg') }}", correct: false }, { image: "{{ asset('assets/image/cat.jpg') }}", correct: false }, { image: "{{ asset('assets/image/rabbit.jpg') }}", correct: false }] },
            { question: "من هو الطبيب؟", options: [{ image: "{{ asset('assets/image/doctor.jpg') }}", correct: true }, { image: "{{ asset('assets/image/teacher.jpg') }}", correct: false }, { image: "{{ asset('assets/image/policeman.jpg') }}", correct: false }, { image: "{{ asset('assets/image/chef.jpg') }}", correct: false }] },
            { question: "ما الصورة التي تُظهر شخصًا يقرأ؟", options: [{ image: "{{ asset('assets/image/reading.jpg') }}", correct: true }, { image: "{{ asset('assets/image/writing.jpg') }}", correct: false }, { image: "{{ asset('assets/image/running.jpg') }}", correct: false }, { image: "{{ asset('assets/image/cooking.jpg') }}", correct: false }] },
            { question: "أي صورة تمثل المطر؟", options: [{ image: "{{ asset('assets/image/rain.jpg') }}", correct: true }, { image: "{{ asset('assets/image/sun.jpg') }}", correct: false }, { image: "{{ asset('assets/image/snow.jpg') }}", correct: false }, { image: "{{ asset('assets/image/cloud.jpg') }}", correct: false }] },
            { question: "أي صورة تعبر عن الليل؟", options: [{ image: "{{ asset('assets/image/night.jpg') }}", correct: true }, { image: "{{ asset('assets/image/morning.jpg') }}", correct: false }, { image: "{{ asset('assets/image/sunset.jpg') }}", correct: false }, { image: "{{ asset('assets/image/noon.jpg') }}", correct: false }] },
            { question: "أين الكرة؟", options: [{ image: "{{ asset('assets/image/ball.jpg') }}", correct: true }, { image: "{{ asset('assets/image/bat.jpg') }}", correct: false }, { image: "{{ asset('assets/image/bicycle.jpg') }}", correct: false }, { image: "{{ asset('assets/image/hat.jpg') }}", correct: false }] },
            { question: "أي صورة تُظهر شخصًا يسبح؟", options: [{ image: "{{ asset('assets/image/swimming.jpg') }}", correct: true }, { image: "{{ asset('assets/image/jumping.jpg') }}", correct: false }, { image: "{{ asset('assets/image/running.jpg') }}", correct: false }, { image: "{{ asset('assets/image/cycling.jpg') }}", correct: false }] },
            { question: "أين صورة المدرسة؟", options: [{ image: "{{ asset('assets/image/school.jpg') }}", correct: true }, { image: "{{ asset('assets/image/hospital.jpg') }}", correct: false }, { image: "{{ asset('assets/image/bank.jpg') }}", correct: false }, { image: "{{ asset('assets/image/shop.jpg') }}", correct: false }] },
            { question: "أي صورة تُظهر شخصًا ينام؟", options: [{ image: "{{ asset('assets/image/sleeping.jpg') }}", correct: true }, { image: "{{ asset('assets/image/running.jpg') }}", correct: false }, { image: "{{ asset('assets/image/eating.jpg') }}", correct: false }, { image: "{{ asset('assets/image/reading.jpg') }}", correct: false }] }
        ];

        let questions = [];
        let currentQuestionIndex = 0;
        let score = 0;

        function getRandomQuestions() {
            questions = allQuestions.sort(() => 0.5 - Math.random()).slice(0, 10);
        }

        function loadQuestion() {
            let currentQuestion = questions[currentQuestionIndex];
            document.getElementById("question").textContent = currentQuestion.question;
            let optionsContainer = document.getElementById("options");
            optionsContainer.innerHTML = "";

            let shuffledOptions = [...currentQuestion.options].sort(() => 0.5 - Math.random());
            shuffledOptions.forEach(option => {
                let optionElement = document.createElement("div");
                optionElement.classList.add("option");
                optionElement.innerHTML = `<img src="${option.image}" alt="Option">`;
                optionElement.addEventListener("click", () => {
                    document.querySelectorAll(".option").forEach(opt => opt.classList.remove("selected"));
                    optionElement.classList.add("selected");
                    document.getElementById("next-btn").disabled = false;
                    optionElement.dataset.correct = option.correct;
                });
                optionsContainer.appendChild(optionElement);
            });

            document.getElementById("next-btn").disabled = true;
            updateProgressBar();
        }

        function nextQuestion() {
            const selectedOption = document.querySelector(".option.selected");
            if (selectedOption && selectedOption.dataset.correct === "true") {
                score++;
                document.getElementById("correctSound").play();
            } else {
                document.getElementById("wrongSound").play();
            }

            currentQuestionIndex++;
            if (currentQuestionIndex < questions.length) {
                loadQuestion();
            } else {
                showResult();
            }
        }

        function showResult() {
            document.getElementById("quiz-container").classList.add("hidden");
            document.getElementById("result-container").classList.remove("hidden");
            document.getElementById("result-text").textContent = `لقد أجبت بشكل صحيح على ${score} من 10 أسئلة!`;

            let parentMessage = '';
            if (score >= 9) {
                parentMessage = 'ممتاز! طفلك يُظهر فهمًا لغويًا قويًا جدًا. استمر في دعمه بمزيد من المحادثات اليومية.';
            } else if (score >= 6) {
                parentMessage = 'جيد! يوجد بعض النقاط التي تحتاج إلى تقوية. جرب تمارين فهم إضافية أو فيديوهات تفاعلية.';
            } else {
                parentMessage = 'يبدو أن طفلك بحاجة إلى دعم إضافي في الفهم اللغوي. يُفضل استشارة أخصائي تخاطب لمساعدته على التحسن.';
            }

            const parentAdvice = document.createElement('p');
            parentAdvice.textContent = parentMessage;
            document.getElementById("result-container").appendChild(parentAdvice);

            document.getElementById("backBtn").style.display = "block";
        }

        function goBack() {
            window.location.href = "{{ route('exam') }}";
        }

        function restartQuiz() {
            currentQuestionIndex = 0;
            score = 0;
            getRandomQuestions();
            document.getElementById("quiz-container").classList.remove("hidden");
            document.getElementById("result-container").classList.add("hidden");
            document.getElementById("progress-bar").style.width = "0%";
            loadQuestion();
        }

        function updateProgressBar() {
            const progress = ((currentQuestionIndex) / questions.length) * 100;
            document.getElementById("progress-bar").style.width = `${progress}%`;
        }

        document.getElementById("next-btn").addEventListener("click", nextQuestion);
        getRandomQuestions();
        loadQuestion();
    </script>
@endsection
