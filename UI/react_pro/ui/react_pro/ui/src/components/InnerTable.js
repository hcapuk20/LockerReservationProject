import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';




import axios from '../api/axios'
import { useEffect, useState } from 'react';
import EditRow from './EditRow';

function InnerTable({ item_id }) {
    const [data, setData] = useState();
    const [render, setRender] = useState(false);
    console.log(render)


    useEffect(() => {

        async function fetchData() {
            try {
                const response = await axios.get(`https://localhost:7125/api/Source/getOwnersOfSource?source_id=${item_id}`)

                setData(response.data.data)
            } catch (err) {
                console.log(err);
            }
        }
        fetchData();
    }, [item_id, render]);

    function CreateTable({ arr }) {
        return (
            <>
                <Table size="small" aria-label="purchases" sx={{ '& > *': { borderBottom: 'unset' } }}>
                    <TableHead>
                        <TableRow sx={{
                                        borderBottom: "1px solid black"
                                    }}>
                            <TableCell>id</TableCell>
                            <TableCell>name</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {arr.map(item => {
                            return (
                                <TableRow sx={{
                                    borderBottom: "1.4px solid white",
                                    
                                    
                                }}>
                                    <TableCell>{item.id}</TableCell>
                                    <TableCell>{item.name}</TableCell>
                                </TableRow>
                            );
                        })}
                    </TableBody>

                </Table>
                <EditRow source_id={item_id} renderMethod={setRender} render={render}  />
            </>
        );
    }



    return (<div>
        {data ? (Object.keys(data).length !== 0 ? <CreateTable arr={data} /> :

            (<div><div>Table is empty </div><EditRow source_id={item_id} renderMethod={setRender} render={render} /></div>)) :
            (<div><div>Table is empty </div><EditRow source_id={item_id} renderMethod={setRender} render={render} /></div>)}
    </div>);


}

export default InnerTable;