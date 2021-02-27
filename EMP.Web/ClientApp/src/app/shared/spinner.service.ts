import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

  loading: boolean | null;

  constructor() {
    this.loading = false;
  }

  isLoading(): boolean {
    return !!this.loading;
  }

  startLoading(): void {
    // setTimeout must be set to prevent
    // ExpressionChangedAfterItHasBeenCheckedError
    setTimeout(() => {
      this.loading = true;
    });
  }

  stopLoading(): void {
    // setTimeout must be used to prevent
    // ExpressionChangedAfterItHasBeenCheckedError
    setTimeout(() => {
      this.loading = false;
    });
  }
}
