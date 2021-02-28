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

    let url = window.location.href;
    let n = url.lastIndexOf('/');
    let id = url.substring(n + 1);
    console.log("id param:", id);

    $.ajax({
        type: "POST",
        url: `/Driver/GetRequiredData?id=${id}`,
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
            console.log(result);
            document.getElementById('start').value = result.storageAddress;
            document.getElementById('end').value = result.receiverAddress;
            calculateAndDisplayRoute(directionsService, directionsRenderer);
           
        },
        error: function (xhr, status, error) {
            var errorMessage = xhr.status + ': ' + xhr.statusText
            alert('Error - ' + errorMessage);
        }
    });
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