import React, { useState } from "react";
import "./login.scss";
import React, { useState } from "react";
import "./login.css";
import axios from "axios";
import Cookies from "js-cookie";

function Login() {
  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post(
        "https://localhost:7195/api/auth/login",
        formData
      );

      if (response.status === 200) {
        const user = response.data;

        Cookies.set("user", JSON.stringify(user));

        window.location.href = "/";
      } else {
        console.error("Login failed");
      }
    } catch (error) {
      console.error("Error occurred during login:", error);
    }
  };

  return (
    <div className="login-wrapper">
      <img
        className="heroImage"
        src="./assets/illustrations/peopleMessaging.png"
      />
      <div className="login-form">
        <h2>Login</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="email" autoFocus="1">
              Email:
            </label>
            <input
              placeholder="Type your email"
              id="email"
              type="email"
              name="email"
              value={formData.email}
              onChange={handleChange}
            />
          </div>
          <div className="form-group">
            <label htmlFor="password">Password:</label>
            <input
              id="password"
              placeholder="Type your password"
              type="password"
              name="password"
              value={formData.password}
              onChange={handleChange}
            />
          </div>
          <button type="submit">Login</button>
        </form>
      </div>
    </div>
  );
}

export default Login;
