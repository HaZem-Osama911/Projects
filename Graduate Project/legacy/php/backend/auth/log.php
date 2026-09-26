<?php
session_start();

require_once '../config/db.php';

// Check if the form was submitted
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $email = isset($_POST['email']) ? trim($_POST['email']) : '';
    $password = isset($_POST['password']) ? $_POST['password'] : '';
    $redirect = isset($_POST['redirect']) ? $_POST['redirect'] : '../../index.php';

    // Validate input
    if (empty($email)) {
        echo "<script>alert('البريد الإلكتروني مطلوب');window.location.href='../../frontend/pages/auth/login.php';</script>";
        exit;
    }

    $stmt = $conn->prepare("SELECT id, username, email, password FROM yassdb WHERE email = ?");

    if ($stmt) {
        $stmt->bind_param("s", $email);
        $stmt->execute();
        $result = $stmt->get_result();

        if ($result->num_rows == 1) {
            $row = $result->fetch_assoc();

            if (password_verify($password, $row['password'])) {
                // Password is correct - set session variables
                $_SESSION["loggeduser"] = $email;
                $_SESSION["username"] = $row['username'];
                $_SESSION["user_id"] = $row['id'];

                // Redirect to home page or specified page
                header('Location: ' . $redirect);
                exit();
            } else {
                echo "<script>alert('كلمة المرور غير صحيحة');window.location.href='../../frontend/pages/auth/login.php';</script>";
            }
        } else {
            echo "<script>alert('البريد الإلكتروني غير موجود');window.location.href='../../frontend/pages/auth/login.php';</script>";
        }

        $stmt->close();
    } else {
        echo "<script>alert('حدث خطأ في النظام');window.location.href='../../frontend/pages/auth/login.php';</script>";
    }
}

// If not a POST request, redirect to login page
if ($_SERVER["REQUEST_METHOD"] != "POST") {
    header('Location: ../../frontend/pages/auth/login.php');
    exit();
}

$conn->close();
?>