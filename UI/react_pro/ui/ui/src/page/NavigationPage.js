import Box from '@mui/material/Box';
import NonAdminPage from '../components/ownedResources';
import Navbar from '../components/Navbar';
function NavigationPage() {




    return (

        <>
            <Navbar landingPage={"HomePage"} />
            <Box sx={{
            
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
            }}>

                <NonAdminPage />


            </Box>
        </>
    );
}

export default NavigationPage;