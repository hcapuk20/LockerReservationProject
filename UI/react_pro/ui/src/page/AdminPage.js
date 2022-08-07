
//import useAuth from "../auth/useAuth";
import { useNavigate } from "react-router-dom";


const DUMMY_DATA = [
    { sourceGroup: '1.kat dolaplar', id: 1 },
    { sourceGroup: '2.kat dolaplar', id: 2 },
    { sourceGroup: '3.kat dolaplar', id: 3 },
    { sourceGroup: '4.kat dolaplar', id: 4 },
    { sourceGroup: '5.kat dolaplar', id: 5 }
]


const DUMMY_DATA2 = [
    { source_id: '1', employee_id: 111 , as: '9', bisey: 1 },
    { source_id: '2', employee_id: 1123, as: '9' , bisey: 1 },
    { source_id: '3', employee_id: 11123121 , as: '9', bisey: 1 },
    { source_id: '4', employee_id: 211231 , as: '9', bisey: 1 },
    { source_id: '5', employee_id: 115656751 , as: '9', bisey: 1 },
    { source_id: '6', employee_id: 21145321 , as: '9', bisey: 1 },
    { source_id: '7', employee_id: 1112121, as: '9', bisey: 1  },
    { source_id: '8', employee_id:[21121,444] , as: '9', bisey: 1 },

]


function AdminPage() {

    //  const { auth } = useAuth();
    // const name = auth.firstName;
    const navigate = useNavigate();



    function getResources(buttonSourceGroup) {
        //request here - send response to creatTabel

        navigate("/contentspage", { state: { arr: DUMMY_DATA2 ,buttonSourceGroup:buttonSourceGroup  } });

    }

    function Button(props) {
        
        return (<button onClick={() => { getResources(props.name) }}> {props.name}</button>)

    }

    function Buttons() {
        //request here


        return (
            <div>
                {DUMMY_DATA.map(
                    button => {
                        return (<Button key={button.id} buttonId={button.id} name={button.sourceGroup} />)
                    }
                )}
            </div>
        );

    }

    return (


        (<div>
            <h1>AdminPage</h1>
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