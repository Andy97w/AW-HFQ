import React from 'react';

const AgesTable = ({ ages }) => (
  <table>
    <thead>
      <tr>
        <th>UserId</th>
        <th>Original Age</th>
        <th>Age + 20</th>
      </tr>
    </thead>
    <tbody>
      {ages.map(age => (
        <tr key={age.userId}>
          <td>{age.userId}</td>
          <td>{age.originalAge}</td>
          <td>{age.agePlusTwenty}</td>
        </tr>
      ))}
    </tbody>
  </table>
);

export default AgesTable;