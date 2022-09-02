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
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from '@mui/material/Typography';
export default function CheckboxesGroup({ closeChecklist, handleEmployeeSubmit, setCloseChecklist, addedGroups, setAddedGroup }) {

    const [sourceGroups, setSourceGroups] = useState([]);
    const [checked, setChecked] = useState([]);
    const [groupsToShow, setGroupsToShow] = useState(addedGroups);

    useEffect(() => {
        setGroupsToShow(addedGroups)
    }, [addedGroups]);


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



    function findGroupName(itemId){
        let element;
         sourceGroups.forEach((source)=>{
             if(source.id === itemId){
                 element =  source.name;
             }
         }
         )
        
         return (<TableCell> {element} </TableCell>);
    }


    if (!closeChecklist) {
        return
    }


    return (

        <Dialog
            open={closeChecklist}
            onClose={() => setCloseChecklist(false)}
         

            aria-labelledby="scroll-dialog-title"
            aria-describedby="scroll-dialog-description"
        >

            {

                groupsToShow.length === 0 ?
                    (<Box sx={{
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                        width: 600
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

                        <Button variant="contained" sx={{ margin: 1, maxWidth }} onClick={() => {
                            let arr = []
                            checked.forEach((element, index) => {
                                if (element) {
                                    arr.push(sourceGroups[index].id)
                                }
                            }
                            );
                            handleEmployeeSubmit(arr)
                        }}  >
                            submit
                        </Button>
                    </Box>) : (

                        <Box sx={{
                            display: 'flex',
                            flexDirection: 'column',
                            alignItems: 'center',
                            width: 600
                        }}>
                            <Typography variant="h3" gutterBottom >
                                Employee Created!
                            </Typography>
                            <Typography variant="h7" gutterBottom >
                                Employee assigned to following source groups:
                            </Typography>
                            <Table size="small" sx={{ '& > *': { borderBottom: 'unset' } }}>

                                <TableHead>
                                    <TableRow sx={{
                                        borderBottom: "1px solid black"
                                    }}>
                                        <TableCell>id</TableCell>
                                        <TableCell>name</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {groupsToShow.map(item => {
                                        return (
                                            <TableRow sx={{
                                                borderBottom: "1.4px solid white",


                                            }}>
                                                <TableCell>{item.sourceGroupId}</TableCell>
                                                {
                                                    findGroupName(item.sourceGroupId)
                                                }
                                                
                                            </TableRow>
                                        );
                                    })}
                                </TableBody>

                            </Table>


                        </Box>

                    )

            }




            <Button sx={{ margin: 1, maxWidth }} onClick={() => {
                setCloseChecklist(false);
                setAddedGroup([]);
            }} >
                close
            </Button>

        </Dialog>
    );
}
