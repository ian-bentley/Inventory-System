import { useEffect, useState } from "react"
import { useParams, useNavigate } from "react-router-dom";
import PageSelector from "../../components/PageSelector"

export default function EmployeeDetails() {
    const baseUrl = import.meta.env.VITE_API_BASE_URL;
    const { id } = useParams()
    const [employee, setEmployee] = useState(null)
    const navigate = useNavigate()

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

    if (!employee) return <div className="mx-[20px] mt-[40px]">Loading...</div>

    return(
        <>
            <section id="employee-details" className="mt-[40px] mb-[40px]" >
                <form className="px-[20px] flex flex-wrap">
                    <div className="w-[443px] flex items-start justify-between">
                        <div>
                            {/*Employee name will be written in format: (first + last)*/}
                            <p className="mb-[10px]">Name: {`${employee.FirstName} ${employee.LastName}`}</p>
                            <p className="mb-[10px]">Department: {employee.Department.Name}</p>
                            <p className="mb-[10px]">Title: {employee.Title}</p>
                            <p className="mb-[10px]">Employee ID: {employee.EmployeeNumber}</p>
                            {/*If employee has a manager, the name will be written in format: (first + last)*/}
                            <p className="mb-[10px]">Manager: {employee.Manager && `${employee.Manager.FirstName} ${employee.Manager.LastName}`}</p>
                            <div className="flex">
                                <label className="mr-[10px]">Address:</label>
                                <div>
                                    {/* Display street 1 and street 2, if it exists, together */}
                                    <p>{`${employee.HomeAddress.Street1} ${employee.HomeAddress.Street2? employee.HomeAddress.Street2: ""}`}</p>
                                    {/* Display city, state, and zip in one line */}
                                    <p className="mb-[10px]">{`${employee.HomeAddress.City} ${employee.HomeAddress.UsState.Initials} ${employee.HomeAddress.Zip}`}</p>
                                </div>
                            </div>
                            <input className="mb-[30px] mr-[10px]"
                            type="checkbox" name="Active"
                            checked={employee.Active}
                            readOnly/>
                            <label htmlFor="active">Active</label>
                        </div>
                        {/* Goes to employee edit for this employee */}
                        <button className="w-[125px] py-[12px] border rounded-sm bg-[#014880] text-white mr-[10px]"
                        type="button"
                        onClick={()=>navigate("/employee/edit/"+employee.Id)}>Edit</button>
                    </div>
                    <div className="flex flex-col ml-[60px]">
                        <label htmlFor="notes">Notes</label>
                        <textarea className="w-[360px] px-[20px] py-[12px] mb-[20px] border rounded-sm resize-none" 
                        name="Notes" rows="6"
                        value={employee.Notes? employee.Notes : ""}
                        readOnly></textarea>
                    </div>
                </form>
            </section>
            <section id="assigned-items" className="px-[20px]">
                <h1 className="mb-[10px]">Assigned Items</h1>
                <div id="assigned-items" className="table">
                    <div className="table-header-group">
                        <div className="table-row">
                            <div className="table-cell w-[130px]">Serial Number</div>
                            <div className="table-cell w-[100px]">Type</div>
                            <div className="table-cell w-[170px]">Model</div>
                        </div>
                    </div>
                </div>
                <div className="table border mb-[20px]">
                    <div className="table-row-group">
                        {employee.Items.map((item,index)=>(
                            <div className="table-row" key={index}>
                                <div className="table-cell w-[130px]">{item.SerialNumber}</div>
                                <div className="table-cell w-[100px]">{item.ItemType.Name}</div>
                                <div className="table-cell w-[170px]">{item.Model}</div>
                            </div>
                        ))}
                    </div>
                </div>
                {employee.Items.length == 0 && <p className="mb-[10px]">No assigned items</p>}
                <PageSelector/>
            </section>
        </>
    )
}