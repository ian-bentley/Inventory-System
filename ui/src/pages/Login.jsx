import { useState } from "react"
import config from "../../ui.config.json"
import { useNavigate } from "react-router-dom"

export default function Login() {
    const notAuthenticatedMessage = "The email and password entered does not match any account on record. Please enter a valid email/password or choose Forgot Password to recover your account."

    const [email, SetEmail] = useState('')
    const [password, SetPassword] = useState('')
    const [notAuthenticated, SetNotAuthenticated] = useState(false)
    const navigate = useNavigate()

    const Login = (event) => {
        // Prevent form from refreshing page
        event.preventDefault()

        // Send login request
        fetch(config.api.url+"login", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                email: email,
                password: password
            }),
            credentials: 'include'
        })
        .then(async response => {
            // If not authenticated
            if (!response.ok)
            {
                if (response.status == 401)
                {
                    // Sets condition that controls display message
                    SetNotAuthenticated(true)
                }
            }
            else
            {
                // Login successful
                navigate("/home")
            }
        })
    }

    return(
        <>
            <form>
                {/*Email input which updates email useState when modified*/}
                <input 
                type="text" id="username" name="username"
                value={email}
                onChange={(event)=>SetEmail(event.target.value)}/>
                
                {/*Password input which updates password useState when modified*/}
                <input 
                type="text" id="password" name="password"
                value={password}
                onChange={(event)=>SetPassword(event.target.value)}/>
                
                {/*Form logic is overwritten to call on Login function*/}
                <button 
                id="login"
                onClick={(event)=>Login(event)}>LOGIN</button>
            </form>

            {/* Displayed message if login fails authentication */}
            <p>{`${notAuthenticated? notAuthenticatedMessage : ""}`}</p>
        </>
    )
}