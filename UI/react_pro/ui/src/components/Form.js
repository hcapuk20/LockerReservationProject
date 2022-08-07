
import classes from './Form.module.css'
import useAuth from '../auth/useAuth';
//import axios from '../api/axios'
import {  useNavigate, useLocation } from 'react-router-dom';
//Link
//const LOGIN_URL = '/auth';


const React = require("react")
function Form() {

    const [formData, setFormData] = React.useState({ firstName: "", passWord: "" });  //değiştir! - bak 
    const [errorMessage, setErrorMessage] = React.useState(false);
    const {setAuth} = useAuth();


    const navigate = useNavigate();
    const location = useLocation();

    const from = location.state?.from?.pathname || "/navigationpage";



    React.useEffect(() => {
        setErrorMessage(false)
    }, [formData.firstName, formData.passWord])



    function HandleInput(event) {
        const { name, value } = event.target
        console.log("name:")
        console.log(name)
        console.log("value:")
        console.log(value)
        setFormData((prevData) => {
            return {
                ...prevData,
                [name]: value
            }

        })
    }

    function HandleSubmit(event) {

        event.preventDefault();
        try {
            //const response = await axios.post(LOGIN_URL,JSON.stringify(formData.firstName,formData.passWord))
             const first =formData.firstName
             const second =formData.passWord
            //setAuth({first,second})
            console.log(first)
            setAuth({firstName: first, password :second});
            console.log(from)
            navigate(from, {replace : true} );
        } catch (error) {
            console.log(error)
            setErrorMessage(true)
        }



    }


    return (


        <div className={classes.main}>
            <form onSubmit={HandleSubmit}>
                <p> Login  </p>
                {errorMessage && <p> Wrong name or password!</p>}
                <br></br>
                <input
                    type="text"
                    placeholder="firstName"
                    name="firstName"
                    onChange={HandleInput}
                    value={formData.firstName}
                />
                <input
                    type="text"
                    placeholder="passWord"
                    name="passWord"
                    onChange={HandleInput}
                    value={formData.passWord}
                />
                <br></br>
                <br></br>
                <br></br>
                <br></br>
                <button type="submit">Send</button>
            </form>
        </div>

    );

}
export default Form;