import { useState } from "react"
import config from "../../ui.config.json"
import { useNavigate } from "react-router-dom"

export default function Login() {
    const notAuthorizedMessage = "The email and password entered does not match any account on record. Please enter a valid email/password or choose Forgot Password to recover your account."

    const [email, SetEmail] = useState('')
    const [password, SetPassword] = useState('')
    const [notAuthorized, SetNotAuthorized] = useState(false)
    const navigate = useNavigate()

    const Login = (event) => {
        event.preventDefault()
        fetch(config.api.url+"login", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                email: email,
                password: password
            })
        })
        .then(async response => {
            if (!response.ok)
            {
                if (response.status == "401")
                {
                    SetNotAuthorized(true)
                }
            }
            else
            {
                navigate("/home")
            }
        })
    }

    return(
        <>
            <form>
                <input 
                type="text" id="username" name="username"
                value={email}
                onChange={(event)=>SetEmail(event.target.value)}/>

                <input 
                type="text" id="password" name="password"
                value={password}
                onChange={(event)=>SetPassword(event.target.value)}/>
                
                <button 
                id="login"
                onClick={(event)=>Login(event)}>LOGIN</button>
            </form>
            <p>{`${notAuthorized? notAuthorizedMessage : ""}`}</p>
        </>
    )
}