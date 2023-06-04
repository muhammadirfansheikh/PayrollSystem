
function togglepassword(e) {
    $(e).toggleClass("fa-eye-slash");
    var control = '.' + $(e).attr("toggle");
    var input = $(control);
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
}

function checkStrength(e) {
    let strength = 0;
    var password = e.value.trim();
    let passwordStrength = document.getElementById("password-strength");
    passwordStrength.classList.remove('progress-bar-warning');
    passwordStrength.classList.remove('progress-bar-success')
    passwordStrength.classList.remove('progress-bar-danger');;
    passwordStrength.classList.remove('progress-bar-info');
    passwordStrength.style = 'width: 0%';
    if (password.length > 0) {
        //if (password.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) {

        //If password contains  upper characters
        if (password.match(/([A-Z])/)) {
            strength += 1;
        }
        //If password contains  lower characters
        if (password.match(/([a-z])/)) {
            strength += 1;
        }
        //If it has numbers 
        if (password.match(/([0-9])/)) {
            strength += 1;
        }

        //If it has one special character
        if (password.match(/(?=.*?[#?!@$%^&*-])/)) {
            strength += 1;
        }
        //If password is greater than 11
        if (password.length > 9) {
            strength += 1;
        }

        // If value is less than 2
        if (strength == 1) {
            passwordStrength.classList.add('progress-bar-danger');
            passwordStrength.style = 'width: 20%';
        }
        else if (strength == 2) {
            passwordStrength.classList.add('progress-bar-danger');
            passwordStrength.style = 'width: 40%';
        }
        else if (strength == 3) {
            passwordStrength.classList.add('progress-bar-warning');
            passwordStrength.style = 'width: 60%';
        } else if (strength == 4) {
            passwordStrength.classList.add('progress-bar-info');
            passwordStrength.style = 'width: 80%';
        } else if (strength == 5) {
            passwordStrength.classList.add('progress-bar-success');
            passwordStrength.style = 'width: 100%';
        }
    }
}
