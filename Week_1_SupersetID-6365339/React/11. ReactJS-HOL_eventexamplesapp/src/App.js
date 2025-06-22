import React, { useState } from 'react';

function App() {
  const [count, setCount] = useState(5);
  const [amount, setAmount] = useState('');
  const [currency, setCurrency] = useState('');

  const handleWelcome = () => {
    alert('Hello! Member1');
  };

  const handleClick = () => {
    alert('You clicked the button!');
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    alert(`Converting ${amount} to ${currency}`);
  };

  return (
    <div
      style={{
        padding: '40px',
        fontFamily: 'Arial',
        fontSize: '1.3rem',
        maxWidth: '500px',
        margin: '40px auto',
        background: '#f8f8f8',
        borderRadius: '12px',
        boxShadow: '0 2px 12px rgba(0,0,0,0.08)',
        minHeight: '100vh'
      }}
    >
      <div style={{ marginBottom: '20px', fontSize: '2rem' }}>{count}</div>

      <div style={{ display: 'flex', flexDirection: 'column', gap: '10px', marginBottom: '32px', alignItems: 'flex-start' }}>
        <button style={{ width: '160px', padding: '8px', fontSize: '1rem' }} onClick={() => setCount(count + 1)}>Increment</button>
        <button style={{ width: '160px', padding: '8px', fontSize: '1rem' }} onClick={() => setCount(count - 1)}>Decrement</button>
        <button style={{ width: '160px', padding: '8px', fontSize: '1rem' }} onClick={handleWelcome}>Say welcome</button>
        <button style={{ width: '160px', padding: '8px', fontSize: '1rem' }} onClick={handleClick}>Click on me</button>
      </div>

      <h2 style={{ color: 'green', marginTop: '40px', fontSize: '2.5rem', fontWeight: 'bold', marginBottom: '30px' }}>
        Currency Convertor!!!
      </h2>

      <form onSubmit={handleSubmit}>
        <div style={{ marginBottom: '16px', display: 'flex', alignItems: 'center' }}>
          <label style={{ width: '100px' }}>
            Amount:
          </label>
          <input
            type="text"
            value={amount}
            onChange={(e) => setAmount(e.target.value)}
            style={{ marginBottom: '0', width: '220px', height: '32px', fontSize: '1rem', padding: '4px' }}
          />
        </div>
        <div style={{ marginBottom: '16px', display: 'flex', alignItems: 'center' }}>
          <label style={{ width: '100px' }}>
            Currency:
          </label>
          <textarea
            value={currency}
            onChange={(e) => setCurrency(e.target.value)}
            rows="1"
            cols="20"
            style={{ marginBottom: '0', width: '220px', height: '32px', fontSize: '1rem', padding: '4px', resize: 'none' }}
          />
        </div>
        <button type="submit" style={{ width: '100px', height: '32px', fontSize: '1rem' }}>Submit</button>
      </form>
    </div>
  );
}

export default App;
