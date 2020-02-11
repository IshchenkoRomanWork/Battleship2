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
    alert('You are active player now');
  });
    this.yourShootResultSubscription = hubConnectionService.YourShootResultSubject.subscribe((shootedJsonData) => {
      console.log("YourShootResult shootedJsonData");
      console.log(shootedJsonData);
      const shootedData = JSON.parse(JSON.stringify(eval("(" + shootedJsonData + ")")));
      const newMap = [...this.map];
      shootedData.forEach(shootedCoord => {
          newMap[10 - shootedCoord.Item1.CoordY][shootedCoord.Item1.CoordX - 1].damaged = true;
          newMap[10 - shootedCoord.Item1.CoordY][shootedCoord.Item1.CoordX - 1].hidden = false;
          if (shootedCoord.Item2) {
            newMap[10 - shootedCoord.Item1.CoordY][shootedCoord.Item1.CoordX - 1].ship = true;
          }
      });
      this.map = newMap;
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
