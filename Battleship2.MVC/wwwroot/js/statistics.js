let currentSorting = "desc/date";




function GetData() {
    var jsonArray = {
        "name": document.forms["winnerfilterform"].elements["winnerfilterinput"].value,
        "remainingShips": document.forms["shipsfilterform"].elements["shipsfilterinput"].value,
        "gameDates": document.forms["datefilterform"].elements["datefilterinput"].value,
        "gameTurns": document.forms["turnsfilterform"].elements["turnsfilterinput"].value,
        "sorting": currentSorting
    }
    var json = JSON.stringify(jsonArray);
    $.ajax({
        type: "GET",
        url: "/api/statistics/get",
        dataType: "json",
        data: "data=" + json,
        success : function(data) {
            console.log(data);
            var rows = document.querySelector("tbody");
            $("tbody").empty();
            data.forEach(function (d) {
                console.log(d);
                $("tbody").append(row(d))
            });
        }
    });
}

function row(stItem) {
    var row = "<tr>"
    row = row.concat("<td>")
    row = row.concat(stItem.winnerName);
    row = row.concat("</td>")
    row = row.concat("<td>")
    row = row.concat(stItem.gameTurnNumber);
    row = row.concat("</td>")
    row = row.concat("<td>")
    row = row.concat(stItem.remainingShips);
    row = row.concat("</td>")
    row = row.concat("<td>")
    row = row.concat(stItem.gameDate);
    row = row.concat("</td>")
    row = row.concat("</tr>")
    return row
}

$(document).ready(() => {
    $('#shipsortheader').on("click", function () {
        if (currentSorting == "desc/ship") {
            currentSorting = "asc/ship"
        }
        else {
            currentSorting == "desc/ship"
        }
    });
    $('#datesortheader').on("click", function () {
        if (currentSorting == "desc/date") {
            currentSorting = "asc/date"
        }
        else {
            currentSorting == "desc/date"
        }
    });
    $(function () {
        $('input[name="datefilterinput"]').daterangepicker();
        var today = new Date();
        $('input[name="datefilterinput"]').data('daterangepicker').setStartDate(new Date(today.getFullYear() - 1, today.getMonth()));
        $('input[name="datefilterinput"]').data('daterangepicker').setEndDate(new Date(today.getFullYear() + 1, today.getMonth()));
        GetData();
    });
})