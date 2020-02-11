import { Mapcell } from './../../mapcell';
import { Subscription } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { HubconnectionService } from 'src/app/services/hubconnection.service';
import { UnaddedshipsService } from 'src/app/services/unaddedships.service';

@Component({
  selector: 'app-yourmap',
  templateUrl: './yourmap.component.html',
  styleUrls: ['./yourmap.component.css']
})
export class YourmapComponent implements OnInit, OnDestroy {
  map: Mapcell[][] = [];
  shipAdditionLocked = false;
  shipsHead = null;
  fastShipsNotClicked = true;
  addShipSubscription: Subscription;
  yourFieldShootedSubscription: Subscription;

  constructor(public hubConnectionService: HubconnectionService,
    public unaddedshipsService: UnaddedshipsService) {
    this.addShipSubscription = hubConnectionService.AddShipSubject.subscribe((jsonshipsection) => {
      const shipToAddCoordData = JSON.parse(JSON.stringify(eval('(' + jsonshipsection + ')')));
      const newMap = [...this.map];
      shipToAddCoordData.forEach(addedCoord => {
        newMap[10 - addedCoord.CoordY][addedCoord.CoordX - 1].ship = true;
      });
      this.map = newMap;
    });
    this.yourFieldShootedSubscription = hubConnectionService.YourFieldShootedSubject.subscribe((shootedJsonData) => {
      console.log("YourField shootedJsonData");
      console.log(shootedJsonData);
      const shootedData = JSON.parse(JSON.stringify(eval('(' + shootedJsonData + ')')));
      const newMap = [...this.map];
      shootedData.forEach(shootedCoord => {
        newMap[10 - shootedCoord.Item1.CoordY][shootedCoord.Item1.CoordX - 1].damaged = true;
      });
      this.map = newMap;
    });
  }

  addCell(coordX, coordY) {
    if (!this.shipAdditionLocked) {
      //console.log(coordX, coordY);
      const newMap = [...this.map];
      const clickedCell = newMap[10 - coordY][coordX - 1];
      if (this.shipsHead == null && !clickedCell.ship) {
        clickedCell.ship = true;
        this.shipsHead = {
          coordX: coordX,
          coordY: coordY,
          cell: clickedCell
        };
      } else {
        const headX = this.shipsHead.coordX;
        const headY = this.shipsHead.coordY;
        let length;
        if ((((length = Math.abs(headX - coordX)) < 4 && headY === coordY) ||
         ((length = Math.abs(headY - coordY)) < 4 && headX === coordX)) &&
            (!clickedCell.ship || (headX === coordX && headY === coordY))) {
            let shipavailable = false;
            length++;
            const availships = this.unaddedshipsService.UnaddedShips;
            shipavailable = availships.some((group) => group[0] === length && group[1] > 0);
            if (shipavailable) {
                //console.log(headX, headY, coordX, coordY, length);
                this.hubConnectionService.AddShip(headX, headY, coordX, coordY, length);
            } else {
                alert('Sorry, no ships remained of that type');
            }
        } else {
            alert('You can\'t create ship here');
        }
        //console.log(headY, headX);
        newMap[10 - headY][headX - 1].ship = false;
        this.shipsHead = null;
    }
    this.map = newMap;
    }
  }
  fastShips() {
    this.hubConnectionService.AddShip(1, 10, 1, 7, 4);
    this.hubConnectionService.AddShip(3, 10, 3, 8, 3);
    this.hubConnectionService.AddShip(5, 10, 5, 9, 2);
    this.hubConnectionService.AddShip(7, 10, 7, 10, 1);
    this.hubConnectionService.AddShip(9, 10, 9, 10, 1);
    this.hubConnectionService.AddShip(7, 8, 7, 8, 1);
    this.hubConnectionService.AddShip(9, 8, 9, 8, 1);
    this.hubConnectionService.AddShip(7, 6, 9, 6, 3);
    this.hubConnectionService.AddShip(7, 4, 7, 3, 2);
    this.hubConnectionService.AddShip(9, 4, 9, 3, 2);
}

  ngOnInit() {
    const newMap = new Array(10).fill(new Array(10).fill({}));
    this.map = newMap.map((row) => row.map(() => (new Mapcell(false,  false, false))));
  }
  ngOnDestroy() {
    this.addShipSubscription.unsubscribe();
    this.yourFieldShootedSubscription.unsubscribe();
  }
}
