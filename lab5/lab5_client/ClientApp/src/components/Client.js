import React, { Component } from 'react';

export class Client extends Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            edit: props.hasOwnProperty("edit") ? props.edit : false,
            client: {}
        };
        this.edit = this.edit.bind(this);
        this.save = this.save.bind(this);
        this.cancel = this.cancel.bind(this);
        this.delete = this.delete.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    componentDidMount() {
        this.setState({ loading: true });
        let client = this.props.match.params.client;
        fetch("https://localhost:44341/api/clients/" + client, { method: "GET", data: { id: 1 } }).then(response => response.json())
            .then(data => {
                this.setState({
                    loading: false,
                    client: data
                })
            }).catch(e => {
                this.setState({
                    loading: false,
                    error: true
                })
            });
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;
        this.state.client[name] = value;
    }


    edit() {
        this.setState({ edit: true });
    }
    cancel() {
        this.setState({ edit: false });
    }
    save() {
        fetch("https://localhost:44341/api/clients/" + this.state.client.id, {
            method: "put",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(
                this.state.client
            ),
        });
        this.setState({ edit: false });
    }
    delete() {
        fetch("https://localhost:44341/api/clients/" + this.state.client.id, { method: "delete" });
        window.location.href = "/clients";
    }

    render() {
        if (this.state.loading) {
            return (
                <div>
                    Загрузка...
                </div>
            );
        } else if (!this.state.edit) {
            let client = this.state.client;
            return (
                <table className="table">
                    <thead>
                        <tr><th></th><th>Имя</th><th>Возраст</th></tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <button type="button" onClick={this.edit} className="btn btn-default" aria-label="Left Align">
                                    <span className="fas fa-edit" aria-hidden="true"></span>
                                </button>
                                <button type="button" onClick={this.delete} className="btn btn-default" aria-label="Left Align">
                                    <span className="fas fa-trash-alt" aria-hidden="true"></span>
                                </button>
                            </td>
                            <td>{client.name}</td>
                            <td>{client.age}</td>
                        </tr>
                    </tbody>
                </table>
            );
        } else {
            let client = this.state.client;
            return (
                <form id="edit">
                    <label>Имя <input onChange={this.handleInputChange} name="name" type="text" defaultValue={client.name} /></label><br />
                    <label>Возраст <input onChange={this.handleInputChange} name="age" type="text" defaultValue={client.age} /></label><br />
                    <input type="button" onClick={this.save} value="Сохранить" className="btn " />
                    <input type="button" onClick={this.cancel} value="Отменить" className="btn" />
                </form>
                );
        }
    }
}
