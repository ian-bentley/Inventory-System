export default function InventoryDetails() {
    return(
        <>
            <section id="item-details">
                <form>
                    <div>
                        <p>Serial Number: DX37HT2</p>
                        <p>Type: Computer</p>
                        <p>Model: Dell Latitude 3390</p>
                        <input type="checkbox" id="active"/>
                        <label htmlFor="active">Active</label>
                    </div>
                    <button id="edit">Edit</button>
                    <div>
                        <label htmlFor="notes">Notes</label>
                        <textarea id="notes"></textarea>
                    </div>
                </form>
            </section>
            <section id="assignment">
                <form>
                    <p>John Smith</p>
                    <div>
                        <button id="assign">Assign</button>
                        <button id="return">Return</button>
                    </div>
                </form>
                <div id="assignment-history" className="table">
                    <div className="table-header-group">
                        <div className="table-row">
                            <div className="table-cell">Status</div>
                            <div className="table-cell">Date</div>
                            <div className="table-cell">Assignee</div>
                            <div className="table-cell">Reason</div>
                        </div>
                    </div>
                    <div className="table-row-group">
                        <div className="table-row">
                            <div className="table-cell">Assigned</div>
                            <div className="table-cell">3/27/25</div>
                            <div className="table-cell">John Smith</div>
                            <div className="table-cell">New hire</div>
                        </div>
                        <div className="table-row">
                            <div className="table-cell">Returned</div>
                            <div className="table-cell">3/12/25</div>
                            <div className="table-cell">Steve James</div>
                            <div className="table-cell">Termination</div>
                        </div>
                    </div>
                </div>
                <div id="page-selector">
                    &lt;&lt; &lt; 1 ... 7 8 9 ... 20 &gt; &gt;&gt;
                </div>
            </section>
        </>
    )
}