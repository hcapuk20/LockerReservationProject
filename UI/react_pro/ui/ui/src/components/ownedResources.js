

import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import useAuth from "../auth/useAuth";
function NonAdminPage() {



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


    const { auth } = useAuth();
    const sources = auth.userData.sources;
    let attributeNames;
    try {
        attributeNames = Object.keys(sources[0])
    } catch {
        return <h1>You do not have any sources.</h1>;
    }


    return (
        <div>
            <Typography variant="h3" gutterBottom sx={{ margin: 5 }} >
                Owned Sources:
            </Typography>
            <TableContainer component={Paper} sx={{ margin: 5, width: 9 / 10 }} >
                <Table aria-label="collapsible table">
                    <TableHead>
                        <TableRow>
                            {attributeNames.map((item) => {
                                return (<TableCell>{item}</TableCell>);
                            })}
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {sources.map((item) => {
                            return (<TableRow>{findAttributes(item)}</TableRow>);
                        })
                        }
                    </TableBody>
                </Table >

            </TableContainer>
        </div>
    );
}

export default NonAdminPage;