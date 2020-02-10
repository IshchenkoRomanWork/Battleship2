//const contextGameId = React.createContext(/*stub*/razorGameId);
//const contextPlayerId = React.createContext(/*stub*/razorPlayerId);
//const contextIsGameCreated = React.createContext(/*stub*/razorGameIsCreated);
razorGameId = $('#gameId').attr('data-marker');
razorPlayerId = $('#playerId').attr('data-marker');
razorGameIsCreated = $('#isCreated').attr('data-marker');
//console.log(razorGameId);
//console.log(razorPlayerId);
//console.log(razorGameIsCreated);
window.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/gamehub")
    .build();

window.hubConnection.start().then(() => {
    if (/*stub*/razorGameIsCreated == 'True') {
        window.hubConnection.invoke('GameCreated', /*stub*/razorPlayerId);
    }
    else {
        window.hubConnection.invoke('PlayerConnected', /*stub*/razorPlayerId, /*stub*/razorGameId);
    }

    $(window).on("unload", function () {
        window.hubConnection.invoke("PlayerLeft");
    })
});

class Game extends React.Component {
    constructor() {
        console.log('game ctor called')
        super();
        window.hubConnection.on("GameStart", () => {
            alert("Game has started");
            this.setState({ showsetup: false })
        });
        window.hubConnection.on("OpponentConnected", (opponentsName) => {
            alert("Your opponent is connected")
            this.setState({ opponentsName: opponentsName })
        });
        window.hubConnection.on("SetId", (gameId) => {
            this.setState({ gameId: gameId })
        });
        window.hubConnection.on("GameEnded", (youarewinner) => {
            if (youarewinner == true) {
                confirm("You've Won!");
            }
            else {
                confirm("You've Lost");
            }
            window.location.href = '/Game/Win'
        });
        window.hubConnection.on("OpponentLeft", () =>  {
            alert("Your opponent left")
            this.setState({ opponentsName: "" })
        });
        window.hubConnection.on("AddShip", (jsonshipsection, length) => {
            var newUnaddedShips = [...this.state.unaddedShips]
            newUnaddedShips[length - 1][1]--;
            this.setState({ unaddedShips: newUnaddedShips })
        });
        this.state =
        {
            gameId: razorGameId,
            playerId: razorPlayerId,
            isGameCreated: razorGameIsCreated,
            unaddedShips: [[1, 4], [2, 3], [3, 2], [4, 1]],
            opponentsName: "",
            showsetup: true,
        }
        console.log('game ctor finished calling')
    }
    render() {
        var setup = this.state.showsetup ? <SetUp unaddedShipList={this.state.unaddedShips} getUnaddedShips={() => { return this.state.unaddedShips }} /> : "";
        return (
            <>
                <h3>Your opponent's Name: {this.state.opponentsName}</h3>
                <h3>Your game's Id: {this.state.gameId}</h3>
                <Board getUnaddedShips={() => { return this.state.unaddedShips }} />
                {setup}
            </>
        )
    }
}

class Board extends React.Component {
    render() {
        return (
            <table>
                <tbody>
                    <tr>
                        <td>
                            <YourMap getUnaddedShips={this.props.getUnaddedShips} />
                        </td>
                        <td>
                            <OpponentsMap />
                        </td>
                        <td>
                            <GameActionList />
                        </td>
                    </tr>
                </tbody>
            </table>
        )
    }
}
class OpponentsMap extends React.Component {
    constructor() {
        super()
        window.hubConnection.on("GameStart", () => {
            console.log("Shooting Locked option start changing in game start, current state: " + this.state.shootingLocked)
            this.setState({ shootingLocked: false })
            console.log("Shooting Locked option changed in game start, current state: " + this.state.shootingLocked)
        });
        window.hubConnection.on("YourTurn", () => {
            alert("Your Turn!")
            console.log("Active player state start changing in YourTurn, current state: " + this.state.youAreActivePlayer)
            this.setState({ youAreActivePlayer: true })
            console.log("Active player state changed in YourTurn, current state: " + this.state.youAreActivePlayer)
        });
        window.hubConnection.on("YourShootResult", (shootedJsonData, shotdata) => {
            var shootedData = JSON.parse(JSON.stringify(eval("(" + shootedJsonData + ")")));
            var shootedCells = [...this.state.opponentCells]
            shootedData.forEach(shootedCoord => {
                shootedCells[shootedCoord.Item1.CoordY - 1][shootedCoord.Item1.CoordX - 1].damaged = true;
                shootedCells[shootedCoord.Item1.CoordY - 1][shootedCoord.Item1.CoordX - 1].hidden = false;
                if (shootedCoord.Item2 == true) {
                    shootedCells[shootedCoord.Item1.CoordY - 1][shootedCoord.Item1.CoordX - 1].ship = true;
                }
            })
            this.setState({ opponentCells: shootedCells })
        });
        var opponentCells = new Array(10).fill(new Array(10).fill({}));
        opponentCells = opponentCells.map((row) => row.map(() => { return { damaged: false, ship: false, hidden: true } }))
        this.state = {
            shootingLocked: true,
            youAreActivePlayer: false,
            opponentCells: opponentCells,
        }
        this.shotCoords = this.shotCoords.bind(this);
    }

    shotCoords(coordX, coordY) {
        console.log("On Shot data: shootingLocked  " + this.state.shootingLocked.toString() + "active player" + this.state.youAreActivePlayer.toString())
        if (!this.state.shootingLocked && this.state.youAreActivePlayer) {
            window.hubConnection.invoke("ShotAt", [coordX, coordY]);
        }
    }
    render() {
        return (
            <>
                <h3>Your Opponent's Map</h3>
                <ShipMap cells={this.state.opponentCells} owner="opponentCells" cellOnClickHandler={this.shotCoords} />
            </>
        )
    }

}
class YourMap extends React.Component {
    constructor() {
        super()
        window.hubConnection.on("YourFieldShooted", (shootedJsonData, shotdata) => {
            var shootedData = JSON.parse(JSON.stringify(eval("(" + shootedJsonData + ")")));
            var shootedCells = [...this.state.playerCells]
            shootedData.forEach(shootedCoord => {
                shootedCells[shootedCoord.Item1.CoordY - 1][shootedCoord.Item1.CoordX - 1].damaged = true;
            })
            this.setState({ playerCells: shootedCells })
        });
        window.hubConnection.on("AddShip", (jsonshipsection, length) => {
            var shipToAddCoordData = JSON.parse(JSON.stringify(eval("(" + jsonshipsection + ")")));
            var addedCells = [...this.state.playerCells]
            shipToAddCoordData.forEach(addedCoord => {
                addedCells[addedCoord.CoordY - 1][addedCoord.CoordX - 1].ship = true;
            })
            this.setState({ playerCells: addedCells })
        });
        var playercells = new Array(10).fill(new Array(10).fill({}));
        playercells = playercells.map((row) => row.map(() => { return { damaged: false, ship: false, hidden: false } }))
        this.state = {
            shipAdditionLocked: false,
            shipsHead: null,
            playerCells: playercells,
            fastShipsNotClicked: true,
        }
        this.addShip = this.addShip.bind(this);
    }

    addShip(coordX, coordY) {
        if (this.state.shipAdditionLocked == false) {
            var changedCells = [...this.state.playerCells]
            var cell = changedCells[coordY - 1][coordX - 1];
            if (this.state.shipshead == null && cell.ship == false) {
                cell.ship = true;
                this.setState({
                    shipshead:
                    {
                        coordX: coordX,
                        coordY: coordY,
                        cell: cell
                    }
                })
            }
            else {
                var headX = this.state.shipshead.coordX;
                var headY = this.state.shipshead.coordY;
                var length;
                if ((((length = Math.abs(headX - coordX)) < 4 && headY == coordY) || ((length = Math.abs(headY - coordY)) < 4 && headX == coordX)) &&
                    (cell.ship == false || (headX == coordX && headY == coordY))) {
                    var shipavailable = new Boolean(false);
                    length++;
                    var availships = this.props.getUnaddedShips();
                    shipavailable = availships.some((group) => { return group[0] == length && group[1] > 0 })
                    if (shipavailable == true) {
                        window.hubConnection.invoke("AddShip", [headX, headY, coordX, coordY, length]);
                    }
                    else {
                        alert("Sorry, no ships remained of that type");
                    }
                }
                else {
                    alert("You can't create ship here")
                }
                changedCells[headY - 1][headX - 1].ship = false;
                this.setState({
                    shipshead: null
                });
            }
            this.setState({
                playerCells: changedCells,
            });
        }
    }

    fastShips() {
        window.hubConnection.invoke("AddShip", [1, 10, 1, 7, 4]);
        window.hubConnection.invoke("AddShip", [3, 10, 3, 8, 3]);
        window.hubConnection.invoke("AddShip", [5, 10, 5, 9, 2]);
        window.hubConnection.invoke("AddShip", [7, 10, 7, 10, 1]);
        window.hubConnection.invoke("AddShip", [9, 10, 9, 10, 1]);
        window.hubConnection.invoke("AddShip", [7, 8, 7, 8, 1]);
        window.hubConnection.invoke("AddShip", [9, 8, 9, 8, 1]);
        window.hubConnection.invoke("AddShip", [7, 6, 9, 6, 3]);
        window.hubConnection.invoke("AddShip", [7, 4, 7, 3, 2]);
        window.hubConnection.invoke("AddShip", [9, 4, 9, 3, 2]);
    }

    render() {
        return (
            <>
                <h3>Your Map</h3>
                <ShipMap cells={this.state.playerCells} cellOnClickHandler={this.addShip} owner="Player" />
                <button type="button" className="btn btn-primary btn-lg" onClick={this.fastShips}>Fast Ships</button>
            </>
        )
    }
}
class ShipMap extends React.Component {
    render() {
        var rowlist = []
        for (let y = 10; y > 0; y--) {
            rowlist.push(this.renderRow(y));
        }
        return (<table>
            <tbody>
                {rowlist}
            </tbody>
        </table>)
    }
    renderRow(y) {
        var cellList = [];

        for (let x = 1; x < 11; x++) { 
            cellList.push(<td key={x.toString() + y.toString()}>
                <MapCell cellbag={this.props.cells[y - 1][x - 1]} keybag={{ coordX: x, coordY: y }} onClick={this.props.cellOnClickHandler} />
            </td>)
        }
        return (
            <tr key={y}>
                {cellList
                }
            </tr>
        )
    }
}
class MapCell extends React.Component {
    constructor() {
        super()
        this.state = {
            hover: false
        }
    }
    render() {
        let cellclass = this.props.cellbag.ship ? "ship" : (this.props.cellbag.hidden ? "hiddencell" : "noship");
        let damaged = this.props.cellbag.damaged ? <div class="hittedfield"></div> : "";
        let hover = this.state.hover ? "shipover" : ""
        return (

            <div className={"cell" + " " + cellclass + " " + hover}
                key={this.props.keybag.coordX.toString() + this.props.keybag.coordY.toString()}
                onClick={() => this.props.onClick(this.props.keybag.coordX, this.props.keybag.coordY)}
                onMouseEnter={() => {
                    this.setState({ hover: true })
                }}
                onMouseLeave={() => {
                    this.setState({ hover: false })
                }}
            >
                {damaged}
            </div>
        )
    }
}

class GameActionList extends React.Component {
    constructor() {
        window.hubConnection.on("YourFieldShooted", (shootedJsonData, shotdata) => {
            console.log("GameActionListFieldShootedCalled shotData: " + shotdata);
            var newMessages = [...this.state.messages]
            newMessages.push(shotdata);
            console.log("This is newMessages")
            console.log(newMessages)
            this.setState({ messages: newMessages });
            console.log("This is this.state.messages after setState called")
            console.log(this.state.messages)
        });
        window.hubConnection.on("YourShootResult", (shootedJsonData, shotdata) => {
            console.log("GameActionListYourShootResultCalled shotData: " + shotdata)
            var newMessages = [...this.state.messages]
            newMessages.push(shotdata);
            console.log("This is newMessages")
            console.log(newMessages)
            this.setState({ messages: newMessages });
            console.log("This is this.state.messages after setState called")
            console.log(this.state.messages)
        });
        super()
        this.state =
        {
            messages: []
        }
    }
    render() {
        var gameActionsListItems = this.state.messages.map((actionString) => <li className="list-group-item">{actionString}</li>);
        return (
            <ul className="list-group" id="shotlist">
                <li className="list-group-item list-group-item-primary">Game actions are displayed here</li>
                {gameActionsListItems}
            </ul>
        )
    }
}

class SetUp extends React.Component {
    constructor() {
        super()
        this.readyButtonClickHandler = this.readyButtonClickHandler.bind(this);
    }
    render() {
        return (
            <>
                <h3>Your Unadded Ships</h3>
                <UnaddedShipList unaddedShipList={this.props.unaddedShipList} />
                <button type="button" className="btn btn-primary btn-lg" onClick={this.readyButtonClickHandler} >Ready</button>
            </>
        )
    }
    readyButtonClickHandler() {
        var noshipsremained = new Boolean(true);
        var availships = this.props.getUnaddedShips();
        noshipsremained = availships.every((group) => group[1] <= 0)
        if (noshipsremained == true) {
            window.hubConnection.invoke("ready");
        }
        else {
            alert("You still have unplaced ships!")
        }
    }
}
class UnaddedShipList extends React.Component {
    render() {
        var unaddedShipList = this.props.unaddedShipList.map((item) => {
            var shipOfCurrentTypeList = [];
            for (let i = 0; i < item[1]; i++) {
                shipOfCurrentTypeList.push(<UnaddedShip key={"unadded" + item[0].toString() + i.toString()} decks={item[0]} />)
            }
            return shipOfCurrentTypeList;
        })
        return (
            <table>
                <tbody>
                    <tr>{unaddedShipList}</tr>
                </tbody>
            </table >
        )
    }
}
class UnaddedShip extends React.Component {
    renderShipDecks() {
        var decks = []
        for (let i = 0; i < this.props.decks; i++) {
            let cellbag = { ship: true, damaged: false, hidden: false }
            decks.push(<MapCell cellbag={cellbag} keybag={{ coordX: 1, coordY: i }} key={"unaddedCell"+ "1" + i.toString()} onClick={() => { }} />)
        }
        return decks;
    }
    render() {
        return (
            <td className="shipexamplecell" >
                <div className="shipexample">
                    {this.renderShipDecks()}
                </div>
            </td >
        )
    }
}



ReactDOM.render(<Game />, document.getElementById('content'));