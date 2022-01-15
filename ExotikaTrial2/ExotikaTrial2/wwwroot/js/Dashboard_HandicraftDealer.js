

google.charts.load('current', {'packages':['corechart']});
google.charts.setOnLoadCallback(drawChart);


// Draw the chart and set the chart values
function drawChart() {
	var data = google.visualization.arrayToDataTable([
		['Month', 'Site Visitors', 'Registrations', 'Listed Products'],
		['January 2021', 25, 2, 5],
		['February 2021', 33, 3, 2],
		['March 2021', 41, 2, 3],
		['April 2021', 23, 1, 2],
		['May 2021', 38, 5, 3],
		['June 2021', 25, 3, 2],
		['July 2021', 18, 2, 2],
		['August 2021', 20, 4, 5]
	]);


	// Add a title and set the width and height of the chart
	var options = { 
		curveType: 'function', width:"100%", height:500,
		legend: { position: 'bottom' }
	};


	// Display the chart inside the <div> element with id="piechart"
	var chart = new google.visualization.LineChart(document.getElementById('StatChart'));
	chart.draw(data, options);
}