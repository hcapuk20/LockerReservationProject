
const MODAL_STYLES = {
    position: 'fixed',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    backgroundColor: '#FFF',
    padding: '300px',
    zIndex: 1000
}

const OVERLAY_STYLES = {
    position: 'fixed',
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    backgroundColor: 'rgba(0, 0, 0, .7)',
    zIndex: 1000
}



function Modal({ modelData, closeModal }) {

    function convertToString(value){

        if (value.length === 0) {
            return ('[]');
        }

        if(typeof value[0]  !== 'object'){
            
            return (toString(value))
        }


        return JSON.stringify(value);;



    }

    if (!modelData.openModal) {
        return
    }
    console.log(modelData.attributeKeys)
    console.log("modelData.keys")
    console.log(modelData.attributeData)
    console.log("modelData.attributeData")
    return (
        <>
            <div style={OVERLAY_STYLES} onClick = {closeModal}/>
            <div style={MODAL_STYLES}>
                <h2>Returned Result:</h2>
                <table>
                    <thead>
                        <tr>
                            {modelData.attributeKeys.map((item) => {
                                return (<th>{item}</th>);
                            })}
                        </tr>
                    </thead>
                    <br></br>
                    <tbody>
                        {modelData.attributeData.map((item) => {
                            console.log(Object.values(item))

                            return (
                                <tr>
                                    {Object.values(item).map(( value) => {
                                       if (!Array.isArray(value)){
                                            return (<td>{value}</td>);
                                        }else{



                                            return (<td>{convertToString(value)}</td>);
                                        }

                                    })}
                                </tr>
                            );
                        })

                        }
                    </tbody>
                </table>

                <button onClick={closeModal}>close modal</button>
            </div>
        </>


    );
}




export default Modal;