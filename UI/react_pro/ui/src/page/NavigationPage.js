//import Form from "../components/Form";,

import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import { useNavigate } from "react-router-dom";
import { useContext } from "react";
import AuthContext from "../context/AuthProvider";
import useAuth from '../auth/useAuth';
import NonAdminPage from '../components/ownedResources';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
function NavigationPage() {
    const { auth } = useAuth();
    const { setAuth } = useContext(AuthContext);
    const navigate = useNavigate();

    const logout = async () => {
        // if used in more components, this should be in context 
        // axios to /logout endpoint 
        setAuth({});
        navigate('/');
    }
    function checkRole() {
        if (auth.userData.role === 'DbManager') {
            navigate("/dbmanager")
        } else {
            alert("not authorized!")
        }

    }



    return (

        <Box sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
        }}>

            <AppBar >

                <Toolbar
                    disableGutters
                    sx={{
                        display: { xs: "flex" },
                        flexDirection: "row",
                        justifyContent: "space-between"
                    }}
                >
                    <Typography variant="h5" gutterBottom sx={{ width: 200, margin: 3, color: '#fff', my: 2 }}>
                        HomePage
                    </Typography>
                    <Box
                        sx={{
                            display: { xs: "none", md: "flex" }
                        }}
                    >
                        <Button variant="outlined" sx={{ width: 200, margin: 0, color: '#fff', my: 2 }} onClick={() => { navigate("/admin") }} >
                            Go to the Admin page
                        </Button>

                        <Button variant="outlined" sx={{ width: 180, margin: 0, color: '#fff', my: 2 }} onClick={checkRole}  >
                            Manage database
                        </Button>
                        <Button variant="contained"  sx={{ width: 150, margin: 1, my: 2 }}  onClick={logout}>
                            Sign Out
                        </Button>
                    </Box>

                </Toolbar>
            </AppBar>

            <NonAdminPage />


        </Box>
    );
}

export default NavigationPage;