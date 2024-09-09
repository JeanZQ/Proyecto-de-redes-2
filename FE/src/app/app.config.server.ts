import { mergeApplicationConfig, ApplicationConfig } from '@angular/core';
import { provideServerRendering } from '@angular/platform-server';
import { appConfig } from './app.config';
import { HttpClient, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { Http2ServerRequest } from 'http2';

const serverConfig: ApplicationConfig = {
  providers: [provideServerRendering()]

};

export const config = mergeApplicationConfig(appConfig, serverConfig);
