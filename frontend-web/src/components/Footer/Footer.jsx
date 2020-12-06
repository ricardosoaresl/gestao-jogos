/*!

=========================================================
* Light Bootstrap Dashboard React - v1.3.0
=========================================================

* Product Page: https://www.creative-tim.com/product/light-bootstrap-dashboard-react
* Copyright 2019 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/light-bootstrap-dashboard-react/blob/master/LICENSE.md)

* Coded by Creative Tim

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/
import React, { Component } from "react";
import { Row, Col } from "react-bootstrap";

class Footer extends Component {
  render() {
    return (
      <footer className="footer">
        <Row>
          <Col>
            <p className="copyright pull-right" style={{ marginRight: 15 }}>
              &copy; {new Date().getFullYear()}{" "}
              <a href="javascrupt:;">
                Ricardo Soares
              </a> - Gestão Jogos
            </p>
          </Col>
        </Row>
      </footer>
    );
  }
}

export default Footer;
