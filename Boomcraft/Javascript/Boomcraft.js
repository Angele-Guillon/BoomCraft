
//  Url du Service WEB - LOCAL
var aURL = "http://localhost:50728/api.asmx/";

//  TOUTES LES URLs DES APIS DOIVENT ETRE PLACEES DANS DES VARIABLES POUR FACILITER LA MAINTENABILITE DU CODE !!!

//  Bases des URLs des différents jeux.
//var aURL_BC = "http://193.190.248.157:8080/api.asmx/";
var aURL_BC = aURL;
var aUrl_FV = "http://artshared.fr/andev1/distribue/api/";
var aUrl_HW = "http://howob.masi-henallux.be/api/";
var aUrl_VC = "https://veggiecrush.masi-henallux.be/rest_server/api/";

var aApiExisting_BC = aURL_BC + "existing";
var aApiExisting_FV = aUrl_FV + "auth/exist/";
var aApiExisting_HW = aUrl_HW + "auth/existing";
var aApiExisting_VC = aUrl_VC + "account/existing";

//  Tableau des noms des APIs de tous les groupes
//var aUrlTab_existing = [aApiExisting_BC, aApiExisting_FV, aApiExisting_HW, aApiExisting_VC];
var aUrlTab_existing = [aApiExisting_FV];

var aApi_Signin_BC = aURL_BC + "signin";
var aApi_Signin_FV = aUrl_FV + "auth/signin";
var aApi_Signin_HW = aUrl_HW + "auth/signin";
var aApi_Signin_VC = aUrl_VC + "account/signin";
//  Tableau des noms des APIs de tous les groupes
var aUrlTab_Signin = [aApi_Signin_BC, aApi_Signin_FV, aApi_Signin_HW, aApi_Signin_VC];

//  Url du Service WEB - SERVEUR - PC-TIM
//  var aURL = "192.168.129.128:8080/api.asmx/";

$(document).ready(function () {
    /**************************************** BOUTON APPEL JAVASCRIPT ****************************************/
    //$('#btn_AppelJavascript').on('click', function () {
    //    call_ServiceWeb("sNom", 20);
    //});
    /**************************************** BOUTON FV DO TRANSACTION ****************************************/
    $('#btn_FV_DoTransaction').on('click', function () {
        FV_doTransaction("joueur1", 100, 100, 100, 100, 1);
    });
    //$('#btn_AppelJavascript').on('click', function () {
    verifierDispoInscription('boomcraft', 'boomcraft@boomcraft.boomcraft');
    //});
    /**************************************** BOUTON ENVOYER DON ****************************************/
});

/* **************************************** VERIFIER DISPO INSCRIPTION **************************************** */
function verifierDispoInscription(sNom, sEmail) {
    //alert('test');
    var sReponse = ["init", "init", "init", "init"];
    //  Paramètres à transmettre aux APIs.
    var aData = "{'username': '" + sNom + "', 'email': '" + sEmail + "'}";
    //var aData = "{\"username\": \"" + sNom + "\", \"email\": \"" + sEmail + "\"}";
    //  Fonctionne pour HoWob.
    //var aData = '{\'username\': \'' + sNom + '\', \'email\': \'' + sEmail + '\'}';
    //  ==>     {\'username\': \'boomcraft\', \'email\': \'boomcraft@boomcraft.boomcraft\'}
    //  Initialisation de booléens à 'false' pour indiquer l'existence d'un nom et d'un email à travers les 4 bases de données des jeux.
    var bExistenceNom = false;
    var bExistenceEmail = false;
    //  Tant que les deux booléens ne sont pas faux, on scrute toutes les bases afin de pouvoir informer l'utilisateur de la disponibilité du username 
    //  même si l'email est déjà utilisé, et réciproquement.
    for (i = 0; i < aUrlTab_existing.length && (bExistenceNom != true && bExistenceEmail != true) ; i++) {
        $.ajax({
            type: "POST",
            url: aUrlTab_existing[i],
            data: aData,
            async: false,
            contentType: "application/json; charset=utf-8;",
            dataType: "json",
            // Fonction SUCCESS WEBSERVICE
            success: function (response, type, xhr) {
                if (response.d) {
                    sReponse[i] = "d ==> " + response.d;
                    alert("d ==> " + response.d);
                }
                else {
                    sReponse[i] = response;
                    alert(response.username.toString() + " " +  response.email.toString());
                }
            },
            // Fonction ECHEC WEBSERVICE
            error: function (xhr, ajaxOptions, thrownError) {
                window.alert('error: ' + xhr.statusText);
                window.alert('error: ' + xhr.username);
                //  Visualisation de l'URL utilisée pour la connexion à l'API.
                //window.alert(aUrlTab_existing[i]);
            }
        })
    }
    //alert(sReponse[0], sReponse[1], sRepons[2], sReponse[3]);
    //alert(sReponse[0], sReponse[1]);
}
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
