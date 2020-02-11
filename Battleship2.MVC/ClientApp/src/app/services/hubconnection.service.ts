import { Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import * as signalRMessagePack from '@microsoft/signalr-protocol-msgpack';

@Injectable({
  providedIn: 'root'
})
export class HubconnectionService {
  private hubConnection: signalR.HubConnection;
  public GameStartSubject: Subject<any> = new Subject<any>();
  public OpponentConnectedSubject: Subject<any> = new Subject<any>();
  public SetIdSubject: Subject<any> = new Subject<any>();
  public GameEndedSubject: Subject<any> = new Subject<any>();
  public OpponentLeftSubject: Subject<any> = new Subject<any>();
  public AddShipSubject: Subject<any> = new Subject<any>();
  public YourTurnSubject: Subject<any> = new Subject<any>();
  public YourShootResultSubject: Subject<any> = new Subject<any>();
  public YourFieldShootedSubject: Subject<any> = new Subject<any>();
  public GameActionSubject: Subject<any> = new Subject<any>();
  public UnaddedShipsSubject: Subject<any> = new Subject<any>();

  public ConnectionStart(gameIsCreated: boolean, playerId: string, gameId: string ) {
    console.log('before connection started');
    this.hubConnection.start().then(() => {
      if (gameIsCreated) {
        console.log('invoking GameCreated');
          this.hubConnection.invoke('GameCreated', playerId);
      } else {
        console.log('invoking PlayerConnected');
          this.hubConnection.invoke('PlayerConnected', playerId, gameId);
      }
  });
}
  public PlayerLeft() {
  this.hubConnection.invoke('PlayerLeft');
}
  public ShotAt(coordX: number, coordY: number) {
    this.hubConnection.invoke('ShotAt', [coordX, coordY]);
}
  public AddShip(headX: number, headY: number, coordX: number, coordY: number, length: number) {
  this.hubConnection.invoke('AddShip', [headX, headY, coordX, coordY, length]);
}
  public Ready() {
  this.hubConnection.invoke('Ready');
}
  private AddEvents() {
    this.hubConnection.on('GameStart', () => {
      console.log("game started");
      this.GameStartSubject.next();
    });
    this.hubConnection.on('OpponentConnected', (opponentsName) => { 
      // console.log("opponentn connected" + opponentsName);
      this.OpponentConnectedSubject.next(opponentsName);
    });
    this.hubConnection.on('SetId', (gameId) => {
      this.SetIdSubject.next(gameId);
    });
    this.hubConnection.on('GameEnded', (youarewinner) => {
      this.GameEndedSubject.next(youarewinner);
    });
    this.hubConnection.on('OpponentLeft', () =>  {
      this.OpponentLeftSubject.next();
    });
    this.hubConnection.on('AddShip', (jsonshipsection, length) => {
      // console.log("Before addship event call, legth is" + length);
      this.AddShipSubject.next(jsonshipsection);
      // console.log("Before UnaddedShipsSubject event call, legth is" + length);
      this.UnaddedShipsSubject.next(length);
      // console.log("after UnaddedShipsSubject event call");
    });
    this.hubConnection.on('YourTurn', () => {
      this.YourTurnSubject.next();
    });
    this.hubConnection.on('YourShootResult', (shootedJsonData, shotdata) => {
      this.YourShootResultSubject.next(shootedJsonData);
      this.GameActionSubject.next(shotdata);
    });
    this.hubConnection.on('YourFieldShooted', (shootedJsonData, shotdata) => {
      this.YourFieldShootedSubject.next(shootedJsonData);
      this.GameActionSubject.next(shotdata);
    });
  }
  constructor() {
    // console.log('before connection built');
    this.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl('/gamehub')
    // .withHubProtocol( new signalRMessagePack.MessagePackHubProtocol())
    .configureLogging(signalR.LogLevel.Information)
    .build();
    // console.log('before events add');
   this.AddEvents();
}
}
