import { useState } from "react";
import axios from "../api/axios";
import Modal from "../components/Modal";
import { useNavigate } from "react-router-dom";
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import Checklist from "../components/Checklist";
import Navbar from "../components/Navbar";



function DbManagerPage() {

        const navigate = useNavigate();

        const [modalData, setModalData] = useState({ openModal: false, attributeKeys: "", attributeData: "" });
        const [closeChecklist, setCloseChecklist] = useState(false)
        const [addedGroups, setAddedGroup] = useState([]);

        const [operations, setOperations] = useState([
                {
                        functionName: "Get All Sources",
                        params: {},
                        type: "get",
                        url: "https://localhost:7125/api/Source/getAllSources"
                },
                {
                        functionName: "Add Source",
                        params: { id: "", sg_id: "" },
                        type: "post",
                        url: "https://localhost:7125/api/Source/addSource?"
                },
                {
                        functionName: "Add Employee",
                        params: { employee_id: "", password: "", name: "", role: "" },
                        type: "post",
                        url: "https://localhost:7125/api/Source/addEmployee?"
                },
                {
                        functionName: "Get All Employees",
                        params: {},
                        type: "get",
                        url: "https://localhost:7125/api/Source/getAllEmployees"
                },
                {
                        functionName: "Get Employee By Id",
                        params: { employee_id: "" },
                        type: "get",
                        url: "https://localhost:7125/api/Source/getEmployeeById?"
                },
                {
                        functionName: "Remove Source",
                        params: { source_id: "" },
                        type: "delete",
                        url: "https://localhost:7125/api/Source/removeSource?"
                },
                {
                        functionName: "Remove Employee",
                        params: { employee_id: "" },
                        type: "delete",
                        url: "https://localhost:7125/api/Source/removeEmployee?"
                },
                {
                        functionName: "Get All Source Groups",
                        params: {},
                        type: "get",
                        url: "https://localhost:7125/api/Source/getAllSourceGroups"
                },
                {
                        functionName: "Add Source Group",
                        params: { sourceGroup_id: "", name: "", cap: "" },
                        type: "post",
                        url: "https://localhost:7125/api/Source/addSourceGroup?"
                },
                {
                        functionName: "Assign Source To Employee",
                        params: { employee_id: "", source_id: "" },
                        type: "post",
                        url: "https://localhost:7125/api/Source/addRelationship?"
                },
                {
                        functionName: "Get Sources Of Employee",
                        params: { employee_id: "" },
                        type: "get",
                        url: "https://localhost:7125/api/Source/getSourcesOfEmployee?"
                },
                {
                        functionName: "Get Owners Of Source",
                        params: { source_id: "" },
                        type: "get",
                        url: "https://localhost:7125/api/Source/getOwnersOfSource?"
                },
                {
                        functionName: "Remove Source Group",
                        params: { source_group_id: "" },
                        type: "delete",
                        url: "https://localhost:7125/api/Source/removeSourceGroup?"
                },
                {
                        functionName: "Remove Source From Employee",
                        params: { employee_id: "", source_id: "" },
                        type: "delete",
                        url: "https://localhost:7125/api/Source/removeRelationship?"
                },
                {
                        functionName: "Add Administration",
                        params: { employee_id: "", sg_id: "" },
                        type: "post",
                        url: "https://localhost:7125/api/Source/addAdministration?"
                },
                {
                        functionName: "Get Sources By Group",
                        params: { group_id: "" },
                        type: "get",
                        url: "https://localhost:7125/api/Source/getSourcesByGroup?"
                },
                {
                        functionName: "Update Source Group",
                        params: { sg_id: "", former_id: "", emp_id: "" },
                        type: "put",
                        url: "https://localhost:7125/api/Source/updateSourceGroup?"
                },
                {
                        functionName: "Update Relationship",
                        params: { emp_id: "", source_id: "", new_source_id: "" },
                        type: "put",
                        url: "https://localhost:7125/api/Source/updateRelationship?"
                },

        ])
        function closeModal() {
                setModalData({ openModal: false, attributeKeys: "", attributeData: "" })
        }

        function handleInput(event, componentIndex) {
                const { name, value } = event.target
                const position = componentIndex;

                setOperations(() => {
                        let arr = []
                        operations.forEach(
                                (item, index) => {
                                        if (index === position) {
                                                item.params[name] = value
                                                arr.push(item)
                                        } else {
                                                arr.push(item)
                                        }
                                }
                        )
                        return arr;
                }
                );

        }
        async function getData(position) {
                let url = operations[position].url;
                let params = operations[position].params;
                if (Object.keys(params).length !== 0) {

                        Object.keys(params).map((item) => {
                                url = url.concat(item, "=", (params[item]), "&");
                                return 0;
                        }
                        )
                        url = url.slice(0, url.length - 1)

                }
                return await axios[operations[position].type](url)
        }



        function handleSubmit(event, componentIndex) {
                event.preventDefault();
                const position = componentIndex;
                if (operations[position].type === "get") {  //here return some kind of data

                        getData(position).then((response) => {

                                if (!response.data.data) {
                                        alert("no record found!")
                                }
                                let foundData = response.data.data;

                                if (!Array.isArray(foundData)) {
                                        let arr = [];
                                        arr.push(foundData);
                                        foundData = arr;
                                }
                           

                                setModalData({
                                        openModal: true,
                                        attributeKeys: Object.keys(foundData[0]),
                                        attributeData: foundData
                                });

                        }).catch((err) => { alert(err); return; })


                } else {
                        getData(position).then((response) => {
                                alert(response.data.message)
                                console.log(response.data)
                        }).catch((err) => {
                                if (err.response) {
                                        alert("unkown error");
                                } else if (err.request) {
                                        alert("error: no response received");
                                } else {
                                        alert("unkown error");
                                }

                                return;
                        })
                }
        }
        async function handleEmployeeSubmit(arr) {
                let position;
                operations.forEach((element, index) => {

                        if (element.functionName === 'Add Employee') {
                                position = index;
                        }
                }
                );
                let params = operations[position].params
           
                let url = operations[position].url;
                if (Object.keys(params).length !== 0) {
                        Object.keys(params).map((item) => {
                                url = url.concat(item, "=", (params[item]), "&");
                                return 0;
                        }
                        )
                        url = url.slice(0, url.length - 1)
                }

                await axios({
                        method: operations[position].type,
                        url: url,
                        data: arr
                }).then(function (response) {
                       
                        if (response.data.statusCode === 200) {   //buna bak bir ara
                                setAddedGroup([])
                                setCloseChecklist(false)
                                alert("employee already exists!")
                        }
                        let addedSourceGroups = response.data.data.sources;  // returned array might be empty
                        
                        if (addedSourceGroups.length !== 0) {
                                setAddedGroup(addedSourceGroups)
                        } else {
                                setAddedGroup(["-1"])
                        }


                }).catch(function (error) {
                        alert("unkown error!")
                        console.log(error);
                });

        }
        return (
                <>
                        <Navbar landingPage={"DbManagerPage"} />
                        <Container component="main" >
                                <Box sx={{
                                        marginTop: 8,
                                        display: 'flex',
                                        flexDirection: 'column',
                                        alignItems: 'center',
                                }}>
                                        {
                                                operations.map((item, componentIndex) => {
                                                        return (
                                                                <>
                                                                        <Typography variant="h3" gutterBottom sx={{ margin: 5 }} >
                                                                                {item.functionName}
                                                                        </Typography>
                                                                        <Box sx={{

                                                                                display: 'flex',
                                                                                flexDirection: 'row',
                                                                                alignItems: 'center',
                                                                        }}>
                                                                                {Object.keys(item.params).map((parameter, index) => {

                                                                                        return (
                                                                                                <TextField
                                                                                                        required
                                                                                                        fullwidth
                                                                                                        type="text"
                                                                                                        sx={{ mt: 2, mb: 1 ,bgcolor: '#FFFFFF'}}
                                                                                                        placeholder={parameter}
                                                                                                        name={parameter}
                                                                                                        onChange={(event) =>
                                                                                                                handleInput(event, componentIndex)
                                                                                                        }

                                                                                                        value={item.params[index]}
                                                                                                />
                                                                                        );
                                                                                })}
                                                                        </Box>
                                                                        <Button
                                                                                type="submit"
                                                                                maxWidth="lg"
                                                                                size="large"
                                                                                variant="contained"
                                                                                sx={{ mt: 2, mb: 1 }}
                                                                                onClick={(event) => {
                                                                                        operations[componentIndex].functionName === 'Add Employee' ?
                                                                                                setCloseChecklist(true) :
                                                                                                handleSubmit(event, componentIndex)

                                                                                }}
                                                                        >
                                                                                Submit
                                                                        </Button>
                                                                </>
                                                        );
                                                })
                                        }

                                        <Checklist
                                                closeChecklist={closeChecklist}
                                                setCloseChecklist={setCloseChecklist}
                                                handleEmployeeSubmit={handleEmployeeSubmit}
                                                addedGroups={addedGroups}
                                                setAddedGroup={setAddedGroup}
                                        >
                                        </Checklist>
                                        <Modal modalData={modalData} closeModal={closeModal} ></Modal>
                                        <Button
                                                type="submit"
                                                maxWidth="lg"
                                                size="large"
                                                variant="contained"
                                                sx={{ mt: 2, mb: 1 }}
                                                onClick={() => { navigate("/navigationpage"); }}
                                        >
                                                return back
                                        </Button>
                                </Box>
                        </Container>
                </>
        )


}
export default DbManagerPage;