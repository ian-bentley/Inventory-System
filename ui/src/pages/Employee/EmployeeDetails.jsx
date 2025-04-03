export default function EmployeeDetails() {
    return(
        <>
            <form>
                <div>
                    <p>Name: John Smith</p>
                    <p>Department: Claims</p>
                    <p>Title: Claims Examiner</p>
                    <p>Employee ID: 1023</p>
                    <p>Manager: James Anderson</p>
                    <label>Address:</label>
                    <p>301 S Fake Ave Ste 1000</p>
                    <p>San Diego, CA 91950</p>
                    <input type="checkbox" id="active"/>
                    <label htmlFor="active">Active</label>
                </div>
                <div>
                    <button id="edit">Edit</button>
                    <button id="delete">Delete</button>
                </div>
                <div>
                    <label htmlFor="notes">Notes</label>
                    <textarea id="notes"></textarea>
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
                    <div className="table-row">
                        <div className="table-cell">DX37HT2</div>
                        <div className="table-cell">Computer</div>
                        <div className="table-cell">Dell Latitude 3390</div>
                    </div>
                </div>
            </div>
            <div id="page-selector">
                &lt;&lt; &lt; 1 ... 7 8 9 ... 20 &gt; &gt;&gt;
            </div>
        </>
    )
}