import React from 'react';
import ChatItem from './ChatItem';
import './ChatList.scss';

const ChatList = ({ onSelectUser }) => {
  // Sample recentChats data for demonstration
  const recentChats = [
    {
      user_id: 1,
      username: 'Alice',
      lastMessage: 'Hello there!',
    },
    {
      user_id: 2,
      username: 'Bob',
      lastMessage: 'Hi, how are you?',
    },
    {
      user_id: 3,
      username: 'Charlie',
      lastMessage: 'Hey!',
    },
    // Add more chat items as needed
  ];

  return (
    <div className="chat-list">
      {recentChats.map(chat => (
        <ChatItem key={chat.user_id} user={chat} onSelectUser={onSelectUser} />
      ))}
    </div>
  );
};

export default ChatList;
