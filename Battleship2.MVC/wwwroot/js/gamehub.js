let shipshead = null;
let shipAdditionLocked = new Boolean(false);
let shootingLocked = new Boolean(true);
let availships = [[1, 4], [2, 3], [3, 2], [4, 1]];
let youareactiveplayer = new Boolean(false);

const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/gamehub")
    .build();
$("td[name=shipcell]").hover(
    function () {
        $(this).attr("data-removed-class", $(this).attr('class'));
        var classList = $(this).attr('class').split(/\s+/);
        $.each(classList, function (index, item) {
            $(this).removeClass(item);
        });
        $(this).addClass('shipover')
    },
    function () {
        $(this).removeClass('shipover')
        var classList = $(this).attr('data-removed-class').split(/\s+/);
        $.each(classList, function (index, item) {
            $(this).addClass(item);
        });
        $(this).attr("data-removed-class", "");
    }
);
$("td[name=shipcell]").on("click",
    function () {
        if (shipAdditionLocked == false) {
            if (shipshead == null) {
                shipshead = $(this);
                $(this).removeClass('noship');
                $(this).addClass('ship');
            }
            else {
                var headx, heady, targetx, targety;
                headx = parseInt(shipshead.attr('data-x'));
                heady = parseInt(shipshead.attr('data-y'));
                targetx = parseInt($(this).attr('data-x'));
                targety = parseInt($(this).attr('data-y'));
                var length;
                if (((length = Math.abs(headx - targetx)) < 4 && heady == targety) || ((length = Math.abs(heady - targety)) < 4 && headx == targetx)) {
                    var shipavailable = new Boolean(false);
                    length++;
                    availships.forEach(item => {
                        if (item[0] == length && item[1] != 0) {
                            shipavailable = true;
                            item[1]--;
                        }
                    });
                    if (shipavailable == true) {
                        hubConnection.invoke("AddShip", [headx, heady, targetx, targety, length, jsPlayerId]);
                    }
                    else {
                        alert("Sorry, no ships remained of that type");
                    }
                    shipshead.removeClass('ship');
                    shipshead.addClass('noship');
                    shipshead = null;
                }
                else {
                    alert("You can't create ship here")
                    shipshead.removeClass('ship');
                    shipshead.addClass('noship');
                    shipshead = null;
                }
            }
        }
    }
);
$('#readybutton').on("click", function () {
    var noshipsremained = new Boolean(true);
    availships.forEach(availshipsoftype => {
        if (availshipsoftype[1] != 0) {
            noshipsremained = true;
        }
    });
    if (noshipsremained == true) {
        hubConnection.invoke("Ready", jsPlayerId);
    }
    else {
        alert("You still have unplaced ships!")
    }
})
$('#td[name=opshipcell]').on("click", function () {
    if (!shootingLocked && youareactiveplayer) {
        headx = parseInt(shipshead.attr('data-x'));
        heady = parseInt(shipshead.attr('data-y'));
        youareactiveplayer = false;
        hubConnection.invoke("ShotAt", [headX, headY, jsPlayerId]);
    }
})
hubConnection.on("AddShip", function (jsonshipsection, length) {
    var shipToAddCoordData = JSON.parse(JSON.stringify(eval("(" + jsonshipsection + ")")));
    shipToAddCoordData.forEach(element => {
        var x = element.CoordX.toString();
        var y = element.CoordY.toString();
        var cellid = "[data-x=" + x + "][data-y=" + y + "]";
        $(cellid).removeClass('noship')
        $(cellid).addClass('ship')
    })
    $('td[data-shiptype=' + length.toString() + ']:first').remove();
});
hubConnection.on("GameStart", function () {
    shootingLocked = false;
    $('#availableships').hide();
});
hubConnection.on("ShipsCantTouch", function () {
    alert("Ships can't touch with angles or sides")
});
hubConnection.on("OpponentConnected", function (opponentsname) {
    alert("Your opponent is connected")
    $('#opponentsname').text("Your opponent's Name: " + opponentsname);
});
hubConnection.on("YourFieldShooted", function (shootedJsonData, shotdata) {
    $('shotlist').append('<li class="list-group-item list-group-item-primary"' + shotdata + '</li>')
    var shootedData = JSON.parse(JSON.stringify(eval("(" + shootedJsonData + ")")));
    shootedData.forEach(shootedcoord => {
        var headXstring = shootedcoord.Item1.CoordX.toString();
        var headYstring = shootedcoord.Item1.CoordY.toString();
        var cellid = "[data-x=" + headXstring + "][data-y=" + headYstring + "][name=shipcell]";
        $(cellid).append('<div class="hittedfield"></div>')
    })
});
hubConnection.on("YourShootResult", function (shootedJsonData, shotdata) {
    $('shotlist').append('<li class="list-group-item list-group-item-primary"' + shotdata + '</li>')
    var shootedData = JSON.parse(JSON.stringify(eval("(" + shootedJsonData + ")")));
    shootedData.forEach(shootedcoord => {
        var x = shootedcoord.Item1.CoordX.toString();
        var y = shootedcoord.Item1.CoordY.toString();
        var cellid = "[data-x=" + x + "][data-y=" + y + "][name=opshipcell]";
        $(cellid).append('<div class="hittedfield"></div>')
        $(cellid).removeClass("hiddencell")
        if (shootedcoord.Item2 = true) {
            $(cellid).addClass("opponentship")
        }
        else {
            $(cellid).addClass("noship")
        }
    })
});
hubConnection.on("YourTurn", function () {
    youareactiveplayer = true;
});
hubConnection.on("SetId", function (data) {
    $('#gamesid').val(data);
});
hubConnection.on("GameEnded", function (youarewinner) {
    if (youarewinner == true) {
        confirm("Youve Won!");
    }
    else {
        confirm("Youve Lost");
    }
    $.get("Game/Win");
});
/*async () => { await */
hubConnection.start().then(function ()
    {
    if (new Boolean(jsGameIsCreated) == false) {
        hubConnection.invoke('PlayerConnected', jsPlayerId, jsGameId);
    }
        else {
        hubConnection.invoke('GameCreated', jsPlayerId);
    }
}).catch(function (err) {
    return console.error(err.toString());
});
/* }*/