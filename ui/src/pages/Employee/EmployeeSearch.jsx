import { useEffect, useState } from "react"
import PageSelector from "../../components/PageSelector"
import { useNavigate } from "react-router-dom"

export default function EmployeeSearch() {
    const baseUrl = import.meta.env.VITE_API_BASE_URL;
    const [employees, setEmployees] = useState(null)
    const [filteredEmployees, setFilteredEmployees] = useState(null)
    const [searchText, setSearchText] = useState("")
    const navigate = useNavigate()

    // Get data for employees list
    useEffect(()=> {
        fetch(`${baseUrl}api/Employee/GetEmployees`, {
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
                setFilteredEmployees(data)
            }
        })
    }, [])

    // Filter list based on search
    const Search = (e) => {
        // Prevent form from refreshing page
        e.preventDefault()

        setFilteredEmployees(employees.filter(employee =>
            employee.FirstName.toLowerCase().includes(searchText.toLowerCase()) ||
            employee.LastName.toLowerCase().includes(searchText.toLowerCase())))
    }

    return(
        <>
            <form id="search-bar" className="mt-[40px] ml-[30px]">
                <input className="w-[240px] px-[20px] py-[12px] mb-[20px] mr-[20px] border rounded-sm"
                type="text"
                placeholder="Enter a name"
                value={searchText}
                onChange={(e)=>setSearchText(e.target.value)}/>
                <button className="w-[125px] py-[12px] border rounded-sm bg-[#014880] text-white"
                onClick={(e)=>Search(e)}>Search</button>
            </form>

            {/* Show loading if employees have not been fetched yet */}
            {!employees && <p className="mx-[30px]">Loading...</p>}

            {/* Show only when employees have been fetched */}
            {employees && <div id="search-results" className="m-auto max-w-[416px] mt-[40px]">
                <div className="table">
                    <div className="table-header-group">
                        <div className="table-row">
                            <div className="table-cell w-[235px]">Name</div>
                            <div className="table-cell w-[110px]">Employee ID</div>
                            <div className="table-cell w-[70px]">Status</div>
                        </div>
                    </div>
                </div>
                <div className="table border mb-[20px]">
                    <div className="table-row-group">
                        {/*As long as employees is set, create a table row for each employee*/}
                        {filteredEmployees && filteredEmployees.map((employee,index)=>(
                            <div className="table-row hover:bg-[#e0dcdc]"
                            key={index}
                            onClick={()=>navigate("/employee/details/"+employee.Id)}
                            >
                                {/* On-click, goes to employee details for this employee */}
                                {/*Employee name will be written in format: (last name, first name)*/}
                                <div className="table-cell w-[235px]">{`${employee.LastName}, ${employee.FirstName}`}</div>
                                <div className="table-cell w-[110px]">{employee.EmployeeNumber}</div>
                                <div className="table-cell w-[70px]">{employee.Active? "Active" : "Disabled"}</div>
                            </div>
                        ))}
                    </div>
                </div>
                <div className="flex justify-between">
                    <PageSelector/>
                    {/* Goes to employee add */}
                    <button 
                    className="ml-[40px] w-[100px] py-[12px] border rounded-sm bg-[#014880] text-white"
                    onClick={()=>navigate('/employee/add')}>Add</button>
                </div>
            </div>}
        </>
    )
}