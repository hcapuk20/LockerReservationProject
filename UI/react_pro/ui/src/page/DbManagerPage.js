//import axios from "../api/axios";
//import useAuth from "../auth/useAuth";
// add delete update employee 
// add delete update source group
// add delete update source
// can an employee multiple sources from the same source group

import { useState } from "react";
import axios from "../api/axios";
import Modal from "../components/Modal";
//const { auth } = useAuth();
//const user = auth.user;
//console.log(auth)
const sytles1 = {
        position: 'relative',
        zIndex: 1
}

function DbManagerPage() {

        const [modelData, setModalData] = useState({ openModal: false, attributeKeys: "", attributeData: "" });

        const [operations, setOperations] = useState([
                {
                        functionName: "GetAllSources",
                        params: {},
                        type: "get",
                        url: "https://localhost:7125/api/Source/getAllSources"
                },
                {
                        functionName: "AddSource",
                        params: { id: "", sg_id: "" },
                        type: "post",
                        url: "https://localhost:7125/api/Source/addSource?"
                },
                {
                        functionName: "AddEmployee",
                        params: { employee_id: "", password: "", name: "" },
                        type: "post",
                        url: "https://localhost:7125/api/Source/addEmployee?"
                },
                {
                        functionName: "GetAllEmployees",
                        params: {},
                        type: "get",
                        url: "https://localhost:7125/api/Source/getAllEmployees"
                },
                {
                        functionName: "GetEmployeeById",
                        params: { employee_id: "" },
                        type: "get",
                        url: "https://localhost:7125/api/Source/getEmployeeById?"
                },
                {
                        functionName: "RemoveSource",
                        params: { source_id: "" },
                        type: "delete",
                        url: "https://localhost:7125/api/Source/removeSource?"
                },
                {
                        functionName: "RemoveEmployee",
                        params: { employee_id: "" },
                        type: "delete",
                        url: "https://localhost:7125/api/Source/removeEmployee?"
                },
                {
                        functionName: "GetAllSourceGroups",
                        params: {},
                        type: "get",
                        url: "https://localhost:7125/api/Source/getAllSourceGroups"
                },
                {
                        functionName: "AddSourceGroup",
                        params: { sourceGroup_id: "", name: "", cap: "" },
                        type: "post",
                        url: "https://localhost:7125/api/Source/addSourceGroup?"
                },
                {
                        functionName: "AddRelationship",
                        params: { employee_id: "", source_id: "" },
                        type: "post",
                        url: "https://localhost:7125/api/Source/addRelationship?"
                },
                {
                        functionName: "GetSourcesOfEmployee",
                        params: { employee_id: "" },
                        type: "get",
                        url: "https://localhost:7125/api/Source/getSourcesOfEmployee?"
                },
                {
                        functionName: "GetOwnersOfSource",
                        params: { source_id: "" },
                        type: "get",
                        url: "https://localhost:7125/api/Source/getOwnersOfSource?"
                },
                {
                        functionName: "RemoveSourceGroup",
                        params: { source_group_id: "" },
                        type: "delete",
                        url: "https://localhost:7125/api/Source/removeSourceGroup?"
                },
                {
                        functionName: "RemoveRelationship",
                        params: { employee_id: "", source_id: "" },
                        type: "delete",
                        url: "https://localhost:7125/api/Source/removeRelationship?"
                },
                {
                        functionName: "AddAdministration",
                        params: { employee_id: "", sg_id: "" },
                        type: "post",
                        url: "https://localhost:7125/api/Source/addAdministration?"
                },
                {
                        functionName: "GetSourcesByGroup",
                        params: { group_id: "" },
                        type: "get",
                        url: "https://localhost:7125/api/Source/getSourcesByGroup?"
                },
                {
                        functionName: "UpdateEmployee",
                        params: {},
                        type: "put",
                        url: ""
                        //?????
                },
                {
                        functionName: "UpdateSourceGroup",
                        params: { sg_id: "", former_id: "", emp_id: "" },
                        type: "put",
                        url: "https://localhost:7125/api/Source/updateSourceGroup?"
                },
                {
                        functionName: "UpdateRelationship",
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
                console.log(name)
                console.log(value)
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
                console.log(Object.keys(params).length)
                console.log((url))
                return await axios[operations[position].type](url).catch((err)=>{ return err});
        }



        function handleSubmit(event, componentIndex) {
                event.preventDefault();
                const position = componentIndex;
                console.log(position)
                if (operations[position].type === "get") {  //here return some kind of data
                      
                                getData(position).then((response) => {
                                        console.log("response.data.data")
                                        console.log(response.data.data)
                                        console.log("response.data.data")

                                        if (!response.data.data) {
                                                alert("no record found!")
                                        }
                                        let foundData = response.data.data;

                                        if (!Array.isArray(foundData)) {
                                                let arr = [];
                                                arr.push(foundData);
                                                foundData = arr;
                                        }
                                        console.log(foundData)

                                        setModalData({
                                                openModal: true,
                                                attributeKeys: Object.keys(foundData[0]),
                                                attributeData: foundData
                                        });

                                }).catch(()=>{alert("unkown error"); return;})


                } else {
                                getData(position).then((response) => {
                                        alert(response.data.message)
                                }).catch(()=>{alert("unkown error"); return;})
                }
        }


        return (
                <div>
                        <div style={sytles1}>
                                {console.log(operations)}
                                {console.log("here")}
                                {
                                        operations.map((item, componentIndex) => {
                                                return (
                                                        <div>
                                                                <h2> {item.functionName}</h2>
                                                                <>
                                                                        {Object.keys(item.params).map((parameter, index) => {

                                                                                return (<form>
                                                                                        <input
                                                                                                type="text"
                                                                                                placeholder={parameter}
                                                                                                name={parameter}
                                                                                                onChange={(event) => handleInput(event, componentIndex)}
                                                                                                value={item.params[index]}>
                                                                                        </input>
                                                                                </form>
                                                                                );
                                                                        })}

                                                                        <button onClick={(event) => { handleSubmit(event, componentIndex) }}>submit</button>

                                                                </>
                                                        </div>
                                                );
                                        })
                                }
                        </div>
                        <Modal modelData={modelData} closeModal={closeModal} ></Modal>
                </div>
        )


}
export default DbManagerPage;