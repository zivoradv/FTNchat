import React, { useState } from "react";
import { Link } from "react-router-dom";
import "./register.scss";
import axios from "axios";

function Register() {
  const [formData, setFormData] = useState({
    username: "",
    email: "",
    password: "",
    confirmPassword: "",
    birthdate: "",
    gender: "male",
    address: "",
  });

  const [errorMessage, setErrorMessage] = useState("");

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    // Password confirmation check
    if (formData.password !== formData.confirmPassword) {
      setErrorMessage("Passwords do not match!");
      return;
    }

    // Birthday validation
    if (!formData.birthdate) {
      setErrorMessage("Are you sure you don't want to enter your birthday?");
      return;
    }

    // Clear previous error messages
    setErrorMessage("");

    // API request using Axios
    axios
      .post("https://localhost:7195/api/users", formData)
      .then((response) => {
        console.log(response.data);
        // Handle successful registration, e.g., redirect to login page
      })
      .catch((error) => {
        console.error(error);
        // Handle registration error, display appropriate message to the user
      });
  };

  return (
    <div
      className="register-wrapper"
      style={{
        backgroundImage: "url('./assets/illustrations/backgroundLines.png')",
      }}
    >
      <img
        className="heroImage"
        src="./assets/illustrations/peopleMessaging.png"
        alt="login.png"
      />
      <div className="registration-form">
        <h2>Register</h2>
        {errorMessage && <div className="error-message">{errorMessage}</div>}
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>
              Username<span className="required-field">*</span>
            </label>
            <input
              type="text"
              name="username"
              value={formData.username}
              onChange={handleChange}
              required
            />
          </div>
          <div className="form-group">
            <label>
              Email<span className="required-field">*</span>
            </label>
            <input
              type="email"
              name="email"
              value={formData.email}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-group">
            <label>
              Password<span className="required-field">*</span>
            </label>
            <input
              type="password"
              name="password"
              value={formData.password}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-group">
            <label>
              Confirm Password<span className="required-field">*</span>
            </label>
            <input
              type="password"
              name="confirmPassword"
              value={formData.confirmPassword}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-group">
            <label>Birthdate:</label>
            <input
              type="date"
              name="birthdate"
              value={formData.birthdate}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-group">
            <label>
              Gender<span className="required-field">*</span>
            </label>
            <select
              name="gender"
              value={formData.gender}
              onChange={handleChange}
            >
              <option value="male">Male</option>
              <option value="female">Female</option>
              <option value="other">Other</option>
            </select>
          </div>

          <div className="form-group">
            <label>Address (Optional):</label>
            <input
              type="text"
              name="address"
              value={formData.address}
              onChange={handleChange}
            />
          </div>

          <button type="submit">Register</button>
          <Link to="/login">Already have an account?</Link>
        </form>
      </div>
    </div>
  );
}

export default Register;
