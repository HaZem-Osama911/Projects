<?php
//session_start();
//
//if (!isset($_SESSION["loggeduser"])) {
//    header('Location: log.php');
//    exit();
//}
//
//$email = trim($_SESSION["loggeduser"]);
//echo "Logged-in user email: " . htmlspecialchars($email) . "<br>";
//
//include "config.php";
//
//if (!$con) {
//    die("Database connection failed: " . mysqli_connect_error());
//}
//
//$viewuser = "SELECT * FROM yassdb WHERE email = ?";
//$stmt = mysqli_prepare($con, $viewuser);
//
//if ($stmt) {
//    mysqli_stmt_bind_param($stmt, "s", $email);
//    mysqli_stmt_execute($stmt);
//    $result = mysqli_stmt_get_result($stmt);
//
//    if ($result) {
//        $row = mysqli_fetch_array($result);
//        if ($row) {
//            $username = $row["username"];
//            $email = $row["email"];
//            $Mobile = $row["Mobile"];
//        } else {
//            echo "User not found. Please check the email in the session and database.<br>";
//
//            echo "Query: SELECT * FROM yassdb WHERE email = '$email'<br>";
//            exit();
//        }
//    } else {
//        echo "Error executing query: " . mysqli_error($con);
//        exit();
//    }
//    mysqli_stmt_close($stmt);
//} else {
//    echo "Error preparing statement: " . mysqli_error($con);
//    exit();
//}
//
//mysqli_close($con);
//?>
<!---->
<!---->
<!--?>-->
<!---->
<!--<!DOCTYPE html>-->
<!--<html lang="en">-->
<!--<head>-->
<!--    <meta http-equiv="X-UA-Compatible" content="IE=edge" />-->
<!--    <title>User Profile</title>-->
<!--</head>-->
<!--<body>-->
<!--    <h1>User Profile</h1>-->
<!--    <label>Username:</label>-->
<!--    <p>--><?php //echo htmlspecialchars($username); ?>
<!--</p>-->
<!---->
<!--    <label>Email:</label>-->
<!--    <p>--><?php //echo htmlspecialchars($email); ?>
<!--</p>-->
<!---->
<!--   -->
<!--    <label>Mobile:</label>-->
<!--    <p>--><?php //echo htmlspecialchars($Mobile); ?>
<!--</p>-->
<!--    <a href="home.php"> الصفحه الرئيسيه</a>>-->
<!--</body>-->
<!--</html>-->
<?php
session_start();

// Check if user is logged in
if (!isset($_SESSION["loggeduser"])) {
    header('Location: login.html');
    exit();
}

$email = trim($_SESSION["loggeduser"]);

include "config.php";

if (!$con) {
    die("Database connection failed: " . mysqli_connect_error());
}

$viewuser = "SELECT * FROM yassdb WHERE email = ?";
$stmt = mysqli_prepare($con, $viewuser);

if ($stmt) {
    mysqli_stmt_bind_param($stmt, "s", $email);
    mysqli_stmt_execute($stmt);
    $result = mysqli_stmt_get_result($stmt);

    if ($result) {
        $row = mysqli_fetch_array($result);
        if ($row) {
            $username = $row["username"];
            $email = $row["email"];
            $mobile = $row["Mobile"];
            $user_id = $row["id"];
        } else {
            echo "<script>alert('المستخدم غير موجود');window.location.href='login.html';</script>";
            exit();
        }
    } else {
        echo "<script>alert('خطأ في الاستعلام');window.location.href='login.html';</script>";
        exit();
    }
    mysqli_stmt_close($stmt);
} else {
    echo "<script>alert('خطأ في إعداد الاستعلام');window.location.href='login.html';</script>";
    exit();
}

mysqli_close($con);
?>

<!DOCTYPE html>
<html>

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>الملف الشخصي</title>
    <link rel="shortcut icon" type="x-icon" href="image/logo.png">
    <link rel="stylesheet" href="css/home.css">
    <link rel="stylesheet" href="css/profile.css">
    <link rel="stylesheet" href="css/all.min.css">
    <link rel="stylesheet" href="css/normalize.css">
    <link rel="preconnect" href="https:fonts.googleapis.com">
    <link rel="preconnect" href="https:fonts.gstatic.com" crossorigin>
    <link href="https:fonts.googleapis.com/css2?family=Cairo:wght@200..1000&display=swap" rel="stylesheet">
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link
        href="https://fonts.googleapis.com/css2?family=Lalezar&family=Work+Sans:ital,wght@0,100..900;1,100..900&display=swap"
        rel="stylesheet">
</head>

<body>
    <div class="header">
        <div class="container">
            <a href="home.php" class="logo"><img src="image/logo.png" alt="logo"></a>
            <input type="checkbox" id="check">
            <label for="check" class="check-list">
                <i class="fa-solid fa-list"></i>
            </label>
            <ul class="main-nav">
                <li class="link"><a href="home.php">الصفحه الرئيسيه</a></li>
                <li class="link"><a href="artical.php">المقالات</a></li>
                <li class="link"><a href="diagnose.php">التشخيصات والعلاجات</a></li>
                <li class="link"><a href="exam.php">الاختبارات</a></li>
                <li class="nav-item"><a href="#Contact" class="nav-link">مساعده</a></li>
                <li class="link"> <a href=""></a> </li>
                <li class="link"><a href="logout.php" class="active">تسجيل الخروج</a></li>
            </ul>
            <div class="profile">
            <input type="checkbox" id="toggle-menu">
                <label for="toggle-menu">
                    <i class="fa-solid fa-user"></i>
                </label>
            </div>
            <div class="menu" id="submenu">
                <div class="sub-menu">
                    <div class="sub-menu-info">
                        <img src="image/user.png" alt="">
                        <h3><?php echo htmlspecialchars($username); ?></h3>
                    </div>
                    <hr>
                    <a href="profile.php" class="sub-menu-link">
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
                    <a href="logout.php" class="sub-menu-link">
                        <i class="fa-solid fa-right-from-bracket"></i>
                        <p>تسجيل الخروج</p>
                        <span>></span>
                    </a>
                </div>
            </div>
        </div>
    </div>

    <section class="profile-section">
        <div class="container">
            <div class="profile-box">
                <h1>الملف الشخصي</h1>
                <div class="profile-info">
                    <div class="profile-image">
                        <img src="image/user.png" alt="User Profile">
                    </div>
                    <div class="user-details">
                        <div class="detail-item">
                            <span class="label">اسم المستخدم:</span>
                            <span class="value"><?php echo htmlspecialchars($username); ?></span>
                        </div>
                        <div class="detail-item">
                            <span class="label">البريد الإلكتروني:</span>
                            <span class="value"><?php echo htmlspecialchars($email); ?></span>
                        </div>
                        <div class="detail-item">
                            <span class="label">رقم الهاتف:</span>
                            <span class="value"><?php echo htmlspecialchars($mobile); ?></span>
                        </div>
                        <div class="actions">
                            <a href="#" class="btn edit-btn">تعديل البيانات</a>
                            <a href="logout.php" class="btn logout-btn">تسجيل الخروج</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <div class="footer">
        <div class="container">
            <div class="box">
                <h3>Thriving Together</h3>
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
                    <li><a href="home.php">الرئيسيه</a></li>
                    <li><a href="signup.php">انشاء حساب</a></li>
                    <li><a href="login.html">تسجيل الدخول</a></li>
                    <li><a href="article.php">المقالات</a></li>
                    <li><a href="diagnoses.php">تشخيصات وعلاجات</a></li>
                    <li><a href="exercises.php">الاختبارات</a></li>
                    <li><a href="profile.php">المعلومات الشخصية</a></li>
                </ul>
            </div>
            <div class="box footer-image">
                <img src="image/1.png" alt="">
            </div>
        </div>
        <p class="copyright">&copy;All Rights Reserved</p>
    </div>

    <script>
    let submenu = document.getElementById("submenu");

    function togglemenu() {
        submenu.classList.toggle("open-menu");
    }
    </script>
</body>

</html>