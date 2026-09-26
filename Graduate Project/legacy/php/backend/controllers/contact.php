<?php
require_once '../config/db.php';

$table_check_query = "SHOW TABLES LIKE 'yasssdb'";
$result = mysqli_query($conn, $table_check_query);

if (mysqli_num_rows($result) == 0) {
   
    $create_table_query = "CREATE TABLE yasssdb (
   
        first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
    phone VARCHAR(20) NOT NULL,
    message TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP

    )";

    if (mysqli_query($conn, $create_table_query)) {
        echo "Table 'users' created successfully.";
    } else {
        echo "Error creating table: " . mysqli_error($conn);
        exit; 
    }
}

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $firstName = isset($_POST['FirstName']) ? htmlspecialchars($_POST['FirstName']) : '';
    $lastName = isset($_POST['LastName']) ? htmlspecialchars($_POST['LastName']) : '';
    $email = isset($_POST['email']) ? htmlspecialchars($_POST['email']) : '';
    $phone = isset($_POST['phone']) ? htmlspecialchars($_POST['phone']) : '';
    $message = isset($_POST['message']) ? htmlspecialchars($_POST['message']) : '';

   
    if (!empty($firstName) && !empty($lastName) && !empty($email) && !empty($phone) && !empty($message)) {
       
        $stmt = $conn->prepare("INSERT INTO yasssdb (first_name, last_name, email, phone, message) VALUES (?, ?, ?, ?, ?)");
        $stmt->bind_param("sssss", $firstName, $lastName, $email, $phone, $message);

        if ($stmt->execute()) {
            echo "Message sent successfully.";
        } else {
            echo "Error: " . $stmt->error;
        }

        $stmt->close();
    } else {
        echo "All fields are required.";
    }
} else {
    echo "Invalid request method.";
}

$conn->close(); 









?>