<?php
if (session_status() === PHP_SESSION_NONE) {
    session_start();
}
$isLoggedIn = isset($_SESSION["loggeduser"]);
$username = $isLoggedIn ? $_SESSION["username"] : '';

if (!isset($level)) {
    $level = "";
}
?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title><?php echo isset($pageTitle) ? $pageTitle : 'Thriving Together'; ?></title>
    <link rel="shortcut icon" type="x-icon" href="<?php echo $level; ?>frontend/assets/image/logo.png">
    <link rel="stylesheet" href="<?php echo $level; ?>frontend/assets/css/home.css">
    <?php if(isset($extraCss)): ?>
    <link rel="stylesheet" href="<?php echo $level; ?>frontend/assets/css/<?php echo $extraCss; ?>">
    <?php endif; ?>
    <link rel="stylesheet" href="<?php echo $level; ?>frontend/assets/css/all.min.css">
    <link rel="stylesheet" href="<?php echo $level; ?>frontend/assets/css/normalize.css">
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Cairo:wght@200..1000&display=swap" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css2?family=Lalezar&family=Work+Sans:ital,wght@0,100..900;1,100..900&display=swap" rel="stylesheet">
</head>
<body>
    <div class="header">
        <div class="container">
            <a href="<?php echo $level; ?>index.php" class="logo"><img src="<?php echo $level; ?>frontend/assets/image/logo.png" alt="logo"></a>
            <input type="checkbox" id="check">
            <label for="check" class="check-list">
                <i class="fa-solid fa-list"></i>
            </label>
            <ul class="main-nav">
                <li class="link"><a href="<?php echo $level; ?>index.php">الصفحة الرئيسية</a></li>
                <li class="link"><a href="<?php echo $level; ?>frontend/pages/articles/article.php">المقالات</a></li>
                <li class="link"><a href="<?php echo $level; ?>frontend/pages/dashboard/diagnose.php">التشخيصات والعلاجات</a></li>
                <li class="link"><a href="<?php echo $level; ?>frontend/pages/exams/exam.php">الاختبارات</a></li>
                <li class="nav-item"><a href="#Contact" class="nav-link">مساعدة</a></li>
                <li class="link"> <a href=""></a></li>

                <?php if($isLoggedIn): ?>
                <li class="link"><a href="<?php echo $level; ?>frontend/pages/dashboard/profile.php">الملف الشخصي</a></li>
                <li class="link"><a href="<?php echo $level; ?>backend/auth/logout.php">تسجيل الخروج</a></li>
                <?php else: ?>
                <li class="link acc"><a href="<?php echo $level; ?>frontend/pages/auth/signup.php" class="active">إنشاء حساب</a></li>
                <li class="link"><a href="<?php echo $level; ?>frontend/pages/auth/login.php">تسجيل الدخول</a></li>
                <?php endif; ?>
            </ul>

            <?php if($isLoggedIn): ?>
            <div class="profile">
                <input type="checkbox" id="toggle-menu">
                <label for="toggle-menu">
                    <i class="fa-solid fa-user"></i>
                </label>
                <div class="menu" id="submenu">
                    <div class="sub-menu">
                        <div class="sub-menu-info">
                            <i class="fa-solid fa-user"></i>
                            <h3><?php echo htmlspecialchars($username); ?></h3>
                        </div>
                        <hr>
                        <a href="<?php echo $level; ?>frontend/pages/dashboard/profile.php" class="sub-menu-link">
                            <i class="fa-solid fa-user-pen"></i>
                            <p>الملف الشخصي</p>
                            <span>></span>
                        </a>
                        <a href="#" class="sub-menu-link">
                            <i class="fa-solid fa-gear"></i>
                            <p>الإعدادات والخصوصية</p>
                            <span>></span>
                        </a>
                        <a href="#Contact" class="sub-menu-link">
                            <i class="fa-solid fa-circle-question"></i>
                            <p>المساعدة والدعم</p>
                            <span>></span>
                        </a>
                        <a href="<?php echo $level; ?>backend/auth/logout.php" class="sub-menu-link">
                            <i class="fa-solid fa-right-from-bracket"></i>
                            <p>تسجيل الخروج</p>
                            <span>></span>
                        </a>
                    </div>
                </div>
            </div>
            <?php endif; ?>
        </div>
    </div>
