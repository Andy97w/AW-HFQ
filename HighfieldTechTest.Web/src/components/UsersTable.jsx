import React from 'react';

const UsersTable = ({ users }) => (
  <table>
    <thead>
      <tr>
        <th>UserId</th>
        <th>First Name</th>
        <th>Last Name</th>
        <th>Email</th>
        <th>Date of Birth</th>
        <th>Favourite Colour</th>
      </tr>
    </thead>
    <tbody>
      {users.map(user => (
        <tr key={user.id}>
          <td>{user.id}</td>
          <td>{user.firstName}</td>
          <td>{user.lastName}</td>
          <td>{user.email}</td>
          <td>{user.dob}</td>
          <td>{user.favouriteColour}</td>
        </tr>
      ))}
    </tbody>
  </table>
);

export default UsersTable;