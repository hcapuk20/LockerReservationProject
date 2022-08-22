import axios from '../api/axios'
import React, { useState, useEffect } from "react"
import Box from '@mui/material/Box';
import FormLabel from '@mui/material/FormLabel';
import FormControl from '@mui/material/FormControl';
import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Dialog from '@mui/material/Dialog';
import Button from '@mui/material/Button';
import { maxWidth } from '@mui/system';


export default function CheckboxesGroup({ closeChecklist, handleEmployeeSubmit, setCloseChecklist }) {

    const [sourceGroups, setSourceGroups] = useState([]);
    const [checked, setChecked] = useState([]);

    useEffect(() => {
        async function fetchData() {
            try {
                const response = await axios.get('https://localhost:7125/api/Source/getAllSourceGroups')
                setSourceGroups(response.data.data);
            } catch (err) {
                alert(err);
            }
        }
        fetchData();
    }, []);

    useEffect(() => {
        let stateArray = new Array(sourceGroups.length);
        stateArray.fill(false);
        setChecked(stateArray);
    }, [sourceGroups]);



    function handleChange(changedIndex) {
        let arr = [];
        checked.forEach((element, index) => {
            if (index === changedIndex) {
                arr.push(!element);
            } else {
                arr.push(element);
            }

        });
        setChecked(arr);

    }






    if (!closeChecklist) {
        return
    }


    return (

        <Dialog
            open={closeChecklist}
            onClose={() => setCloseChecklist(false)}
            maxWidth="xl"

            aria-labelledby="scroll-dialog-title"
            aria-describedby="scroll-dialog-description"
        >

            <Box sx={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                width: 300
            }}>
                <FormControl sx={{ m: 3 }} component="fieldset" variant="standard">
                    <FormLabel component="legend">Choose source groups</FormLabel>
                    <FormGroup>
                        {sourceGroups.map((source, index) => {

                            return (<FormControlLabel
                                control={
                                    <Checkbox checked={checked[index]} onChange={() => { handleChange(index) }} name={source.name} />
                                }
                                label={source.name}
                            />)
                        }
                        )
                        }
                    </FormGroup>
                </FormControl>
            </Box>
                <Button variant="contained"  sx={{ margin: 1, maxWidth }} onClick={() => {
                    let arr = []
                    checked.forEach((element, index) => {
                        if (element) {
                            arr.push(sourceGroups[index])
                        }
                    }
                    );
                    handleEmployeeSubmit(arr)
                }}  >
                    submit
                </Button>
                <Button  sx={{ margin: 1  , maxWidth}} onClick={() => setCloseChecklist(false)}>
                    close
                </Button>
            
        </Dialog>
    );
}
