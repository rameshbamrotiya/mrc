var apiTocken = "8fSZs_bEtGlt1xvYB64UPBSQUB4JYQlBGAWNUmKNi_mrDMxEfUUC1jeP4kDMsIJeHLA";
var email = "hardikmistryhhh@gmail.com";

function GetCountry(eCountry,ehfCountry,vCountry)
{
    var strDropDown = "";
    var settings = {
        "url": "https://www.universal-tutorial.com/api/getaccesstoken",
        "method": "GET",
        "timeout": 0,
        "headers": {
            "api-token": apiTocken,
            "Accept": "application/json",
            "user-email": email
        },
    };

    $.ajax(settings).done(function (response) {
        var token = response.auth_token;

        var settingsCountry = {
            "url": "https://www.universal-tutorial.com/api/countries/",
            "method": "GET",
            "timeout": 0,
            "headers": {
                "Accept": "application/json",
                "Authorization": "Bearer "+token
            },
        };
        $.ajax(settingsCountry).done(function (responseCountry) {
            strDropDown+="<option >Select Country</option>";
            for (var i = 0; i < responseCountry.length; i++) {
                strDropDown += "<option value='" + responseCountry[i].country_name + "'>" + responseCountry[i].country_name + "</option>";
            }

            document.getElementById(eCountry).innerHTML = strDropDown;
            if (!(vCountry === undefined || vCountry === null)) {
                document.getElementById(eCountry).value = vCountry;
                document.getElementById(ehfCountry).value = vCountry;
            }
        });
    });
}

function GetState(eState, ehfState, country, vState) {
    var strDropDown = "";
    var settings = {
        "url": "https://www.universal-tutorial.com/api/getaccesstoken",
        "method": "GET",
        "timeout": 0,
        "headers": {
            "api-token": apiTocken,
            "Accept": "application/json",
            "user-email": email
        },
    };

    $.ajax(settings).done(function (response) {
        var token = response.auth_token;

        var settingsCountry = {
            "url": "https://www.universal-tutorial.com/api/states/" + country,
            "method": "GET",
            "timeout": 0,
            "headers": {
                "Accept": "application/json",
                "Authorization": "Bearer " + token
            },
        };
        $.ajax(settingsCountry).done(function (responseCountry) {
            strDropDown += "<option >Select State</option>";
            for (var i = 0; i < responseCountry.length; i++) {
                strDropDown += "<option value='" + responseCountry[i].state_name + "'>" + responseCountry[i].state_name + "</option>";
            }
            document.getElementById(eState).innerHTML = strDropDown;         
            if (!(vState === undefined || vState === null)) {
                document.getElementById(eState).value = vState;
                document.getElementById(ehfState).value = vState;
            }
        });
    });
}

function GetCities(eCities,ehfCities, State, vCities) {
    var strDropDown = "";
    var settings = {
        "url": "https://www.universal-tutorial.com/api/getaccesstoken",
        "method": "GET",
        "timeout": 0,
        "headers": {
            "api-token": apiTocken,
            "Accept": "application/json",
            "user-email": email
        },
    };

    $.ajax(settings).done(function (response) {
        var token = response.auth_token;

        var settingsCountry = {
            "url": "https://www.universal-tutorial.com/api/cities/" + State,
            "method": "GET",
            "timeout": 0,
            "headers": {
                "Accept": "application/json",
                "Authorization": "Bearer " + token
            },
        };
        $.ajax(settingsCountry).done(function (responseCountry) {
            strDropDown += "<option >Select City</option>";
            for (var i = 0; i < responseCountry.length; i++) {
                strDropDown += "<option value='" + responseCountry[i].city_name + "'>" + responseCountry[i].city_name + "</option>";
            }
            document.getElementById(eCities).innerHTML = strDropDown;
            if (!(vCities === undefined || vCities === null)) {
                document.getElementById(eCities).value = vCities;
                document.getElementById(ehfCities).value = vCities;
            }
        });
    });
}