
import {  useNavigate, useLocation } from 'react-router-dom';
const React = require("react")

function EditPage() {
    const location = useLocation();
   // const navigate = useNavigate();
  //  const from = location.state?.from?.pathname || "/navigationpage";
    let attributesArr = [];
    attributesArr = location.state.arr;
    let  buttonSourceGroup=location.state.buttonSourceGroup;
    console.log(buttonSourceGroup)
    const navigate = useNavigate();

    const obj = {};

    for (const key of attributesArr) {
         obj[key] = "";
    }

    const [formData, setFormData] = React.useState({obj});  
    const [message, setMessage] = React.useState("");  



    function HandleInput(event) {
        setMessage("")
        const { name, value } = event.target

        setFormData((prevData) => {
            return {
                ...prevData,
                [name]: value
            }

        })

    }

    function HandleUpdate(event) {
        event.preventDefault();
        try{
            setMessage("succesful")
        }catch{
            setMessage("not succesful")
        }
    }

    function HandleDelete(event) {
        event.preventDefault();
        try{
            setMessage("succesful")
        }catch{
            setMessage("not succesful")
        }
    }
    
    function HandleAdd(event) {
        event.preventDefault();
        try{
            setMessage("succesful")
        }catch{
            setMessage("not succesful")
        }
    }



    return (
        <div >
            <form>
                <p> Editing Source Group : '{buttonSourceGroup}' </p>
                {message === '' ? <div></div> : <div>{message}</div> }
                <br></br>
                {attributesArr.map(item => {

                    return (
                        <input
                        type="text"
                        placeholder={item}
                        name={item}
                        onChange={HandleInput}
                        value={formData.name}
                    />
                    );
                })}

                <br></br>
                <br></br>

                <button onClick={HandleUpdate} >update </button>
                <button onClick={HandleDelete}   >delete</button>
                <button onClick={HandleAdd}  >add</button>
            </form>
            <button onClick= {() => {navigate("/admin");}} >  return back  </button>
        </div>

    );
}

export default EditPage;