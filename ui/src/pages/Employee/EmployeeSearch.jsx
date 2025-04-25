import { useEffect, useState } from "react"
import PageSelector from "../../components/PageSelector"
import config from "../../../ui.config.json"
import { useNavigate } from "react-router-dom"

export default function EmployeeSearch() {
    const [employees, setEmployees] = useState(null)
    const navigate = useNavigate()

    // Get data for employees list
    useEffect(()=> {
        fetch(config.api.url+"api/Employee/GetEmployees", {
            credentials: "include"
          })
        .then (async response => {
            if (!response.ok)
            {
                // If not authenticated
                if (response.status == 401)
                {
                    navigate("/login")
                }
                // If unauthorized to view inventory
                if (response.status == 403)
                {
                    navigate("/unauthorizedpage")
                }
            }
            else
            {
                const data = await response.json()
                setEmployees(data)
            }
        })
    }, [])

    return(
        <>
            <form id="search">
                <input type="text"/>
                <button>Search</button>
            </form>
            <div id="search-results" className="table">
                <div className="table-header-group">
                    <div className="table-row">
                        <div className="table-cell">Name</div>
                        <div className="table-cell">Employee ID</div>
                        <div className="table-cell">Status</div>
                    </div>
                </div>
                <div className="table-row-group">
                    {/*As long as employees is set, create a table row for each employee*/}
                    {employees && employees.map((employee,index)=>(
                        <div className="table-row"
                        key={index}
                        onClick={()=>navigate("/employee/details/"+employee.Id)}
                        >
                            {/* On-click, goes to employee details for this employee */}
                            {/*Employee name will be written in format: (last name, first name)*/}
                            <div className="table-cell">{`${employee.LastName}, ${employee.FirstName}`}</div>
                            <div className="table-cell">{employee.EmployeeNumber}</div>
                            <div className="table-cell">{employee.Active? "Active" : "Disabled"}</div>
                        </div>
                    ))}
                </div>
            </div>
            <PageSelector/>
            {/* Goes to employee add */}
            <button onClick={()=>navigate('/employee/add')}>Add</button>
        </>
    )
}