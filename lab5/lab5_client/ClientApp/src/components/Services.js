import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class Services extends Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            add: false,
            services: [],
            new_service: {}
        };
        this.delete = this.delete.bind(this);
        this.add = this.add.bind(this);
        this.add_add = this.add_add.bind(this);
        this.cancel = this.cancel.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    delete(id) {
        fetch("https://localhost:44341/api/services/" + id, { method: "delete" })
            .then(response => response.json())
            .then(data => {
                this.setState({ services: this.state.services.filter(n => n.id !== data.id) })
            });
    }

    add() {
        this.setState({ add: true, new_service: {} });
    }

    cancel() {
        this.setState({ add: false });
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;
        this.state.new_service[name] = value;
    }

    add_add() {
        fetch("https://localhost:44341/api/services/", {
            method: "post",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(
                this.state.new_service
            )
        }).then(response => response.json())
            .then(data => {
                this.setState({ services: [...this.state.services, data], add: false })
            });
    }

    componentDidMount() {
        this.setState({ loading: true });
        fetch("https://localhost:44341/api/services").then(response => response.json())
            .then(data => {
                this.setState({
                    loading: false,
                    services: data
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
                            <tr><th colSpan='2'></th><th>Название</th><th>Цена</th></tr>
                        </thead>
                        <tbody>
                            {this.state.services.map((service, index) => {
                                return (
                                    <tr key={index}>
                                        <td>
                                            <Link to={"/services/" + service.id}>Подробнее</Link>
                                        </td>
                                        <td>
                                            <button type="button" onClick={() => this.delete(service.id)} className="btn btn-default" aria-label="Left Align">
                                                <span className="fas fa-trash-alt" aria-hidden="true"></span>
                                            </button>
                                        </td>
                                        <td>{service.name}</td><td>{service.price}</td></tr>
                                );
                            })}
                        </tbody>
                    </table>
                </>
            );
        } else {
            return (
                <form id="edit">
                    <label>Название <input onChange={this.handleInputChange} name="name" type="text" /></label><br />
                    <label>Цена <input onChange={this.handleInputChange} name="price" type="text" /></label><br />
                    <input type="button" onClick={this.add_add} value="Добавить" className="btn" />
                    <input type="button" onClick={this.cancel} value="Отменить" className="btn" />
                </form>
            )
        }
    }
}
