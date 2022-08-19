

import axios from '../api/axios'
const React = require("react")


////inputlara type check yapman lazım!!!

function EditRow({source_id, renderMethod }) {


    let attributesArr = [];
    attributesArr = ['id','name']

    const obj = {};

    for (const key of attributesArr) {
         obj[key] = " ";
    }

    const [formData, setFormData] = React.useState({obj});  
    const [message, setMessage] = React.useState(" ");  



    function HandleInput(event) {
        setMessage("")
        console.log(event.target)
        const { name, value } = event.target
        console.log(name)
        console.log(value)
        setFormData((prevData) => {
            return {
                ...prevData,
                [name]: value
            }

        })

    }




    async function handleDelete(event) {
        event.preventDefault(); 
        let response;
        try{                            
             response =   axios.delete( `https://localhost:7125/api/Source/removeRelationship?employee_id=${formData.id}&source_id=${source_id}`)
            setMessage((await response).data.message)
            renderMethod(render => !render);  
        }catch(err){
            setMessage("error")
        }
    }
    

    async  function handleAdd(event) {
        event.preventDefault(); 
        let response;
        try{
             response =   axios.post( `https://localhost:7125/api/Source/addRelationship?employee_id=${formData.id}&source_id=${source_id}`)
            setMessage((await response).data.message)
            renderMethod(render => !render);
        }catch(err){
            setMessage("error")
        }
    }



    return (
        <div >
            <form>
                {message === '' ? <div></div> : <div>{message}</div> }
                <br></br>

                {

                
                attributesArr.map((item ,index) => {

                    
                    return (
                        <input
                        type="text"
                        placeholder={item}
                        name={item}
                        onChange={HandleInput}
                        value={ formData[index]}
                        key ={index} //bak
                    />
                    );
                })}

                <br></br>
                <br></br>
                <button onClick={handleDelete}   >remove employee </button>
                <button onClick={handleAdd}  >add employee </button>
            </form>
            <br></br>
                <br></br>
        </div>

    );
}

export default EditRow;