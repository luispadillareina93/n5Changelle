
import axios from 'axios';

export const permissionsApi = axios.create({
    baseURL:'https://localhost:44340/api'
});