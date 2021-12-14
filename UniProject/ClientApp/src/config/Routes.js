import Roles from './Roles';
import Home from '../components/Home';
import Login from '../components/Login';
import Register from '../components/Register';
import User from '../components/User';
import Admin from '../components/Admin';
import Cart from '../components/Cart';

export default [
    {
        component: Home,
        path: '/',
        exact: true,
    },
    {
        component: Login,
        isAuth: true,
        path: '/login',
        exact: true,
    },
    {
        component: Cart,
        path: '/cart',
        exact: true,
        roles: [
            Roles.ADMIN,
            Roles.USER
        ],
    },
    {
        component: Register,
        path: '/register',
        isAuth: true,
    },
    {
        component: User,
        path: '/user',
        roles: [
            Roles.ADMIN
        ],
    },
    {
        component: Admin,
        path: '/Admin',
        roles: [
            Roles.ADMIN
        ],
    },
]