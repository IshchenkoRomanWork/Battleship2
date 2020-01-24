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
        if (shipAdditionLocked == false)
        {
            if (shipshead == null && $(this).hasClass('ship') == false)
            {
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
                if ((((length = Math.abs(headx - targetx)) < 4 && heady == targety) || ((length = Math.abs(heady - targety)) < 4 && headx == targetx)) &&
                    ($(this).hasClass('ship') == false || (headx == targetx && heady == targety)))
                {
                    var shipavailable = new Boolean(false);
                    length++;
                    availships.forEach(item => {
                        if (item[0] == length && item[1] != 0) {
                            shipavailable = true;
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
$('td[name=opshipcell]').on("click", function () {

    if (!shootingLocked && youareactiveplayer) {
        headx = parseInt($(this).attr('data-x'));
        heady = parseInt($(this).attr('data-y'));
        hubConnection.invoke("ShotAt", [headx, heady, jsPlayerId]);
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
    availships[length - 1][1]--;
    $('td[data-shiptype=' + length.toString() + ']:first').remove();
});
hubConnection.on("GameStart", function () {
    shootingLocked = false;
    alert("Game has started");
    $('#setup').hide();
});
hubConnection.on("ShipsCantTouch", function () {
    alert("Ships can't touch with angles or sides")
});
hubConnection.on("OpponentConnected", function (opponentsname) {
    alert("Your opponent is connected")
    $('#opponentsname').text("Your opponent's Name: " + opponentsname);
});
hubConnection.on("YourFieldShooted", function (shootedJsonData, shotdata) {
    $('#shotlist').append('<li class="list-group-item">' + shotdata + '</li>')
    var shootedData = JSON.parse(JSON.stringify(eval("(" + shootedJsonData + ")")));
    shootedData.forEach(shootedcoord => {
        var headXstring = shootedcoord.Item1.CoordX.toString();
        var headYstring = shootedcoord.Item1.CoordY.toString();
        var cellid = "[data-x=" + headXstring + "][data-y=" + headYstring + "][name=shipcell]";
        $(cellid).append('<div class="hittedfield"></div>')
    })
});
hubConnection.on("YourShootResult", function (shootedJsonData, shotdata) {
    $('#shotlist').append('<li class="list-group-item">' + shotdata + '</li>')
    var shootedData = JSON.parse(JSON.stringify(eval("(" + shootedJsonData + ")")));
    shootedData.forEach(shootedcoord => {
        var x = shootedcoord.Item1.CoordX.toString();
        var y = shootedcoord.Item1.CoordY.toString();
        var cellid = "[data-x=" + x + "][data-y=" + y + "][name=opshipcell]";
        $(cellid).append('<div class="hittedfield"></div>')
        $(cellid).removeClass("hiddencell")
        if (shootedcoord.Item2 == true) {
            $(cellid).addClass("opponentship")
        }
        else {
            $(cellid).addClass("noship")
        }
    })
});
hubConnection.on("YourTurn", function () {
    alert("Your Turn!")
    youareactiveplayer = true;
});
hubConnection.on("SetId", function (data) {
    $('#gamesid').text($('#gamesid').text() + data);
});
hubConnection.on("GameEnded", function (youarewinner) {
    if (youarewinner == true) {
        confirm("You've Won!");
    }
    else {
        confirm("You've Lost");
    } 
    window.location.href = '/Game/Win'
});
hubConnection.on("OpponentLeft", function ()
{
    alert("Your opponent left")
    $('#opponentsname').text("Your opponent's Name");
});

hubConnection.start().then(function () {
    if (jsGameIsCreated == 'True') {
        hubConnection.invoke('GameCreated', jsPlayerId);
    }
        else {
        hubConnection.invoke('PlayerConnected', jsPlayerId, jsGameId);
    }

    $(window).on("unload", function () {
        hubConnection.invoke("PlayerLeft", jsPlayerId);
    })
});

