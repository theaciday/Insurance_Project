import { useEffect } from "react";
import { Table } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux"
import { useNavigate } from "react-router";



const ListCustomers = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    useEffect(() => {
        dispatch(customerActions.getAll());
    })
    const customers = useSelector((state) => {
        state.customers.data;
    })
    const onProfileClick = (id) => {
        navigate(`profile/${id}`);
    }
    return (
        <Table striped bordered variant="dark">
            {
                <>
                    <thead >
                        <tr>
                            <th>
                                Id:
                            </th>
                            <th>
                                Email:
                            </th>
                            <th>
                                Roles:
                            </th>
                            <th>
                                Created Date:
                            </th>
                            <th>
                                Updated Date:
                            </th>
                            <th>
                                Is Active:
                            </th>
                            <th>
                                Is email confirmed:
                            </th>
                            <th>
                                Profile link:
                            </th>
                        </tr>
                    </thead>
                    <tbody  >
                        {
                            accounts.map((account, index) =>
                                <tr key={index} style={{
                                }}>
                                    <td>{account.id}</td>
                                    <td>{account.email}</td>
                                    <td style={{
                                        'overflowY': 'auto',
                                        'height': 50
                                    }}>
                                        {account.roles.map((role, index) =>
                                            <div key={index}>
                                                {role}
                                            </div>
                                        )}
                                    </td>
                                    <td>
                                        {account.createdDate}
                                    </td>
                                    <td>
                                        {account.updatedDate}
                                    </td>
                                    <td>
                                        <Form.Check type='switch' onChange={(e) => onChangeActivity(account.id, e.target.checked, uri)} id={account.id} style={{ fontSize: 20, }} checked={account.isActive} />
                                    </td>
                                    <td>
                                        {account.emailConfirmed.toString()}
                                    </td>
                                    <td>
                                        <Button onClick={() => onProfileClick(account.id)}>Profile</Button>
                                    </td>
                                </tr>

                            )}
                    </tbody>
                </>
            }
        </Table>
    )
}
export const ListCustomers;