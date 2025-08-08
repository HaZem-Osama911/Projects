<?php
//session_start();
//session_destroy(); // تدمير الجلسة
//header('Location: login.html'); // إعادة التوجيه إلى صفحة تسجيل الدخول بعد الخروج
//exit();
//?>
<?php
session_start();

// Clear all session variables
$_SESSION = array();

// Destroy the session
session_destroy();

// Redirect to login page
header('Location: login.html');
exit();
?>