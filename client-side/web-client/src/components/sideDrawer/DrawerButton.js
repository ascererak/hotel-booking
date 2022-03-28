import React from "react";
import "./style.css";

const DrawerToggleButton = props => {
  return (
    <button className="toggle-button" onClick={props.click}>
      <div className="toggle-button-line" />
      <div className="toggle-button-line" />
      <div className="toggle-button-line" />
    </button>
  );
};
export default DrawerToggleButton;
