import React, { useState, useEffect } from 'react';

const LandingPage = () => {

  const [users, setUsers] = useState([]);

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
