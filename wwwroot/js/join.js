/* JavaScript file for the forms in join.cshtml */
var LoginForm = document.getElementById("LoginForm");
var RegForm = document.getElementById("RegForm");
var Indicator = document.getElementById("Indicator");
function register() {
    LoginForm.style.transform = "translateX(400px)";
    RegForm.style.transform = "translateX(400px)";
    Indicator.style.transform = "translateX(0px)";
}
function login() {
    LoginForm.style.transform = "translateX(0px)";
    RegForm.style.transform = "translateX(0px)";
    Indicator.style.transform = "translateX(100px)";
}