
//  Url du Service WEB
var aURL = "http://localhost:50728/Boomcraft_WebService.asmx/";

$(document).ready(function () {
    // Function code here.
    /**************************************** BOUTON NOUVEL ELEMENT ****************************************/
    $('#btn_AppelJavascript').on('click', function () {
        // On force un timeout pour que le focus fonctionne sur IE.
        call_ServiceWeb("sNom", 20);
    });
});

/* **************************************** CALL SERVICE WEB **************************************** */
// Appel du Service WEB.
function call_ServiceWeb(sNom, iAge) {
    var aData = "{sNom:'" + sNom + "',iAge:'" + iAge + "'}";
    $.ajax({
        type: "POST",
        url: aURL + "get_Test",
        data: aData,
        async: false,
        contentType: "application/json; charset=utf-8;",
        dataType: "json",
        // Fonction SUCCESS WEBSERVICE
        success: function (response, type, xhr) {
            window.alert(response.d);
        },
        // Fonction ECHEC WEBSERVICE
        error: function (xhr, ajaxOptions, thrownError) {
            window.alert('error: ' + xhr.statusText);
        }
    })
}
///* **************************************** CALL SERVICE WEB **************************************** */
//// Appel du Service WEB.
//function call_ServiceWeb(sNom, iAge) {
//    $.ajax({
//        type: 'GET',
//        url: aURL + "HelloWorld",
//        dataType: 'json',
//        success: function (response, type, xhr) {
//            window.alert("Success");
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            window.alert("Error");
//        }
//    })
//}