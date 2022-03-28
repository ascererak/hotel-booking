import React from "react";
import { Route } from "react-router-dom";
import HotelsOnMap from "./map/HotelsOnMap";
import About from "./about/About";
import Contacts from "./contacts/Contacts";
import Header from "./shared/Header";
import "./App.css";
import Form from "./authorization/Form";
import { Form as RegistrationForm } from "./registration/Registration";
import SideDrawer from "./sideDrawer/SideDrawer";
import Backdrop from "./backdrop/Backdrop";
import Profile from "./profile/Profile";
import HomePage from "./hotels/HomePage";
import { BookingForm } from "./booking/BookingForm";

class App extends React.Component {
  state = {
    sideDrawerOpen: false,
    user: { email: "", role: "manager", token: null }
  };
  drawerToggleClickHandler = () => {
    this.setState(prevState => {
      return {
        sideDrawerOpen: !prevState.sideDrawerOpen
      };
    });
  };
  backdropClickHandler = () => {
    this.setState({ sideDrawerOpen: false });
  };
  loginSuccess = (token) => {
    localStorage.setItem("jwt", token);
    fetch(`/api/account/get/${localStorage.getItem("jwt")}`)
      .then(res => res.json())
      .then(res => 
        {
          console.log(res.role);
          if (res || res.email || res.role) {
            localStorage.setItem("name", res.name);
            localStorage.setItem("role", res.role);
            this.forceUpdate();
          }
        });
    
  };
  logoutCallback = () => {
    fetch(`/api/account/logout`, {
      method: "post",
      headers: {
        "Content-type": "application/json",
      },
      body: JSON.stringify({token: localStorage.getItem("jwt")})
    })
    .then(res => {
      if (res.status === 200) {
        localStorage.removeItem("jwt");
        localStorage.removeItem("name");
        localStorage.removeItem("role");
        this.forceUpdate();
      }
    })
  };

  render() {
    const { sideDrawerOpen } = this.state;

    return (
      <div className="container p-0 border" style={{ minHeight: "100%" }}>
        {sideDrawerOpen && <Backdrop click={this.backdropClickHandler} />}
        {console.log(localStorage.getItem("jwt"))}
        <Header
          drawerClickHandler={this.drawerToggleClickHandler}
          logoutCallback={this.logoutCallback}
        />
        {sideDrawerOpen && <SideDrawer />}
        <div className="container pt-5">
          <Route exact path="/" component={HomePage} />
          <Route path="/map" component={HotelsOnMap} />
          <Route path="/about" component={About} />
          <Route path="/contacts" component={Contacts} />
          <Route
            path="/login"
            render={props => (
              <Form {...props} successCallback={this.loginSuccess} />
            )}
          />
          <Route
            path="/profile"
            component={() => <Profile {...this.state.user} />}
          />
          <Route
            path="/registration"
            render={props => (
              <RegistrationForm
                type={"registration"}
                submitText={"Register"}
                successCallback={this.loginSuccess}
              />
            )}
          />
          <Route
            path="/booking"
            render={
              props => (<BookingForm />)}
          />
        </div>
      </div>
    );
  }
}

export default App;
