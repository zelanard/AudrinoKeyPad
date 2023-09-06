// get the element with the id "time"
var timeEle = document.getElementById("time");

// Create a function to update the time
function updateTime() {
    timeEle.innerHTML = new Date();
}

// Call the function immediately to set the initial time
updateTime();

// Set an interval to update the time every nth milliseconds
setInterval(updateTime, 1000);