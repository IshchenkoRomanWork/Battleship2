import { Injectable, Inject } from '@angular/core';
import { CanActivate, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
    export class AuthGuard implements CanActivate, CanActivateChild {
      constructor(private http: HttpClient) {}

     canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean>|boolean {
        return this.http.get<boolean>('https://localhost:5000/api/gameapi/islogged');
    }

     canActivateChild(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean>|boolean {
       return this.canActivate(next, state);
    }
}
