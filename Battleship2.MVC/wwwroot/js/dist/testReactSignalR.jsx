window.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/testhub")
    .build();

window.hubConnection
    .start()
    .then(() => console.log('Connection started!'))
    .catch(err => console.log('Error while establishing connection :('))

class CommentBox extends React.Component {
    constructor(props) {
        super(props);
        //const hubConnection = new signalR.HubConnectionBuilder()
        //    .withUrl("/testhub")
        //    .build();

        //hubConnection
        //    .start()
        //    .then(() => console.log('Connection started!'))
        //    .catch(err => console.log('Error while establishing connection :('))
        this.state = {
            message: 'Click me!',
            //hubConnection: hubConnection
        }
        /*this.state.*/window.hubConnection.on('message', (receivedMessage) => {
            console.log("commentbox event message invoked");
        });
    }
    componentDidMount = () => {

    }

    render() {
        console.log(this);
        return (
            <CommentItem /*hubConnection={this.state.hubConnection} *//>
        );
    }
}

class CommentItem extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            message: 'I am Comment Item!',
            //hubConnection: this.props.hubConnection,
        };

       /* this.state.*/window.hubConnection.on('message', (receivedMessage) => {
            console.log("commentitems event message invoked");
            this.setState({ message: receivedMessage })
        });

        console.log("commentitem constructor called");
    }
    render() {
        console.log(this);
        return (
            <button className="commentItem" onClick={() => {
                /*this.state.*/window.hubConnection.invoke("Ready")
            }}> {this.state.message} </ button>
        );
    }
}

ReactDOM.render(<CommentBox />, document.getElementById('content'));