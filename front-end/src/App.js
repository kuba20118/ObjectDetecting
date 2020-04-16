import React from "react";
import "./assets/main.scss";
import { BrowserRouter, Route } from "react-router-dom";
import { Container } from "react-bootstrap";
import ImageUploadContainer from "./containers/ImageUploadContainer";
import StatsContainer from "./containers/StatsContainer";
import Navigation from "./components/Navigation";

const App = () => {
  return (
    <Container className="main">
      <BrowserRouter>
        <Navigation />
        <Route exact path="/" component={ImageUploadContainer} />
        <Route path="/statystyki" component={StatsContainer} />
      </BrowserRouter>
    </Container>
  );
};

export default App;
