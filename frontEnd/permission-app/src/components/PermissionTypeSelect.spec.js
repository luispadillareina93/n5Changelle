import React from 'react';
import { render, screen } from '@testing-library/react';
import PermissionTypeSelect from './PermissionTypeSelect';

// Mock del hook usePermissionsTypeList
jest.mock('../hooks/usePermissionTypeList', () => ({
    usePermissionsTypeList: () => ({
        permissionsType: [
            { id: 1, descripcion: 'Admin' },

        ],
        loading: false,
    }),
}));
describe('PermissionTypeSelect', () => {
    
    it('renders the component', () => {
        render(<PermissionTypeSelect field={{ name: 'test', value: '', onChange: () => { } }} />);

        expect(screen.getByText('Tipo Permiso')).toBeInTheDocument();
    });

    it('should display the options list correctly', () => {
        const field = {
            name: 'tipoPermiso',
            value: 1,
            onChange: jest.fn(),
        };

        render(<PermissionTypeSelect field={field} />);

        const selectElement = screen.getByText('Tipo Permiso');
        expect(selectElement).toBeInTheDocument();


        const option1 = screen.getByText('Admin');
        expect(option1).toBeInTheDocument();

    });


});
