
//import useAuth from "../auth/useAuth";
import { useNavigate, useLocation } from "react-router-dom";
import classes from './ContentsPage.module.css'
import InnerTable from "../components/InnerTable";
import { useState } from "react";

function ContentsPage() {
    const location = useLocation();
    const navigate = useNavigate();

    // const navigate = useNavigate();
    //  const from = location.state?.from?.pathname || "/navigationpage";
    let arrayWithData = [];

    arrayWithData = location.state.arr;


    let stateArray = new Array(arrayWithData.length);
    stateArray.fill(false);


    const [showInnerTable, setShowInnerTable] = useState(stateArray)
    if (arrayWithData.length === 0) {
        return (
            <div>
                <h1>no data found</h1>
                <button onClick={()=>{navigate("/admin")}}> return back</button>
            </div>);
    }
    const attrArray = Object.keys(arrayWithData[0]);
    attrArray.push('actions');


    const [showInnerTable, setShowInnerTable] = useState(stateArray)

    function showContent(change_index) {
        console.log(change_index)
        const newState = showInnerTable.map((value, index) => {
            if (index === change_index) {
                console.log("changed")
                return (!value);
            }
            return value;
        });
        setShowInnerTable(newState);
    }

    function findAttributes(item) {   //buraları değiştir

        const attr = []
        for (const [, value] of Object.entries(item)) {
            attr.push(value)
        }
        return (attr.map(item => {
            return (<td>{item}</td>)
        })
        )
    }

    return (
        <div>
            <table className={classes.table}>
                <thead >
                    <tr >
                        {attrArray.map((item, index) => {
                            return (
                                <th key={index} >{item}</th>
                            );
                        })}
                    </tr>
                </thead>
                <tbody>
                    {arrayWithData.map((item, index) => {
                        return (
                            <div>
                                <tr>
                                    <td key={item.id}>
                                        {//key kısmını değiştir!
                                            findAttributes(item)
                                        }
                                    </td>
                                    <td>
                                        <button onClick={() => showContent(index)} >show content</button>
                                    </td>
                                </tr>
                                {
                                    (showInnerTable[index]) &&
                                    (<div style={{ position: 'relative', left: '30px'  }}>
                                        <InnerTable item_id={item.id} />
                                    </div>)
                                }
                            </div>

                        );
                    })}


                </tbody>

            </table>
            <br />
            <button onClick={() => { navigate("/admin"); }}  >  return to admin page </button>
            <br />

        </div>
    );
}


export default ContentsPage;
