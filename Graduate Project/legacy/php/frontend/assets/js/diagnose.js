document.addEventListener('DOMContentLoaded', () => {
    const sections = [...document.querySelectorAll('.exercise-content')];
    const exercises = [...document.querySelectorAll('.exercise[data-answer]')];
    const progressBar = document.getElementById('progressBar');

    function normalize(value) {
        return String(value || '')
            .trim()
            .replace(/\s+/g, ' ')
            .toLocaleLowerCase('ar');
    }

    function updateProgress() {
        if (!progressBar || exercises.length === 0) return;

        const answered = exercises.filter((exercise) => exercise.classList.contains('answered')).length;
        progressBar.style.width = `${Math.round((answered / exercises.length) * 100)}%`;
    }

    function checkAnswer(exercise, control, value) {
        const isCorrect = normalize(value) === normalize(exercise.dataset.answer);

        exercise.classList.add('answered');
        control.classList.remove('answer-correct', 'answer-incorrect');
        control.classList.add(isCorrect ? 'answer-correct' : 'answer-incorrect');
        control.setAttribute('aria-label', isCorrect ? 'إجابة صحيحة' : 'إجابة غير صحيحة');
        updateProgress();
    }

    window.showExercises = (sectionId) => {
        sections.forEach((section) => {
            section.hidden = section.id !== sectionId;
        });

        const selected = document.getElementById(sectionId);
        selected?.scrollIntoView({ behavior: 'smooth', block: 'start' });
    };

    window.scrollToExercises = () => {
        document.querySelector('.therapy-filter')?.scrollIntoView({ behavior: 'smooth' });
    };

    exercises.forEach((exercise) => {
        exercise.querySelectorAll('button, img[data-answer]').forEach((control) => {
            control.addEventListener('click', () => {
                const value = control.dataset.answer || control.textContent;
                checkAnswer(exercise, control, value);
            });
        });

        exercise.querySelectorAll('input[type="text"]').forEach((input) => {
            input.addEventListener('keydown', (event) => {
                if (event.key === 'Enter') {
                    event.preventDefault();
                    checkAnswer(exercise, input, input.value);
                }
            });
        });
    });

    if (sections.length > 0) {
        window.showExercises(sections[0].id);
    }
    updateProgress();
});
