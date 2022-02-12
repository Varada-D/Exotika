/* Toggle between adding and removing the "responsive" class to topnav when the user clicks on the icon */

function myFunction() {
	var x = document.getElementById("navbar");

	if (x.className === "topnav") {
		x.className += " responsive";
	}

	else {
		x.className = "topnav";
	}
}




/* Shrink the header on scroll */

window.onscroll = function() { scrollFunction() };

function scrollFunction() {
	if (document.body.scrollTop>80 || document.documentElement.scrollTop>80)
		{
			document.getElementById('logo').style.width = "25vh";
			//document.getElementById('SignInRegisterBtnLS').style.marginTop = "2.5vh";
			document.getElementById('btns-lg-screen').style.marginTop = "2vh";
			document.getElementById('btns-lg-screen').style.fontSize = "90%";
		}
	else
		{
			document.getElementById('logo').style.width = "40vh";
			//document.getElementById('SignInRegisterBtnLS').style.marginTop = "2.5vh";
			document.getElementById('btns-lg-screen').style.marginTop = "5.5vh";
			document.getElementById('btns-lg-screen').style.fontSize = "100%";
	}
}