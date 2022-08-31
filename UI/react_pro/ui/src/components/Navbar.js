
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import { useNavigate } from "react-router-dom";
import { useContext } from "react";
import AuthContext from "../context/AuthProvider";
import useAuth from '../auth/useAuth';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';



function Navbar({ landingPage }) {

    const { auth } = useAuth();
    const { setAuth } = useContext(AuthContext);
    const navigate = useNavigate();
    const accesibleGroups = auth.userData.sourceGroups;
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
                <Typography variant="h5" gutterBottom sx={{ width: 450, margin: 3, color: '#fff', my: 2 }}>
                    {landingPage}
                </Typography>
                <Box
                    sx={{
                        display: { xs: "none", md: "flex" }
                    }}
                >
                    {({ landingPage }.landingPage === "HomePage") &&
                        <Button variant="outlined" sx={{ width: 200, margin: 0, color: '#fff', my: 2 }} onClick={() => {
                            
                                navigate("/admin")
                            
                        }} >
                            {
                                " Go to the Admin page"
                            }

                        </Button>}
                        {({ landingPage }.landingPage !== "HomePage") && (accesibleGroups.length !== 0)&&
                        <Button variant="outlined" sx={{ width: 200, margin: 0, color: '#fff', my: 2 }} onClick={() => {
                            
                                navigate("/navigationpage")
                        
                        }} >
                            {
                                " Go to home page"
                            }

                        </Button>}



                    {auth.userData.role === 'DbManager' &&
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