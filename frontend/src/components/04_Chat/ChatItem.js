import React from 'react';

const ChatItem = ({ user, onSelectUser }) => {
  const { user_id, username, lastMessage } = user;

  return (
    <div className="chat-item" onClick={() => onSelectUser(user_id)}>
      <div className="username">{username}</div>
      <div className="last-message">{lastMessage}</div>
    </div>
  );
};

export default ChatItem;
