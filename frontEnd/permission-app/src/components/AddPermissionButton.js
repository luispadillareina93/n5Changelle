import React from 'react';
import { Button } from '@mui/material';
import '../App.css';


function AddPermissionButton({ onClick }) {
  const buttonStyle = {
    marginLeft: 'auto',
    marginTop: '16px', 
    marginBottom: '16px'
  };
  return (
    <Button variant="contained" color="primary" style={buttonStyle} onClick={onClick} >Añadir Permisos</Button>
  );
}

export default AddPermissionButton;
