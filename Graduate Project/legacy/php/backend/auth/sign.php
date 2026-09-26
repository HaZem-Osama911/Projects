<?php
session_start();

require_once '../config/db.php';

// Check if table exists, if not create it
$table_check_query = "SHOW TABLES LIKE 'yassdb'";
$result = mysqli_query($conn, $table_check_query);

if (mysqli_num_rows($result) == 0) {
    $create_table_query = "CREATE TABLE yassdb (
        id INT(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
        username VARCHAR(50) NOT NULL,
        email VARCHAR(100) NOT NULL UNIQUE,
        password VARCHAR(255) NOT NULL,
        Mobile VARCHAR(25)
    )";

    if (mysqli_query($conn, $create_table_query)) {
        echo "<script>alert('تم إنشاء قاعدة البيانات بنجاح');</script>";
    } else {
        echo "<script>alert('خطأ في إنشاء قاعدة البيانات: " . mysqli_error($conn) . "');</script>";
        exit;
    }
}

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $username = isset($_POST['username']) ? trim($_POST['username']) : '';
    $email = isset($_POST['email']) ? trim($_POST['email']) : '';
    $password = isset($_POST['password']) ? $_POST['password'] : '';
    $Mobile = isset($_POST['Mobile']) ? $_POST['Mobile'] : '';

    // Validate inputs
    if (empty($username) || empty($email) || empty($password) || empty($Mobile)) {
        echo "<script>alert('جميع الحقول مطلوبة');window.location.href='../../frontend/pages/auth/signup.php';</script>";
        exit();
    }

    // Check if email already exists
    $check_email = $conn->prepare("SELECT email FROM yassdb WHERE email = ?");
    $check_email->bind_param("s", $email);
    $check_email->execute();
    $email_result = $check_email->get_result();

    if ($email_result->num_rows > 0) {
        echo "<script>alert('البريد الإلكتروني مسجل بالفعل');window.location.href='../../frontend/pages/auth/signup.php';</script>";
        exit();
    }
    $check_email->close();

    // Process valid registration
    $hashed_password = password_hash($password, PASSWORD_DEFAULT);

    $stmt = $conn->prepare("INSERT INTO yassdb (username, email, password, Mobile) VALUES (?, ?, ?, ?)");
    $stmt->bind_param("ssss", $username, $email, $hashed_password, $Mobile);

    if ($stmt->execute()) {
        $_SESSION['account_created'] = true;
        $_SESSION['username'] = $username;

        // Success message with JavaScript alert
        echo "<script>
            alert('تم إنشاء الحساب بنجاح! يرجى تسجيل الدخول.');
            window.location.href = '../../frontend/pages/auth/login.php';
        </script>";
        exit();
    } else {
        echo "<script>alert('خطأ: " . $stmt->error . "');window.location.href='../../frontend/pages/auth/signup.php';</script>";
    }

    $stmt->close();
}

$conn->close();
?>
