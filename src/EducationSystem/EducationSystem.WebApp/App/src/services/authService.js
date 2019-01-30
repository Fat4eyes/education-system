import React, {Component} from 'react'

const defaultState = {
  token: '',
  roles: [],
  email: ''
};

const TOKEN = 'token';
const ROLES = 'roles';
const EMAIL = 'email';

const set = (key, value) => localStorage.setItem(key, value);
const get = key => localStorage.getItem(key);
const check = key => !!get(key);
const remove = key => localStorage.removeItem(key);

const {Provider, Consumer} = React.createContext(defaultState);

class AuthenticateProvider extends Component {
  state = {
    ...defaultState
  };

  signIn = async model => {
    // let response = await handleFetch('/api/auth/signin', {
    //   method: "POST",
    //   headers: {
    //     'Content-Type': 'application/json'
    //   },
    //   body: JSON.stringify(model)
    // });
    //
    // // {
    // //   token: "",
    // //   roles: ["", ""]
    // //   email: ""
    // // }
    //
    // let result = JSON.parse(await response.json());

    let result = {
      token: "token",
      roles: ["ADMIN"],
      email: "admin@admin.com"
    };

    const {token, roles, email} = result;

    set(TOKEN, token);
    set(ROLES, roles.join(';'));
    set(EMAIL, email);
    this.setState({...result}, () => console.log(this.state))
  };
  signOut = () => {
    remove(TOKEN);
    remove(ROLES);
    remove(EMAIL);
    this.setState({...defaultState}, () => console.log(this.state))
  };
  isAuthenticated = () => check(TOKEN) && check(ROLES) && check(EMAIL);
  inRole = role => this.isAuthenticated() && !!get.split(';').find(r => r === role);
  getEmail = () => get(EMAIL);

  action = {
    signIn: this.signIn,
    signOut: this.signOut,
    isAuthenticated: this.isAuthenticated,
    inRole: this.inRole,
    getEmail: AuthenticateProvider.getEmail
  };

  render() {
    return <Provider value={{...this.state, ...this.action}}>
      {this.props.children}
    </Provider>
  }
}

class Authenticated extends Component {
  render() {
    const {children, inRole: role} = this.props;

    if (!!role) {
      return <Consumer>
        {({inRole}) => inRole(role) ? children : <></>}
      </Consumer>
    }

    return <Consumer>
      {({isAuthenticated}) => isAuthenticated() ? children : <></>}
    </Consumer>
  }
}

class UnAuthenticated extends Component {
  render() {
    return <Consumer>
      {({isAuthenticated}) => !isAuthenticated() ? this.props.children : <></>}
    </Consumer>
  }
}

export {
  Authenticated,
  UnAuthenticated,
  AuthenticateProvider,
  Consumer as AuthenticateConsumer
};