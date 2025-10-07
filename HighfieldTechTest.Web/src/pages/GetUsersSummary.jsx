import React, { useEffect, useState } from 'react';
import UsersTable from '../components/UsersTable';
import AgesTable from '../components/AgesTable';
import ColoursTable from '../components/ColoursTable';
import { API_ENDPOINTS } from '../config/api';
import '../styles/GetUsersSummary.css';

const TABS = [
    { key: 'users', label: 'Users' },
    { key: 'ages', label: 'Ages (+20)' },
    { key: 'colours', label: 'Colour Statistics' }
];

const GetUsersSummary = () => {
    const [users, setUsers] = useState([]);
    const [ages, setAges] = useState([]);
    const [topColours, setTopColours] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [activeTab, setActiveTab] = useState('users');

    useEffect(() => {
        fetch(API_ENDPOINTS.USERS_SUMMARY)
            .then((response) => {
                if (!response.ok) {
                    throw new Error('Failed to fetch users summary');
                }
                return response.json();
            })
            .then((data) => {
                setUsers(data.users || []);
                setAges(data.ages || []);
                setTopColours(data.topColours || []);
                setLoading(false);
            })
            .catch((err) => {
                setError(err.message);
                setLoading(false);
            });
    }, []);

    return (
        <div>
            <h1>Highfield Qualification Users</h1>
            <h2>Summary</h2>
            
            {loading && <div className="status-message loading">Loading users summary...</div>}
            {error && <div className="status-message error">Error: {error}</div>}
            {!loading && !error && !users.length && <div className="status-message no-data">No users found.</div>}
            
            {!loading && !error && users.length > 0 && (
                <>
                    <div className="tabs-container">
                        {TABS.map(tab => (
                            <button
                                key={tab.key}
                                onClick={() => setActiveTab(tab.key)}
                                className={`tab-btn${activeTab === tab.key ? ' active' : ''}`}
                            >
                                {tab.label}
                            </button>
                        ))}
                    </div>

                    <div className="centered-table">
                        {activeTab === 'users' && <UsersTable users={users} />}
                        {activeTab === 'ages' && <AgesTable ages={ages} />}
                        {activeTab === 'colours' && <ColoursTable topColours={topColours} />}
                    </div>
                </>
            )}
        </div>
    );
};

export default GetUsersSummary;