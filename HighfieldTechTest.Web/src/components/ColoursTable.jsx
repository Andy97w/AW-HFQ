import React from 'react';

const ColoursTable = ({ topColours }) => (
  <table>
    <thead>
      <tr>
        <th>Colour</th>
        <th>Count</th>
      </tr>
    </thead>
    <tbody>
      {topColours.map(colour => (
        <tr key={colour.colour}>
          <td>{colour.colour}</td>
          <td>{colour.count}</td>
        </tr>
      ))}
    </tbody>
  </table>
);

export default ColoursTable;