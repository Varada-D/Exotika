let subBtn = document.querySelector('#submit')
			
subBtn.addEventListener('click', (e) => {
	let cnf_val = document.getElementById('confPassword').value
	let pass_val = document.getElementById('password').value
	if(!(pass_val === cnf_val)) {
		e.preventDefault()
		window.alert('Passwords do not match!')
	}
	else {
		window.location.href="../SignIn.html";
	}
			
})