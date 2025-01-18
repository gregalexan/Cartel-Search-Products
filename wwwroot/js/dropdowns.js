/* JS File to handle the dropdowns for Products and Suppliers */

/* When the user clicks on the button,
toggle between hiding and showing the dropdown content */
function showAccountDropdown() {
  document.getElementById("accountDropDown").classList.toggle("show");
}
function showCategoriesDropdown() {
  document.getElementById("categoriesDropDown").classList.toggle("show");
}
// Close the dropdown menu if the user clicks outside of it
window.onclick = function(event) {
    if (!event.target.matches('.dropbtn')) {
      var dropdowns = document.getElementsByClassName("dropdown-content");
      var i;
      for (i = 0; i < dropdowns.length; i++) {
        var openDropdown = dropdowns[i];
        if (openDropdown.classList.contains('show')) {
          openDropdown.classList.remove('show');
        }
      }
    }
}
