<?php

$conn = new mysqli("localhost", "acs_unity", "akashrao","acs_unity");

if ($conn->connect_error) {
	echo "connection failed"; die("Connection failed!");
} 
$sql = "SELECT * FROM `memory_task` ORDER BY `Timestamp` DESC";


	$result = mysqli_query($conn,$sql);
	?>

<!DOCTYPE html>
<html>
<head>
<style>
table {
    font-family: arial, sans-serif;
    border-collapse: collapse;
    width: 100%;
}

td, th {
    border: 1px solid #dddddd;
    text-align: left;
    padding: 8px;
}

tr:nth-child(even) {
    background-color: #dddddd;
}
</style>
</head>
<body>
<center><h1>SEARCH TASK</h1></center>
<br><br><br>
<p>For column 'capture': 2 = game initialization, 1 = correct capture, 0 = wrong capture.<br>
'Timestamp' = the time at server when data entry was made.<br>
'toc' = time of capture; the time at client at which the marker was captured.</p>
<br><br><br>
<table>
  <tr>
    <th>Sno</th>
    <th>Timestamp</th>
    <th>subject</th>
    <th>capture</th>
    <th>toc</th>
    <th>comments</th>
  </tr>
  
    <?php
    	while($row = mysqli_fetch_array($result,MYSQLI_ASSOC)) {
    		echo '<tr>';
			echo '<td>' . $row['Sno'] . '</td>';
			echo '<td>' . $row['Timestamp'] . '</td>';
			echo '<td>' . $row['subject'] . '</td>';
			echo '<td>' . $row['capture'] . '</td>';
			echo '<td>' . $row['toc'] . '</td>';
			echo '<td>' . $row['comments'] . '</td>';
			echo '</tr>';
		}
    ?>
  
</table>

</body>
</html>


	<?php


mysql_close($conn);

?>