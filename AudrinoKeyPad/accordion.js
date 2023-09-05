/** Check that there are accordions, 
 * if there are, create a variable with all accordions in it.
 */
if (document.getElementsByClassName("accordion") != null) {
    var acc = document.getElementsByClassName("accordion");
}

/** Add event listeners to the accordions.
 * on click, toggle the active class of the accordion 
 * then toggle the hidden class accordions next sibling.
 */
for (var i = 0; i < acc.length; i++) {
    acc[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var panel = this.nextElementSibling;
        panel.classList.toggle("hidden");
    });
}