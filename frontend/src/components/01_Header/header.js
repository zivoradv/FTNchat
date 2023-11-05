import React from "react";
import { Link } from "react-router-dom";
import {
  Home,
  People,
  Group,
  Login,
  Person,
  Notifications,
  ExitToApp,
} from "@mui/icons-material";
import "./header.scss";

function Header() {
  return (
    <div className="header">
      <div className="logo">
        <img src="./assets/logo.png" alt="logo.png"/>{" "}
        <span className="header-text"> FTNchat </span>
      </div>
      <div className="menu">
        <Link to="/">
          <Home /> <span className="header-text"> Home </span>
        </Link>
        <Link to="/friends">
          <People /> <span className="header-text"> Friends </span>
        </Link>
        <Link to="/groups">
          <Group /> <span className="header-text"> Groups </span>
        </Link>
        <Link to="/login">
          <Login /> <span className="header-text"> Login </span>
        </Link>
        <Link to="/register">
          <ExitToApp /> <span className="header-text"> Register </span>
        </Link>
        <Link to="/profile">
          <Person /> <span className="header-text"> Profile </span>
        </Link>
        <Link to="/notifications">
          <Notifications /> <span className="header-text"> Notifications </span>
        </Link>
      </div>
    </div>
  );
}

export default Header;
