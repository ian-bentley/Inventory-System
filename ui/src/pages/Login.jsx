import { useState } from "react"
import config from "../../ui.config.json"
import { useNavigate } from "react-router-dom"

export default function Login() {
    const notAuthenticatedMessage = "The email and password entered does not match any account on record. Please enter a valid email/password or choose Forgot Password to recover your account."

    const [email, SetEmail] = useState('')
    const [password, SetPassword] = useState('')
    const [notAuthenticated, SetNotAuthenticated] = useState(false)
    const navigate = useNavigate()

    const Login = (e) => {
        // Prevent form from refreshing page
        e.preventDefault()

        // Send login request
        fetch(config.api.url+"login?useCookies=true", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Email: email,
                Password: password
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
            <form className="flex flex-col items-center mt-[210px]">
                {/*Email input which updates email useState when modified*/}
                <input
                className="w-[240px] px-[20px] py-[12px] mb-[20px] border rounded-sm"
                type="email" name="email"
                placeholder="Email"
                value={email}
                onChange={(event)=>SetEmail(event.target.value)}/>
                
                {/*Password input which updates password useState when modified*/}
                <input
                className="w-[240px] px-[20px] py-[12px] mb-[30px] border rounded-sm"
                type="password" name="password"
                placeholder="Password"
                value={password}
                onChange={(event)=>SetPassword(event.target.value)}/>
                
                {/*Form logic is overwritten to call on Login function*/}
                <button 
                className="w-[120px] py-[12px] border rounded-sm bg-[#014880] text-white"
                onClick={(event)=>Login(event)}>LOGIN</button>
            </form>

            {/* Displayed message if login fails authentication */}
            <p>{`${notAuthenticated? notAuthenticatedMessage : ""}`}</p>
        </>
    )
}