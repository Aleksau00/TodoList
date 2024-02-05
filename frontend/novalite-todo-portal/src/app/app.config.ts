import {ApplicationConfig, importProvidersFrom} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import {BrowserAnimationsModule, provideAnimations, provideNoopAnimations} from '@angular/platform-browser/animations';
import {HTTP_INTERCEPTORS, provideHttpClient, withFetch, withInterceptorsFromDi} from "@angular/common/http";
import {environment} from "./environment";
import {
  MSAL_GUARD_CONFIG,
  MSAL_INSTANCE,
  MSAL_INTERCEPTOR_CONFIG, MsalBroadcastService, MsalGuard, MsalGuardConfiguration,
  MsalInterceptor, MsalInterceptorConfiguration,
  MsalService
} from "@azure/msal-angular";
import {
  BrowserCacheLocation,
  InteractionType,
  IPublicClientApplication, LogLevel,
  PublicClientApplication
} from "@azure/msal-browser";

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),provideHttpClient(withFetch()),
              provideNoopAnimations(),
              provideHttpClient(withInterceptorsFromDi()),
              {
                provide: HTTP_INTERCEPTORS,
                useClass: MsalInterceptor,
                multi: true
              },
              {
                provide: MSAL_INSTANCE,
                useFactory: MSALInstanceFactory
              },
              {
                provide: MSAL_GUARD_CONFIG,
                useFactory: MSALGuardConfigFactory
              },
              {
                provide: MSAL_INTERCEPTOR_CONFIG,
                useFactory: MSALInterceptorConfigFactory
              },
              MsalService,
              MsalGuard,
              MsalBroadcastService
            ]

};

export function loggerCallback(logLevel: LogLevel, message: string) {
  console.log(`MSAL Logging (${LogLevel[logLevel]}): ${message}`);
}

export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: environment.msalConfig.auth.clientId,
      authority: environment.msalConfig.auth.authority,
      redirectUri: '/',
      postLogoutRedirectUri: '/'
    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage
    },
    system: {
      allowNativeBroker: false, // Disables WAM Broker
      loggerOptions: {
        loggerCallback,
        logLevel: LogLevel.Info,
        piiLoggingEnabled: false
      }
    }
  });
}

export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  const protectedResourceMap = new Map<string, Array<string>>();
  protectedResourceMap.set('http://localhost:7232/', environment.apiConfig.scopes);

  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap
  };
}

export function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: [...environment.apiConfig.scopes]
    },
    loginFailedRoute: '/login-failed'
  };
}



