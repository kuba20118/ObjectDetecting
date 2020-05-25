import React, { useState } from "react";
import { Form, Button, Col, Row } from "react-bootstrap";
import { Link } from "react-router-dom";
import withLoading from "../hoc/withLoading";

const ReviewForm = ({ onSubmit, isSent }) => {
  const [isCorrect, setIsCorrect] = useState(0);
  const [isIncorrect, setIsIncorrect] = useState(0);
  const [notFound, setNotFound] = useState(0);
  const [multipleFound, setMultipleFound] = useState(0);
  const [isIncorrectBox, setIsIncorrectBox] = useState(0);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const formData = {
      correct: isCorrect ? parseInt(isCorrect, 10) : 0,
      incorrect: isIncorrect ? parseInt(isIncorrect, 10) : 0,
      notFound: notFound ? parseInt(notFound, 10) : 0,
      multipleFound: parseInt(multipleFound, 10),
      incorrectBox: parseInt(isIncorrectBox, 10),
    };
    // send data...
    onSubmit(formData);
  };

  return (
    <>
      {!isSent ? (
        <Form onSubmit={handleSubmit}>
          <Row>
            <Col>
              <Form.Group>
                <Form.Label>Poprawnie wykryte obiekty</Form.Label>
                <Form.Control
                  type="number"
                  value={isCorrect}
                  onChange={(e) => setIsCorrect(e.currentTarget.value)}
                />
              </Form.Group>

              <Form.Group>
                <Form.Label>Niepoprawnie wykryte obiekty </Form.Label>
                <Form.Control
                  type="number"
                  value={isIncorrect}
                  onChange={(e) => setIsIncorrect(e.currentTarget.value)}
                />
              </Form.Group>
              <Form.Group>
                <Form.Label>Niewykryte obiekty</Form.Label>
                <Form.Control
                  type="number"
                  value={notFound}
                  onChange={(e) => setNotFound(e.currentTarget.value)}
                />
              </Form.Group>
            </Col>
            <Col>
              <Form.Group className="review-checboxes">
                <Form.Label>Wiele znalezionych obiektów?</Form.Label>
                <Form.Check
                  type="radio"
                  checked={!!multipleFound}
                  onChange={() => setMultipleFound(1)}
                />
                <Form.Check
                  type="radio"
                  checked={!multipleFound}
                  onChange={() => setMultipleFound(0)}
                />
              </Form.Group>
              <Form.Group className="review-checboxes">
                <Form.Label>Niepoprawna ramka?</Form.Label>
                <Form.Check
                  type="radio"
                  checked={!!isIncorrectBox}
                  onChange={() => setIsIncorrectBox(1)}
                />
                <Form.Check
                  type="radio"
                  checked={!isIncorrectBox}
                  onChange={() => setIsIncorrectBox(0)}
                />
              </Form.Group>
            </Col>
          </Row>
          <Row className="justify-content-end">
            <Button
              variant="primary"
              type="submit"
              style={{ margin: "0 15px" }}
            >
              Wyślij opinię
            </Button>
          </Row>
        </Form>
      ) : (
        <div>
          <p>Dziękujemy za Twoją opinię.</p>
          <Link to="/statystyki">
            <Button
              variant="primary"
              type="submit"
              style={{ margin: "0 15px" }}
            >
              Przejdź do statystyk
            </Button>
          </Link>
        </div>
      )}
    </>
  );
};

export default withLoading(ReviewForm);
