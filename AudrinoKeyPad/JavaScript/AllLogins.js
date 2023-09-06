const jsonFilePath = '../../bin/Debug/LoginData.json';
var LastLogin = document.getElementById("allPass");

// Use the fetch API to load the JSON file
function fetchJson(){
    fetch(jsonFilePath)
        .then(response => {
            return response.json();
        })
        .then(data => {
            let str = "<ul>";
            for(let i = 0; i < data.attempts.length; i++ ){
                str += "<li>Login Attempt number: " + data.attempts[i].time + "Password Pressed: " + data.attempts[i].password +"</li>";
            }
            str += "</ul>";
            LastLogin.innerHTML = str;
        })
        .catch(error => {
            console.error('There was a problem fetching the JSON file:', error);
        });
}

// Call the function immediately to set the initial time
fetchJson();

// Set an interval to update the time every nth milliseconds
setInterval(fetchJson, 1000);