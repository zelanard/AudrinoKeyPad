if(document.getElementsByClassName("accordion") != null){
    var acc = document.getElementsByClassName("accordion");
    var i;    
}

for (i = 0; i < acc.length; i++) {
    acc[i].addEventListener("click", function() {
        this.classList.toggle("active");
        var panel = this.nextElementSibling;
        if (panel.style.display === "block") {
            try {
                panel.style.display = "none";                
                console.log("none");
            } catch (error) {
                console.log("Error None");                
            }
        }else{
            try {
                panel.style.display = "block";
                console.log("block");                
            } catch (error) {
                console.log("Error Block");                
            }
        }
    });
}

function accordion(){
    var runOnce = 0;
    try {
        $(document).click(function(event){
            if(event.target.classList.contains("tiny_accordion") && runOnce == 0){
                event.target.classList.toggle("active");
                var panel = event.target.nextElementSibling;
                if (panel.style.display === "block") {
                    try {
                        panel.style.display = "none";                
                        console.log("none");
                        runOnce = 1;
                    } catch (error) {
                        console.log("Error None");                
                    }
                }else{
                    try {
                        panel.style.display = "block";
                        console.log("block");                
                        runOnce = 1;
                    } catch (error) {
                        console.log("Error Block");                
                    }
                }    
            }
        });
    } catch (error) {
        console.log("error accordion().setAccord")
    }
}