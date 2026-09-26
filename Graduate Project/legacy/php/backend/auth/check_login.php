<?php
// This helper file will be included in other PHP pages to check login status
function check_login() {
    if (!isset($_SESSION)) {
        session_start();
    }
    return isset($_SESSION["loggeduser"]);
}

function get_username() {
    if (!isset($_SESSION)) {
        session_start();
    }
    return isset($_SESSION["username"]) ? $_SESSION["username"] : '';
}

// Function to generate the header navigation based on login status
function generate_nav($isLoggedIn, $username) {
    $nav = '<ul class="main-nav">
        <li class="link"><a href="../controllers/home.php">الصفحة الرئيسية</a></li>
        <li class="link"><a href="../controllers/article.php">المقالات</a></li>
        <li class="link"><a href="../controllers/diagnose.php">التشخيصات والعلاجات</a></li>
        <li class="link"><a href="../controllers/exam.php">الاختبارات</a></li>
        <li class="nav-item"><a href="#Contact" class="nav-link">مساعدة</a></li>
        <li class="link"> <a href=""></a></li>';

    if($isLoggedIn) {
        $nav .= '<li class="link"><a href="../controllers/profile.php">الملف الشخصي</a></li>
                <li class="link"><a href="logout.php">تسجيل الخروج</a></li>';
    } else {
        $nav .= '<li class="link acc"><a href="../../frontend/pages/auth/signup.html" class="active">إنشاء حساب</a></li>
                <li class="link"><a href="../../frontend/pages/auth/login.html">تسجيل الدخول</a></li>';
    }

    $nav .= '</ul>';

    if($isLoggedIn) {
        $nav .= '<div class="profile">
                <img src="../../frontend/assets/image/user.png" alt="" onclick="togglemenu()">
            </div>
            <div class="menu" id="submenu">
                <div class="sub-menu">
                    <div class="sub-menu-info">
                        <img src="../../frontend/assets/image/user.png" alt="">
                        <h3>' . htmlspecialchars($username) . '</h3>
                    </div>
                    <hr>
                    <a href="../controllers/profile.php" class="sub-menu-link">
                        <i class="fa-solid fa-user-pen"></i>
                        <p>الملف الشخصي</p>
                        <span>></span>
                    </a>
                    <a href="#" class="sub-menu-link">
                        <i class="fa-solid fa-gear"></i>
                        <p>الإعدادات</p>
                        <span>></span>
                    </a>
                    <a href="#Contact" class="sub-menu-link">
                        <i class="fa-solid fa-circle-question"></i>
                        <p>المساعدة</p>
                        <span>></span>
                    </a>
                    <a href="logout.php" class="sub-menu-link">
                        <i class="fa-solid fa-right-from-bracket"></i>
                        <p>تسجيل الخروج</p>
                        <span>></span>
                    </a>
                </div>
            </div>';
    } else {
        $nav .= '<div class="profile">
                <img src="../../frontend/assets/image/user.png" alt="" onclick="togglemenu()">
            </div>
            <div class="menu" id="submenu">
                <div class="sub-menu">
                    <div class="sub-menu-info">
                        <img src="../../frontend/assets/image/user.png" alt="">
                        <h3>مرحباً بك</h3>
                    </div>
                    <hr>
                    <a href="../../frontend/pages/auth/login.html" class="sub-menu-link">
                        <i class="fa-solid fa-right-from-bracket"></i>
                        <p>تسجيل الدخول</p>
                        <span>></span>
                    </a>
                    <a href="../../frontend/pages/auth/signup.html" class="sub-menu-link">
                        <i class="fa-solid fa-user-pen"></i>
                        <p>إنشاء حساب</p>
                        <span>></span>
                    </a>
                </div>
            </div>';
    }

    return $nav;
}
?>