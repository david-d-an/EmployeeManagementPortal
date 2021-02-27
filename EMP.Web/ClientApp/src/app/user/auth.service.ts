import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { UserManager, User } from 'oidc-client';
import { AuthContext } from './model/auth-context';
import { AppConfig } from 'src/app/app.config';
// import { Constants } from './constants';
// import { CoreModule } from './core.module';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _userManager: UserManager;
  get userManager() {
    // Make sure to load UserManager only once only after page is all rendered
    if (!this._userManager) {
      this.loadConfig();
    }
    return this._userManager;
  }
  private _user: User;
  private _loginChangedSubject = new Subject<boolean>();

  get userName() {
    // console.log(`(this._user['profile']['name']: ${this._user['profile']['name']}`);
    return this._user['profile']['name'];
  }
  get userRole() {
    // console.log(`this._user['profile']['role']: ${this._user['profile']['role']}`);
    return this._user['profile']['role'];
  }

  loginChanged = this._loginChangedSubject.asObservable();
  authContext: AuthContext;

  constructor(private _httpClient: HttpClient) { }

  initAuthSession(): Promise<any> {
    return this.userManager.getUser().then((user) => {
      this._user = user;
      if (!user || user.expired) {
        this.userManager.signinRedirect();
        return false;
      } else {
        return true;
      }
    });
  }

  preCheckAuthSession(originalUri: string): void {
    this.userManager.signinRedirect();
  }

  loadConfig(): void {
    const oidcConfig = AppConfig.settings.oid;
    const stsSettings = {
      authority: oidcConfig.stsAuthority,
      client_id: oidcConfig.clientId,
      redirect_uri: `${oidcConfig.clientRoot}signin-callback`,
      scope: `openid profile ${oidcConfig.apiId}`,
      response_type: 'code',
      // response_type: for auth code flow with PKCE: 'code', for implicit flow: 'id_token token'
      post_logout_redirect_uri: `${oidcConfig.clientRoot}signout-callback`,
      automaticSilentRenew: true,
      silent_redirect_uri: `${oidcConfig.clientRoot}assets/silent-callback.html`

      // authority: Constants.stsAuthority,
      // client_id: Constants.clientId,
      // redirect_uri: `${Constants.clientRoot}signin-callback`,
      // scope: 'openid profile projects-api',
      // response_type: 'code',
      // // response_type: for auth code flow with PKCE: 'code', for implicit flow: 'id_token token'
      // post_logout_redirect_uri: `${Constants.clientRoot}signout-callback`,
      // automaticSilentRenew: true,
      // silent_redirect_uri: `${Constants.clientRoot}assets/silent-callback.html`
    };

    this._userManager = new UserManager(stsSettings);
    this._userManager.events.addAccessTokenExpired(_ => {
      this._loginChangedSubject.next(false);
    });

    // console.log('addUserLoaded initiated');
    this._userManager.events.addUserLoaded(user => {
      if (!!this._user !== !!user) {
        this._user = user;
        this.loadSecurityContext();
        this._loginChangedSubject.next(!!user && !user.expired);
      }
    });
  }

  login() {
    return this.userManager.signinRedirect();
  }

  isLoggedIn(): Promise<boolean> {
    return this.userManager.getUser().then(user => {
      const userCurrent = !!user && !user.expired;
      // console.log(`user: ${JSON.stringify(user)}`);
      // console.log(`this._user: ${JSON.stringify(this._user)}`);
      // console.log(`userCurrent: ${userCurrent}`);
      if (!!(this._user) !== !!user) {
        // console.log(`next(userCurrent)`);
        this._loginChangedSubject.next(userCurrent);
      }
      if (userCurrent && !this.authContext) {
        // console.log(`loadSecurityContext()`);
        this.loadSecurityContext();
      }
      // console.log('this._user = user;');
      this._user = user;
      return userCurrent;
    });
  }

  completeLogin() {
    return this.userManager.signinRedirectCallback().then(user => {
      console.log(`user: ${JSON.stringify(user)}`);
      console.log(`user.profile: ${JSON.stringify(user.profile)}`);
      console.log(`user.profile.name: ${JSON.stringify(user?.profile?.name)}`);
      console.log(`user.profile.role: ${JSON.stringify(user?.profile?.role)}`);

      this._user = user;
      this._loginChangedSubject.next(!!user && !user.expired);
      return user;
    });
  }

  logout() {
    this.userManager.signoutRedirect();
  }

  completeLogout() {
    this._user = null;
    this._loginChangedSubject.next(false);
    return this.userManager.signoutRedirectCallback();
  }

  getAccessToken(): Promise<string | null> {
    return this.userManager.getUser().then(user => {
      if (!!user && !user.expired) {
        return user.access_token;
      } else {
        return null;
      }
    });
  }

  loadSecurityContext() {
    // this._httpClient
    //   .get<AuthContext>(`${Constants.apiRoot}Projects/AuthContext`)
    //   .subscribe(
    //     context => {
    //       this.authContext = new AuthContext();
    //       this.authContext.claims = context.claims;
    //       this.authContext.userProfile = context.userProfile;
    //     },
    //     error => console.error(error)
    //   );
  }

  getUser(): Promise<User> {
    return this.userManager.getUser();
  }
}
