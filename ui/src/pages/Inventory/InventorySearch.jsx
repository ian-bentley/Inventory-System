import PageSelector from "../../components/PageSelector"

export default function InventorySearch() {
    return(
        <>
            <form id="search">
                <input type="text"/>
                <button>Search</button>
            </form>
            <div id="search-results" className="table">
                <div className="table-header-group">
                    <div className="table-row">
                        <div className="table-cell">Serial Number</div>
                        <div className="table-cell">Type</div>
                        <div className="table-cell">Model</div>
                        <div className="table-cell">Status</div>
                        <div className="table-cell">Assigned to</div>
                    </div>
                </div>
                <div className="table-row-group">
                    <div className="table-row">
                        <div className="table-cell">DX37HT2</div>
                        <div className="table-cell">Computer</div>
                        <div className="table-cell">Dell Latitude 3390</div>
                        <div className="table-cell">Active</div>
                        <div className="table-cell">John Smith</div>
                    </div>
                </div>
            </div>
            <PageSelector/>
            <button id="add-item">Add</button>
        </>
    )
}