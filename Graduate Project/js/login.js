const sec = document.querySelector('.sec');
const registerBtn = document.querySelector('.register-btn');
const loginBtn = document.querySelector('.login-btn');

registerBtn.addEventListener('click', () => {
    sec.classList.add('active');
});

loginBtn.addEventListener('click', () => {
    sec.classList.remove('active');
});
