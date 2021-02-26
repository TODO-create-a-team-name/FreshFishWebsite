var sAddr;
var startFiled = document.getElementById('start');
var endFiled = document.getElementById('end');
function initMap() {
    
    const directionsService = new google.maps.DirectionsService();
    const directionsRenderer = new google.maps.DirectionsRenderer();
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 7,
        center: { lat: 41.85, lng: -87.65 },
    });
    directionsRenderer.setMap(map);
  
        

    $.ajax({
        type: "POST",
        url: "/Driver/GetRequiredData",
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
            document.getElementById('start').value = result.sAddr;
            document.getElementById('end').value = result.dAddr;
            calculateAndDisplayRoute(directionsService, directionsRenderer);
           
        },
        error: function (xhr, status, error) {
            var errorMessage = xhr.status + ': ' + xhr.statusText
            alert('Error - ' + errorMessage);
        }
    });


    document.getElementById('start').addEventListener("change", onChangeHandler);
    document.getElementById('end').addEventListener("change", onChangeHandler);
}

function getURL() {

    
    var url = "https://www.google.com/maps?f=d&saddr="; url += document.getElementById("start").value + "&daddr=" + document.getElementById("end").value + "&dirflg=d";
    window.open(url, '_blank');
    }


function calculateAndDisplayRoute(directionsService, directionsRenderer) {
    directionsService.route(
        {
            origin: {
                query: document.getElementById("start").value,
            },
            destination: {
                query: document.getElementById("end").value,
            },
            travelMode: google.maps.TravelMode.DRIVING,
        },
        (response, status) => {
            if (status === "OK") {
                directionsRenderer.setDirections(response);
            } else {
                window.alert("Directions request failed due to " + status);
            }
        }
    );
    
}