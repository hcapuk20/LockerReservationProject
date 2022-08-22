
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
//import useAuth from "../auth/useAuth";
import { useNavigate } from "react-router-dom";
import Buttons from "../components/Buttons";



function AdminPage() {

    //  const { auth } = useAuth();
    // const name = auth.firstName;
    const navigate = useNavigate();




    return (


        (
            <Box sx={{
                marginTop: 8,
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
            }}>
            <Typography variant="h2" gutterBottom >
                Admin Page
            </Typography>

            <Typography variant="h4" gutterBottom >
                Source Groups
            </Typography>
            <Buttons />
            <br />
            <br />
            <br />            
            <Button variant="outlined" sx={{ width: 345, margin: 1 }} onClick={() => {  navigate("/navigationpage")}}  >
            return to home page
            </Button>

        </ Box>)

    );
}

export default AdminPage;