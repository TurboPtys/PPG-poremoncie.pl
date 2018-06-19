function myMap() {
    var myCenter = new google.maps.LatLng(51.126989, 17.028897);
    var mapCanvas = document.getElementById("map");
    var mapOptions = { center: myCenter, zoom: 15 };
    var map = new google.maps.Map(mapCanvas, mapOptions);
    var marker = new google.maps.Marker({ position: myCenter });
    marker.setMap(map);
}




function RegOrgBtnAction(){
    document.getElementById("Nip").hidden = false;
    document.getElementById("RegName").innerHTML = 'Nazwa';
    document.getElementById("inputName").placeholder = 'Nazwa';
    document.getElementById("RegOrgBtn").style.borderBottom = "8px solid  #ffffff";
    document.getElementById("RegCliBtn").style.borderBottom = 'hidden';
}

function RegCliBtnAction() {
    document.getElementById("Nip").hidden = true;
    document.getElementById("RegName").innerHTML = "Imię";
    document.getElementById("inputName").placeholder = "Imię";
    document.getElementById("RegCliBtn").style.borderBottom = "8px solid  #ffffff";
    document.getElementById("RegOrgBtn").style.borderBottom = 'hidden';
}