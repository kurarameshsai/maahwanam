﻿var OAUTHURL = 'https://accounts.google.com/o/oauth2/auth?';
var VALIDURL = 'https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=';
var SCOPE = 'https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email';
var CLIENTID = '940368706527-qu2oqpfn1ad533kr1bpclh3qbleau1ug.apps.googleusercontent.com';
var REDIRECT = 'http://www.ahwanam.com/';
var LOGOUT = 'http://www.ahwanam.com/';
var TYPE = 'token';
var _url = OAUTHURL + 'scope=' + SCOPE + '&client_id=' + CLIENTID + '&redirect_uri=' + REDIRECT + '&response_type=' + TYPE;
var acToken;
var tokenType;
var expiresIn;
var user;
var loggedIn = false;

function glogin() {

    var win = window.open(_url, "windowname1", 'width=800, height=600');
    var pollTimer = window.setInterval(function () {
        try {
            console.log(win.document.URL);
            if (win.document.URL.indexOf(REDIRECT) != -1) {
                window.clearInterval(pollTimer);
                var url = win.document.URL;
                acToken = gup(url, 'access_token');
                tokenType = gup(url, 'token_type');
                expiresIn = gup(url, 'expires_in');

                win.close();

                validateToken(acToken);
            }
        }
        catch (e) {

        }
    }, 500);
}

function gup(url, name) {
    namename = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\#&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(url);
    if (results == null)
        return "";
    else
        return results[1];
}

function validateToken(token) {

    getUserInfo();

    $.get(VALIDURL + token, function (data, status) {
       // alert(data.email);

        $.ajax({
            url: '/UserRegistration/GoogleLogin/',
            type: 'POST',
            data: data,
            success: function () {
                if (data == 'failed') {
                    alert('This email is registared as Vendor please login with Your Credentials');
                }
                else {
                    var url1 = document.referrer;
                    window.location.href = url1;// "/NHomePage/Index/";
                }

            }
        });
    });
}

function getUserInfo() {


    $.ajax({

        url: 'https://www.googleapis.com/oauth2/v1/userinfo?access_token=' + acToken,
        data: null,
        success: function (resp) {
            user = resp;

        },
    })



}
