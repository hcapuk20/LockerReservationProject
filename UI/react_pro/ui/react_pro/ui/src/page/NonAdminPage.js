
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
//import Form from "../components/Form";
import { useNavigate } from "react-router-dom"
import useAuth from "../auth/useAuth";
function NonAdminPage() {

    const navigate = useNavigate();

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
    const sources = auth.user.sources;
    console.log("sources")
    console.log(sources)
    console.log("sources")
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
            <Button variant="outlined" sx={{ width: 300, margin: 5 }} onClick={() => { navigate("/navigationpage") }} >
                Go to the navigationpage page
            </Button>
        </div>
    );
}

export default NonAdminPage;