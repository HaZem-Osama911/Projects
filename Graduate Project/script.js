function validation(){
	var valid = true;
	
	formLabels = document.getElementsByTagName("label");
	
	var username = document.regForm.username.value;
	if(username==""){
		formLabels[0].innerHTML="username: [Required]";
		formLabels[0].style="color: red";
		valid = false;
	}
	else if( !isNaN(username)){
		formLabels[0].innerHTML="username: [Text Only]";
		formLabels[0].style="color: red";
		valid = false;
	}
	else {
		formLabels[0].innerHTML="username:";
		formLabels[0].style="color: black";
		valid = (valid) ? true : false;
	}
	
	
	var email = document.regForm.email.value;
	var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
	if(email==""){
		formLabels[1].innerHTML="Email: [Required]";
		formLabels[1].style="color: red";
		valid = false;
	}
	else if(!re.test(email)){
		formLabels[1].innerHTML="Email: [Incorrect Email]";
		formLabels[1].style="color: red";
		valid = false;
	}
	else {
		formLabels[1].innerHTML="Email:";
		formLabels[1].style="color: black";
		valid = (valid) ? true : false;
	}
	
	var password = document.regForm.password.value;
	if(password == ""){
		formLabels[2].innerHTML="Password: [Required]";
		formLabels[2].style="color: red";
		valid = false;
	}
	else if(password.length < 8){
		formLabels[2].innerHTML="Password: [Must be > 8]";
		formLabels[2].style="color: red";
		valid = false;
	}
	else {
		formLabels[2].innerHTML="Password:";
		formLabels[2].style="color: black";
		valid = (valid) ? true : false;
	}
	
	
	
	var mobile = document.regForm.mobile.value;
	if( isNaN(mobile)){
		formLabels[3].innerHTML="Mobile: [Numbers Only]";
		formLabels[3].style="color: red";
		valid = false;
	}
	else {
		formLabels[3].innerHTML="Mobile:";
		formLabels[3].style="color: black";
		valid = (valid) ? true : false;
	}
	
	return valid;
}
