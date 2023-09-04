import React, { useState, useEffect } from 'react';
import './App.css';

function App() {
    const [users, setUsers] = useState([]);
    const [newUser, setNewUser] = useState({});

    useEffect(() => {
        fetchUsers();
    }, []);

    const fetchUsers = async () => {
        const response = await fetch('/api/User/get-user'); // Assuming your API runs on the same host
        const data = await response.json();
        setUsers(data);
    };

    const handleAddUser = async () => {
        const response = await fetch('/api/User/add-user', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newUser),
        });
        await response.json();
        fetchUsers();
    };

    return (
        <div className="App">
            <h1>User Management App</h1>
            <div>
                <h2>Users</h2>
                <ul>
                    {users.map((user) => (
                        <li key={user.userId}>
                            {user.name} {user.surname} - {user.userName}
                            <br />
                            Password: {user.password}
                            <br />
                            Security Question: {user.securityQuestion}
                            <br />
                            Security Answer: {user.securityAnswer}
                            <br />
                            Date of Register: {new Date(user.dateOfRegister)}
                        </li>
                    ))}
                </ul>
            </div>
            <div>
                <h2>Add User</h2>
                <input
                    type="text"
                    placeholder="Name"
                    onChange={(e) => setNewUser({ ...newUser, name: e.target.value })}
                />
                <input
                    type="text"
                    placeholder="Surname"
                    onChange={(e) => setNewUser({ ...newUser, surname: e.target.value })}
                />
                <input
                    type="text"
                    placeholder="Username"
                    onChange={(e) => setNewUser({ ...newUser, userName: e.target.value })}
                />
                <input
                    type="password"
                    placeholder="Password"
                    onChange={(e) => setNewUser({ ...newUser, password: e.target.value })}
                />
                <input
                    type="text"
                    placeholder="Security Question"
                    onChange={(e) => setNewUser({ ...newUser, securityQuestion: e.target.value })}
                />
                <input
                    type="text"
                    placeholder="Security Answer"
                    onChange={(e) => setNewUser({ ...newUser, securityAnswer: e.target.value })}
                />
                <button onClick={handleAddUser}>Add User</button>
            </div>
        </div>
    );
}

export default App;
