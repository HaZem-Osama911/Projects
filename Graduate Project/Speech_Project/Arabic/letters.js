document.addEventListener("DOMContentLoaded", function () {
  const lettersGrid = document.getElementById("lettersGrid");

  const arabicLetters = [
    "أ",
    "ب",
    "ت",
    "ث",
    "ج",
    "ح",
    "خ",
    "د",
    "ذ",
    "ر",
    "ز",
    "س",
    "ش",
    "ص",
    "ض",
    "ط",
    "ظ",
    "ع",
    "غ",
    "ف",
    "ق",
    "ك",
    "ل",
    "م",
    "ن",
    "ه",
    "و",
    "ي",
  ];

  const letterNames = {
    أ: "ألف",
    ب: "باء",
    ت: "تاء",
    ث: "ثاء",
    ج: "جيم",
    ح: "حاء",
    خ: "خاء",
    د: "دال",
    ذ: "ذال",
    ر: "راء",
    ز: "زاي",
    س: "سين",
    ش: "شين",
    ص: "صاد",
    ض: "ضاد",
    ط: "طاء",
    ظ: "ظاء",
    ع: "عين",
    غ: "غين",
    ف: "فاء",
    ق: "قاف",
    ك: "كاف",
    ل: "لام",
    م: "ميم",
    ن: "نون",
    ه: "هاء",
    و: "واو",
    ي: "ياء",
  };

  arabicLetters.forEach((letter) => {
    const letterButton = document.createElement("button");
    letterButton.textContent = letter;
    letterButton.classList.add(
      "letter-button",
      "animate__animated",
      "animate__fadeInUp"
    );

    letterButton.addEventListener("mouseenter", () => {
      letterButton.style.transform = "translateY(-10px)";
      letterButton.style.backgroundColor = "#ff6f61";
      letterButton.style.borderColor = "#ff6f61";
      letterButton.style.boxShadow = "0 10px 20px rgba(255, 111, 97, 0.3)";
    });

    letterButton.addEventListener("mouseleave", () => {
      letterButton.style.transform = "translateY(0)";
      letterButton.style.backgroundColor = "rgba(255, 255, 255, 0.2)";
      letterButton.style.borderColor = "rgba(255, 255, 255, 0.3)";
      letterButton.style.boxShadow = "none";
    });

    letterButton.addEventListener("click", () => {
      letterButton.classList.add("animate__rubberBand");
      setTimeout(() => {
        window.location.href = `test.html?letter=${letterNames[letter]}`;
      }, 500);
    });

    lettersGrid.appendChild(letterButton);
  });
});
