import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import PermissionForm from './PermissionForm';

const mockOnSubmit = jest.fn((nombreEmpleado,apellidoEmpleado,fechaPermiso)=>{
    return Promise.resolve({nombreEmpleado,apellidoEmpleado,fechaPermiso})
});

describe('PermissionForm', () => {
    it('should be render the form correctly', () => {
        render(<PermissionForm onSubmit={mockOnSubmit} isEdit={false} />);

        expect(screen.getByText('Nombre Empleado')).toBeInTheDocument();
        expect(screen.getByText('Apellido Empleado')).toBeInTheDocument();
        expect(screen.getByText('Tipo Permiso')).toBeInTheDocument();
        expect(screen.getByText('Fecha Permiso')).toBeInTheDocument();

        expect(screen.getByText('Enviar')).toBeInTheDocument();
    });

    it('should handle changes to the form', async () => {
        render(<PermissionForm onSubmit={mockOnSubmit} isEdit={false} />);

        const nombreEmpleadoInput = screen.getByRole('textbox', { name: /Nombre Empleado/i });

        const apellidoEmpleadoInput = screen.getByRole('textbox', { name: /Apellido Empleado/i });
        const fechaPermisoInput = screen.getByTestId('fechaPermiso').querySelector("input")

        await waitFor(() => {
            fireEvent.change(nombreEmpleadoInput, { target: { value: 'Luis' } });
            fireEvent.change(apellidoEmpleadoInput, { target: { value: 'Padilla' } });
            fireEvent.change(fechaPermisoInput, { target: { value: '2023-10-09' } });

        });

        expect(nombreEmpleadoInput.value).toBe('Luis');
        expect(apellidoEmpleadoInput.value).toBe('Padilla');
        expect(fechaPermisoInput.value).toBe('2023-10-09');

    });

   

});
