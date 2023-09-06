const jsonFilePath = '../../bin/Debug/LoginData.json';
var LastLogin = document.getElementById("last_login");

// Use the fetch API to load the JSON file
function fetchJson(){
    fetch(jsonFilePath)
        .then(response => {
            return response.json();
        })
        .then(data => {
            LastLogin.innerHTML = "Last Login Attempt Nr.: " + data.attempts[data.attempts.length - 1].time + "<br>Password Pressed: " + data.attempts[data.attempts.length - 1].password;
        })
        .catch(error => {
            console.error('There was a problem fetching the JSON file:', error);
        });
}

// Call the function immediately to set the initial time
fetchJson();

// Set an interval to update the time every nth milliseconds
setInterval(fetchJson, 1000);