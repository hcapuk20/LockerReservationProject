

import Box from '@mui/material/Box';
import Buttons from "../components/Buttons";
import Navbar from '../components/Navbar';


function AdminPage() {

    return (


        (
            <>

                <Navbar landingPage={"AdminPage"} />
                <Box sx={{
                    
                    marginTop: 4,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }}>
                    <Buttons />


                </ Box>

            </>

        )

    );
}

export default AdminPage;