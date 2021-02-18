﻿
function initMap() {
    var startFiled = new google.maps.places.Autocomplete(document.getElementById('start'));
    var endFiled= new google.maps.places.Autocomplete(document.getElementById('end'));
    const directionsService = new google.maps.DirectionsService();
    const directionsRenderer = new google.maps.DirectionsRenderer();
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 7,
        center: { lat: 41.85, lng: -87.65 },
    });
    directionsRenderer.setMap(map);

    const onChangeHandler = function () {
        calculateAndDisplayRoute(directionsService, directionsRenderer);
    };

    document
        .getElementById("start")
        .addEventListener("change", onChangeHandler);
    document
        .getElementById("end")
        .addEventListener("change", onChangeHandler); 
}

function getURL() {
    var url = "https://www.google.com/maps?f=d&saddr="; url += document.getElementById("start").value + "&daddr=" + document.getElementById("end").value + "&dirflg=d";
        window.open(url, '_blank');
      //  document.location.href = url;
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
