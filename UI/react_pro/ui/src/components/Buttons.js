
import SButton from "./SButton.js"
import Typography from '@mui/material/Typography';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import useAuth from '../auth/useAuth';
import TableContainer from '@mui/material/TableContainer';
import Paper from '@mui/material/Paper';
import { indigo } from '@mui/material/colors'
function Buttons() {
    const { auth } = useAuth();
    const accesibleGroups = auth.userData.sourceGroups;



    if (accesibleGroups.length !== 0) {
        return (
            <>
                <Typography variant="h4"  >
                    Source Groups
                </Typography>

                <TableContainer component={Paper} sx={{ mt: 4, ml: 4, width: 9 / 10,border: '2px solid ',borderColor: indigo[400] }} >
                    <Table >
                        <TableHead>
                            <TableRow>
                                <TableCell> Id</TableCell>
                                <TableCell>  Name</TableCell>
                                <TableCell>  Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {

                                accesibleGroups.map(
                                    button => {
                                        return (
                                            <TableRow>
                                                <TableCell>    {button.id}</TableCell>
                                                <TableCell>    {button.name}</TableCell>
                                                <TableCell>    <SButton key={button.id} buttonId={button.id} name={button.name} /></TableCell>
                                            </TableRow>
                                        )
                                    }
                                )


                            }
                        </TableBody>
                    </Table>

                </TableContainer>
            </>
        );
    } else {
        return (
            <Typography variant="h4"  >
                You have no adminship!
            </Typography>);
    }

}

export default Buttons;