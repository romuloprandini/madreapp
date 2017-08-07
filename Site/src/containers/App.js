import _ from 'lodash'
import React, { Component } from 'react'
import { connect } from 'react-redux'
import PropTypes from 'prop-types'
import { browserHistory } from 'react-router'
import Breadcrumbs from 'react-breadcrumbs'
import NotificationSystem from 'react-notification-system'

import Header from '../components/Header'
import Sidebar from '../components/Sidebar'
import Aside from '../components/Aside'
import Footer from '../components/Footer'

class App extends Component {
  static propTypes = {
    // Injected by React Router
    children: PropTypes.node
  }
  
  componentWillReceiveProps(nextProps)
  {
    if(!_.isEmpty(nextProps.message)) {     
    this.refs.notificationSystem.addNotification({
        title: nextProps.message.title,
        message: nextProps.message.message,
        level: nextProps.message.type,
        position: 'tr'
      })
    }
  }

  render() {
    const { children } = this.props
    return (
        <div className="app">
        <NotificationSystem ref="notificationSystem" />
        <Header />
        <div className="app-body">
          <Sidebar {...this.props}/>
          <main className="main">
            <Breadcrumbs
               wrapperElement="ol"
               wrapperClass="breadcrumb"
               itemClass="breadcrumb-item"
               separator=""
               routes={this.props.routes}
               params={this.props.params}
             />
            <div className="container-fluid">
                {children}
            </div>
          </main>
          <Aside />
        </div>
        <Footer />
      </div>
    )
  }
}


const mapStateToProps = (state) => {
  return { message: state.message }
}

export default connect(mapStateToProps)(App);