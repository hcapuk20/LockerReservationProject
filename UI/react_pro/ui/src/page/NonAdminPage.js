//import Form from "../components/Form";
import useAuth from "../auth/useAuth";
function NonAdminPage() {
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


    const { auth } = useAuth();
    const sources = auth.user.sources;
    let attributeNames;
    try {
        attributeNames = Object.keys(sources[0])
    } catch {
        return <h1>You do not have any sources.</h1>;
    }

    console.log(attributeNames)
    console.log(sources[0].id)






    return (
        <div>
        <h2>Owned Sources:</h2>
        <table>
            <thead>
                <tr>
                    {attributeNames.map((item) => {
                        return (<th>{item}</th>);
                    })}
                </tr>
            </thead>
            <br></br>
            <tbody>
                {sources.map((item) => {
                    return (<tr>{findAttributes(item)}</tr>);
                })
                }
            </tbody>
        </table>
        </div>
    );
}

export default NonAdminPage;