import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Clients } from './components/Clients';
import { Client } from './components/Client';

import './custom.css'
import { Home } from './components/Home';
import { Services } from './components/Services';
import { Orders } from './components/Orders';
import { Service } from './components/Service';
import { Order } from './components/Order';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/clients/:client' component={Client} />
            <Route exact path='/clients' component={Clients} />
            <Route path='/services/:service' component={Service} />
            <Route exact path='/services' component={Services} />
            <Route path='/orders/:order' component={Order} />
            <Route exact path='/orders' component={Orders} />
      </Layout>
    );
  }
}
