import React, { useState } from 'react';
import logo from './logo.svg';
import 'bootstrap/dist/css/bootstrap.min.css'
import './App.css';
import {Container, Row, Col} from 'react-bootstrap';



function App() {
  const [titleStyle, setTitleStyle] = useState("");
  return (    
    
    <Container>
      <Row>
        <p style={{font: titleStyle}}>CatFinder</p>
      </Row>
      
      <Row>
        <button onClick={()=>setTitleStyle("bold 30px Comic Sans MS, serif")}>
          Comic Sans-inator
        </button>
      </Row>          
         
    </Container>
  );
}

export default App;
