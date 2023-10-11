import React, { useState } from 'react';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Button, Pagination } from '@mui/material';
import { useNavigate } from 'react-router-dom';


function PermissionTable({ permissions }) {
    const rowsPerPage = 5; // Número de filas por página
    const [page, setPage] = useState(1);
    const navigate = useNavigate();



    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };
    const handleEditClick = (item) => {
        navigate(`/edit-permission/`,{ state: {item }});
    };
    
    const startIndex = (page - 1) * rowsPerPage;
    const endIndex = startIndex + rowsPerPage;

    return (
        <div>

            <TableContainer component={Paper} >
                <Table className="permission-table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Id</TableCell>
                            <TableCell>Nombre Empleado</TableCell>
                            <TableCell>Apellido Empleado</TableCell>
                            <TableCell>Tipo Permiso</TableCell>
                            <TableCell>Fecha Permiso</TableCell>
                            <TableCell>Acciones</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {permissions.slice(startIndex, endIndex).map((permission) => (
                            <TableRow key={permission.id}>
                                <TableCell>{permission.id}</TableCell>
                                <TableCell>{permission.nombreEmpleado}</TableCell>
                                <TableCell>{permission.apellidoEmpleado}</TableCell>
                                <TableCell>{permission.tipoPermiso}</TableCell>
                                <TableCell>{permission.fechaPermiso}</TableCell>
                                <TableCell>
                                    <Button variant="outlined" color="primary" onClick={() => handleEditClick(permission)}>Editar</Button>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            <Pagination
                count={Math.ceil(permissions.length / rowsPerPage)}
                page={page}
                onChange={handleChangePage}
                color="primary"
                style={{ marginTop: 16, display: 'flex', justifyContent: 'center' }}
            />
        </div>
    );
}

export default PermissionTable;
