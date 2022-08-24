import {  Navigate,Outlet } from "react-router-dom";
import useAuth from "../auth/useAuth";


const RequireAuth= () => {
    const {auth} = useAuth();

    return(
        auth.firstName
        ? <Outlet/>
        :<Navigate to = "/" />
        )

}   
export default RequireAuth;