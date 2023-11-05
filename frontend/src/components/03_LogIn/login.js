import React, { useContext, useState } from "react";
import { userContext } from "../../App";
import "./login.scss";
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";

function Login() {
  const navigate = useNavigate();
  const { loginUser } = useContext(userContext);
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

        loginUser(JSON.parse(user));

        navigate("/");
      } else {
        console.error("Login failed");
      }
    } catch (error) {
      console.error("Error occurred during login:", error);
    }
  };

  return (
    <div
      className="login-wrapper"
      style={{
        backgroundImage: "url('./assets/illustrations/backgroundLines.png')",
      }}
    >
      <img
        className="heroImage"
        src="./assets/illustrations/peopleMessaging.png"
        alt="login.png"
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
          <Link to="/register">Don't have an account?</Link>
        </form>
      </div>
    </div>
  );
}

export default Login;
