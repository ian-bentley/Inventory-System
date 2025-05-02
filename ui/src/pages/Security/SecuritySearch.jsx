import { useEffect, useState } from "react"
import PageSelector from "../../components/PageSelector"
import { useNavigate } from "react-router-dom"

export default function SecuritySearch() {
    const baseUrl = import.meta.env.VITE_API_BASE_URL;
    const [accesses, setAccesses] = useState(null)
    const [filteredAccesses, setFilteredAccesses] = useState(null)
    const [searchText, setSearchText] = useState("")
    const navigate = useNavigate()

    // Get data for access list
    useEffect(()=> {
        fetch(`${baseUrl}api/Security/GetAccesses`, {
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
                setFilteredAccesses(data)
            }
        })
    }, [])

    // Filter list based on search
    const Search = (e) => {
        // Prevent form from refreshing page
        e.preventDefault()

        setFilteredAccesses(accesses.filter(accesses =>
            accesses.Email.toLowerCase().includes(searchText.toLowerCase())))
    }


    return(
        <>
            <form id="search-bar" className="mt-[40px] ml-[30px]">
                <input className="w-[240px] px-[20px] py-[12px] mb-[20px] mr-[20px] border rounded-sm"
                type="text"
                placeholder="Enter a name"
                value={searchText}
                onChange={(e)=>setSearchText(e.target.value)}/>
                <button className="w-[125px] py-[12px] border rounded-sm bg-[#014880] text-white"
                onClick={(e)=>Search(e)}>Search</button>
            </form>

            {/* Show loading if accesses have not been fetched yet */}
            {!accesses && <p className="mx-[30px]">Loading...</p>}

            {/* Show only if accesses have been fetched */}
            {accesses && <div id="search-results" className="m-auto max-w-[416px] mt-[40px]">
                <div className="table">
                    <div className="table-header-group">
                        <div className="table-row">
                            <div className="table-cell w-[416px]">Email</div>
                        </div>
                    </div>
                </div>
                <div className="table border mb-[20px]">
                    <div className="table-row-group">
                        {/*As long as accesses is set, create a table row for each access*/}
                        {filteredAccesses && filteredAccesses.map((access,index)=>(
                            <div className="table-row hover:bg-[#e0dcdc]"
                            key={index}
                            onClick={()=>navigate("/security/edit/"+access.UserId)}
                            >
                                {/* On-click, goes to access edit for this access */}
                                <div className="table-cell w-[416px]">{access.Email}</div>
                            </div>
                        ))}
                    </div>
                </div>
                <div className="flex justify-between">
                    <PageSelector/>
                    {/* Goes to security add */}
                    <button 
                    className="ml-[40px] w-[100px] py-[12px] border rounded-sm bg-[#014880] text-white"
                    onClick={()=>navigate('/security/add')}>Add</button>
                </div>
            </div>}
        </>
    )
}