import { HubconnectionService } from './hubconnection.service';
import { Injectable, OnDestroy } from '@angular/core';
import { of, Observable, Subscription, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UnaddedshipsService implements OnDestroy {
public UnaddedShips: number[][] = [[1, 4], [2, 3], [3, 2], [4, 1]];
public UnaddedShipsSubject: Subject<any> = new Subject<any>();
private UnaddedShipRemovedSubscription: Subscription;
constructor(private hubConnectionService: HubconnectionService) {
  this.UnaddedShipRemovedSubscription = hubConnectionService.UnaddedShipsSubject.subscribe((length) => {
    this.UnaddedShips[length - 1][1]--;
    this.UnaddedShipsSubject.next(this.UnaddedShips);
    // console.log("after unadded service Subscription event call");
    // console.log(this.UnaddedShips)
 });
 this.UnaddedShipsSubject.next(this.UnaddedShips);
}
ngOnDestroy(): void {
  this.UnaddedShipRemovedSubscription.unsubscribe();
}
}
