import { useEffect, useState } from "react"
import PageSelector from "../../components/PageSelector"
import config from "../../../ui.config.json"
import { useNavigate } from "react-router-dom"

export default function InventorySearch() {
    const [items, setItems] = useState(null)
    const [filteredItems, setFilteredItems] = useState(null)
    const [searchText, setSearchText] = useState("")
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
                setFilteredItems(data)
            }
        })
    }, [])

    // Filter list based on search
    const Search = (e) => {
        // Prevent form from refreshing page
        e.preventDefault()

        setFilteredItems(items.filter(item =>
            item.SerialNumber.toLowerCase().includes(searchText.toLowerCase())))
    }

    return(
        <>
            <form id="search-bar" className="mt-[40px] ml-[30px]">
                <input className="w-[240px] px-[20px] py-[12px] mb-[20px] mr-[20px] border rounded-sm"
                type="text"
                placeholder="Enter a serial number"
                value={searchText}
                onChange={(e)=>setSearchText(e.target.value)}/>
                <button className="w-[125px] py-[12px] border rounded-sm bg-[#014880] text-white"
                onClick={(e)=>Search(e)}>Search</button>
            </form>
            <div id="search-results" className="mx-[30px] max-w-[410px] min-[830px]:max-w-[770px] mt-[40px]">
                <div className="table">
                    <div className="table-header-group mb-[10px]">
                        <div className="table-row">
                            <div className="table-cell w-[130px]">Serial Number</div>
                            <div className="hidden min-[830px]:table-cell w-[100px]">Type</div>
                            <div className="table-cell w-[170px]">Model</div>
                            <div className="table-cell w-[110px]">Status</div>
                            <div className="hidden min-[830px]:table-cell w-[260px]">Assigned to</div>
                        </div>
                    </div>
                </div>
                <div className="table border mb-[20px]">
                    <div className="table-row-group">
                        {/*As long as items is set, create a table row for each item*/}
                        {filteredItems && filteredItems.map((item,index)=>(
                            <div className="table-row hover:bg-[#e0dcdc]"
                            key={index}
                            onClick={()=>navigate("/inventory/details/"+item.Id)}
                            >
                                {/* On-click, goes to item details for this item */}
                                <div className="table-cell w-[130px]">{item.SerialNumber}</div>
                                <div className="hidden min-[830px]:table-cell w-[100px]">{item.ItemType.Name}</div>
                                <div className="table-cell w-[170px]">{item.Model}</div>
                                <div className="table-cell w-[110px]">{item.Active? "Active" : "Disabled"}</div>
                                {/*Assigned employee name will be written in format: (last name, first name)*/}
                                {/*Is not displayed if there is no assigned employee*/}
                                {item.AssignedTo && <div className="hidden min-[830px]:table-cell w-[260px]">{`${item.AssignedTo.LastName}, ${item.AssignedTo.FirstName}`}</div>}
                            </div>
                        ))}
                    </div>
                </div>
                <div className="flex justify-between">
                    <PageSelector/>
                    {/* Goes to inventory add */}
                    <button 
                    className="ml-[40px] w-[100px] py-[12px] border rounded-sm bg-[#014880] text-white"
                    onClick={()=>navigate('/inventory/add')}>Add</button>
                </div>
            </div>
        </>
    )
}