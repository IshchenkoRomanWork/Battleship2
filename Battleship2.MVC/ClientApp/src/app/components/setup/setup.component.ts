import { UnaddedshipsService } from '../../services/unaddedships.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-setup',
  templateUrl: './setup.component.html',
  styleUrls: ['./setup.component.css']
})
export class SetupComponent implements OnInit, OnDestroy {
  unaddedShipList: number[] = []; // 1, 2, 2, 2, 3, 4
  subscription: Subscription;

  constructor(private unaddedshipsService: UnaddedshipsService) {
    this.subscription = unaddedshipsService.UnaddedShipsSubject.subscribe(unaddedShips => {
      // console.log("Setup subscription called, unaddedShips is" + unaddedShips);
      this.unaddedShipList = this.getUnaddedShipList(unaddedShips);
    });
   }

   getUnaddedShipList(unaddedShips) {
    const newUnaddedShips = unaddedShips.map((typeAndNumber) => {
      return new Array(typeAndNumber[1]).fill(typeAndNumber[0]);
    });
    let unaddedShipList = [];
    newUnaddedShips.forEach(element => {
      unaddedShipList = unaddedShipList.concat(element);
    });
    return unaddedShipList;
   }

  ngOnInit() {
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

}
