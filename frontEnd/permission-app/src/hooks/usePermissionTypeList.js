import { useEffect, useState } from "react";
import { permissionsApi } from '../api/PermissionApi';

export const usePermissionsTypeList = () => {

    const [permissionsType, setPermissionsType] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        getPermissionsType();
        setLoading(false);

    }, [])

    const getPermissionsType = async () => {
        const result = await permissionsApi.get('/PermissionType/GetPermissionsType');
        setPermissionsType(result.data)
        setLoading(false);
    }
    return {
        permissionsType,
        loading
    }
}