

import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import Collapse from '@mui/material/Collapse';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { useNavigate, useLocation } from "react-router-dom";
import InnerTable from "../components/InnerTable";
import { useState } from "react";
import Navbar from '../components/Navbar';
import { indigo } from '@mui/material/colors'


function ContentsPage() {
    const location = useLocation();
    const navigate = useNavigate();

    // const navigate = useNavigate();
    //  const from = location.state?.from?.pathname || "/navigationpage";
    let arrayWithData = [];

    arrayWithData = location.state.arr;
  

    let stateArray = new Array(arrayWithData.length);
    stateArray.fill(false);


    const [showInnerTable, setShowInnerTable] = useState(stateArray)
    if (arrayWithData.length === 0) {
        return (
            <div>
                <h1>no data found</h1>
                <button onClick={() => { navigate("/admin") }}> return back</button>
            </div>);
    }




    function showContent(change_index) {
        const newState = showInnerTable.map((value, index) => {
            if (index === change_index) {
                console.log("changed")
                return (!value);
            }
            return value;
        });
        setShowInnerTable(newState);
    }

    function findAttributes(item) {   //buraları değiştir

        const attr = []
        for (const [, value] of Object.entries(item)) {
            attr.push(value)
        }
        return (attr.map(item => {
            return (<TableCell>{item}</TableCell>)
        })
        )
    }

    return (
        <div>
            <Navbar landingPage={`Contents of "${location.state.buttonSourceGroup}" `}/>
            <TableContainer component={Paper} sx={{ mt: 4,ml: 4, width: 9 / 10 , border:  '2px solid ' ,borderColor: indigo[400] }} >
                <Table >
                    <TableHead>
                        <TableRow >	
                        <TableCell>Id</TableCell>
                        <TableCell>Source Group Id</TableCell>
                        <TableCell>Space</TableCell>
                        <TableCell>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody  >
                        {arrayWithData.map((item, index) => {
                            return (
                                <>
                                    <TableRow sx={{
                                        borderBottom: "1.4px solid grey"
                                    }}>
                                        {
                                            findAttributes(item)
                                        }
                                        <TableCell>
                                            <Box sx={{
                                                display: 'flex',
                                                flexDirection: 'row',
                                                alignItems: 'center',
                                            }}>
                                                <Button size="small" variant="outlined" sx={{ margin: 1 }} onClick={() => showContent(index)} >
                                                    Show content
                                                </Button>
                                            </Box>

                                        </TableCell>
                                    </TableRow>
                                    <TableRow sx={{
                                        borderBottom: "1.4px solid white"
                                    }}>
                                        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
                                            <Collapse in={showInnerTable[index]} timeout="auto" unmountOnExit>
                                                {
                                                    (<Box sx={{ margin: 1 }}>
                                                        <InnerTable item_id={item.id} />
                                                    </Box>)
                                                }
                                            </Collapse>
                                        </TableCell>
                                    </TableRow>
                                </>

                            );
                        })}
                    </TableBody>
                </Table>
            </TableContainer>
            <Button size="small"   variant="contained"sx={{ margin: 4 }} onClick={() => { navigate("/admin"); }} >
                Return to admin page
            </Button>


            <br />

        </div>
    );
}


export default ContentsPage;