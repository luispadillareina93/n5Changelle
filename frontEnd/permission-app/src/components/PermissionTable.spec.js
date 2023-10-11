import React from 'react';
import { render, screen} from '@testing-library/react';
import PermissionTable from './PermissionTable';
import {BrowserRouter} from 'react-router-dom'

// Supongamos que tienes una lista de permisos ficticios para las pruebas
const permissions = [
    {
        id: 1,
        nombreEmpleado: 'John',
        apellidoEmpleado: 'Doe',
        tipoPermiso: 'Vacaciones',
        fechaPermiso: '2023-10-09',
    },
];

test('should render table with permissions data', () => {
    render(<BrowserRouter><PermissionTable permissions={permissions} /></BrowserRouter>);

    // Verifica que los datos de permisos se rendericen en la tabla
    expect(screen.getByText('John')).toBeInTheDocument();
    expect(screen.getByText('Doe')).toBeInTheDocument();
    expect(screen.getByText('Vacaciones')).toBeInTheDocument();
    expect(screen.getByText('2023-10-09')).toBeInTheDocument();
});


