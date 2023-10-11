import React from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import PermissionsForm from '../components/PermissionForm';
import { permissionsApi } from '../api/PermissionApi';

import '../App.css';

function EditPermissionPage() {
    const navigate = useNavigate();

    const { state } = useLocation();
    const { item } = state;
    const handleSubmit = async (values, { resetForm }) => {
        let data = values;
        data.id = item.id;
        
        const result = await permissionsApi.put('/Permission/ModifyPermissions', data);

        if (result.status === 200)
            navigate('/');

    };

    return (
        <div className='container'>
            <h1>Formulario de Permiso</h1>
            <PermissionsForm onSubmit={handleSubmit} data={item} isEdit={true} />
        </div>
    );
}

export default EditPermissionPage;
