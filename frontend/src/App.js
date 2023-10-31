import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import Header from './components/01_Header/header';
import LandingPage from './components/landingPage';
// import FriendsPage from './components/friendsPage';
// import GroupsPage from './components/groupsPage';
import LoginPage from './components/03_LogIn/login';
import RegisterPage from './components/02_Register/register';
// import ProfilePage from './components/profilePage';
// import NotificationsPage from './components/notificationsPage';

function App() {
  return (
    <Router>
      <div className="App">
        <Header />
        <Routes>
          <Route path="/" exact element={ <LandingPage/> } />
          {/* <Route path="/friends" component={FriendsPage} />
          <Route path="/groups" component={GroupsPage} /> */}
          <Route path="/login" element={ <LoginPage/> } />
          <Route path="/register" element={ <RegisterPage/> } />
          {/* <Route path="/profile" component={ProfilePage} />
          <Route path="/notifications" component={NotificationsPage} /> */}
        </Routes>
      </div>
    </Router>
  );
}

export default App;
