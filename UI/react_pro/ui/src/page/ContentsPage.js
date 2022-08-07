
//import useAuth from "../auth/useAuth";
import { useNavigate, useLocation } from "react-router-dom";
import classes from './ContentsPage.module.css'


function ContentsPage() {
    const location = useLocation();
    const navigate = useNavigate();
    // const navigate = useNavigate();
    //  const from = location.state?.from?.pathname || "/navigationpage";
    let arrayWithData = [];
    arrayWithData = location.state.arr;
    let buttonSourceGroup = location.state.buttonSourceGroup;
    const attrArray = Object.keys(arrayWithData[0]);



    function findAttributes(item) {
        const attr = []
        for (const [, value] of Object.entries(item)) {
            if(Array.isArray(value)){
                attr.push("[")
                value.forEach(element => {
                    attr.push(element)
                });
                attr.push("]")
            }else{
            attr.push(value)
            }
        }
        return (attr.map(item => {
            return (<td>{item}</td>)
        })
        )
    }

    return (
        <div>
            <table className={classes.table}>
                <thead>
                    <tr>
                        {attrArray.map(item => {
                            return (
                                <td>{item}</td>
                            );
                        })}
                    </tr>
                </thead>
                <tbody>
                    {arrayWithData.map(item => {
                        return (
                            <tr key={item.employee_id}>
                                {
                                    findAttributes(item)
                                }
                            </tr>
                        );
                    })}
                </tbody>

            </table>
            <br />
            <button onClick={() => { navigate("/editpage", { state: { arr: attrArray ,buttonSourceGroup :buttonSourceGroup} }); }}  >   edit source group  </button>
            <button onClick={() => { navigate("/admin"); }}  >  return to admin page </button>
            <br />

        </div>
    );
}


export default ContentsPage;