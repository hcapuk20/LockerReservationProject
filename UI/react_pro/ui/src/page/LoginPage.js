

import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import useAuth from '../auth/useAuth';
import { useNavigate } from 'react-router-dom';
import axios from '../api/axios';


const React = require("react")
function LoginPage() {
    const [formData, setFormData] = React.useState({ firstName: "", passWord: "" }); 
    const [errorMessage, setErrorMessage] = React.useState(false);
    const { setAuth } = useAuth();
    const navigate = useNavigate();

    React.useEffect(() => {
        setErrorMessage(false)
    }, [formData.firstName, formData.passWord])

    function HandleInput(event) {
        const { name, value } = event.target
        setFormData((prevData) => {
            return {
                ...prevData,
                [name]: value
            }
        })
    }
    const handleSubmit = async (event) => {

        event.preventDefault();
        try {

            const first = formData.firstName
            const second = formData.passWord




            const response = await axios.post(`/api/Token/login?id=${first}&password=${second}`);
            axios.defaults.headers.common['Authorization'] = `Bearer ${response.data.token}`
            setAuth({ firstName: first, password: second, userData: response.data.employee });
            navigate('/navigationpage');

        } catch (error) {
            console.log(error)
            setErrorMessage(true)
        }
    }

    return (

        <Container component="main" maxWidth="xs">
            <Box
                sx={{
                    marginTop: 8,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }}
            >
                <Typography variant="h2" sx={{ margin: '15px' }}>
                    Sign in
                </Typography>
                <Box component="form" onSubmit={handleSubmit} sx={{ mt: 1 }}>
                    {errorMessage &&
                        <Typography variant="h6" sx={{ margin: '15px' }}>
                            Invalid id or password!
                        </Typography>}
                    <TextField
                        sx={{ margin: '6px' }}
                        required
                        fullWidth
                        type="text"
                        placeholder="id"
                        name="firstName"
                        onChange={HandleInput}
                        value={formData.firstName}
                    />
                    <TextField
                        sx={{ margin: '6px' }}
                        required
                        fullWidth
                        type="password"
                        placeholder="passWord"
                        name="passWord"
                        onChange={HandleInput}
                        value={formData.passWord}
                    />
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                    >
                        Sign In
                    </Button>
                </Box>
            </Box>
        </Container>

    );
}

export default LoginPage;