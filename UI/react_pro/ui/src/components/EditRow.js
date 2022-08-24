

import axios from '../api/axios';
import TextField from '@mui/material/TextField';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';

const React = require("react")


////inputlara type check yapman lazım!!!

function EditRow({ source_id, renderMethod }) {


    let attributesArr = [];
    attributesArr = ['id', 'name']
    const obj = {};

    for (const key of attributesArr) {
        obj[key] = " ";
    }

    const [formData, setFormData] = React.useState({ obj });
    const [message, setMessage] = React.useState(" ");



    function HandleInput(event) {
        setMessage("")
        console.log(event.target)
        const { name, value } = event.target
        console.log(name)
        console.log(value)
        setFormData((prevData) => {
            return {
                ...prevData,
                [name]: value
            }

        })

    }




    async function handleDelete(event) {
        event.preventDefault();
        let response;
        try {
            response = axios.delete(`https://localhost:7125/api/Source/removeRelationship?employee_id=${formData.id}&source_id=${source_id}`)

            setMessage((await response).data.message)
            renderMethod(render => !render);
        } catch (err) {
            setMessage("error")
        }
    }


    async function handleAdd(event) {
        event.preventDefault();
        let response;
        try {
            response = axios.post(`https://localhost:7125/api/Source/addRelationship?employee_id=${formData.id}&source_id=${source_id}`)
            setMessage((await response).data.message)
            renderMethod(render => !render);
        } catch (err) {
            setMessage("error")
        }
    }



    return (
        <div >
            <form>
                {message === '' ? <div></div> :
                    <Typography variant="h5" sx={{ margin: 2 }}>
                        {message}
                    </Typography>}
                <br></br>
                <Box sx={{

                    display: 'flex',
                    flexDirection: 'row',
                    alignItems: 'center',
                }}>
                    {attributesArr.map((item, index) => {


                        return (

                            <TextField
                                required
                                size="small"
                                type="text"
                                sx={{ mt: 2, mb: 1 }}
                                placeholder={item}
                                name={item}
                                onChange={HandleInput}
                                value={formData[index]}
                                key={index} //bak
                            />
                        );
                    })}
                </Box>
                <br></br>
                <br></br>
                <Box sx={{
                    display: 'flex',
                    flexDirection: 'row',
                    alignItems: 'center',
                }}>
                    <Button size="small" variant="outlined" sx={{ margin: 1 }} onClick={handleDelete}   >
                        remove employee
                    </Button>
                    <Button size="small" variant="outlined" sx={{ margin: 1 }} onClick={handleAdd}  >
                        add employee
                    </Button>
                </Box>
            </form>
            <br></br>
            <br></br>
        </div>

    );
}

export default EditRow;