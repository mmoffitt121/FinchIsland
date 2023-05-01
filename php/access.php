<?php
// get the input from the user
$email = $_GET['email'];
$pass = $_GET['pass'];

// connect to the database
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "licenses";

$conn = new mysqli($servername, $username, $password, $dbname);

// check the connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// build the SQL query with the user input
$sql = "SELECT * FROM primarylicenses WHERE Email = '$email' AND Password = '$pass'";

// execute the query and get the result set
$result = $conn->query($sql);

// display the results
if ($result !== false) {
    while($row = $result->fetch_assoc()) {
        echo $row["Id"]. "," .  
            $row["FirstName"]. "," . 
            $row["LastName"] . "," . 
            $row["Email"]; //"ID: " . $row["Id"] . " - Name: " . $row["FirstName"] . " - Email: " . $row["Email"] . "<br>";
    }
} else {
    echo "0 results";
}

// close the database connection
$conn->close();

// test url: http://localhost/app/access.php?email=admin@admin.com&pass=9e7cd9cb5a63a3591e16f4d835f32a1c4a84ab66e39ae27aa448c03b66bf63e7
?>