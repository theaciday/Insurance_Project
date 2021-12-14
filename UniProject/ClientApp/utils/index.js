import { intersection } from 'lodash';
import { useSelector } from 'react-redux';

export function isArrayWithLength(arr) {
    return (Array.isArray(arr) && arr.length)
}

export function getAllowedRoutes(routes) {
    const user = localStorage.getItem('token');
    user = useSelector(state => state.u)
    return routes.filter(({ permissions }) => {
        if (!permissions) return true;
        else if (!isArrayWithLength(permissions)) return true;
        else return intersection(permissions,roles)
    })
}