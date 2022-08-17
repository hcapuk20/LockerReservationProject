
import axios from '../api/axios'
import { useEffect, useState } from 'react';
import EditRow from './EditRow';

function InnerTable({ item_id }) {
    const [data, setData] = useState();
    const [render, setRender] = useState(false);
    console.log(render)


    useEffect(() => {

        async function fetchData() {
            try {
                const response = await axios.get(`https://localhost:7125/api/Source/getOwnersOfSource?source_id=${item_id}`)

                setData(response.data.data)
            } catch (err) {
                console.log(err);
            }
        }
        fetchData();
    }, [item_id ,render]);

    function CreateTable({ arr }) {


        return (
        <div>
            <table >
                <thead>
                    <tr>
                        <th>id</th>
                        <th>name</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        {arr.map(item => {
                            return (
                                <tr>
                                    <td>{item.id}</td>
                                    <td>{item.name}</td>
                                </tr>
                            );
                        })}
                    </tr>

                </tbody>

            </table>
            <EditRow source_id = {item_id} renderMethod = {setRender} render = {render}/>
        </div>


        );


    }



    return (<div>
        {data ? (Object.keys(data).length !== 0 ? <CreateTable arr={data} /> : 

        (<div><div>Table is empty </div><EditRow source_id = {item_id} renderMethod = {setRender} render = {render}/></div>)) : 
        (<div><div>Table is empty </div><EditRow source_id = {item_id} renderMethod = {setRender} render = {render}/></div>)}
    </div>);


}

export default InnerTable;