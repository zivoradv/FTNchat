import React, { useState, useEffect } from 'react';

const LandingPage = () => {

    const [users, setUsers] = useState([]);

  useEffect(() => {
    // Fetch users from the API endpoint
    fetch('https://localhost:7195/api/Users')
      .then((response) => response.json())
      .then((data) => setUsers(data))
      .catch((error) => console.error('Error fetching users:', error));
  }, []); // Empty dependency array ensures this effect runs once after initial render

  console.log(users)
  return (
    <div>
      <h2>User Table</h2>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Username</th>
            <th>Created At</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
            <tr key={user.userId}>
              <td>{user.userId}</td>
              <td>{user.username}</td>
              <td>{user.createdAt}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default LandingPage;
