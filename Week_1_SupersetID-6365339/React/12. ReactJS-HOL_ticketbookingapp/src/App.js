import React, { useState } from 'react';
import Greeting from './components/Greeting';
import LoginButton from './components/LoginButton';
import LogoutButton from './components/LogoutButton';

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  const handleLoginClick = () => setIsLoggedIn(true);
  const handleLogoutClick = () => setIsLoggedIn(false);

  const button = isLoggedIn ? (
    <LogoutButton onClick={handleLogoutClick} />
  ) : (
    <LoginButton onClick={handleLoginClick} />
  );

  return (
    <div style={{ textAlign: 'center', marginTop: '100px' }}>
      <Greeting isLoggedIn={isLoggedIn} />
      {button}
    </div>
  );
}

export default App;
