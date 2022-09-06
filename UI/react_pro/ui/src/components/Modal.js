
import Button from '@mui/material/Button';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from '@mui/material/Typography';
import Dialog from '@mui/material/Dialog';
import Box from '@mui/material/Box';
function Modal({ modalData, closeModal }) {

    function convertToString(value) {

        if (value.length === 0) {
            return ('[]');
        }

        if (typeof value[0] !== 'object') {

            return (toString(value))
        }


        return JSON.stringify(value);;



    }

    if (!modalData.openModal) {
        return
    }





    return (
        <Box   >

            <Dialog
                
                open={modalData.openModal}
                onClose={closeModal}
                aria-labelledby="scroll-dialog-title"
               
            >
                <Box sx= {{width : 700}}>
                <Typography variant="h3"  >
                    Returned Result:
                </Typography>

                <TableContainer >
                    <Table >
                        <TableHead>
                            <TableRow>
                                {modalData.attributeKeys.map((item) => {
                                    return (<TableCell>{item}</TableCell>);
                                })}
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {modalData.attributeData.map((item) => {
                            
                                return (
                                    <TableRow>
                                        {Object.values(item).map((value) => {
                                            if (!Array.isArray(value)) {
                                                return (<TableCell>{value}</TableCell>);
                                            } else {



                                                return (<td>{convertToString(value)}</td>);
                                            }

                                        })}
                                    </TableRow>
                                );
                            })

                            }
                        </TableBody>
                    </Table >
                </TableContainer>
                <Button variant="outlined" sx={{ width: 300, margin: 5 }} onClick={closeModal} >
                    Close Modal
                </Button>
                </Box>
            </Dialog>
        </Box>


    );
}




export default Modal;