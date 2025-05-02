import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

export default function SecurityAdd() {
    const baseUrl = import.meta.env.VITE_API_BASE_URL;

    // Access data
    const [newUser, setNewUser] = useState({
        email: "",
        password: ""
    })
    const [passwordConfirm, setPasswordConfirm] = useState("")
    const navigate = useNavigate()

    const Register = (e) => {
        // Prevent form from refreshing page
        e.preventDefault()

        if (newUser.password != passwordConfirm) {
            alert("Passwords do not match.")
            setNewUser(prev => ({
                ...prev,
                password: ""
            }));
            setPasswordConfirm("")
            return;
        }

        // Send registration
        fetch(`${baseUrl}register`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            credentials: "include",
            body: JSON.stringify(newUser)
        })
        .then(async response => {
            if (!response.ok)
            {

            }
            else
            {
                alert("User registered successfuly!")
                navigate("/security")
            }
        })
    }

    // If the form input changes, update the user based on the change
    const handleChange = (e) => {
        const { name, value } = e.target
        setNewUser(prev => ({ ...prev, [name]: value }))
    }

    return(
        <>
            <form className="flex flex-col items-center mt-[210px]">
                <input className="w-[240px] px-[20px] py-[12px] mb-[20px] border rounded-sm"
                type="email" name="email"
                placeholder="Email"
                value={newUser.email}
                onChange={(handleChange)}/>
                <input className="w-[240px] px-[20px] py-[12px] mb-[20px] border rounded-sm"
                type="password" name="password"
                placeholder="Password"
                value={newUser.password}
                onChange={handleChange}/>
                <input className="w-[240px] px-[20px] py-[12px] mb-[30px] border rounded-sm"
                type="password" name="passwordConfirm"
                placeholder="Confirm password"
                value={passwordConfirm}
                onChange={(e)=>setPasswordConfirm(e.target.value)}/>
                <button className="w-[120px] py-[12px] border rounded-sm bg-[#014880] text-white"
                onClick={(event)=>Register(event)}>Register</button>
            </form>
        </>
    )
}