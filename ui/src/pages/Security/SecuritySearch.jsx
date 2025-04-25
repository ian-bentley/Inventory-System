import { useEffect, useState } from "react"
import PageSelector from "../../components/PageSelector"
import config from "../../../ui.config.json"
import { useNavigate } from "react-router-dom"

export default function SecuritySearch() {
    const [accesses, setAccesses] = useState(null)
    const navigate = useNavigate()

    // Get data for access list
    useEffect(()=> {
        fetch(config.api.url+"api/Security/GetAccesses", {
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
                // If unauthorized to view security
                if (response.status == 403)
                {
                    navigate("/unauthorizedpage")
                }
            }
            else
            {
                const data = await response.json()
                setAccesses(data)
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
                        <div className="table-cell">Email</div>
                    </div>
                </div>
                <div className="table-row-group">
                    {/*As long as accesses is set, create a table row for each access*/}
                    {accesses && accesses.map((access,index)=>(
                        <div className="table-row"
                        key={index}
                        onClick={()=>navigate("/security/edit/"+access.UserId)}
                        >
                            {/* On-click, goes to access edit for this access */}
                            <div className="table-cell">{access.Email}</div>
                        </div>
                    ))}
                </div>
            </div>
            <PageSelector/>
            <button onClick={()=>navigate("/security/add")}>Add</button>
        </>
    )
}