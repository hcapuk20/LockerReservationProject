import { useNavigate } from "react-router-dom";
import axios from '../api/axios'
import Button from '@mui/material/Button';


function SButton(props) {

    const navigate = useNavigate();


    function getResources(buttonSourceGroup,buttonId) {
        

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
    
    return (<Button   size="small" variant="outlined" onClick={() => { getResources(props.name,props.buttonId) } }> check source group</Button>)

}

export default SButton;