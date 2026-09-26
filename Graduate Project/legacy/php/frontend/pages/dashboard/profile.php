<?php
$level = '../../../';
$pageTitle = 'الملف الشخصي';
require_once $level . 'frontend/partials/header.php';

if (!$isLoggedIn) {
    echo "<script>window.location.href='../auth/login.php';</script>";
    exit();
}

$email = trim($_SESSION["loggeduser"]);
require_once $level . 'backend/config/db.php';

if (!$conn) {
    die("Database connection failed: " . mysqli_connect_error());
}

$viewuser = "SELECT * FROM yassdb WHERE email = ?";
$stmt = mysqli_prepare($conn, $viewuser);

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
            echo "<script>alert('المستخدم غير موجود');window.location.href='../auth/login.php';</script>";
            exit();
        }
    } else {
        echo "<script>alert('خطأ في الاستعلام');window.location.href='../auth/login.php';</script>";
        exit();
    }
    mysqli_stmt_close($stmt);
} else {
    echo "<script>alert('خطأ في إعداد الاستعلام');window.location.href='../auth/login.php';</script>";
    exit();
}
// don't close connection here because footer might need it, or it's fine.
?>
<section class="profile-section">
        <div class="container">
            <div class="profile-box">
                <h1>الملف الشخصي</h1>
                <div class="profile-info">
                    <div class="profile-image">
                        <img src="../../assets/image/user.png" alt="User Profile">
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
                            <a href="../../../backend/auth/logout.php" class="btn logout-btn">تسجيل الخروج</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    
<?php require_once $level . 'frontend/partials/footer.php'; ?>
