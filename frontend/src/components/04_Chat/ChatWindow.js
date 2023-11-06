import React from 'react';
import './ChatWindow.scss'; // Import the SCSS file for styling

function ChatWindow() {
  return (
    <div className="chat-window">
      <div className="chat-header">Chat with User</div>
      <div className="chat-messages">
        {/* Messages will be displayed here */}
      </div>
      <div className="chat-input">
        <input type="text" placeholder="Type your message..." />
        <button>Send</button>
      </div>
    </div>
  );
}

export default ChatWindow;
