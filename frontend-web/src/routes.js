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
import Dashboard from "views/Dashboard.jsx";

import AmigosIndex from "views/Amigos/index.jsx";
import AmigosEditar from "views/Amigos/editar.jsx";
import JogosIndex from "views/Jogos/index.jsx";
import JogosEditar from "views/Jogos/editar.jsx";

const dashboardRoutes = [
  {
    path: "/dashboard",
    name: "Dashboard",
    icon: "pe-7s-graph",
    component: Dashboard,
    layout: "/admin",
    visible: true
  },
  {
    path: "/amigos",
    name: "Amigos",
    icon: "fa fa-user",
    component: AmigosIndex,
    layout: "/admin",
    visible: true
  },
  {
    path: "/amigos-editar/",
    name: "Amigos Editar",
    icon: "fa fa-user",
    component: AmigosEditar,
    layout: "/admin",
    visible: false
  },
  {
    path: "/jogos",
    name: "Jogos",
    icon: "fa fa-play",
    component: JogosIndex,
    layout: "/admin",
    visible: true
  },
  {
    path: "/jogos-editar/",
    name: "Jogos Editar",
    icon: "fa fa-play",
    component: JogosEditar,
    layout: "/admin",
    visible: false
  },
];

export default dashboardRoutes;
