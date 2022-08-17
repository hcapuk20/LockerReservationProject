import { useNavigate } from "react-router-dom";
import axios from '../api/axios'
import useAuth from "../auth/useAuth";
//for each source group has a button for it 
//if clicked, send request to backend ,navigate to contentspage 
//send request to contents page
//backend de buunu için ayrı fonksiyon yazılsa filet kısmına gerek kalmaz

function Button(props) {

    const navigate = useNavigate();
    const { auth } = useAuth();
    const adminGroups = auth.user.sourceGroups;
    console.log(auth.user.sourceGroups)
    function getResources(buttonSourceGroup,buttonId) {
        console.log(adminGroups)
        
        let isAuthorized =false;

        adminGroups.forEach(element => {
            if(element.id === buttonId){
                isAuthorized = true;
            }   
            
        });

        if(!isAuthorized){
            props.setErrorMessage(true);
            return;
        }

        async function fetchData() {
            try {
                
                const response = await axios.get('https://localhost:7125/api/Source/getAllSources')
                //get owners of the source 

                navigate("/contentspage", { state: { arr: response.data.data.filter(data => data.sourceGroupId === buttonId ) ,buttonSourceGroup:buttonSourceGroup  } });

            } catch (err) {
                console.log(err);
            }
        }

        fetchData();


        //verify the admin for each source group

    
    }
    
    return (<button onClick={() => { getResources(props.name,props.buttonId) } }> {props.name}</button>)

}

export default Button;