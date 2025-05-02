import { useEffect, useState } from "react";
import InventoryForm from "../../components/InventoryForm";
import { useNavigate } from "react-router-dom";

export default function AddItemPage() {
    const baseUrl = import.meta.env.VITE_API_BASE_URL;

    // Item data
    const [item, setItem] = useState({
        Active: true,
        SerialNumber: "",
        ItemTypeId: "",
        Model: "",
        Notes: ""
    })

    const [itemTypes, setItemTypes] = useState(null)
    const navigate = useNavigate()

    // Get item types
    useEffect(()=> {
        fetch(`${baseUrl}api/Inventory/GetItemTypes`, {
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

    // If a form input changes, update the item data based on the change
    const handleChange = (e) => {
        const { name, value, type, checked } = e.target
        setItem(prev => ({ 
            ...prev, 
            [name]: type == "checkbox"? checked : value }))
    }

    // Add item
    const handleSubmit = async (e) => {
        e.preventDefault()

        fetch(`${baseUrl}api/Inventory/AddItem`, {
            method: "POST",
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
                alert("Item added successfully!");
                navigate("/inventory")
            }
        })
    }

    return (
        <InventoryForm
            item={item}
            itemTypes={itemTypes}
            onChange={handleChange}
            onSubmit={handleSubmit}
        />
    )
}