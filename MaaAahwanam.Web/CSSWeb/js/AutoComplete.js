$(document).ready(function () {
    var LocationList = [];
    var EventsList = [];
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: window.location.href.split("/")[0] + '//' + window.location.href.split("/")[2] + '/Index/AutoCompleteCountry',
        data: {},
        dataType: "json",
        success: function (result) {
            LocationList = $.map(result.Listoflocations, function (item) { return item });
            EventsList = $.map(result.ListofEvents, function (item) { return item });
            for (var i = 0; i < EventsList.length; i++) {
                $("#e1").append($("<option></option>").val(EventsList[i]).html(EventsList[i]))
                $("#e2").append($("<option></option>").val(EventsList[i]).html(EventsList[i]))
            }
        },
        error: function (result) {
            alert("Error");
        }
    });
    $("#txt_Location_Top").autocomplete({
        delay: 0,
        source: function (request, response) {
            var results = $.ui.autocomplete.filter(LocationList, request.term);
            if (!results.length) {
                response([{ label: 'No results found...', val: -1 }]);
            } else {
                response(results);
            }
        },
        select: function (e, u) {
            if (u.item.val == -1) {
                return false;
            }
        }
    });
    $("#txt_Location_Bottom").autocomplete({
        delay: 0,
        source: function (request, response) {
            var results = $.ui.autocomplete.filter(LocationList, request.term);
            if (!results.length) {
                response([{ label: 'No results found...', val: -1 }]);
            } else {
                response(results);
            }
        },
        select: function (e, u) {
            if (u.item.val == -1) {
                return false;
            }
        }
    });    
});
