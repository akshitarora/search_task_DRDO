<?php 

//to accept the incoming data from URL and put in database.

$conn = new mysqli("localhost", "acs_unity", "akashrao","acs_unity");

if ($conn->connect_error) {
	echo "connection failed"; die("Connection failed!");
} 

$subject = $_GET["s"];
$capture = $_GET["c"];
$toc = $_GET["toc"];
$comments = $_GET["comm"];

if($comments == "captured"){
	if($capture == "10" || $capture == "2_MK12Sniper" || $capture == "3_M41AssaultRifle" || $capture == "19_magnetometersensor" || $capture == "20_NGIAma") {
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

$sql = "INSERT INTO memory_task (subject, capture, toc, comments) VALUES ('$subject','$capture','$toc','$comments');";

if ($conn->query($sql) === TRUE) {
	echo "You have captured: " . $_GET["c"];
}

mysql_close($conn);

?>