import React from 'react';
import './App.css';
import officeImage from './image.jpg'; // Place image in src folder

function App() {
  const offices = [
    { Name: "DBS", Rent: 50000, Address: "Chennai" },
    { Name: "WeWork", Rent: 70000, Address: "Bangalore" },
    { Name: "SmartWorks", Rent: 55000, Address: "Hyderabad" }
  ];

  return (
    <div className="App">
      <h1>Office Space , at Affordable Range</h1>

      {offices.map((office, index) => {
        const rentColor = office.Rent <= 60000 ? 'textRed' : 'textGreen';

        return (
          <div key={index}>
            <img src={officeImage} width="15%" height="15%" alt="Office Space" />
            <h1>Name: {office.Name}</h1>
            <h3 className={rentColor}>Rent: Rs. {office.Rent}</h3>
            <h3>Address: {office.Address}</h3>
            <hr />
          </div>
        );
      })}
    </div>
  );
}

export default App;
