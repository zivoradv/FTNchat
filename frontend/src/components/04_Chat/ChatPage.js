import React from 'react';
import ChatList from './ChatList';
import ChatWindow from './ChatWindow';

const ChatPage = () => {
  return (
    <div className="main-container">
      <ChatList />
      <ChatWindow />
    </div>
  );
};

export default ChatPage;
