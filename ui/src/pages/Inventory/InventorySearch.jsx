import { useEffect, useState } from "react"
import PageSelector from "../../components/PageSelector"
import config from "../../../ui.config.json"
import { useNavigate } from "react-router-dom"

export default function InventorySearch() {
    const [items, setItems] = useState(null)
    const navigate = useNavigate()
    
    // Get data for items list
    useEffect(()=> {
        fetch(config.api.url+"api/Inventory/GetItems", {
            credentials: "include"
          })
        .then (async response => {
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
            }
            else
            {
                const data = await response.json()
                setItems(data)
            }
        })
    }, [])

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
                    {/*As long as items is set, create a table row for each item*/}
                    {items && items.map((item,index)=>(
                        <div className="table-row"
                        key={index}
                        onClick={()=>navigate("/inventory/details/"+item.Id)}
                        >
                            {/* On-click, goes to item details for this item */}
                            <div className="table-cell">{item.SerialNumber}</div>
                            <div className="table-cell">{item.ItemType.Name}</div>
                            <div className="table-cell">{item.Model}</div>
                            <div className="table-cell">{item.Active? "Active" : "Disabled"}</div>
                            {/*Assigned employee name will be written in format: (last name, first name)*/}
                            {/*Is not displayed if there is no assigned employee*/}
                            {item.AssignedTo && <div className="table-cell">{`${item.AssignedTo.LastName}, ${item.AssignedTo.FirstName}`}</div>}
                        </div>
                    ))}
                </div>
            </div>
            <PageSelector/>
            {/* Goes to inventory add */}
            <button onClick={()=>navigate('/inventory/add')}>Add</button>
        </>
    )
}