import React, { Component } from 'react';

export class Order extends Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            edit: props.hasOwnProperty("edit") ? props.edit : false,
            order: {},
            clients: [],
            services: []
        };
        this.edit = this.edit.bind(this);
        this.save = this.save.bind(this);
        this.cancel = this.cancel.bind(this);
        this.delete = this.delete.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    componentDidMount() {
        this.setState({ loading: true });
        let order = this.props.match.params.order;
        fetch("https://localhost:44341/api/orders/" + order, { method: "GET"}).then(response => response.json())
            .then(data => {
                this.setState({
                    loading: false,
                    order: data
                })
            }).catch(e => {
                this.setState({
                    loading: false,
                    error: true
                })
            });
        fetch("https://localhost:44341/api/clients").then(response => response.json())
            .then(data => {
                this.setState({
                    clients: data
                });
            })
        fetch("https://localhost:44341/api/services").then(response => response.json())
            .then(data => {
                this.setState({
                    services: data
                });
            })
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;
        this.state.order[name] = value;
    }


    edit() {
        this.setState({ edit: true });
    }
    cancel() {
        this.setState({ edit: false });
    }
    save() {
        fetch("https://localhost:44341/api/orders/" + this.state.order.id, {
            method: "put",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(
                this.state.order
            ),
        });
        this.setState({ edit: false });
    }
    delete() {
        fetch("https://localhost:44341/api/orders/" + this.state.order.id, { method: "delete" });
        window.location.href = "/orders";
    }

    render() {
        if (this.state.loading || this.state.order.isEmpty()) {
            return (
                <div>
                    Загрузка...
                </div>
            );
        } else if (!this.state.edit) {
            let order = this.state.order;
            return (
                <>
                <table className="table">
                    <thead>
                        <tr><th></th><th>Клиент</th><th>Сумма</th></tr>
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
                            <td>{order.client.name}</td>
                            <td>{order.services.reduce(function (p, c) { return p + (c.count * c.service.price) }, 0)}</td>
                        </tr>
                    </tbody>
                    </table>
                    <table className="table">
                    <thead>
                        <tr><th></th><th>Сервис</th><th>Количество</th><th>Цена</th></tr>
                    </thead>
                    <tbody>
                        {order.services.map((service, index) => {
                            return (
                                <tr key={index}><td></td><td>{service.service.name}</td><td>{service.count}</td><td>{service.service.price}</td></tr>
                            )
                        })}
                    </tbody>
                    </table>
                    </>
            );
        } else {
            let order = this.state.order;
            return (
                <form id="edit">
                    <label>
                        Клиент
                        <select name="clientid" onChange={this.handleInputChange}>
                            {this.state.clients.map((client, index) => {
                                return (
                                    <option key={index} value={client.id} defaultChecked={client.id == order.client.id}>{client.name}</option>
                                )
                            })}
                        </select>
                    </label><br />
                    <label>
                        Сервис
                        <select name="serviceid" onChange={this.handleInputChange}>
                            {this.state.services.map((service, index) => {
                                return (
                                    <option key={index} value={service.id} defaultChecked={service.id == order.services[0].service.id}>{service.name}</option>
                                )
                            })}
                        </select>
                    </label><br />
                    <label>Количество <input onChange={this.handleInputChange} defaultValue={order.count} name="count" type="text" /></label><br />
                    <label>Дата <input onChange={this.handleInputChange} name="date" defaultValue={order.date} type="date" /></label><br />
                    <input type="button" onClick={this.save} value="Добавить" className="btn" />
                    <input type="button" onClick={this.cancel} value="Отменить" className="btn" />
                </form>
            );
        }
    }
}

Object.prototype.isEmpty = function () {
    for (var key in this) {
        if (this.hasOwnProperty(key))
            return false;
    }
    return true;
}
