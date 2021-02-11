// import { Injectable } from '@angular/core';
// import { User } from './user';

// @Injectable({
//   providedIn: 'root'
// })
// export class AuthService {
//   currentUser: User;
//   private _redirectUrl: string;

//   get redirectUrl(): string {
//     return this._redirectUrl;
//   }
//   set redirectUrl(value: string) {
//     this._redirectUrl = value ? value.trim() : value;
//   }

//   get isLoggedIn(): boolean {
//     return !!this.currentUser;
//   }

//   constructor() {
//     const localUserName: string = localStorage.getItem('currentUserName').trim();
//     const localId: number = localStorage.getItem('currentUserId').trim() ? +localStorage.getItem('currentUserId') : null;
//     const localIsAdmin: boolean = localStorage.getItem('currentUserIsAdmin').trim() ?
//                                   localStorage.getItem('currentUserIsAdmin').trim() === 'true' :
//                                   false;
//     if (localUserName && localId) {
//       this.currentUser = {
//         id: localId,
//         userName: localUserName,
//         isAdmin: localIsAdmin
//       };
//     } else {
//       this.currentUser = null;
//     }
//   }

//   login(userName: string, password: string): void {
//     if (!userName || !password) {
//       alert('Please enter your userName and password');
//       return;
//     }
//     if (userName === 'admin') {
//       this.currentUser = {
//         id: 1,
//         userName,
//         isAdmin: true
//       };
//       alert('Admin login');
//     } else {
//       this.currentUser = {
//         id: 2,
//         userName,
//         isAdmin: false
//       };
//       alert(`User: ${this.currentUser.userName} logged in`);
//     }

//     // localStorage.setItem('currentUserName', this.currentUser.userName);
//     // localStorage.setItem('currentUserId', this.currentUser.id.toString());
//     // localStorage.setItem('currentUserIsAdmin', this.currentUser.isAdmin.toString());
//     this.setLocalUserStorate(this.currentUser.userName,
//                              this.currentUser.id.toString(),
//                              this.currentUser.isAdmin.toString());
//   }

//   logout(): void {
//     this.currentUser = null;
//     this.setLocalUserStorate(null, null, null);
//   }

//   setLocalUserStorate(userName: string,
//                       userId: string,
//                       isAdmin: string) : void {
//     localStorage.setItem('currentUserName', userName);
//     localStorage.setItem('currentUserId', userId);
//     localStorage.setItem('currentUserIsAdmin', isAdmin);
//   }
// }
