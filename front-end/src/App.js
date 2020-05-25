import React from "react";
import "./assets/main.scss";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import { Container } from "react-bootstrap";
import ImageUploadContainer from "./containers/ImageUploadContainer";
import StatsContainer from "./containers/StatsContainer";
import Navigation from "./components/Navigation";

const App = () => {
  return (
    <Container className="main">
      <BrowserRouter>
        <Navigation />
        <Switch>
          <Route exact path="/" component={() => <ImageUploadContainer />} />
          <Route
            exact
            path="/statystyki"
            component={() => <StatsContainer />}
          />
        </Switch>
      </BrowserRouter>
    </Container>
  );
};

export default App;
