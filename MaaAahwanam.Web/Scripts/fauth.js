﻿window.fbAsyncInit = function () {
    // FB JavaScript SDK configuration and setup
    FB.init({
        appId: '152565978688349', //'152565978688349',// FB App ID
        cookie: true,  // enable cookies to allow the server to access the session
        xfbml: true,  // parse social plugins on this page
        version: 'v2.8' // use graph api version 2.8
    });

    // Check whether the user already logged in
    //FB.getLoginStatus(function (response) {
    //    if (response.status === 'connected') {
    //        //display user data
    //        getFbUserData();
    //                  }
    //});
};

// Load the JavaScript SDK asynchronously
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

// Facebook login with JavaScript SDK
function fbLogin() {
    FB.login(function (response) {

        if (response.authResponse) {
            // Get and display the user profile data
            getFbUserData();

        } else {
            document.getElementById('status').innerHTML = 'User cancelled login or did not fully authorize.';
        }
    }, { scope: 'email' });
}

// Fetch the user profile data from facebook
function getFbUserData() {
    //FB.api('/me?fields=id,name,gender,email,birthday,first_name,currency,last_name,locale,timezone,verified,picture,age_range', function (me) {
    // FB.api('/me', { locale: 'en_US', fields: 'id,first_name,last_name,email,link,gender,locale,picturename,birthday,currency,timezone,verified,age_range' },
    FB.api('/me', { locale: 'en_US', fields: 'id,first_name,last_name,email,link,gender,locale,picture,timezone,verified,age_range,name' },
         function (response) {
             var url1 = document.referrer;
             var data = {
                 Name: response.name,
                 ID: response.id,
                 gender: response.gender,
                 birthday: response.birthday,
                 email: response.email,
                 firstname: response.first_name,
                 currency: response.currency,
                 lastname: response.last_name,
                 location: response.locale,
                 timezone: response.timezone,
                 verified: response.verified,
                 picture: response.picture,
                 agerange: response.age_range
             }
             $.ajax({
                 url: '/NUserRegistration/facebookLogin/',
                 type: 'POST',
                 data: data,
                 success: function () {
                     if (data == 'failed') {
                         alert('This email is registared as Vendor please login with Your Credentials')
                     }
                     else {
                         var url1 = document.referrer;
                         window.location.href = url1;// "/NHomePage/Index/";
                     }
                 },
             });
             // Save user data
             //  saveUserData(response);
             // });
         }
    )
}

// Save user data to the database
/*function saveUserData(userData){
    $.post('userData.php', {oauth_provider:'facebook',userData: JSON.stringify(userData)}, function(data){ return true; });
}*/

// Logout from facebook
function fbLogout() {
    FB.logout();
    // location.reload();
    //   FB.logout( function () {
    //document.getElementById('fbLink').setAttribute("onclick", "fbLogin()");
    //document.getElementById('fbLink').innerHTML = 'login';
    //document.getElementById('userData').innerHTML = '';
    //document.getElementById('status').innerHTML = 'You have successfully logout from Facebook.';
    //});
}