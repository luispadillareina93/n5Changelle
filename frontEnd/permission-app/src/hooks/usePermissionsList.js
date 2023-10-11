import { useEffect, useState } from "react";
import { permissionsApi } from '../api/PermissionApi';


export const usePermissionsList = () => {

    const [permissions, setPermissions] = useState([]);
    useEffect(() => {
        getPermissions();
    }, [])

    const getPermissions = async () => {
        const result = await permissionsApi.get('/Permission/GetPermissions');
        setPermissions(result.data)
    }
    return {
        permissions
    }
}