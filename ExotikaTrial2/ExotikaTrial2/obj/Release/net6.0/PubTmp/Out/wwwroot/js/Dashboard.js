

google.charts.load('current', {'packages':['corechart']});
google.charts.setOnLoadCallback(drawChart);


// Draw the chart and set the chart values
function drawChart() {
	var data = google.visualization.arrayToDataTable([
		['Month', 'Visitor', 'Resort Owner', 'Vendor', 'Handicraft Dealer'],
		['January 2021', 25, 2, 5, 2],
		['February 2021', 33, 3, 2, 1],
		['March 2021', 41, 2, 3, 4],
		['April 2021', 23, 1, 2, 0],
		['May 2021', 38, 0, 3, 1],
		['June 2021', 25, 3, 2, 1],
		['July 2021', 18, 0, 2, 3],
		['August 2021', 20, 0, 0, 2]
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