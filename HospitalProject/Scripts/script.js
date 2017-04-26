
/* I used a stack overflow answer as a guide to get this working. It can be found here:
http://stackoverflow.com/questions/14586916/google-maps-directions-from-users-geo-location
*/


function initializeMap() {
    //start of first attempt
    if (navigator.geolocation) { //checks to see if the browser supports geolocation
        navigator.geolocation.getCurrentPosition(function (position) {
            var myLat = position.coords.latitude;
            var myLong = position.coords.longitude;
            var myPosition = new google.maps.LatLng(myLat, myLong);
            var directionsService = new google.maps.DirectionsService();
            var directionsDisplay = new google.maps.DirectionsRenderer();
            var mapOptions = //this sets the maps options
            {
                zoom: 14,
                center: myPosition,
                mapTypeControl: true,
                navigationControlOptions:
                    {
                        style: google.maps.NavigationControlStyle.SMALL
                    },
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            map = new google.maps.Map(document.getElementById('map'), mapOptions);
            directionsDisplay.setMap(map);
            directionsDisplay.setPanel(document.getElementById('directionsPanel'));
            var request = {
                origin: myPosition,
                destination: "1 Health Care Cres, Manitouwadge, ON P0T 2C0",
                travelMode: google.maps.DirectionsTravelMode.DRIVING
            };
            directionsService.route(request, function (response, status) {
                if (status == google.maps.DirectionsStatus.OK) {
                    directionsDisplay.setDirections(response);
                }
            });
            
        });
       
    } // end of first attempt

    //start of second attempt

    //end of second attempt


    // basic working attempt to render marker at hospital coordinates.
    //        var hospital = {
    //            lat: 49.1277,
    //            lng: -85.8247
    //        };
    //        var map = new google.maps.Map(document.getElementById('map'), {

    //            zoom: 14,
    //            center: hospital
    //        });

    //        var marker = new google.maps.Marker({
    //            position: hospital,
    //            map: map
    //        }); //end of basic working attempt to render marker at hospital coordinates.
    //}
}//end of initialize map

