
//  Url du Service WEB - LOCAL
var aURL = "http://localhost:50728/Boomcraft_WebService.asmx/";

var aUrl_FV = "http://artshared.fr/andev1/distribue/android/";

//  Url du Service WEB - SERVEUR - PC-TIM
//  var aURL = "192.168.129.128:8080/Boomcraft_WebService.asmx/";

$(document).ready(function () {
    /**************************************** BOUTON APPEL JAVASCRIPT ****************************************/
    //$('#btn_AppelJavascript').on('click', function () {
    //    call_ServiceWeb("sNom", 20);
    //});
    /**************************************** BOUTON FV DO TRANSACTION ****************************************/
    $('#btn_FV_DoTransaction').on('click', function () {
        FV_doTransaction("joueur1", 100, 100, 100, 100, 1);
    });
    /**************************************** BOUTON ENVOYER DON ****************************************/
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
/* **************************************** FV DO TRANSACTION **************************************** */
//    Récupération des ressources et transfert de FarmVillage vers Boomcraft.
//    Disponible uniquement avec le FarmVillage du joueur actif (UUID).
function FV_doTransaction(sUUID, iWood, iFood, iGold, iRock, bCentralise) {
    var aData = "{UUID:'" + sUUID + "',wood:'" + iWood + "',food:'" + iFood + "',gold:'" + iGold + "',rock:'" + iRock + "',centralise:'" + bCentralise + "'}";
    $.ajax({
        type: "POST",
        url: aUrl_FV + "do_transaction",
        data: aData,
        async: false,
        contentType: "application/json; charset=utf-8;",
        dataType: "json",
        // Fonction SUCCESS WEBSERVICE
        success: function (response, type, xhr) {
            var sTest = "{ code: 0, 'msg_code': 'OK. La transaction a été effectuée.'}";
            //window.alert(response.code);
            //window.alert(response.msg_code);
            //window.alert(response[0][0]['code']);
            //window.alert(response[0][0]['msg_code']);
            window.alert(sTest.code);
            window.alert(sTest.msg_code);
            window.alert(sTest[0][0]['code']);
            window.alert(sTest[0][0]['msg_code']);
        },
        // Fonction ECHEC WEBSERVICE
        error: function (xhr, ajaxOptions, thrownError) {
            window.alert('error: ' + xhr.statusText);
        }
    })
}
///* ****************************************  **************************************** */