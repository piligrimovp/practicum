import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class Clients extends Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            add: false,
            clients: [],
            new_client: {}
        };
        this.delete = this.delete.bind(this);
        this.add = this.add.bind(this);
        this.add_add = this.add_add.bind(this);
        this.cancel = this.cancel.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    delete(id) {
        fetch("https://localhost:44341/api/clients/" + id, { method: "delete" })
            .then(response => response.json())
            .then(data => {
                this.setState({ clients: this.state.clients.filter(n => n.id !== data.id) })
            });
    }

    add() {
        this.setState({ add: true, new_client: {} });
    }

    cancel() {
        this.setState({ add: false });
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;
        this.state.new_client[name] = value;
    }

    add_add() {
        fetch("https://localhost:44341/api/clients/", {
            method: "post",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(
                this.state.new_client
            )
        }).then(response => response.json())
            .then(data => {
                this.setState({ clients: [...this.state.clients, data], add: false })
            });
    }

    componentDidMount() {
        this.setState({ loading: true });
        fetch("https://localhost:44341/api/clients").then(response => response.json())
            .then(data => {
                this.setState({
                    loading: false,
                    clients: data
                })
            }).catch(e => {
                this.setState({
                    loading: false,
                    error: true
                })
            });
    }

    render() {
        if (this.state.loading) {
            return (
                <div>
                    Загрузка...
                </div>
            );
        } else if (!this.state.add) {
            return (
                <>
                <button type="button" onClick={this.add} className="btn btn-default" aria-label="Left Align">
                    <span className="fas fa-plus" aria-hidden="true"></span>
                </button>
                <table className="table">
                    <thead>
                        <tr><th colSpan='2'></th><th>Имя</th><th>Возраст</th></tr>
                    </thead>
                    <tbody>
                        {this.state.clients.map((client, index) => {
                            return (
                                <tr key={index}>
                                    <td>
                                        <Link to={"/clients/" + client.id}>Подробнее</Link>
                                    </td>
                                    <td>
                                        <button type="button" onClick={() => this.delete(client.id)} className="btn btn-default" aria-label="Left Align">
                                            <span className="fas fa-trash-alt" aria-hidden="true"></span>
                                        </button>
                                    </td>
                                    <td>{client.name}</td><td>{client.age}</td></tr>
                            );
                        })}
                    </tbody>
                    </table>
                    </>
            );
        } else {
            return (
                <form id="edit">
                    <label>Имя <input onChange={this.handleInputChange} name="name" type="text" /></label><br />
                    <label>Возраст <input onChange={this.handleInputChange} name="age" type="text" /></label><br />
                    <input type="button" onClick={this.add_add} value="Добавить" className="btn" />
                    <input type="button" onClick={this.cancel} value="Отменить" className="btn" />
                </form>
            )
        }
    }
}
