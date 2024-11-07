import React from 'react';
import PermissionsForm from '../components/PermissionForm';
import { permissionsApi } from '../api/PermissionApi';
import { useNavigate } from 'react-router-dom';

import '../App.css';

function AddPermission() {
  const navigate = useNavigate();

  const handleSubmit = async (values, { resetForm }) => {
    const result = await permissionsApi.post('/Permission/RequestPermissions', values);
    console.log(result)
    if (result.status === 200)
      navigate('/');
  };

  return (
    <div className='container'>
      <h1>Formulario de Permiso Test</h1>
      <PermissionsForm onSubmit={handleSubmit} />
    </div>
  );
}

export default AddPermission;
