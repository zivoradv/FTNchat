import React, { useState, useEffect, createContext } from "react";
import Cookies from "js-cookie";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import "./App.css";
import Header from "./components/01_Header/header";
import LandingPage from "./components/LandingPage/landingPage";
// import FriendsPage from './components/friendsPage';
// import GroupsPage from './components/groupsPage';
import LoginPage from "./components/03_LogIn/login";
import RegisterPage from "./components/02_Register/register";
// import ProfilePage from './components/profilePage';
// import NotificationsPage from './components/notificationsPage';

export const userContext = createContext(null);

function App() {
  const [user, setUser] = useState(null);

  useEffect(() => {
    loginUser({ username: "alooo" }); // manuelno logovanje korisnika radi testiranja
  }, []);

  function loginUser(user) {
    Cookies.set("user", JSON.stringify(user));
    setUser(user);
  }

  function logoutUser() {
    Cookies.remove("user");
    setUser(null);
  }
  return (
    <userContext.Provider value={{ user, loginUser, logoutUser }}>
      <Router>
        <div className="App">
          <Header />
          <Routes>
            <Route path="/" exact element={<LandingPage />} />
            {/* <Route path="/friends" component={FriendsPage} />
          <Route path="/groups" component={GroupsPage} /> */}
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            {/* <Route path="/profile" component={ProfilePage} />
          <Route path="/notifications" component={NotificationsPage} /> */}
          </Routes>
        </div>
      </Router>
    </userContext.Provider>
  );
}

export default App;
