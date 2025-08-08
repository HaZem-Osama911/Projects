<?php
////$db_server = "localhost";
////$db_user = "root";
////$db_pass = "";
////$db_name = "yasdb";
////
////
////$conn = mysqli_connect($db_server, $db_user, $db_pass, $db_name);
////
////
////if (!$conn) {
////    die("Connection failed: " .mysqli_connect_error());
////}
////
////if ($_SERVER["REQUEST_METHOD"] == "POST") {
////    $email = isset($_POST['email']) ? trim($_POST['email']) : '';
////    $password = isset($_POST['password']) ? $_POST['password'] : '';
////
////
////    if (empty($email)) {
////        echo "Invalid email.";
////        exit;
////    }
////
////    $stmt = $conn->prepare("SELECT email, password FROM yassdb WHERE email = ?");
////
////    if ($stmt) {
////        $stmt->bind_param("s", $email);
////        $stmt->execute();
////        $result = $stmt->get_result();
////
////        if ($result->num_rows == 1) {
////            $row = $result->fetch_assoc();
////
////            if (password_verify($password, $row['password'])) {
////                session_start();
////                $_SESSION["loggeduser"] =$email;
////                header('Location: profile.php');
////            } else {
////                echo "Invalid password.";
////            }
////        } else {
////            echo "Invalid email.";
////        }
////
////        $stmt->close();
////    } else {
////        echo "Error preparing statement: " . $conn->error;
////    }
////}
////
////$conn->close();
////?>
<?php
//session_start(); // Start the session at the beginning
//
//$db_server = "localhost";
//$db_user = "root";
//$db_pass = "";
//$db_name = "yasdb";
//
//$conn = mysqli_connect($db_server, $db_user, $db_pass, $db_name);
//
//if (!$conn) {
//    die("Connection failed: " . mysqli_connect_error());
//}
//
//// Check if the form was submitted
//if ($_SERVER["REQUEST_METHOD"] == "POST") {
//    $email = isset($_POST['email']) ? trim($_POST['email']) : '';
//    $password = isset($_POST['password']) ? $_POST['password'] : '';
//
//    // Validate input
//    if (empty($email)) {
//        echo "<script>alert('البريد الإلكتروني مطلوب');window.location.href='login.html';</script>";
//        exit;
//    }
//
//    $stmt = $conn->prepare("SELECT id, username, email, password FROM yassdb WHERE email = ?");
//
//    if ($stmt) {
//        $stmt->bind_param("s", $email);
//        $stmt->execute();
//        $result = $stmt->get_result();
//
//        if ($result->num_rows == 1) {
//            $row = $result->fetch_assoc();
//
//            if (password_verify($password, $row['password'])) {
//                // Password is correct - set session variables
//                $_SESSION["loggeduser"] = $email;
//                $_SESSION["username"] = $row['username'];
//                $_SESSION["user_id"] = $row['id'];
//
//                // Redirect to profile page
//                header('Location: profile.php');
//                exit(); // Important to prevent further execution
//            } else {
//                echo "<script>alert('كلمة المرور غير صحيحة');window.location.href='login.html';</script>";
//            }
//        } else {
//            echo "<script>alert('البريد الإلكتروني غير موجود');window.location.href='login.html';</script>";
//        }
//
//        $stmt->close();
//    } else {
//        echo "<script>alert('حدث خطأ في النظام');window.location.href='login.html';</script>";
//    }
//}
//
//// If not a POST request, redirect to login page
//if ($_SERVER["REQUEST_METHOD"] != "POST") {
//    header('Location: login.html');
//    exit();
//}
//
//$conn->close();
//?>
<?php
session_start(); // Start the session at the beginning

$db_server = "localhost";
$db_user = "root";
$db_pass = "";
$db_name = "yasdb";

$conn = mysqli_connect($db_server, $db_user, $db_pass, $db_name);

if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}

// Check if the form was submitted
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $email = isset($_POST['email']) ? trim($_POST['email']) : '';
    $password = isset($_POST['password']) ? $_POST['password'] : '';
    $redirect = isset($_POST['redirect']) ? $_POST['redirect'] : 'home.php';

    // Validate input
    if (empty($email)) {
        echo "<script>alert('البريد الإلكتروني مطلوب');window.location.href='login.html';</script>";
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
                echo "<script>alert('كلمة المرور غير صحيحة');window.location.href='login.html';</script>";
            }
        } else {
            echo "<script>alert('البريد الإلكتروني غير موجود');window.location.href='login.html';</script>";
        }

        $stmt->close();
    } else {
        echo "<script>alert('حدث خطأ في النظام');window.location.href='login.html';</script>";
    }
}

// If not a POST request, redirect to login page
if ($_SERVER["REQUEST_METHOD"] != "POST") {
    header('Location: login.html');
    exit();
}

$conn->close();
?>