import React from 'react';
import { render, fireEvent } from '@testing-library/react';
import AddPermissionButton from './AddPermissionButton';

describe('AddPermissionButton', () => {
    
    it('renders the button correctly', () => {
      const { getByText } = render(<AddPermissionButton onClick={() => {}} />);
      const button = getByText('Añadir Permisos');
      expect(button).toBeInTheDocument();
      expect(button).toHaveClass('MuiButton-containedPrimary'); // Verifica la clase CSS del botón
    });
  
    it('calls the onClick function when the button is clicked', () => {
      const onClickMock = jest.fn();
      const { getByText } = render(<AddPermissionButton onClick={onClickMock} />);
      const button = getByText('Añadir Permisos');
  
      fireEvent.click(button);
  
      expect(onClickMock).toHaveBeenCalledTimes(1);
    });
  });
  