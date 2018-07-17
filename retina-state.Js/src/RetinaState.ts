import { Blazor } from './Blazor';

export class RetinaState {
  requestHandler: any;
  methodName: string;
  typeName: string;
  namespace: string;
  assemblyName: string;

  constructor () {
    this.assemblyName = 'RetinaState';
    this.namespace = 'RetinaState.Features.JavaScriptInterop';
    this.typeName = 'JavaScriptInstanceHelper';
    this.methodName = 'Handle';

    this.requestHandler =
      Blazor.platform.findMethod(this.assemblyName, this.namespace, this.typeName, this.methodName);
  }

  dispatchRequest (request) {
    const requestAsJson = JSON.stringify(request);

    console.log(`Dispatching request: ${requestAsJson}`);

    const requestAsString = Blazor.platform.toDotNetString(requestAsJson);

    // tslint:disable-next-line:no-null-keyword
    Blazor.platform.callMethod(this.requestHandler, null, [requestAsString]);
  }
}
