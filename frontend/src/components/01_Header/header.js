import React, { useContext } from "react";
import { userContext } from "../../App";
import { Link, useLocation } from "react-router-dom";
import {
  Home,
  People,
  Group,
  Login,
  Person,
  Notifications,
  ExitToApp,
  Logout,
} from "@mui/icons-material";
import "./header.scss";

function Header() {
  const { user, logoutUser } = useContext(userContext);
  const location = useLocation();

  return (
    <div className="header">
      <Link to="/" className="logo">
        <img src="./assets/logo.png" alt="logo.png" />{" "}
        <span className="header-text"> FTNchat </span>
      </Link>
      <div className="menu">
        {user ? (
          <>
            <Link to="/profile">
              <Person /> <span className="header-text"> Profile </span>
            </Link>
            <Link to="/friends">
              <People /> <span className="header-text"> Friends </span>
            </Link>
            <Link to="/groups">
              <Group /> <span className="header-text"> Groups </span>
            </Link>
            <Link to="/notifications">
              <Notifications />{" "}
              <span className="header-text"> Notifications </span>
            </Link>
            <Link to="/login" onClick={logoutUser}>
              <Logout /> <span className="header-text"> Log out </span>
            </Link>
          </>
        ) : (
          <>
            {location.pathname !== "/login" ? (
              <Link to="/login">
                <Login /> <span className="header-text"> Log in </span>
              </Link>
            ) : (
              <Link to="/register">
                <ExitToApp /> <span className="header-text"> Register </span>
              </Link>
            )}
          </>
        )}
      </div>
    </div>
  );
}

export default Header;
