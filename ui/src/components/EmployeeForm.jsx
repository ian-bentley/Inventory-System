export default function EmployeeForm() {
    return(
        <>
            <form>
                <label>Enter the information below:</label>
                <div>
                    <input type="text" id="first-name" name="firstName"/>
                    <input type="text" id="last-name" name="lastName"/>
                </div>
                <input type="text" id="employee-id" name="EmployeeId"/>
                <select id="department" name="department">
                    <option id="no-selection" value=''>Select an option...</option>
                </select>
                <input type="text" id="title" name="title"/>
                <select id="manager" name="manager">
                    <option id="no-selection" value=''>Select an option...</option>
                </select>
                <label>Address:</label>
                <input type="text" id="address-1" name="address1"/>
                <input type="text" id="address-2" name="address2"/>
                <input type="text" id="city" name="city"/>
                <select id="state" name="state">
                    <option id="no-selection" value=''>...</option>
                </select>
                <input type="text" id="zip" name="zip"/>
                <button id="save">Save</button>
            </form>
        </>
    )
}