import { Platform } from './Platform';

// tslint:disable-next-line:interface-over-type-literal
type BlazorType = {
  platform: Platform,
  // tslint:disable-next-line:ban-types
  registerFunction (identifier: string, implementation: Function)
};

export const Blazor: BlazorType = window['Blazor'];
