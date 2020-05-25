import React from "react";
import { Nav } from "react-bootstrap";
import { Link } from "react-router-dom";

const Navigation = () => {
  return (
    <Nav defaultActiveKey="/">
      <Nav.Item>
        <Link to="/">Strona główna</Link>
      </Nav.Item>
      <Nav.Item>
        <Link to="/statystyki">Statystki</Link>
      </Nav.Item>
    </Nav>
  );
};

export default Navigation;
