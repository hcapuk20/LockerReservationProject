
import classes from './Form.module.css'
import useAuth from '../auth/useAuth';
//import axios from '../api/axios'
import { useNavigate, useLocation } from 'react-router-dom';
import axios from '../api/axios';
//Link
//const LOGIN_URL = '/auth';


const React = require("react")
function Form() {



    const [formData, setFormData] = React.useState({ firstName: "", passWord: "" });  //değiştir! - bak 
    const [errorMessage, setErrorMessage] = React.useState(false);
    const { setAuth } = useAuth();
    const navigate = useNavigate();
    const location = useLocation();

    const from = location.state?.from?.pathname || "/navigationpage";



    React.useEffect(() => {
        setErrorMessage(false)
    }, [formData.firstName, formData.passWord])



    function HandleInput(event) {
        const { name, value } = event.target
        setFormData((prevData) => {
            return {
                ...prevData,
                [name]: value
            }

        })
    }
    async function getAdminGroups(first) {
        const response = await axios.get(`https://localhost:7125/api/Source/getEmployeeById?employee_id=${first}`);
        console.log(response)
        return (response?.data?.data);  //////burası nullsa error veriyor düzelt

    }

    function HandleSubmit(event) {

        event.preventDefault();
        try {

            const first = formData.firstName
            const second = formData.passWord

            getAdminGroups(first)
                .then((result) => {
                    setAuth({ firstName: first, password: second, user: result });

                    (from !== "/nonadmin" && from !== "/admin" && from !== "/navigationpage") ?
                        navigate('/navigationpage') :
                        navigate(from, { replace: true });
                }
                )




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