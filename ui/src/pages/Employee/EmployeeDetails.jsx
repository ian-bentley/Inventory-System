import { useEffect, useState } from "react"
import config from "../../../ui.config.json";
import { useParams, useNavigate } from "react-router-dom";
import PageSelector from "../../components/PageSelector"

export default function EmployeeDetails() {
    const { id } = useParams()
    const [employee, setEmployee] = useState(null)
    const navigate = useNavigate()

    // Get employee by id parameter
    useEffect(()=> {
        fetch(config.api.url+"api/Employee/GetEmployee?id="+id, {
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

                //If employee with that id doesn't exist
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

    if (!employee) return <div>Loading...</div>

    return(
        <>
            <form id="employee-details">
                <div>
                    {/*Employee name will be written in format: (first + last)*/}
                    <p>Name: {`${employee.FirstName} ${employee.LastName}`}</p>
                    <p>Department: {employee.Department.Name}</p>
                    <p>Title: {employee.Title}</p>
                    <p>Employee ID: {employee.EmployeeNumber}</p>
                    {/*If employee has a manager, the name will be written in format: (first + last)*/}
                    <p>Manager: {employee.Manager && `${employee.Manager.FirstName} ${employee.Manager.LastName}`}</p>
                    <label>Address:</label>
                    {/* Display street 1 and street 2, if it exists, together */}
                    <p>{`${employee.HomeAddress.Street1} ${employee.HomeAddress.Street2? employee.HomeAddress.Street2: ""}`}</p>
                    {/* Display city, state, and zip in one line */}
                    <p>{`${employee.HomeAddress.City} ${employee.HomeAddress.UsState.Initials} ${employee.HomeAddress.Zip}`}</p>
                    <input type="checkbox" id="active" 
                    checked={employee.Active}/>
                    <label htmlFor="active">Active</label>
                </div>
                {/* Goes to employee edit for this employee */}
                <button onClick={()=>navigate("/employee/edit/"+employee.Id)}>Edit</button>
                <div>
                    <label htmlFor="notes">Notes</label>
                    <textarea id="notes" value={employee.Notes? employee.Notes : ""}></textarea>
                </div>
            </form>
            <div id="assigned-items" className="table">
                <div className="table-header-group">
                    <div className="table-row">
                        <div className="table-cell">Serial Number</div>
                        <div className="table-cell">Type</div>
                        <div className="table-cell">Model</div>
                    </div>
                </div>
                <div className="table-row-group">
                    {employee.Items.map((item,index)=>(
                            <div className="table-row" key={index}>
                            <div className="table-cell">{item.SerialNumber}</div>
                            <div className="table-cell">{item.ItemType.Name}</div>
                            <div className="table-cell">{item.Model}</div>
                        </div>
                    ))}
                </div>
            </div>
            <PageSelector/>
        </>
    )
}