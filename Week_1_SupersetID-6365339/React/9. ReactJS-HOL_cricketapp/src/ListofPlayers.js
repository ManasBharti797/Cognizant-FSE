import React from 'react';

const ListofPlayers = () => {
  const players = [
    { name: "Mr. Jack", value: 50 },
    { name: "Mr. Michael", value: 70 },
    { name: "Mr. John", value: 40 },
    { name: "Mr. Ann", value: 61 },
    { name: "Mr. Elisabeth", value: 61 },
    { name: "Mr. Sachin", value: 95 },
    { name: "Mr. Dhoni", value: 100 },
    { name: "Mr. Virat", value: 84 },
    { name: "Mr. Jadeja", value: 64 },
    { name: "Mr. Raina", value: 75 },
    { name: "Mr. Rohit", value: 80 }
  ];

  const lowScorers = players.filter(p => p.value < 70);

  return (
    <div>
      <h2>All Players</h2>
      <ul>
        {players.map((p, index) => (
          <li key={index}>{p.name} - {p.value}</li>
        ))}
      </ul>

      <h3>Players with value below 70</h3>
      <ul>
        {lowScorers.map((p, index) => (
          <li key={index}>{p.name} - {p.value}</li>
        ))}
      </ul>
    </div>
  );
};

export default ListofPlayers;
