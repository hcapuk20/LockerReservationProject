
import DirectionsBusIcon from '@mui/icons-material/DirectionsBus';
import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import useAuth from "../auth/useAuth";
import axios from '../api/axios';
import InventoryIcon from '@mui/icons-material/Inventory';
import React, { useState, useEffect } from "react"
import ArticleIcon from '@mui/icons-material/Article';
import Box from '@mui/material/Box';
import { indigo } from '@mui/material/colors'

function NonAdminPage() {
    const [sources, setSources] = useState([]);
    const [sourceGroups ,setSourceGroups] =  useState([]);
    const { auth } = useAuth();


    useEffect(() => {

        async function fetchData() {
            try {
                const response = await axios.get(`https://localhost:7125/api/Source/getSourcesOfEmployee?employee_id=${auth.firstName}`)
                console.log(response)
                setSources(response.data.data)
            } catch (err) {
                console.log(err);
            }
        }
        fetchData();
    }, [auth.firstName]);

    useEffect(() => {

        async function fetchData() {
            try {
                const response = await axios.get(`https://localhost:7125/api/Source/getAllSourceGroups`)
          
                setSourceGroups(response.data.data)
            } catch (err) {
                console.log(err);
            }
        }
        fetchData();
    }, [sources]);






    function findAttributes(item) {

        const attr = []
        let icon ="" ;
        for (const [name, value] of Object.entries(item)) {
            if(name === 'sourceGroupId'){ //convert id to actual name

                for (let index = 0; index < sourceGroups.length; index++){
                    if(sourceGroups[index].id === value){
                        icon= sourceGroups[index].name
                      
                        attr.push(<TableCell>{sourceGroups[index].name}</TableCell>)
                    }
                }



            }else if (name === 'space'){  //ignore space

            }else{
                attr.push(<TableCell>{value}</TableCell>)
            }
         
        }
        if(icon.includes("servis")){
            attr.push(<TableCell><DirectionsBusIcon/></TableCell>)
        }else if(icon.includes("dolap")){
            attr.push(<TableCell><InventoryIcon/></TableCell>)
            
        }else{
            attr.push(<TableCell><ArticleIcon/></TableCell>)
        }



        return (attr.map(item => {
            return ( 
            item
            )
        })
        )
    }

    


    let attributeNames;
    try {
        attributeNames = Object.keys(sources[0])
    } catch {
        return <h1>You do not have any sources.</h1>;
    }


    return (
        <Box  sx={{
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
        }}>
            <Typography variant="h3" gutterBottom sx={{ margin: 3 }} >
                Owned Sources
            </Typography>
            <TableContainer component={Paper} sx={{ margin: 1, width: 490 , border: '2px solid ',borderColor: indigo[400]} } >
                <Table aria-label="collapsible table">
                    <TableHead>
                        <TableRow>
                            {attributeNames.map((item) => {

                                if(item === 'sourceGroupId'){
                                    return (<TableCell>Name</TableCell>);
                                }else if(item === 'space'){
                                    return(<></>);
                                }else{
                                    return (<TableCell>Id</TableCell>);
                                }
                                
                            })}
                         <TableCell></TableCell>

                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {sources.map((item) => {
                            return (
                            <TableRow>{findAttributes(item)}</TableRow>
                            );
                        })
                        }
                         
                    </TableBody>
                </Table >

            </TableContainer>
        </Box>
    );
}

export default NonAdminPage;