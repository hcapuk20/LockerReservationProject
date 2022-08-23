
import SButton from "./SButton.js"
import axios from '../api/axios'
import React, { useState, useEffect } from "react"
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

        <Box >
            {errorMessage &&
                <Box sx={{
                    marginTop: 4,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }} >
                    <Typography variant="h5" gutterBottom  >
                        not authorized!
                    </Typography>
                </Box>
            }
            <Box sx={{
                marginTop: 4,
                display: 'flex',
                flexDirection: 'row',
                alignItems: 'center',
            }}>
                {sourceGroups.map(
                    button => {
                        return (<SButton key={button.id} buttonId={button.id} name={button.name} setErrorMessage={setErrorMessage} />)
                    }
                )}
            </Box>
        </Box>
    );

}

export default Buttons;