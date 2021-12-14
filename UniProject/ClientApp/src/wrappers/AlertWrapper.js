import { useSelector } from 'react-redux';
import React from 'react'

const AlertWrapper = () => {
    const alert = useSelector(state => state.alert);
    return alert.message ?
        <div className={`alert ${alert.type}`}>{alert.message}</div>
        : null

}

export default AlertWrapper