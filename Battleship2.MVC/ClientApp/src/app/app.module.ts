import { HubconnectionService } from 'src/app/services/hubconnection.service';
import { UnaddedshipsService } from 'src/app/services/unaddedships.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { YourmapComponent } from './components/yourmap/yourmap.component';
import { OpponentsmapComponent } from './components/opponentsmap/opponentsmap.component';
import { GameactionsComponent } from './components/gameactions/gameactions.component';
import { GameboardComponent } from './components/gameboard/gameboard.component';
import { SetupComponent } from './components/setup/setup.component';
import { ArrayPipe } from './array.pipe';

@NgModule({
   declarations: [
      AppComponent,
      GameboardComponent,
      YourmapComponent,
      OpponentsmapComponent,
      GameactionsComponent,
      SetupComponent,
      ArrayPipe
   ],
   imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'gameangular', component: GameboardComponent},
    ])
  ],
  providers: [
    UnaddedshipsService,
    HubconnectionService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
