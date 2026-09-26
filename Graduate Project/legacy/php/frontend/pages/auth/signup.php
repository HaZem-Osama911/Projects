<?php
$level = '../../../';
$pageTitle = 'إنشاء حساب';
$extraCss = 'login.css';
require_once $level . 'frontend/partials/header.php';
?>
<section class="sec1">
    <div class="container">
        <div class="form-box Login">
            <h1>إنشاء حساب</h1>
            <form action="../../../backend/auth/sign.php" method="post" id="signup-form">
                <div class="input-box">
                    <input type="text" name="username" required placeholder="اسم المستخدم">
                    <i class="fa-solid fa-user"></i>
                </div>
                <div class="input-box">
                    <input type="email" name="email" required placeholder="البريد الإلكتروني">
                    <i class="fa-solid fa-envelope"></i>
                </div>
                <div class="input-box">
                    <input type="password" name="password" required placeholder="كلمة المرور">
                    <i class="fa-solid fa-lock"></i>
                </div>
                <div class="input-box">
                    <input type="text" name="Mobile" required placeholder="رقم الهاتف">
                    <i class="fa-solid fa-phone"></i>
                </div>
                <div class="input-box">
                    <button class="btn" type="submit">إنشاء حساب</button>
                </div>

                <div class="regi-link">
                    <p>لديك حساب بالفعل؟ <a href="login.php" class="loginlink">تسجيل الدخول</a></p>
                </div>
            </form>
        </div>
    </div>
</section>
<?php require_once $level . 'frontend/partials/footer.php'; ?>
