import React from 'react';
import axios from 'axios';

export default class EmployeeList extends React.Component {
    state = {
        employees: []
    }
    componentDidMount() {
        axios.get('https://localhost:7125/api/source/getAllEmployees')
            // https://localhost:7125/api/source/getAllEmployees
            .then(res => {
                const employees = res.data;
                this.setState({ employees });
            })
    }

    render() {
        return (
            <ul>
                {
                    this.state.employees
                        .map(employee =>
                            <li key={employee.id}>ID: {employee.id} Name: {employee.name}</li>
                        )
                }
            </ul>
        )
    }
}
