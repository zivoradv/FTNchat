import React, { useState } from 'react';
import "./register.css";

function Register() {
    const [formData, setFormData] = useState({
        username: '',
        email: '',
        password: '',
        confirmPassword: '',
        birthdate: '',
        gender: 'male',
        address: '',
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();


    };

    return (
        <div className="registration-form">
            <h2>Register</h2>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>
                        Username:
                        <input type="text" name="username" value={formData.username} onChange={handleChange} required />
                    </label>
                </div>
                <div className="form-group">
                    <label>
                        Email:
                        <input type="email" name="email" value={formData.email} onChange={handleChange} required />
                    </label>
                </div>

                <div className="form-group">
                    <label>
                        Password:
                        <input type="password" name="password" value={formData.password} onChange={handleChange} required />
                    </label>
                </div>

                <div className="form-group">
                    <label>
                        Confirm Password:
                        <input type="password" name="confirmPassword" value={formData.confirmPassword} onChange={handleChange} required />
                    </label>
                </div>

                <div className="form-group">
                    <label>
                        Birthdate:
                        <input type="date" name="birthdate" value={formData.birthdate} onChange={handleChange} required />
                    </label>
                </div>

                <div className="form-group">
                    <label>
                        Gender:
                        <select name="gender" value={formData.gender} onChange={handleChange}>
                            <option value="male">Male</option>
                            <option value="female">Female</option>
                            <option value="other">Other</option>
                        </select>
                    </label>
                </div>

                <div className="form-group">
                    <label>
                        Address (Optional):
                        <input type="text" name="address" value={formData.address} onChange={handleChange} />
                    </label>
                </div>

                <div className="form-group">
                    <button type="submit">Register</button>
                </div>
            </form>
        </div>
    );
}

export default Register;
