
import SButton from "./SButton.js"
import axios from '../api/axios'
import React, { useState, useEffect } from "react"
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';


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