export default function EmployeeForm({employee, departments, employees, usStates, onChange, onSubmit}) {
    
    if (!employee) return <div>Loading...</div>
   
    return(
        <>
            <form onSubmit={onSubmit}>
                <label>Enter the information below:</label>
                <div>
                    <input type="text" name="FirstName"
                    value={employee.FirstName}
                    onChange={onChange}/>
                    <input type="text" name="LastName"
                    value={employee.LastName}
                    onChange={onChange}/>
                </div>
                <input type="text" name="EmployeeNumber"
                value={employee.EmployeeNumber}
                onChange={onChange}/>
                <select name="DepartmentId"
                value={employee.DepartmentId}
                onChange={onChange}>
                    <option id="no-selection" value="">Select an option...</option>
                    {departments && departments.map((department,index)=>(
                        <option 
                        key={index} 
                        value={department.Id}>
                            {department.Name}
                        </option>
                    ))}
                </select>
                <input type="text" name="Title"
                value={employee.Title}
                onChange={onChange}/>
                <select name="ManagerId"
                value={employee.ManagerId || ""}
                onChange={onChange}>
                    <option id="no-selection" value="">Select an option...</option>
                    {employees && employees.map((manager,index)=>(
                        <option 
                        key={index} 
                        value={manager.Id}>
                            {/*Employee name will be written in format: (last name, first name)*/}
                            {`${manager.LastName}, ${manager.FirstName}`}
                        </option>
                    ))}
                </select>
                <label>Address:</label>
                <input type="text" name="HomeAddress.Street1"
                value={employee.HomeAddress.Street1}
                onChange={onChange}
                />
                <input type="text" name="HomeAddress.Street2"
                value={employee.HomeAddress.Street2}
                onChange={onChange}
                />
                <input type="text"name="HomeAddress.City"
                value={employee.HomeAddress.City}
                onChange={onChange}/>
                <select name="HomeAddress.UsStateId"
                value={employee.HomeAddress.UsStateId}
                onChange={onChange}
                >
                    <option id="no-selection" value=''>...</option>
                    {usStates && usStates.map((usState,index)=>(
                        <option 
                        key={index} 
                        value={usState.Id}>
                            {usState.Initials}
                        </option>
                    ))}
                </select>
                <input type="text"name="HomeAddress.Zip"
                value={employee.HomeAddress.Zip}
                onChange={onChange}
                />
                <button>Save</button>
            </form>
        </>
    )
}