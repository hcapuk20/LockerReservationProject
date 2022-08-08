import React from 'react';
import axios from 'axios';

export default class SourceList extends React.Component {
    state = {
        sources: []
    }
    componentDidMount() {
        axios.get('https://localhost:7125/api/source/getAllSources')
            // https://localhost:7125/api/source/getAllSources
            .then(res => {
                const sources = res.data;
                this.setState({ sources });
            })
    }

    render() {
        return (
            <ul>
                {
                    this.state.sources
                        .map(source =>
                            <li key={source.id}>ID: {source.id} Source Group: {source.sourceGroupId}</li>
                        )
                }
            </ul>
        )
    }
}
