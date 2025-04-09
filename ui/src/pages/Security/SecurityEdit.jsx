export default function SecurityEdit() {
    return(
        <>
            <form>
                <div>
                    <div>
                        <p>Name: John Smith</p>
                        <p>Username: 1023</p>
                        <input type="text" id="password" name="password"/>
                        <input type="text" id="password-confirm" name="passwordConfirm"/>
                    </div>
                    <div>
                        <input type="checkbox" id="active"/>
                        <label htmlFor="active">Active</label>
                    </div>
                </div>
                <div>
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
                </div>
                <div>
                    <label htmlFor="notes">Notes</label>
                    <textarea id="notes"></textarea>
                </div>
                <button id="save">Save</button>
            </form>
        </>
    )
}