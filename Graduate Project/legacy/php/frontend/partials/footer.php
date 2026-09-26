    <div class="footer">
        <div class="container">
            <div class="box">
                <h3 class="glow">Thriving Together</h3>
                <ul class="social">
                    <li>
                        <a href="#" class="facebook">
                            <i class="fab fa-facebook-f"></i>
                        </a>
                    </li>
                    <li>
                        <a href="#" class="twitter">
                            <i class="fab fa-twitter"></i>
                        </a>
                    </li>
                    <li>
                        <a href="#" class="email">
                            <i class="fa-solid fa-envelope"></i>
                        </a>
                    </li>
                </ul>
                <p class="text">
                    "نحن هنا من أجل ان ننهض سويا"
                </p>
            </div>
            <div class="box">
                <ul class="links">
                    <li><a href="<?php echo $level; ?>index.php">الرئيسيه</a></li>
                    <?php if(!isset($_SESSION["loggeduser"])): ?>
                    <li><a href="<?php echo $level; ?>frontend/pages/auth/signup.php">إنشاء حساب</a></li>
                    <li><a href="<?php echo $level; ?>frontend/pages/auth/login.php">تسجيل الدخول</a></li>
                    <?php else: ?>
                    <li><a href="<?php echo $level; ?>frontend/pages/dashboard/profile.php">الملف الشخصي</a></li>
                    <li><a href="<?php echo $level; ?>backend/auth/logout.php">تسجيل الخروج</a></li>
                    <?php endif; ?>
                    <li><a href="<?php echo $level; ?>frontend/pages/articles/article.php">المقالات</a></li>
                    <li><a href="<?php echo $level; ?>frontend/pages/dashboard/diagnose.php">التشخيصات والعلاجات</a></li>
                    <li><a href="<?php echo $level; ?>frontend/pages/exams/exam.php">الاختبارات</a></li>
                </ul>
            </div>
            <div class="box footer-image">
                <img src="<?php echo $level; ?>frontend/assets/image/1.png" alt="">
            </div>
        </div>
        <p class="copyright">&copy;All Rights Reserved</p>
    </div>

    <script>
        let submenu = document.getElementById("submenu");
        function togglemenu() {
            if(submenu) submenu.classList.toggle("open-menu");
        }
    </script>
</body>
</html>
