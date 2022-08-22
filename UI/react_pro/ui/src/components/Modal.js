
import Button from '@mui/material/Button';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from '@mui/material/Typography';
import Dialog from '@mui/material/Dialog';

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
    console.log(modalData.attributeKeys)




    return (
        <div>

<Dialog
        open={modalData.openModal}
        onClose={closeModal}
        maxWidth="xl"
        aria-labelledby="scroll-dialog-title"
        aria-describedby="scroll-dialog-description"
      >
                <Typography variant="h2" gutterBottom >
                    Returned Result:
                </Typography>

                <TableContainer>
                    <Table aria-label="collapsible table">
                        <TableHead>
                            <TableRow>
                                {modalData.attributeKeys.map((item) => {
                                    return (<TableCell>{item}</TableCell>);
                                })}
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {modalData.attributeData.map((item) => {
                                console.log(Object.values(item))

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

            </Dialog>
        </div>


    );
}




export default Modal;