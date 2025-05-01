import { useEffect, useState } from "react"
import config from "../../../ui.config.json";
import { useParams, useNavigate } from "react-router-dom";

export default function SecurityEdit() {
    const { id } = useParams()
    const [access, setAccess] = useState(null)
    const [newPassword, setNewPassword] = useState("")
    const navigate = useNavigate()

    // Get access by id parameter
    useEffect(()=> {
        fetch(config.api.url+"api/Security/GetAccess?id="+id, {
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
                // If unauthorized to view security
                if (response.status == 403)
                {
                    navigate("/unauthorizedpage")
                }

                //If access with that id doesn't exist
                if (response.status == 404)
                {
                    navigate("/404")
                }
            }
            else
            {
                const data = await response.json()
                setAccess(data)
            }
        })
    }, [])

    // If a form input changes, update the access based on the change
    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;

        setAccess(prev => ({
        ...prev,
        [name]: type == "checkbox"? checked : value
        }))
    }

    // Reset password
    const ResetPassword = ()=> {
        if (newPassword != "")
        {
            fetch(config.api.url+"api/Security/ForceResetPassword", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                credentials: "include",
                body: JSON.stringify({
                    UserId: access.UserId,
                    NewPassword: newPassword
                })
            })
            .then(async response => {
                if (!response.ok)
                {
                    // If not authenticated
                    if (response.status == 401)
                    {
                        navigate("/login")
                    }
    
                    // If unauthorized to edit security
                    if (response.status == 403)
                    {
                        navigate("/unauthorizedaction")
                    }
                }
                else
                {
                    alert("Password reset successfully!")
                    navigate("/security")
                }
            })
        }
    }

    // Update access
    const UpdateAccess = ()=> {
        console.log(access)
        fetch(config.api.url+"api/Security/UpdateAccess", {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            credentials: "include",
            body: JSON.stringify(access)
        })
        .then(async response => {
            if (!response.ok)
            {
                // If not authenticated
                if (response.status == 401)
                {
                    navigate("/login")
                }

                // If unauthorized to edit security
                if (response.status == 403)
                {
                    navigate("/unauthorizedaction")
                }
            }
            else
            {
                alert("Access updated successfully!")
                navigate("/security")
            }
        })
    }

    if (!access) return <div className="mx-[20px] mt-[40px]">Loading...</div>

    return(
        <>
            <form className="px-[20px] mt-[40px]">
                <div className="flex flex-wrap">
                    <div>
                        <p className="mb-[10px]">Email: {access.Email}</p>
                        <div className="flex flex-col mb-[40px]">
                            <input className="w-[240px] px-[20px] py-[12px] mb-[10px] border rounded-sm"
                            type="password" name="NewPassword"
                            placeholder="Password"
                            value={newPassword}
                            onChange={(e)=>setNewPassword(e.target.value)}/>
                            <input className="w-[240px] px-[20px] py-[12px] mb-[30px] border rounded-sm"
                            type="password" name="passwordConfirm"
                            placeholder="Confirm password"
                            /> 
                            <button className="w-[120px] py-[12px] mb-[30px] border rounded-sm bg-[#014880] text-white"
                            type="button"
                            onClick={ResetPassword}>Reset</button>
                            <label>Permissions:</label>
                            <div>
                                <input className="mr-[10px]"
                                type="checkbox" name="ViewEmployees"
                                checked={access.ViewEmployees}
                                onChange={handleChange}/>
                                <label htmlFor="view-employees">View employees</label>
                            </div>
                            <div>
                                <input className="mr-[10px]"
                                type="checkbox" name="EditEmployees"
                                checked={access.EditEmployees}
                                onChange={handleChange}/>
                                <label htmlFor="EditEmployees">Edit employees</label>
                            </div>
                            <div>
                                <input className="mr-[10px]"
                                type="checkbox" name="ViewInventory"
                                checked={access.ViewInventory}
                                onChange={handleChange}/>
                                <label htmlFor="ViewInventory">View inventory</label>
                            </div>
                            <div>
                                <input className="mr-[10px]"
                                type="checkbox" name="EditInventory"
                                checked={access.EditInventory}
                                onChange={handleChange}/>
                                <label htmlFor="EditInventory">Edit inventory</label>
                            </div>
                            <div>
                                <input className="mr-[10px]"
                                type="checkbox" name="ViewSecurity"
                                checked={access.ViewSecurity}
                                onChange={handleChange}/>
                                <label htmlFor="ViewSecurity">View security</label>
                            </div>
                            <div>
                                <input className="mr-[10px]"
                                type="checkbox" name="EditSecurity"
                                checked={access.EditSecurity}
                                onChange={handleChange}/>
                                <label htmlFor="EditSecurity">Edit security</label>
                            </div>
                        </div>
                    </div>
                
                    {/* <div className="flex flex-col mb-[20px] ml-[60px]">
                        <label className="mb-[10px]"
                        htmlFor="notes">Notes</label>
                        <textarea className="w-[360px] px-[20px] py-[12px] mb-[20px] border rounded-sm resize-none" 
                        name="Notes" rows="6"></textarea>
                    </div> */}
                </div>
                <button className="w-[120px] py-[12px] border rounded-sm bg-[#014880] text-white"
                type="button"
                onClick={UpdateAccess}>Save</button>
            </form>
        </>
    )
}