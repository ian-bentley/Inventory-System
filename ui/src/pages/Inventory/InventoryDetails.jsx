import { useEffect, useState } from "react"
import config from "../../../ui.config.json";
import { useParams, useNavigate } from "react-router-dom";
import PageSelector from "../../components/PageSelector"

export default function InventoryDetails() {
    const { id } = useParams()
    const [item, setItem] = useState(null)
    const navigate = useNavigate()

    // Get item by id parameter
    useEffect(()=> {
        fetch(config.api.url+"api/Inventory/GetItem?id="+id, {
            credentials: "include"
          })
        .then(async response => {
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

                //If item with that id doesn't exist
                if (response.status == 404)
                {
                    navigate("/404")
                }
            }
            else
            {
                const data = await response.json()
                setItem(data)
            }
        })
    }, [])

    if (!item) return <div>Loading...</div>
    
    return(
        <>
            <section id="item-details">
                <form>
                    <div>
                        <p>Serial Number: {item.SerialNumber}</p>
                        <p>Type: {item.ItemType.Name}</p>
                        <p>Model: {item.Model}</p>
                        <input type="checkbox" id="active" 
                        checked={item.Active}/>
                        <label htmlFor="active">Active</label>
                    </div>
                    {/* Goes to inventory edit for this item */}
                    <button onClick={()=>navigate("/inventory/edit/"+item.Id)}>Edit</button>
                    <div>
                        <label htmlFor="notes">Notes</label>
                        <textarea id="notes" value={item.Notes? item.Notes : ""}></textarea>
                    </div>
                </form>
            </section>
            <section id="assignment">
                <form>
                    {/* Show assigned to name if it exists */}
                    <p>{`Assigned To: ${item.AssignedTo? `${item.AssignedTo.FirstName} ${item.AssignedTo.LastName}` : ""}`}</p>
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
                        {item.ItemEvents.map((itemEvent,index)=>(
                            <div className="table-row" key={index}>
                                <div className="table-cell">{itemEvent.EventType.Name}</div>
                                <div className="table-cell">{itemEvent.DateTime}</div>
                                {/* Display assigned name in format: (last, first) */}
                                <div className="table-cell">{`${itemEvent.Employee.LastName} ${itemEvent.Employee.FirstName}`}</div>
                                <div className="table-cell">{itemEvent.Reason}</div>
                            </div>
                        ))}
                    </div>
                </div>
                <PageSelector/>
            </section>
        </>
    )
}