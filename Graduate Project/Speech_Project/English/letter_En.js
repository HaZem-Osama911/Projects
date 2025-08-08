document.addEventListener("DOMContentLoaded", function () {
  const lettersGrid = document.getElementById("lettersGrid");

  const englishLetters = [
    "A",
    "B",
    "C",
    "D",
    "E",
    "F",
    "G",
    "H",
    "I",
    "J",
    "K",
    "L",
    "M",
    "N",
    "O",
    "P",
    "Q",
    "R",
    "S",
    "T",
    "U",
    "V",
    "W",
    "X",
    "Y",
    "Z",
  ];

  const letterNames = {
    A: "A",
    B: "B",
    C: "C",
    D: "D",
    E: "E",
    F: "F",
    G: "G",
    H: "H",
    I: "I",
    J: "J",
    K: "K",
    L: "L",
    M: "M",
    N: "N",
    O: "O",
    P: "P",
    Q: "Q",
    R: "R",
    S: "S",
    T: "T",
    U: "U",
    V: "V",
    W: "W",
    X: "X",
    Y: "Y",
    Z: "Z",
  };

  englishLetters.forEach((letter) => {
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
        window.location.href = `test_En.html?letter=${letterNames[letter]}`;
      }, 500);
    });

    lettersGrid.appendChild(letterButton);
  });
});
