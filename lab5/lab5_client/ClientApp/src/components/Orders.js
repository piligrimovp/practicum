import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class Orders extends Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            add: false,
            orders: [],
            clients: [],
            services: []
        };
        this.delete = this.delete.bind(this);
        this.add = this.add.bind(this);
        this.add_add = this.add_add.bind(this);
        this.cancel = this.cancel.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    delete(id) {
        fetch("https://localhost:44341/api/orders/" + id, { method: "delete" })
            .then(response => response.json())
            .then(data => {
                this.setState({ orders: this.state.orders.filter(n => n.id !== data.id) })
            });
    }

    add() {
        this.setState({ add: true });
    }

    cancel() {
        this.setState({ add: false });
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;
        this.state.new_order[name] = value;
    }

    add_add() { 
        fetch("https://localhost:44341/api/orders", {
            method: "post",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                clientid: this.state.new_order.clientid
            })
        }).then(response => response.json())
            .then(data => {
                this.state.new_order['orderid'] = data.id;
                this.setState({ orders: [...this.state.orders, data] })
                console.log(this.state.orders)
                fetch("https://localhost:44341/api/orderservices", {
                    method: "post",
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(
                        this.state.new_order
                    )
                }).then(response => response.json())
                    .then(data => {
                        let index = this.state.orders.findIndex(x => x.id == data.orderId);
                        this.state.orders[index].services.push(data);
                        this.setState({ add: false });
                });
        });
    }

    componentDidMount() {
        this.setState({ loading: true, new_order: {} });
        fetch("https://localhost:44341/api/orders").then(response => response.json())
            .then(data => {
                this.setState({
                    loading: false,
                    orders: data
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
                this.state.new_order.clientid = data[0].id; 
            })
        fetch("https://localhost:44341/api/services").then(response => response.json())
            .then(data => {
                this.setState({
                    services: data
                });
                this.state.new_order.serviceid = data[0].id;
            })
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
                            <tr><th colSpan='2'></th><th>Клиент</th><th>Кол-во сервисов</th><th>Сумма</th></tr>
                        </thead>
                        <tbody>
                            {this.state.orders.map((order, index) => {
                                return (
                                    <tr key={index}>
                                        <td>
                                            <Link to={"/orders/" + order.id}>Подробнее</Link>
                                        </td>
                                        <td>
                                            <button type="button" onClick={() => this.delete(order.id)} className="btn btn-default" aria-label="Left Align">
                                                <span className="fas fa-trash-alt" aria-hidden="true"></span>
                                            </button>
                                        </td>
                                        <td>{order.client.name}</td>
                                        <td>{order.services.length}</td>
                                        <td>{order.services.reduce(function (p, c) { return p + (c.count * c.service.price)},0)}</td>
                                    </tr>
                                );
                            })}
                        </tbody>
                    </table>
                </>
            );
        } else {
            return (
                <form id="edit">
                    <label>
                        Клиент
                        <select name="clientid" onChange={this.handleInputChange}>
                            {this.state.clients.map((client, index) => {
                                return (
                                    <option key={index} value={client.id}>{client.name}</option>
                                    )
                            })}
                            </select>
                    </label><br/>
                    <label>
                        Сервис
                        <select name="serviceid" onChange={this.handleInputChange}>
                            {this.state.services.map((service, index) => {
                                return (
                                    <option key={index} value={service.id}>{service.name}</option>
                                )
                            })}
                        </select>
                    </label><br/>
                    <label>Количество <input onChange={this.handleInputChange} name="count" type="text" /></label><br />
                    <label>Дата <input onChange={this.handleInputChange} name="date" type="date" /></label><br />
                    <input type="button" onClick={this.add_add} value="Добавить" className="btn" />
                    <input type="button" onClick={this.cancel} value="Отменить" className="btn" />
                </form>
            )
        }
    }
}
