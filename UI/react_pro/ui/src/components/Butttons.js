
import Button from "./Button.js"
import axios from '../api/axios'
import React, { useState, useEffect } from "react"
//get all the source groups
//for each source group, create a new button 

function Buttons() {

    const [sourceGroups, setSourceGroups] = useState([]);
    const [errorMessage, setErrorMessage] = useState(false);
    useEffect(() => {
        async function fetchData() {
            try {
                const response = await axios.get('https://localhost:7125/api/Source/getAllSourceGroups')
                setSourceGroups(response.data.data);
            } catch (err) {
                console.log(err);
            }
        }
        fetchData();
    }, []);


    return (

        <div>
            { errorMessage && <h3 >   not authorized !</h3>}
            {sourceGroups.map(
                button => {
                    return (<Button key={button.id} buttonId={button.id} name={button.name} setErrorMessage = {setErrorMessage}/>)
                }
            )}
        </div>
    );

}

export default Buttons;