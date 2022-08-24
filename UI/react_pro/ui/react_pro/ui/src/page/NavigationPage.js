//import Form from "../components/Form";,

import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import { useNavigate } from "react-router-dom";
import { useContext } from "react";
import AuthContext from "../context/AuthProvider";



function NavigationPage() {

    const { setAuth } = useContext(AuthContext);
    const navigate = useNavigate();

    const logout = async () => {
        // if used in more components, this should be in context 
        // axios to /logout endpoint 
        setAuth({});
        navigate('/');
    }




    return (
        <Box sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
        }}>

            <Typography variant="h2" gutterBottom >
                Home
            </Typography>

            <Typography variant="h4" gutterBottom>
                You are logged in.
            </Typography>
            <Button variant="outlined" sx={{ width: 345, margin: 1 }} onClick={() => { navigate("/admin") }} >
                Go to the Admin page
            </Button>

            <Button variant="outlined" sx={{ width: 345, margin: 1 }} onClick={() => { navigate("/nonadmin") }}>
                Check your sources
            </Button>

            <Button variant="outlined" sx={{ width: 345, margin: 1 }} onClick={() => { navigate("/dbmanager") }}  >
                Manage database
            </Button>


            <Button onClick={logout}>Sign Out</Button>

        </Box>
    );
}

export default NavigationPage;