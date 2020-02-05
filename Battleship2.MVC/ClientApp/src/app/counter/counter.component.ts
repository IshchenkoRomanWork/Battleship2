import { Component } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;
  public signalRLables = [];
  public hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/testhub")
    .build();

  public incrementCounter() {
    this.hubConnection.invoke("Ready")
    this.currentCount++;
  }

  constructor() {
    this.hubConnection.start();
    console.log(this.hubConnection);

    this.hubConnection.on('message', (receivedMessage) => {
      this.signalRLables = [...this.signalRLables]
      this.signalRLables.push(receivedMessage);
      this.signalRLables.reverse()
      console.log("commentbox event message invoked");
    });
  }
}
