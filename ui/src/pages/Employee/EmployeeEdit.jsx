import EmployeeForm from "../../components/EmployeeForm";
import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";

export default function EmployeeEdit() {
    const baseUrl = import.meta.env.VITE_API_BASE_URL;
    const { id } = useParams()
    const [employee, setEmployee] = useState(null)
    const [departments, setDepartments] = useState(null)
    const [employees, setEmployees] = useState(null)
    const [usStates, setUsStates] = useState(null)
    const navigate = useNavigate()

    // Get departments
    useEffect(()=> {
        fetch(`${baseUrl}api/Employee/GetDepartments`, {
            credentials: "include"
        })
        .then(async response => {
            if (!response.ok)
                {
                    // If not authenticated
                    if (response.status == 401)
                    {
                        navigate("/login")
                    }
    
                    // If unauthorized to edit employee
                    if (response.status == 403)
                    {
                        navigate("/unauthorizedaction")
                    }
                }
            else
            {
                const data = await response.json()
                setDepartments(data)
            }
        })
    }, [])

    // Get employees
    useEffect(()=> {
        fetch(`${baseUrl}api/Employee/GetEmployees`, {
            credentials: "include"
        })
        .then(async response => {
            if (!response.ok)
                {
                    // If not authenticated
                    if (response.status == 401)
                    {
                        navigate("/login")
                    }
    
                    // If unauthorized to edit employee
                    if (response.status == 403)
                    {
                        navigate("/unauthorizedaction")
                    }
                }
            else
            {
                const data = await response.json()
                setEmployees(data)
            }
        })
    }, [])

    // Get US states
    useEffect(()=> {
        fetch(`${baseUrl}api/Employee/GetUsStates`, {
            credentials: "include"
        })
        .then(async response => {
            if (!response.ok)
                {
                    // If not authenticated
                    if (response.status == 401)
                    {
                        navigate("/login")
                    }
    
                    // If unauthorized to edit employee
                    if (response.status == 403)
                    {
                        navigate("/unauthorizedaction")
                    }
                }
            else
            {
                const data = await response.json()
                setUsStates(data)
            }
        })
    }, [])

    // Get employee by id parameter
    useEffect(()=> {
        fetch(`${baseUrl}api/Employee/GetEmployee?id=${id}`, {
            credentials: "include"
          })
        .then(async response => {
            if (!response.ok)
            {
                // If not authenticated
                if (response.status == 401)
                {
                    navigate("/login")
                }

                // If unauthorized to view employee
                if (response.status == 403)
                {
                    navigate("/unauthorizedpage")
                }

                // If employee with that id doesn't exist
                if (response.status == 404)
                {
                    navigate("/404")
                }
            }
            else
            {
                const data = await response.json()
                setEmployee(data)
            }
        })
    }, [])

    // If a form input changes, update the employee based on the change
    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;

        if (name.includes(".")) {
            const [parentKey, childKey] = name.split(".");
            setEmployee(prev => ({
            ...prev,
            [parentKey]: {
                ...prev[parentKey],
                [childKey]: value
            }
            }))
        } else {
            setEmployee(prev => ({
            ...prev,
            [name]: type == "checkbox"? checked : value
            }))
        }
    }

    // Edit employee
    const handleSubmit = async (e) => {
        e.preventDefault()

        fetch(`${baseUrl}api/Employee/UpdateEmployee`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            credentials: "include",
            body: JSON.stringify(employee)
        })
        .then(async response => {
            if (!response.ok)
            {
                // If not authenticated
                if (response.status == 401)
                {
                    navigate("/login")
                }

                // If unauthorized to edit employees
                if (response.status == 403)
                {
                    navigate("/unauthorizedaction")
                }
            }
            else
            {
                alert("Employee updated successfully!");
                navigate("/employee/details/"+id)
            }
        })
    }

    return(
        <>
            <EmployeeForm
            employee={employee}
            departments={departments}
            employees={employees}
            usStates={usStates}
            onChange={handleChange}
            onSubmit={handleSubmit}
            />
        </>
    )
}