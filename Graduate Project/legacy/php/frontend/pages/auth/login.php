<?php
$level = '../../../';
$pageTitle = 'تسجيل الدخول';
$extraCss = 'login.css';
require_once $level . 'frontend/partials/header.php';
?>
<section class="sec1">
    <div class="container">
        <div class="form-box Login">
            <h1>تسجيل الدخول</h1>
            <form action="../../../backend/auth/log.php" method="post">
                <div class="input-box">
                    <input name="email" type="text" required placeholder="البريد الإلكتروني">
                    <i class="fa-solid fa-envelope"></i>
                </div>
                <div class="input-box">
                    <input name="password" type="password" required placeholder="كلمة المرور">
                    <i class="fa-solid fa-lock"></i>
                </div>
                <div class="remember-forget">
                    <label><input type="checkbox">تذكرني</label>
                    <a href="signup.php">هل نسيت كلمة المرور؟</a>
                </div>
                <div class="input-box">
                    <button class="btn" type="submit">تسجيل الدخول</button>
                </div>
                <div class="regi-link">
                    <p>ليس لديك حساب؟ <a href="signup.php" class="signuplink">إنشاء حساب</a></p>
                </div>
            </form>
        </div>
    </div>
</section>
<?php require_once $level . 'frontend/partials/footer.php'; ?>
