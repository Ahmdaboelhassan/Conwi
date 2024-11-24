import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoaderService {
  private loadingSubject: BehaviorSubject<boolean> =
    new BehaviorSubject<boolean>(false);
  isLoading$ = this.loadingSubject.asObservable();

  StartLoading() {
    this.loadingSubject.next(true);
  }

  StopLoading() {
    this.loadingSubject.next(false);
  }
}
