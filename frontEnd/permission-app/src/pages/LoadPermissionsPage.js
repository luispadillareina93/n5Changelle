import React from 'react';
import { useNavigate } from 'react-router-dom';
import PermissionTable from '../components/PermissionTable';
import AddPermissionButton from '../components/AddPermissionButton';
import { usePermissionsList } from '../hooks/usePermissionsList';
import '../App.css';

function LoadPermissionPage() {

  const { permissions } = usePermissionsList();
  const navigate = useNavigate();

  const handleAddPermissionClick = () => {
    navigate('/add-permission');
  };

  return (
    <div className='container'>
      <h1>Permisos</h1>
      <AddPermissionButton onClick={handleAddPermissionClick} />
      <PermissionTable  permissions={permissions} />
    </div>
  );
}

export default LoadPermissionPage;
