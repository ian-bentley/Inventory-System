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

    if (!item) return <div className="mx-[20px] mt-[40px]">Loading...</div>
    
    return(
        <>
            <section id="item-details" className="mt-[40px]">
                <form className="px-[20px] flex flex-wrap">
                    <div className="w-[443px] flex items-start justify-between">
                        <div>
                            <p className="mb-[10px]">Serial Number: {item.SerialNumber}</p>
                            <p className="mb-[10px]">Type: {item.ItemType.Name}</p>
                            <p className="mb-[10px]">Model: {item.Model}</p>
                            <input className="mb-[30px] mr-[10px]"
                            type="checkbox" id="active" 
                            checked={item.Active}/>
                            <label htmlFor="active">Active</label>
                        </div>
                        {/* Goes to inventory edit for this item */}
                        <button className="w-[125px] py-[12px] border rounded-sm bg-[#014880] text-white mr-[10px]"
                        type="button"
                        onClick={()=>navigate("/inventory/edit/"+item.Id)}>Edit</button>
                    </div>
                    <div className="flex flex-col ml-[60px]">
                        <label className="mb-[10px]"
                        htmlFor="notes">Notes</label>
                        <textarea id="notes" className="w-[360px] px-[20px] py-[12px] mb-[20px] border rounded-sm resize-none" 
                        rows="6"
                        value={item.Notes? item.Notes : ""}></textarea>
                    </div>
                </form>
            </section>
            <section id="assignment">
                <form className="flex justify-between items-center px-[20px] mb-[40px] max-w-[480px]">
                    {/* Show assigned to name if it exists */}
                    <p>{`Assigned To: ${item.AssignedTo? `${item.AssignedTo.FirstName} ${item.AssignedTo.LastName}` : ""}`}</p>
                    <div className="flex flex-col">
                        <button id="assign" className="w-[125px] py-[12px] border rounded-sm bg-[#014880] text-white mr-[10px] mb-[20px]"
                        >Assign</button>
                        <button id="return" className="w-[125px] py-[12px] border rounded-sm bg-[#014880] text-white mr-[10px]"
                            >Return</button>
                    </div>
                </form>
                <div id="assignment-history" className="px-[20px]">
                    <div className="table">
                        <div className="table-header-group">
                            <div className="table-row">
                                <div className="table-cell w-[100px]">Status</div>
                                <div className="table-cell w-[90px]">Date</div>
                                <div className="table-cell w-[220px]">Assignee</div>
                                <div className="hidden min-[800px]:table-cell w-[390px]">Reason</div>
                            </div>
                        </div>
                    </div>
                    <div className="table border mb-[20px]">
                        <div className="table-row-group">
                            {item.ItemEvents.map((itemEvent,index)=>(
                                <div className="table-row" key={index}>
                                    <div className="table-cell w-[100px]">{itemEvent.EventType.Name}</div>
                                    {/* DateTime will be truncated after the date */}
                                    <div className="table-cell w-[90px]">{itemEvent.DateTime.split('T')[0]}</div>
                                    {/* Display assigned name in format: (last, first) */}
                                    <div className="table-cell w-[220px]">{`${itemEvent.Employee.LastName}, ${itemEvent.Employee.FirstName}`}</div>
                                    <div className="hidden min-[800px]:table-cell w-[390px]">{itemEvent.Reason}</div>
                                </div>
                            ))}
                        </div>
                    </div>
                </div>
                <PageSelector/>
            </section>
        </>
    )
}