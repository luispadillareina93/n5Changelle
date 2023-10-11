import React from 'react';
import { usePermissionsTypeList } from '../hooks/usePermissionTypeList';
import { Select, MenuItem, InputLabel, FormControl } from '@mui/material';

const PermissionTypeSelect = ({ field }) => {

  const { permissionsType, loading } = usePermissionsTypeList();

  return (
    <FormControl fullWidth required>
      <InputLabel >Tipo Permiso</InputLabel>
      <Select name={field.name} value={field.value} onChange={field.onChange}  >
        {loading ? (<MenuItem disabled>Loading...</MenuItem>) :
          (permissionsType.map((option) => (
            <MenuItem key={option.id} value={option.id}  aria-label={option.descripcion} >
              {option.descripcion}
            </MenuItem>
          )))}
      </Select>
    </FormControl>

  );
};

export default PermissionTypeSelect;
