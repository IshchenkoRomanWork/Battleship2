class StatisticsBoard extends React.Component {
    constructor() {
        super()
        this.winnerName = $('#playerName').attr('data-marker');
        this.gameTurnNumber = 0;
        this.remainingWinnerShips = 0;
        let today = new Date();
        this.startDate = new Date(today.getFullYear() - 1, today.getMonth())
        this.endDate = new Date(today.getFullYear() + 1, today.getMonth())
        this.currentSorting = "desc/date";
        this.state =
        {
            rows: []
        }
        this.binds()
        this.getData()
    }
    binds() {
        this.setWinnerName = this.setWinnerName.bind(this);
        this.setGameTurnNumber = this.setGameTurnNumber.bind(this);
        this.setRemainingWinnerShips = this.setRemainingWinnerShips.bind(this);
        this.setDates = this.setDates.bind(this);
        this.setSorting = this.setSorting.bind(this);
        this.getData = this.getData.bind(this);
        this.renderRows = this.renderRows.bind(this);
    }
    async getData() {
        var jsonArray = {
            "name": this.winnerName,
            "remainingShips": this.remainingWinnerShips,
            "gameDates": this.startDate + " - " + this.endDate,
            "gameTurns": this.gameTurnNumber,
            "sorting": this.currentSorting
        }
        var json = JSON.stringify(jsonArray);
        var result = await $.ajax({
            type: "GET",
            url: "/api/statistics/get",
            dataType: "json",
            data: "data=" + json,
            success: (data) => {
                console.log(data);
                this.setState({ rows: data });
            },
            error: (data) => {
                console.log(data);
            }
        });
    }
    renderRows() {
        return this.state.rows.map((row) => {
            return (<Row winnerName={row.winnerName} gameTurnNumber={row.gameTurnNumber} remainingShips={row.remainingShips} gameDate={row.gameDate} />)
        })
    }
    setWinnerName(winnerName) {
        this.winnerName = winnerName;
    }
    setGameTurnNumber(gameTurnNumber) {
        this.gameTurnNumber = gameTurnNumber;
    }
    setRemainingWinnerShips(remainingWinnerShips) {
        this.remainingWinnerShips = remainingWinnerShips;
    }
    setDates(gameDatesString) {
        gameDates = gameDatesString.split(' - ');
        this.startDate = gameDates[0];
        this.endDate = gameDates[1];
    }
    setSorting(type) {
        if (type == "shipSorting") {
            if (this.currentSorting == "desc/ship") {
                this.currentSorting = "asc/ship"
            }
            else {
                this.currentSorting == "desc/ship"
            }
        }
        else if (type = "dateSorting") {
            if (this.currentSorting == "desc/date") {
                this.currentSorting = "asc/date"
            }
            else {
                this.currentSorting == "desc/date"
            }
        }
    }
    render() {
        return (
            <>
                <h1>Statistics</h1>
                <input type="button" value="Filter" onClick={this.getData} />

                <table className="table">

                    <thead>
                        <tr>
                            <WinnerHeader winnerName={this.winnerName} onChange={this.setWinnerName} />
                            <GameTurnHeader gameTurnNumber={this.gameTurnNumber} onChange={this.setGameTurnNumber} />
                            <ShipHeader shipNumber={this.remainingWinnerShips} onChange={this.setRemainingWinnerShips} setSorting={this.setSorting} />
                            <DateHeader startDate={this.startDate} endDate={this.endDate} onChange={this.setDates} setSorting={this.setSorting} />
                        </tr>
                    </thead>
                    <tbody>
                        {this.renderRows()}
                    </tbody>

                </table>

            </>
        )
    }
}

function Row(props) {
    return (
        <tr>
            <td>{props.winnerName}</td>
            <td>{props.gameTurnNumber}</td>
            <td>{props.remainingShips}</td>
            <td>{props.gameDate}</td>
        </tr>
    )
}

class WinnerHeader extends React.Component {
    constructor(props) {
        super(props)
        this.state =
        {
            currentValue: this.props.winnerName,
        }
        this.changeHandler = this.changeHandler.bind(this)
    }

    changeHandler(event) {
        var value = event.target.value
        this.props.onChange(value)
        this.setState({ currentValue: value })
        event.preventDefault();
    }

    render() {
        return (
            <th>
                <label>Winner Name</label>
                <input type="text" value={this.state.currentValue} onChange={() => { this.changeHandler(event) }} />
            </th>
        )
    }
}
class GameTurnHeader extends React.Component {
    constructor(props) {
        super(props)
        this.state =
        {
            currentValue: this.props.gameTurnNumber,
        }
        this.changeHandler = this.changeHandler.bind(this)
    }

    changeHandler(event) {
        var value = event.target.value
        this.props.onChange(value)
        this.setState({ currentValue: value })
        event.preventDefault();
    }

    render() {
        return (
            <th>
                <label>Game Turn Number</label>
                <input type="text" value={this.state.currentValue} onChange={() => { this.changeHandler(event) }} />
            </th>
        )
    }
}
class ShipHeader extends React.Component {
    constructor(props) {
        super(props)
        this.state =
        {
            currentValue: this.props.shipNumber,
            color: 'lightblue',
        }
        this.handleClick = this.handleClick.bind(this);
        this.changeHandler = this.changeHandler.bind(this)
    }

    handleClick() {
        this.props.setSorting("shipSorting");
        var newColor = this.state.color == 'lightblue' ? 'yellow' : 'lightblue';
        this.setState({
            color: newColor
        });
    }

    changeHandler(event) {
        var value = event.target.value
        this.props.onChange(value)
        this.setState({ currentValue: value })
        event.preventDefault();
    }

    render() {
        return (
            <th >
                <div className="sortheader" style={{ background: this.state.color }} onClick={this.handleClick}>
                    <label>Remaining winner ships</label>
                    <input type="text" value={this.state.currentValue} onChange={() => { this.changeHandler(event) }} />
                </div>
            </th>
        )
    }
}
class DateHeader extends React.Component {
    constructor(props) {
        super(props)
        this.state =
        {
            currentValue: this.props.startDate + " - " + this.props.endDate,
            color: 'lightblue',
        }
        this.handleClick = this.handleClick.bind(this);
        this.changeHandler = this.changeHandler.bind(this)
    }
    componentDidMount() {
        $('input[name="datefilterinput"]').daterangepicker();
        $('input[name="datefilterinput"]').data('daterangepicker').setStartDate(this.state.currentValue.split(' - ')[0]);
        $('input[name="datefilterinput"]').data('daterangepicker').setEndDate(this.state.currentValue.split(' - ')[1]);
    }

    changeHandler(event) {
        var value = event.target.value
        this.props.onChange(value)
        this.setState({ currentValue: value })
        event.preventDefault();
    }

    handleClick() {
        this.props.setSorting("dateSorting");
        var newColor = this.state.color == 'lightblue' ? 'yellow' : 'lightblue';
        this.setState({
            color: newColor
        });
    }
    render() {
        return (
            <th >
                <div className="sortheader" style={{ background: this.state.color }} onClick={this.handleClick} >
                    <label>Game Date</label>
                    <input type="text" name="datefilterinput" onChange={() => { this.changeHandler(event) }} />
                </div>
            </th>
        )
    }
}

ReactDOM.render(<StatisticsBoard />, document.getElementById('content'));