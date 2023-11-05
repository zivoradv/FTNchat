import React, { useState, useEffect } from 'react';
import Cookies from 'js-cookie';

const LandingPage = () => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    const storedUser = Cookies.get('user');
    if (storedUser) {
      const parsedUser = JSON.parse(storedUser);
      setUser(parsedUser);
    }
  }, []);

  return (
    <div>
      <h2>Welcome to FTNchat Application!</h2>
      {user ? (
        <div>
          <h3>Hello, {user.username}!</h3>
        </div>
      ) : (
        <p>
          Welcome guest! Please register or login to explore the FTNchat application.
        </p>
      )}
    </div>
  );
};

export default LandingPage;
