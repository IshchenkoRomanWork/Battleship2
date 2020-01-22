let currentSorting = "desc/ship";

$("filterbutton").on("click", function () {
    GetData();
});

$("th[name=shipsortheader]").on("click", function () {
    if (currentSorting == "desc/ship") {
        currentSorting = "asc/ship"
    }
    else {
        currentSorting == "desc/ship"
    }
});
$("th[name=datesortheader]").on("click", function () {
    if (currentSorting == "desc/date") {
        currentSorting = "asc/date"
    }
    else {
        currentSorting == "desc/date"
    }
});

function GetData() {
    var jsonArray = [{
        "name": document.forms["winnerfilterform"].elements["winnerfilterinput"].value,
        "remainingShips": document.forms["shipsfilterform"].elements["shipsfilterinput"].value,
        "gameDates": document.forms["datefilterform"].elements["datefilterinput"].value,
        "gameTurns": document.forms["turnsfilterform"].elements["turnsfilterinput"].value,
        "sorting": currentSorting
    }]
    var json = JSON.stringify(jsonArray);
    $.get("/api/statistics/get", json,
        function (data) {
        console.log(data);
        var rows = document.querySelector("tbody");
        $("tbody").empty();
        data.forEach(function (d) {
            console.log(d);
            rows.append(row(d));
        });
    });
}

function row(stItem) {
    var row = "<tr>"
    row.append("<td>")
    row.append.stItem[0];
    row.append("</td>")
    row.append("<td>")
    row.append.stItem[1];
    row.append("</td>")
    row.append("<td>")
    row.append.stItem[2];
    row.append("</td>")
    row.append("<td>")
    row.append.stItem[3];
    row.append("</td>")
    row.append("</tr>")
    return 
}

$(document).ready(() => {
    $(function () {
        $('input[name="datefilterinput"]').daterangepicker();

        GetData();
    });
})