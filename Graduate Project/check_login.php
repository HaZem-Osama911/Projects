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
        <li class="link"><a href="home.php">الصفحه الرئيسيه</a></li>
        <li class="link"><a href="artical.php">المقالات</a></li>
        <li class="link"><a href="diagnose.php">التشخيصات والعلاجات</a></li>
        <li class="link"><a href="exam.php">الاختبارات</a></li>
        <li class="nav-item"><a href="#Contact" class="nav-link">مساعده</a></li>
        <li class="link"> <a href=""></a></li>';

    if($isLoggedIn) {
        $nav .= '<li class="link"><a href="profile.php">الملف الشخصي</a></li>
                <li class="link"><a href="logout.php">تسجيل الخروج</a></li>';
    } else {
        $nav .= '<li class="link acc"><a href="sighnup.html" class="active">انشاء حساب</a></li>
                <li class="link"><a href="login.html">تسجيل الدخول</a></li>';
    }

    $nav .= '</ul>';

    if($isLoggedIn) {
        $nav .= '<div class="profile">
                <img src="image/user.png" alt="" onclick="togglemenu()">
            </div>
            <div class="menu" id="submenu">
                <div class="sub-menu">
                    <div class="sub-menu-info">
                        <img src="image/user.png" alt="">
                        <h3>' . htmlspecialchars($username) . '</h3>
                    </div>
                    <hr>
                    <a href="profile.php" class="sub-menu-link">
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
                <img src="image/user.png" alt="" onclick="togglemenu()">
            </div>
            <div class="menu" id="submenu">
                <div class="sub-menu">
                    <div class="sub-menu-info">
                        <img src="image/user.png" alt="">
                        <h3>مرحباً بك</h3>
                    </div>
                    <hr>
                    <a href="login.html" class="sub-menu-link">
                        <i class="fa-solid fa-right-from-bracket"></i>
                        <p>تسجيل الدخول</p>
                        <span>></span>
                    </a>
                    <a href="sighnup.html" class="sub-menu-link">
                        <i class="fa-solid fa-user-pen"></i>
                        <p>انشاء حساب</p>
                        <span>></span>
                    </a>
                </div>
            </div>';
    }

    return $nav;
}
?>