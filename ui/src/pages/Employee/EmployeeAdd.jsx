import { useEffect, useState } from "react";
import config from "../../../ui.config.json";
import { useNavigate } from "react-router-dom";
import EmployeeForm from '../../components/EmployeeForm'

export default function EmployeeAdd() {
    // Employee data
    const [employee, setEmployee] = useState({
        Active: true,
        FirstName: "",
        LastName: "",
        EmployeeNumber: "",
        DepartmentId: "",
        Title: "",
        ManagerId: "",
        HomeAddress: {
            Street1: "",
            Street2: "",
            City: "",
            UsStateId: "",
            Zip: ""
        },
        Notes: ""
    })

    const [departments, setDepartments] = useState(null)
    const [employees, setEmployees] = useState(null)
    const [usStates, setUsStates] = useState(null)
    const navigate = useNavigate()

    // Get departments
    useEffect(()=> {
        fetch(config.api.url+"api/Employee/GetDepartments", {
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
        fetch(config.api.url+"api/Employee/GetEmployees", {
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
        fetch(config.api.url+"api/Employee/GetUsStates", {
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

    // If the form input changes, update the employee based on the change
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

    // Add employee
    const handleSubmit = async (e) => {
        e.preventDefault()

        fetch(config.api.url+"api/Employee/AddEmployee", {
            method: "POST",
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

                // If unauthorized to edit employee
                if (response.status == 403)
                {
                    navigate("/unauthorizedaction")
                }
            }
            else
            {
                alert("Employee added successfully!")
                navigate("/employee")
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