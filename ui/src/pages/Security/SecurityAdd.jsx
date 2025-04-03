export default function SecurityAdd() {
    return(
        <>
            <form>
                <input type="text" id="employee-id" name="employeeId"/>
                <p>Name: John Smith</p>
                <p>Is this valid?</p>
                <input type="text" id="password" name="password"/>
                <input type="text" id="password-confirm" name="passwordConfirm"/>
                <label>Permissions:</label>
                <input type="checkbox" id="view-employees"/>
                <label htmlFor="view-employees">View employees</label>
                <input type="checkbox" id="edit-employees"/>
                <label htmlFor="edit-employees">Edit employees</label>
                <input type="checkbox" id="view-inventory"/>
                <label htmlFor="view-inventory">View inventory</label>
                <input type="checkbox" id="edit-inventory"/>
                <label htmlFor="edit-inventory">Edit inventory</label>
                <input type="checkbox" id="view-security"/>
                <label htmlFor="view-security">View security</label>
                <input type="checkbox" id="edit-security"/>
                <label htmlFor="edit-security">Edit security</label>
                <button id="save">Save</button>
            </form>
        </>
    )
}