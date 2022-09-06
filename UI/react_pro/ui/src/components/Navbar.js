
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import { useNavigate } from "react-router-dom";
import { useContext } from "react";
import AuthContext from "../context/AuthProvider";
import useAuth from '../auth/useAuth';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';

import Pic from '../images/images.js';


function Navbar({ landingPage }) {

    const { auth } = useAuth();
    const { setAuth } = useContext(AuthContext);
    const navigate = useNavigate();
    const logout = async () => {
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


        <AppBar position="sticky">

            <Toolbar
                disableGutters
                sx={{
                    display: { xs: "flex" },
                    flexDirection: "row",
                    justifyContent: "space-between"
                }}
            >
                <Box sx={{
                    display: { xs: "none", md: "flex" }
                }}>
                    
                    <Box sx={{  mt: 2.5, ml: 2.5, mr: 0 }} >
                        <Pic />
                    </Box>
                    <Typography variant="h5" sx={{ width: 450, margin: 1.5, color: '#fff', my: 2, ml: 1 }}>

                        {landingPage}

                    </Typography>
                </Box>
                <Box
                    sx={{
                        display: { xs: "none", md: "flex" }
                    }}
                >
                    {({ landingPage }.landingPage === "HomePage") &&
                        <Button variant="outlined" sx={{ width: 300, margin: 0, color: '#fff', my: 2 }} onClick={() => {

                            navigate("/admin")

                        }} >
                            {
                                " Go to the Admin page"
                            }

                        </Button>}
                    {({ landingPage }.landingPage !== "HomePage") &&
                        <Button variant="outlined" sx={{ width: 300, margin: 0, color: '#fff', my: 2 }} onClick={() => {

                            navigate("/navigationpage")

                        }} >
                            {
                                " Go to home page"
                            }

                        </Button>}



                    {auth.userData.role === 'DbManager' && ({ landingPage }.landingPage !== "DbManagerPage") &&
                        <Button variant="outlined" sx={{ width: 180, margin: 0, color: '#fff', my: 2 }} onClick={checkRole}  >
                            Manage database
                        </Button>}
                    <Button variant="contained" sx={{ width: 150, margin: 1, my: 2 }} onClick={logout}>
                        Sign Out
                    </Button>
                </Box>

            </Toolbar>
        </AppBar>




    );
}

export default Navbar;