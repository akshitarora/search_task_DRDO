<?php 

//to accept the incoming data from URL and put in database.

$conn = new mysqli("localhost", "acs_unity", "akashrao","acs_unity");

if ($conn->connect_error) {
	echo "connection failed"; die("Connection failed!");
} 

if ($_GET["s"] && $_GET["c"] && $_GET["toc"] && ($_GET["comm"] || $_POST["comm"])) { //checking if all parameters have been passed in URL or not.

$subject = $_GET["s"];
$capture = $_GET["c"];
$toc = $_GET["toc"];
if($_GET["comm"])
	$comments = $_GET["comm"];
else 
	$comments = $_POST["comm"];

if($comments == "captured"){
	if($capture == "13_thermal" || $capture == "14" || $capture == "45" || $capture == "test") {
		$comments = "Correct Capture! (" . $capture . ")";
		$capture = 1;
	}
	else {
		$comments = "Wrong Capture! (" . $capture . ")";
		$capture = 0;
	}
} else {
	$capture = 2;
}

$sql = "INSERT INTO memory_task (subject, capture, toc, comments) VALUES ('$subject','$capture','" . $_GET["toc"] . "','" .$comments. "');";

if ($conn->query($sql) === TRUE) {
	echo "You have captured: " . $_GET["c"];
}

}

mysql_close($conn);

?>