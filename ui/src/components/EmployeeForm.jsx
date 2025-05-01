export default function EmployeeForm({employee, departments, employees, usStates, onChange, onSubmit}) {
    
    if (!employee) return <div>Loading...</div>
   
    return(
        <>
            <form className="flex flex-col items-center"
            onSubmit={onSubmit}>
                <label className="mt-[50px] mb-[20px]">Enter the employee information below:</label>
                <div className="w-[240px]">
                    <input className="mb-[30px] mr-[10px]"
                    type="checkbox" name="Active"
                    checked={employee.Active}
                    onChange={onChange}/>
                    <label htmlFor="active">Active</label>
                </div>
                <div className="flex flex-col items-center">
                    <input className="w-[240px] px-[20px] py-[12px] mb-[20px] border rounded-sm"
                    type="text" name="FirstName"
                    placeholder="First name"
                    value={employee.FirstName}
                    onChange={onChange}/>
                    <input className="w-[240px] px-[20px] py-[12px] mb-[20px] border rounded-sm"
                    type="text" name="LastName"
                    placeholder="Last name"
                    value={employee.LastName}
                    onChange={onChange}/>
                </div>
                <input className="w-[240px] px-[20px] py-[12px] mb-[20px] border rounded-sm"
                type="text" name="EmployeeNumber"
                placeholder="Employee number"
                value={employee.EmployeeNumber}
                onChange={onChange}/>
                <select className="w-[240px] px-[20px] py-[12px] mb-[20px] border rounded-sm"
                name="DepartmentId"
                value={employee.DepartmentId}
                onChange={onChange}>
                    <option id="no-selection" value="">Select a department...</option>
                    {departments && departments.map((department,index)=>(
                        <option 
                        key={index} 
                        value={department.Id}>
                            {department.Name}
                        </option>
                    ))}
                </select>
                <input className="w-[240px] px-[20px] py-[12px] mb-[20px] border rounded-sm"
                type="text" name="Title"
                placeholder="Title"
                value={employee.Title}
                onChange={onChange}/>
                <select className="w-[240px] px-[20px] py-[12px] mb-[30px] border rounded-sm"
                name="ManagerId"
                value={employee.ManagerId || ""}
                onChange={onChange}>
                    <option id="no-selection" value="">Select manager...</option>
                    {employees && employees.map((manager,index)=>(
                        <option 
                        key={index} 
                        value={manager.Id}>
                            {/*Employee name will be written in format: (last name, first name)*/}
                            {`${manager.LastName}, ${manager.FirstName}`}
                        </option>
                    ))}
                </select>
                <label className="mb-[15px]">Address:</label>
                <input className="w-[240px] px-[20px] py-[12px] mb-[20px] border rounded-sm"
                type="text" name="HomeAddress.Street1"
                placeholder="Street 1"
                value={employee.HomeAddress.Street1}
                onChange={onChange}
                />
                <input className="w-[240px] px-[20px] py-[12px] mb-[20px] border rounded-sm"
                type="text" name="HomeAddress.Street2"
                placeholder="Street 2 (optional)"
                value={employee.HomeAddress.Street2}
                onChange={onChange}
                />
                <input className="w-[240px] px-[20px] py-[12px] mb-[20px] border rounded-sm"
                type="text"name="HomeAddress.City"
                placeholder="City"
                value={employee.HomeAddress.City}
                onChange={onChange}/>
                <div className="flex">
                    <select className="w-[110px] px-[20px] py-[12px] mb-[20px] mr-[20px] border rounded-sm"
                    name="HomeAddress.UsStateId"
                    value={employee.HomeAddress.UsStateId}
                    onChange={onChange}
                    >
                        <option id="no-selection" value=''>State...</option>
                        {usStates && usStates.map((usState,index)=>(
                            <option 
                            key={index} 
                            value={usState.Id}>
                                {usState.Initials}
                            </option>
                        ))}
                    </select>
                    <input className="w-[110px] px-[20px] py-[12px] mb-[20px] border rounded-sm"
                    type="text"name="HomeAddress.Zip"
                    placeholder="Zip"
                    value={employee.HomeAddress.Zip}
                    onChange={onChange}
                    />
                </div>
                <div className="flex flex-col">
                    <label className="mb-[10px]"
                    htmlFor="notes">Notes</label>
                    <textarea className="w-[360px] px-[20px] py-[12px] mb-[20px] border rounded-sm resize-none" 
                    name="Notes" rows="6"
                    value={employee.Notes? employee.Notes : ""}
                    onChange={onChange}></textarea>
                </div>
                <button className="w-[125px] py-[12px] border rounded-sm bg-[#014880] text-white"
                >Save</button>
            </form>
        </>
    )
}