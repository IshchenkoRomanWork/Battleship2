import { HttpClient } from '@angular/common/http';
import { Subscription, BehaviorSubject, Observable } from 'rxjs';
import { HubconnectionService } from 'src/app/services/hubconnection.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-gameboard',
  templateUrl: './gameboard.component.html',
  styleUrls: ['./gameboard.component.css']
})
export class GameboardComponent implements OnInit, OnDestroy {
  gameId = '';
  playerId = '';
  opponentsName = '';
  isGameCreated = false;
  showSetup = false;
  gameStartSubscription: Subscription;
  opponentConnectedSubscription: Subscription;
  setIdSubscription: Subscription;
  gameEndedSubscription: Subscription;
  opponentLeftSubscription: Subscription;
  playerIdSubscription: Subscription;
  gameid: string;
  playerIdBehaviourSubject: BehaviorSubject<string>;
  constructor(public hubConnectionService: HubconnectionService,
    route: ActivatedRoute,
    public http: HttpClient) {
    if (route.snapshot.paramMap.get('gameid')) {
      console.log(route.snapshot.paramMap.get('gameid'));
      console.log(typeof route.snapshot.paramMap.get('gameid'));
      this.gameId = route.snapshot.paramMap.get('gameid');
    }
    // console.log('before gameboard subscriptions made');
    this.gameStartSubscription = hubConnectionService.GameStartSubject.subscribe(() => {
      alert('Game has started');
      this.showSetup = false;
    });
    this.opponentConnectedSubscription = hubConnectionService.OpponentConnectedSubject.subscribe((opponentsName) => {
      alert('Your opponent is connected');
      this.opponentsName = opponentsName;
    });
    this.setIdSubscription = hubConnectionService.SetIdSubject.subscribe((gameid) => {
      this.gameId = gameid;
    });
    this.gameEndedSubscription = hubConnectionService.GameEndedSubject.subscribe((youarewinner) => {
      if (youarewinner) {
        confirm('Youve Won!');
      } else {
        confirm('Youve Lost!');
      }
      window.location.href = '/Game/Win';
    });
    this.opponentLeftSubscription = hubConnectionService.OpponentLeftSubject.subscribe((gameid) => {
      alert('Your opponent left');
      this.opponentsName = '';
    });
    // this.playerIdBehaviourSubject = convertObservableToBehaviorSubject<string>
    //   (this.http.get('./../api/gameapi/getplayerid') as Observable<string>, 'initial');

    // this.playerIdSubscription = this.playerIdBehaviourSubject.asObservable().subscribe((playerid) => {
    //   if (playerid !== 'initial') {
        // console.log(playerid);
    //     this.playerId = playerid;
    //   }
      // console.log(playerid);
    // });
  }
  ngOnInit() {
    // console.log('before promise from gameapi call made');
    const promise = this.http.get<string>('https://localhost:5000/api/gameapi/getplayerid').toPromise();
    try {
    // console.log('before promise handler made');
    promise.then((playeridnumber) => {
      const playerid = playeridnumber.toString();
      // console.log('promise handler called, playerid is = ' + playerid);
      this.playerId = playerid;
      // console.log(typeof this.playerId);
      console.log('Calling hubConnection with ' + this.gameid + ' as gameid and ');
      console.log(this.gameId === '');
      // console.log('as isCreated');
      this.hubConnectionService.ConnectionStart(this.gameId === '', this.playerId, this.gameId);
    });
    // console.log('after promise handler made');
    } catch (exception) {
      // console.error(exception);
    }

  }

  ngOnDestroy() {
    this.gameStartSubscription.unsubscribe();
    this.opponentConnectedSubscription.unsubscribe();
    this.setIdSubscription.unsubscribe();
    this.gameEndedSubscription.unsubscribe();
  }
}

export function convertObservableToBehaviorSubject<T>(observable: Observable<T>, initValue: T): BehaviorSubject<T> {
  const subject = new BehaviorSubject(initValue);
  observable.subscribe({
      next: x => subject.next(x)
  });

  return subject;
}
