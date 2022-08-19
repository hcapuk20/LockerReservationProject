
//import useAuth from "../auth/useAuth";
import { useNavigate } from "react-router-dom";
import Buttons from "../components/Butttons";



function AdminPage() {

    //  const { auth } = useAuth();
    // const name = auth.firstName;
    const navigate = useNavigate();




    return (


        (<div>
            <h1>AdminPage</h1>

            <h2>Source 
                 Groups:</h2>
            <br />
            <Buttons />
            <br />
            <br />
            <br />
            <br />
            <button onClick={() => { navigate("/navigationpage") }}>  return to home page  </button>

        </div>)

    );
}

export default AdminPage;