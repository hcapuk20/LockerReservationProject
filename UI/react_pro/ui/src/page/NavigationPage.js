//import Form from "../components/Form";
import classes from './NavigationPage.module.css'
import { useNavigate, Link } from "react-router-dom";
import { useContext } from "react";
import AuthContext from "../context/AuthProvider";



function NavigationPage(){

        const { setAuth } = useContext(AuthContext);
        const navigate = useNavigate();
    
        const logout = async () => {
            // if used in more components, this should be in context 
            // axios to /logout endpoint 
            setAuth({});
            navigate('/');
        }




    return(
        <div className={classes.nav}>
            <h1>Home</h1>
            <br />
            <p>You are logged in!</p>
            <br />
            <Link to="/admin">Go to the Admin page</Link>
            <br />
            <br />
            <Link to="/nonadmin">Check your sources</Link>
            <br />
            <br />
            <Link to="/dbmanager">Manage database</Link>
            <br />
            <br />
            <div >
                <button onClick={logout}>Sign Out</button>
            </div>
        </div>
    );
}

export default NavigationPage;