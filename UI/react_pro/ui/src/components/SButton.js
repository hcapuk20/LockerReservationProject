import { useNavigate } from "react-router-dom";
import axios from '../api/axios'
import useAuth from "../auth/useAuth";




import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import { CardActionArea } from '@mui/material';
//for each source group has a button for it 
//if clicked, send request to backend ,navigate to contentspage 
//send request to contents page
//backend de buunu için ayrı fonksiyon yazılsa filet kısmına gerek kalmaz

function SButton(props) {

    const navigate = useNavigate();
    const { auth } = useAuth();
    const adminGroups = auth.user.sourceGroups;
    console.log(auth.user.sourceGroups)
    function getResources(buttonSourceGroup,buttonId) {
        
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
    
    return (<Button   variant='text'onClick={() => { getResources(props.name,props.buttonId) } }> {props.name}</Button>)

}

export default SButton;