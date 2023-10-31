import React, { useState, useEffect } from 'react';
import axios from 'axios'
const LandingPage = () => {

  const [users, setUsers] = useState([]);

  useEffect(() => {
    axios.get('https://localhost:7195/api/Users')
      .then((response) => {
        setUsers(response.data);
      })
      .catch((error) => {
        console.error('Error fetching users:', error);
      });
  }, []); 

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
            <tr key={user.UserId}>
              <td>{user.UserId}</td>
              <td>{user.Username}</td>
              <td>{user.CreatedAt}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default LandingPage;
