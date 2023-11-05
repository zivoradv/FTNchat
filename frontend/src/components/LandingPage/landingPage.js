import React, { useContext } from "react";
import { userContext } from "../../App";
import "./landingPage.scss";

const LandingPage = () => {
  const { user } = useContext(userContext);

  return (
    <div className="landingPage">
      {user ? (
        <h1>Welcome back, {user.username}!</h1>
      ) : (
        <h1>
          Connect with Friends and Colleagues!
          <br />
          Please register or login to use the FTNchat.
        </h1>
      )}
      <img
        className="heroImage"
        src="./assets/illustrations/peopleTalking.png"
        alt="login.png"
      />
    </div>
  );
};

export default LandingPage;
