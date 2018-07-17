import { Blazor } from './Blazor';
import { RetinaState } from './RetinaState';

const ReduxExtensionName = '__REDUX_DEVTOOLS_EXTENSION__';
const DevToolsName = 'devTools';

const exportedFunctions = window['RetinaState'][DevToolsName] = {};

export class ReduxDevTools {
  IsEnabled: boolean;
  DevTools: any;
  Extension: any;
  Config: {
    name: string;
    features: {
      pause: boolean;
      lock: boolean;
      persist: boolean;
      export: boolean;
      import: boolean;
      jump: boolean;
      skip: boolean;
      reorder: boolean;
      dispatch: boolean;
      test: boolean;
    };
  };
  RetinaState: RetinaState;

  constructor () {
    this.RetinaState = new RetinaState(); // Depends on this functionality
    this.Config = {
      name: 'Retina State',
      features: {
        pause: false, // start/pause recording of dispatched actions
        lock: false, // lock/unlock dispatching actions and side effects
        persist: false, // persist states on page reloading
        export: false, // export history of actions in a file
        import: false, // import history of actions from a file
        jump: true, // jump back and forth (time traveling)
        skip: false, // skip (cancel) actions
        reorder: false, // drag and drop actions in the history list
        dispatch: false, // dispatch custom actions or action creators
        test: false, // generate tests for the selected actions
      },
    };
    this.Extension = this.GetExtension();
    this.DevTools = this.GetDevTools();
    this.IsEnabled = this.DevTools ? true : false;
    this.Init();
  }

  Init () {
    if (this.IsEnabled) {
      this.DevTools.subscribe(ReduxDevTools.MessageHandler);

      const functionName = 'ReduxDevToolsDispatch';

      exportedFunctions[functionName] = ReduxDevTools.ReduxDevToolsDispatch;
      console.log(`${functionName} function registered with Blazor`);
    }
  }

  GetExtension () {
    const extension = window[ReduxExtensionName];

    if (!extension) {
      console.log('Redux DevTools are not installed.');
    }

    return extension;
  }

  GetDevTools () {
    const devTools = this.Extension && this.Extension.connect(this.Config);

    if (!devTools) {
      console.log('Unable to connect to Redux DevTools.');
    }

    return devTools;
  }

  static MapRequestType (message) {
    const dispatchRequests = {
      COMMIT: undefined,
      IMPORT_STATE: undefined,
      JUMP_TO_ACTION: 'RetinaState.Behaviors.ReduxDevTools.Features.JumpToState.JumpToStateRequest',
      JUMP_TO_STATE: 'RetinaState.Behaviors.ReduxDevTools.Features.JumpToState.JumpToStateRequest',
      RESET: undefined,
      ROLLBACK: undefined,
      TOGGLE_ACTION: undefined,
    };

    let blazorRequestType;

    switch (message.type) {
      case 'START':
        blazorRequestType = 'RetinaState.Behaviors.ReduxDevTools.Features.Start.StartRequest';
        break;
      case 'STOP':
        // blazorRequestType = 'RetinaState.Behaviors.ReduxDevTools.Features.Stop.StopRequest';
        break;
      case 'DISPATCH':
        blazorRequestType = dispatchRequests[message.payload.type];
        break;
    }

    console.log(`Redux Dev tools type: ${message.type} maps to ${blazorRequestType}`);

    return blazorRequestType;
  }

  static MessageHandler (message) {
    console.log('ReduxDevTools.MessageHandler');
    console.log(message);

    let jsonRequest;
    const requestType = ReduxDevTools.MapRequestType(message);

    if (requestType) { // If we don't map this type then there is nothing to dispatch just ignore.
      jsonRequest = {
        // TODO: make sure non Requests from assemblies other than BlazorState also work.
        RequestType: requestType,
        Payload: message,
      };

      reduxDevTools.RetinaState.DispatchRequest(jsonRequest);
    } else {
      console.log(`messages of this type are currently not supported`);
    }
  }

  static ReduxDevToolsDispatch (action, state) {
    if (action === 'init') {
      console.log('ReduxDevTools.js: Dispatching redux action: init');
      return window[DevToolsName].init(state);
    } else {
      console.log('ReduxDevTools.js: Dispatching redux action', { action });
      return window[DevToolsName].send(action, state);
    }
  }
}

let reduxDevTools;

window['RetinaState']['ReduxDevTools'] = {
  create: () => {
    console.log('js - RetinaState.ReduxDevTools.create');
    reduxDevTools = new ReduxDevTools();

    return reduxDevTools.IsEnabled;
  },
};

