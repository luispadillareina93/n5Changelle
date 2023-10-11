import React  from 'react';
import { useFormik, Field, Form, Formik } from 'formik';
import * as Yup from 'yup';
import PermissionTypeSelect from './PermissionTypeSelect';
import moment from 'moment'
import {
    Button,
    TextField,
} from '@mui/material';

const validationSchema = Yup.object({
    nombreEmpleado: Yup.string().required('Campo requerido'),
    apellidoEmpleado: Yup.string().required('Campo requerido'),
    tipoPermisoId: Yup.string().required('Campo requerido'),
});

const PermissionForm = ({ onSubmit ,data,isEdit}) => {

    const inputStyle = {
        marginTop: '16px',
        marginBottom: '16px',
    };

    const formik = useFormik({});

    return (
        <Formik
            initialValues={{
                nombreEmpleado: isEdit?data.nombreEmpleado:'',
                apellidoEmpleado: isEdit?data.apellidoEmpleado:'',
                tipoPermisoId: isEdit?data.tipoPermisoId:'',
                fechaPermiso: isEdit?moment(data.fechaPermiso).format("YYYY-MM-DD"):'',
            }}
            validationSchema={validationSchema}
            onSubmit={onSubmit}
        >
            {({ handleSubmit}) => (
                <Form onSubmit={handleSubmit} data-testid="permissionForm">
                    <Field
                        required
                        fullWidth
                        htmlFor="nombreEmpleado" 
                        id="nombreEmpleado"
                        name="nombreEmpleado"
                        label="Nombre Empleado"
                        variant="outlined"
                        as={TextField}
                        data-testid="nombreEmpleado"
                        
                    />
                    <Field
                        required
                        fullWidth
                        htmlFor="apellidoEmpleado" 
                        id="apellidoEmpleado"
                        name="apellidoEmpleado"
                        label="Apellido Empleado"
                        variant="outlined"
                        as={TextField}
                        style={inputStyle}
                        error={formik.touched.apellidoEmpleado && Boolean(formik.errors.apellidoEmpleado)}
                        helperText={formik.touched.apellidoEmpleado && formik.errors.apellidoEmpleado}
                        data-testid="apellidoEmpleado"

                    />

                    <Field
                        required
                        htmlFor="tipoPermisoId" 
                        name="tipoPermisoId"
                        component={PermissionTypeSelect}
                        style={inputStyle}
                    />
                    <Field
                        required
                        fullWidth
                        htmlFor="fechaPermiso" 
                        id="fechaPermiso"
                        name="fechaPermiso"
                        as={TextField}
                        label="Fecha Permiso"
                        variant="outlined"
                        type="date"
                        style={inputStyle}
                        InputLabelProps={{
                            shrink: true,
                        }}
                        data-testid="fechaPermiso"

                    />
                    <Button type="submit"  variant="contained" color="primary" style={inputStyle}>
                        Enviar
                    </Button>
                </Form>
            )}
        </Formik>
    );
};

export default PermissionForm;
