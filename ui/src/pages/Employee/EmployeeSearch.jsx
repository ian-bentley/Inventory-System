import PageSelector from "../../components/PageSelector";

export default function EmployeeSearch() {
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
                    <div className="table-row">
                        <div className="table-cell">John Smith</div>
                        <div className="table-cell">1023</div>
                        <div className="table-cell">Active</div>
                    </div>
                </div>
            </div>
            <PageSelector/>
            <button id="add-item">Add</button>
        </>
    )
}