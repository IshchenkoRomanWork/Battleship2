import { Subscription } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { HubconnectionService } from 'src/app/services/hubconnection.service';

@Component({
  selector: 'app-gameactions',
  templateUrl: './gameactions.component.html',
  styleUrls: ['./gameactions.component.css']
})
export class GameactionsComponent implements OnInit, OnDestroy {

  gameActions: string[] = [];
  gameActionSubscription: Subscription;
  constructor( hubConnectionService: HubconnectionService) {
    this.gameActionSubscription = hubConnectionService.GameActionSubject.subscribe((shotData) => {
      this.gameActions.push(shotData);
    });
   }

  ngOnInit() {
  }

  ngOnDestroy() {
  }

}
