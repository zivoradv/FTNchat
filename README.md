
---

# Web Chat Application

This is a simple web chat application developed using ASP.NET Core and MySQL. It allows users to send messages to each other in real-time. Users can register, log in, and send messages to other users.

## Table of Contents

- [Features](#features)
- [Database Schema](#database-schema)
- [Setup](#setup)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Features

- **User Authentication:** Users can register with a username and password. Passwords are securely hashed and stored in the database.
- **Real-time Messaging:** Users can send and receive messages in real-time.
- **Message History:** Message timestamps are recorded, allowing users to view message history.
- **Secure Communication:** Messages are transmitted securely and stored encrypted in the database.
- **User Management:** Admins can manage user accounts, reset passwords, and view user activity.

## Database Schema

The application uses two tables in the MySQL database:

### Users Table

- `UserID INT PRIMARY KEY AUTO_INCREMENT`: Unique identifier for each user.
- `Username VARCHAR(50) NOT NULL`: User's username.
- `Password VARCHAR(100) NOT NULL`: Securely hashed password.
- `CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP`: Timestamp indicating when the user account was created.

### ChatMessages Table

- `MessageID INT PRIMARY KEY AUTO_INCREMENT`: Unique identifier for each message.
- `SenderID INT`: ID of the user sending the message (references Users table).
- `ReceiverID INT`: ID of the user receiving the message (references Users table).
- `MessageText TEXT`: Content of the message.
- `Timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP`: Timestamp indicating when the message was sent.

## Setup

1. Clone the repository to your local machine.
2. Create a MySQL database and execute the provided SQL queries to create the necessary tables.
3. Configure the database connection string in the `appsettings.json` file.
4. Build and run the ASP.NET Core application.

## Usage

1. Register a new account or log in with existing credentials.
2. Start sending and receiving messages with other users.
3. Explore the chat history and enjoy real-time communication!

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvement, please open an issue or create a pull request.

## License

This project is licensed under the [MIT License](LICENSE).

--- 

Feel free to modify this template according to your specific application features and requirements!