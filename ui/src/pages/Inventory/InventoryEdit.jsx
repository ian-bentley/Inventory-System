import { useEffect, useState } from "react";
import InventoryForm from "../../components/InventoryForm";
import config from "../../../ui.config.json";
import { useParams, useNavigate } from "react-router-dom";

export default function InventoryEdit() {
    const { id } = useParams()
    const [item, setItem] = useState(null)
    const [itemTypes, setItemTypes] = useState(null)
    const navigate = useNavigate()

    // Get item types
    useEffect(()=> {
        fetch(config.api.url+"api/Inventory/GetItemTypes", {
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
    
                    // If unauthorized to edit inventory
                    if (response.status == 403)
                    {
                        navigate("/unauthorizedaction")
                    }
                }
            else
            {
                const data = await response.json()
                setItemTypes(data)
            }
        })
    }, [])

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

                // If item with that id doesn't exist
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

    // If a form input changes, update the item based on the change
    const handleChange = (e) => {
        const { name, value, type, checked } = e.target
        setItem(prev => ({ 
            ...prev, 
            [name]: type == "checkbox"? checked : value }))
    }

    // Edit item
    const handleSubmit = async (e) => {
        e.preventDefault()

        fetch(config.api.url+"api/Inventory/UpdateItem", {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            credentials: "include",
            body: JSON.stringify(item)
        })
        .then(async response => {
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
                alert("Item updated successfully!");
                navigate("/inventory/details/"+id)
            }
        })
    }
    
    return(
        <>
            <InventoryForm
            item={item}
            itemTypes={itemTypes}
            onChange={handleChange}
            onSubmit={handleSubmit}
            />
        </>
    )
}