import React from "react";
import { Nav } from "react-bootstrap";
import { NavLink } from "react-router-dom";

const Navigation = () => {
  return (
    <Nav defaultActiveKey="/">
      <Nav.Item>
        <NavLink exact to="/">
          Strona główna
        </NavLink>
      </Nav.Item>
      <Nav.Item>
        <NavLink to="/statystyki">Statystki</NavLink>
      </Nav.Item>
    </Nav>
  );
};

export default Navigation;
