import { useEffect, useState } from "react"
import config from "../../../ui.config.json";
import { useParams, useNavigate } from "react-router-dom";
import PageSelector from "../../components/PageSelector"

export default function InventoryDetails() {
    const { id } = useParams()
    const [item, setItem] = useState(null)
    const [employees, setEmployees] = useState(null)
    const [showReturnPopup, setShowReturnPopup] = useState(false)
    const [showAssignPopup, setShowAssignPopup] = useState(false)
    const navigate = useNavigate()

    // Item event data
    const [itemEvent, setItemEvent] = useState({
        ItemId: id,
        EmployeeId: "",
        EventTypeId: "",
        DateTime: "",
        Reason: ""
    })

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

    // Get employees
    useEffect(()=> {
        fetch(config.api.url+"api/Employee/GetEmployees", {
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
    
                    // If unauthorized to view employees
                    if (response.status == 403)
                    {
                        navigate("/unauthorizedaction")
                    }
                }
            else
            {
                const data = await response.json()
                setEmployees(data)
            }
        })
    }, [])

    // If a form input changes, update the item event data based on the change
    const handleChange = (e) => {
        const { name, value } = e.target;

        if (name.includes(".")) {
            const [parentKey, childKey] = name.split(".");
            setItemEvent(prev => ({
            ...prev,
            [parentKey]: {
                ...prev[parentKey],
                [childKey]: value
            }
            }))
        } else {
            setItemEvent(prev => ({
            ...prev,
            [name]: value
            }))
        }
    }

    // Return item
    const ReturnItem = ()=> {

        // Set event type to be a return and the employee id to the assigned employee
        itemEvent.EventTypeId = 2
        itemEvent.EmployeeId = item.AssignedTo.Id

        // Remove assigned employee and update item
        item.AssignedToId = null

        fetch(config.api.url+"api/Inventory/UpdateItem", {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            credentials: "include",
            body: JSON.stringify(item)
        }).then(async response => {
            if (!response.ok)
            {
                // If not authenticated
                if (response.status == 401)
                {
                    navigate("/login")
                }

                // If unauthorized to edit inventory
                if (response.status == 403)
                {
                    navigate("/unauthorizedaction")
                }
            }
            else
            {
                // Add item event
                fetch(config.api.url+"api/Inventory/AddItemEvent", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    credentials: "include",
                    body: JSON.stringify(itemEvent)
                }).then(async response => {
                    if (!response.ok)
                    {
                        // If not authenticated
                        if (response.status == 401)
                        {
                            navigate("/login")
                        }

                        // If unauthorized to edit inventory
                        if (response.status == 403)
                        {
                            navigate("/unauthorizedaction")
                        }
                    }
                    else
                    {
                        alert("Returned successfully!")
                        window.location.reload()
                    }
                })
            }

            CloseReturnPopup()
        })
    }

    // Assign item
    const AssignItem = ()=> {

        // Set event type to be an assign
        itemEvent.EventTypeId = 1

        // Set item Assigned To id to the selected employee id
        item.AssignedToId = itemEvent.EmployeeId

        // Add assigned employee and update item
        fetch(config.api.url+"api/Inventory/UpdateItem", {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            credentials: "include",
            body: JSON.stringify(item)
        }).then(async response => {
            if (!response.ok)
            {
                // If not authenticated
                if (response.status == 401)
                {
                    navigate("/login")
                }

                // If unauthorized to edit inventory
                if (response.status == 403)
                {
                    navigate("/unauthorizedaction")
                }
            }
            else
            {
                // Add item event
                fetch(config.api.url+"api/Inventory/AddItemEvent", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    credentials: "include",
                    body: JSON.stringify(itemEvent)
                }).then(async response => {
                    if (!response.ok)
                    {
                        // If not authenticated
                        if (response.status == 401)
                        {
                            navigate("/login")
                        }

                        // If unauthorized to edit inventory
                        if (response.status == 403)
                        {
                            navigate("/unauthorizedaction")
                        }
                    }
                    else
                    {
                        alert("Assigned successfully!")
                        window.location.reload()
                    }
                })
            }

            CloseReturnPopup()
        })
    }

    const CloseReturnPopup = ()=> {
        setShowReturnPopup(false)
    }

    const OpenReturnPopup = ()=> {
        setShowReturnPopup(true)
    }

    const CloseAssignPopup = ()=> {
        setShowAssignPopup(false)
    }

    const OpenAssignPopup = ()=> {
        setShowAssignPopup(true)
    }

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
                            checked={item.Active}
                            readOnly/>
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
                        value={item.Notes? item.Notes : ""}
                        readOnly></textarea>
                    </div>
                </form>
            </section>
            <section id="assignment">
                <form className="flex justify-between items-center px-[20px] mb-[40px] max-w-[480px]">
                    {/* Show assigned to name if it exists */}
                    <p>{`Assigned To: ${item.AssignedTo? `${item.AssignedTo.FirstName} ${item.AssignedTo.LastName}` : ""}`}</p>
                    <div className="flex flex-col">
                        {!item.AssignedTo && <button id="assign" className="w-[125px] py-[12px] border rounded-sm bg-[#014880] text-white mr-[10px]"
                        type="button"
                        onClick={OpenAssignPopup}>Assign</button>}
                        {item.AssignedTo && <button id="return" className="w-[125px] py-[12px] border rounded-sm bg-[#014880] text-white mr-[10px]"
                        type="button"
                        onClick={OpenReturnPopup}>Return</button>}
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

            {/* Return Item Popup */}
            {showReturnPopup && <div className="fixed inset-0 z-50 bg-black/50">
                <div className="w-[400px] m-auto mt-[100px] bg-white p-[20px] flex flex-col">
                    <h1 className="text-2xl m-auto mb-[20px]">RETURN ITEM</h1>
                    <p>{`Assigned to: ${item.AssignedTo.FirstName} ${item.AssignedTo.LastName}`}</p>
                    <div>
                        <label className="mr-[5px]">Date returned:</label>
                        <input 
                        type="date" name="DateTime"
                        value={itemEvent.DateTime}
                        onChange={handleChange}/>
                    </div>
                    <label htmlFor="reason">Reason</label>
                    <textarea name="Reason" className="w-[360px] px-[20px] py-[12px] mb-[20px] border rounded-sm resize-none" 
                    rows="2"
                    value={itemEvent.Reason}
                    onChange={handleChange}></textarea>
                    <div className="m-auto">
                        <button className="w-[120px] py-[12px] mr-[20px] border rounded-sm bg-[#014880] text-white"
                        onClick={CloseReturnPopup}>Cancel</button>
                        <button className="w-[120px] py-[12px] border rounded-sm bg-[#014880] text-white"
                        onClick={ReturnItem}>Save</button>
                    </div>
                </div>
            </div>}

            {/* Assign Item Popup */}
            {showAssignPopup && <div className="fixed inset-0 z-50 bg-black/50">
                <div className="w-[400px] m-auto mt-[100px] bg-white p-[20px] flex flex-col">
                    <h1 className="text-2xl m-auto mb-[20px]">ASSIGN ITEM</h1>
                    <select className="w-[240px] px-[20px] py-[12px] mb-[10px] border rounded-sm"
                    name="EmployeeId"
                    value={itemEvent.EmployeeId}
                    onChange={handleChange}>
                        <option id="no-selection" value="">Select employee...</option>
                        {employees && employees.map((employee,index)=>(
                            <option 
                            key={index} 
                            value={employee.Id}>
                                {/*Employee name will be written in format: (last name, first name)*/}
                                {`${employee.LastName}, ${employee.FirstName}`}
                            </option>
                        ))}
                    </select>
                    <div>
                        <label className="mr-[5px]">Date assigned:</label>
                        <input 
                        type="date" name="DateTime"
                        value={itemEvent.DateTime}
                        onChange={handleChange}/>
                    </div>
                    <label htmlFor="reason">Reason</label>
                    <textarea name="Reason" className="w-[360px] px-[20px] py-[12px] mb-[20px] border rounded-sm resize-none" 
                    rows="2"
                    value={itemEvent.Reason}
                    onChange={handleChange}></textarea>
                    <div className="m-auto">
                        <button className="w-[120px] py-[12px] mr-[20px] border rounded-sm bg-[#014880] text-white"
                        onClick={CloseAssignPopup}>Cancel</button>
                        <button className="w-[120px] py-[12px] border rounded-sm bg-[#014880] text-white"
                        onClick={AssignItem}>Save</button>
                    </div>
                </div>
            </div>}
        </>
    )
}