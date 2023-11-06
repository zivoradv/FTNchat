import React, { useContext, useState, useEffect } from "react";
import { userContext } from "../../App";
import { Link, useLocation } from "react-router-dom";
import {
  People,
  Group,
  Login,
  Person,
  Notifications,
  ExitToApp,
  Search,
} from "@mui/icons-material";
import "./header.scss";

function Header() {
  const { user, logoutUser } = useContext(userContext);
  const location = useLocation();
  const [searchResults, setSearchResults] = useState([]);
  const [searchQuery, setSearchQuery] = useState("");

  const handleSearch = async () => {
    try {
      const response = await fetch(`https://localhost:7195/api/search?q=${searchQuery}`);
      if (response.status === 200) {
        const data = await response.json();
        setSearchResults(data);
      } else if (response.status === 400) {
        console.error("Search request failed: Bad Request - Query parameter is required.");
      } else {
        console.error("Search request failed: Internal Server Error");
      }
    } catch (error) {
      console.error("Error while searching:", error);
    }
  };

  useEffect(() => {
    if (searchQuery) {
      handleSearch();
    } else {
      setSearchResults([]);
    }
  }, [searchQuery]);

  return (
    <div className="header">
      <Link to="/" className="logo">
        <img src="./assets/logo.png" alt="logo.png" />{" "}
        <span className="header-text"> FTNchat </span>
      </Link>
      <div className="menu">
        {user ? (
          <>
            <div className="search-bar-container">
              <div className="search-input">
                <input
                  type="search"
                  className="search-bar"
                  placeholder="Search for a friend..."
                  value={searchQuery}
                  onChange={(e) => setSearchQuery(e.target.value)}
                />
              </div>
              {searchResults.length > 0 && (
                <div className="search-results">
                  {searchResults.map((result) => (
                    <div key={result.id}>
                      <span>{result.username}</span>
                    </div>
                  ))}
                </div>
              )}
            </div>

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
              <ExitToApp /> <span className="header-text"> Log out </span>
            </Link>
          </>
        ) : (
          <>
            {location.pathname !== "/login" && (
              <Link to="/login">
                <Login /> <span className="header-text"> Log in </span>
              </Link>
            )}

            {location.pathname !== "/register" && (
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
