import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux"
import { adminActions } from "../../actions/adminActions";
import ListCustomers from "./ListCustomers";


const Customers = () => {
    const dispatch = useDispatch();


    return (
        <div>List Accounts:
            <ListCustomers />
        </div>

    )
}
export default Customers;