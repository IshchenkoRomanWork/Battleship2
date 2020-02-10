import { Mapcell } from './../../mapcell';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { HubconnectionService } from 'src/app/services/hubconnection.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-opponentsmap',
  templateUrl: './opponentsmap.component.html',
  styleUrls: ['./opponentsmap.component.css']
})
export class OpponentsmapComponent implements OnInit, OnDestroy {
  map: Mapcell[][] = [];
  shootingLocked = true;
  youAreActivePlayer = false;
  gameStartSubscription: Subscription;
  yourTurnSubscription: Subscription;
  yourShootResultSubscription: Subscription;

  constructor(private hubConnectionService: HubconnectionService) {
    this.gameStartSubscription = hubConnectionService.GameStartSubject.subscribe(() => {
    this.shootingLocked = false;
  });
    this.yourTurnSubscription = hubConnectionService.YourTurnSubject.subscribe(() => {
    this.youAreActivePlayer = true;
  });
    this.yourShootResultSubscription = hubConnectionService.YourShootResultSubject.subscribe(() => {
    this.youAreActivePlayer = true;
  });
   }

  shotCell(coordX, coordY) {
    console.log("On Shot data: shootingLocked  " + this.shootingLocked.toString()
     + "active player" + this.youAreActivePlayer.toString())
    if (!this.shootingLocked && this.youAreActivePlayer) {
      this.hubConnectionService.ShotAt(coordX, coordY);
    }
}

  ngOnInit() {
    const newMap = new Array(10).fill(new Array(10).fill({}));
    this.map = newMap.map((row) => row.map(() => (new Mapcell(false,  false, true))));
  }
  ngOnDestroy() {
    this.gameStartSubscription.unsubscribe();
    this.yourTurnSubscription.unsubscribe();
    this.yourShootResultSubscription.unsubscribe();
  }
}
