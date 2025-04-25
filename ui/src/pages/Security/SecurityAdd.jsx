import { useEffect, useState } from "react";
import config from "../../../ui.config.json";
import { useNavigate } from "react-router-dom";

export default function SecurityAdd() {
    // Access data
    const [newUser, setNewUser] = useState({
        email: "",
        password: ""
    })
    const navigate = useNavigate()

    const Register = (e) => {
        // Prevent form from refreshing page
        e.preventDefault()

        // Send registration
        fetch(config.api.url+"register", {
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
            <form>
                <input type="text" name="email"
                value={newUser.email}
                onChange={(handleChange)}/>
                <input type="text" name="password"
                value={newUser.password}
                onChange={handleChange}/>
                <input type="text" id="password-confirm" name="passwordConfirm"/>
                <button onClick={(event)=>Register(event)}>Register</button>
            </form>
        </>
    )
}