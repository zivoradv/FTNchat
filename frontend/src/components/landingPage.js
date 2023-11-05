import React, { useContext } from "react";
import { userContext } from "../App";

const LandingPage = () => {
  const { user } = useContext(userContext);

  return (
    <div>
      <h2>Welcome to FTNchat Application!</h2>
      {user ? (
        <div>
          <h3>Hello, {user.username}!</h3>
        </div>
      ) : (
        <p>
          Welcome guest! Please register or login to explore the FTNchat
          application.
        </p>
      )}
    </div>
  );
};

export default LandingPage;
