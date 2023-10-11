import React from 'react';
import { Route, Routes as ReactRoutes } from 'react-router-dom';

import LoadPermissionsPage from './pages/LoadPermissionsPage';
import AddPermissionPage from './pages/AddPermissionPage';
import EditPermissionPage from './pages/EditPermissionPage';


function Routes() {
    return (
        <ReactRoutes>
            <Route  path="/" element={<LoadPermissionsPage />} />
            <Route  path="/add-permission" element={<AddPermissionPage />} />
            <Route  path="/edit-permission"  element={<EditPermissionPage />} />

        </ReactRoutes>

    );
}

export default Routes;
