import React, { useState } from "react";
import { Form, Button, Col, Row } from "react-bootstrap";

const ReviewForm = ({ onSubmit }) => {
  const [isOk, setIsOk] = useState(true);
  const [undetectedObj, setUndetectedObj] = useState(0);
  const [wrongDetectedObj, setWrongDetectedObj] = useState(0);
  const [incorrectDetectedObj, setIncorrectDetectedObj] = useState(0);

  const handleSubmit = (e) => {
    const data = {
      ok: isOk,
      undetectedObj,
      wrongDetectedObj,
      incorrectDetectedObj,
    };

    // send data...
    onSubmit(data);
  };

  return (
    <Form onSubmit={handleSubmit}>
      <Row>
        <Form.Group as={Col} className="review-checboxes">
          <Form.Label>Czy wszystko ok?</Form.Label>
          <Form.Check
            type="radio"
            checked={!!isOk}
            onChange={() => setIsOk(true)}
          />
          <Form.Check
            type="radio"
            checked={!isOk}
            onChange={() => setIsOk(false)}
          />
        </Form.Group>
        <Col>
          <Form.Group>
            <Form.Label>Niewykryte obiekty</Form.Label>
            <Form.Control
              type="number"
              value={undetectedObj}
              onChange={(e) => setUndetectedObj(e.currentTarget.value)}
            />
          </Form.Group>

          <Form.Group>
            <Form.Label>Źle wykryte </Form.Label>
            <Form.Control
              type="number"
              value={wrongDetectedObj}
              onChange={(e) => setWrongDetectedObj(e.currentTarget.value)}
            />
          </Form.Group>
          <Form.Group>
            <Form.Label>Niepoprawnie wykryte</Form.Label>
            <Form.Control
              type="number"
              value={incorrectDetectedObj}
              onChange={(e) => setIncorrectDetectedObj(e.currentTarget.value)}
            />
          </Form.Group>
        </Col>
      </Row>
      <Row className="justify-content-end">
        <Button variant="primary" type="submit" style={{ margin: "0 15px" }}>
          Wyślij opinię
        </Button>
      </Row>
    </Form>
  );
};

export default ReviewForm;
