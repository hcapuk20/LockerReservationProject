import React from 'react';
import axios from 'axios';

export default class SourceGroupList extends React.Component {
    state = {
        sourceGroups: []
    }
    componentDidMount() {
        axios.get('https://localhost:7125/api/source/getAllSourceGroups')
            // https://localhost:7125/api/source/getAllSourceGroups
            .then(res => {
                const sourceGroups = res.data;
                this.setState({ sourceGroups });
            })
    }

    render() {
        return (
            <ul>
                {
                    this.state.sourceGroups
                        .map(sourceGroup =>
                            <li key={sourceGroup.id}>ID: {sourceGroup.id} Name: {sourceGroup.name} Capacity: {sourceGroup.capacity}</li>
                        )
                }
            </ul>
        )
    }
}
