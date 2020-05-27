import React, { useState } from "react";
import { Form, Button, Col, Row } from "react-bootstrap";
import { Link } from "react-router-dom";
import withLoading from "../hoc/withLoading";
import { useForm } from "react-hook-form";

const onlyNumsPattern = /^\d+$/;

const ReviewForm = ({ onSubmit, isSent, foundObjectsNum = 0 }) => {
  const [isNotValid, setIsNotValid] = useState(false);
  const { register, errors, handleSubmit, getValues } = useForm();

  const onSubmitForm = (data) => {
    if (isNotValid) return;
    const { correct, incorrect, notFound, multipleFound, incorrectBox } = data;

    const formData = {
      correct: correct ? parseInt(correct, 10) : 0,
      incorrect: incorrect ? parseInt(incorrect, 10) : 0,
      notFound: notFound ? parseInt(notFound, 10) : 0,
      multipleFound: multipleFound ? parseInt(multipleFound, 10) : 0,
      incorrectBox: incorrectBox ? parseInt(incorrectBox, 10) : 0,
    };
    // send data...
    onSubmit(formData);
  };

  const handleRelatedValueChange = (e) => {
    const valuesObj = getValues();
    const relatedValuesSum = Object.keys(valuesObj)
      .map((key) => ({
        name: key,
        value: valuesObj[key],
      }))
      .filter((value) => value.name !== "notFound")
      .map((val) => ({
        ...val,
        value: parseInt(val.value, 10),
      }))
      .reduce((sum, val) =>
        sum.hasOwnProperty("value") ? sum.value + val.value : sum + val.value
      );

    if (relatedValuesSum !== foundObjectsNum) setIsNotValid(true);
    else {
      setIsNotValid(false);
    }
  };

  return (
    <>
      {!isSent ? (
        <Form noValidate onSubmit={handleSubmit(onSubmitForm)}>
          <Row>
            <Col>
              <Form.Group controlId="validation1">
                <Form.Label>Poprawnie wykryte obiekty</Form.Label>
                <Form.Control
                  name="correct"
                  type="number"
                  ref={register({
                    pattern: onlyNumsPattern,
                    min: 0,
                  })}
                  defaultValue="0"
                  onChange={handleRelatedValueChange}
                  isInvalid={errors.correct}
                />
                <Form.Control.Feedback type="invalid">
                  Wartość musi być liczbą większą lub równa 0.
                </Form.Control.Feedback>
              </Form.Group>

              <Form.Group controlId="validation2">
                <Form.Label>Niepoprawnie wykryte obiekty </Form.Label>
                <Form.Control
                  name="incorrect"
                  type="number"
                  ref={register({ pattern: onlyNumsPattern, min: 0 })}
                  defaultValue="0"
                  onChange={handleRelatedValueChange}
                  isInvalid={errors.incorrect}
                />
                <Form.Control.Feedback type="invalid">
                  Wartość musi być liczbą większą lub równa 0.
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group controlId="validation4">
                <Form.Label>Wiele znalezionych obiektów</Form.Label>
                <Form.Control
                  name="multipleFound"
                  type="number"
                  ref={register({ pattern: onlyNumsPattern, min: 0 })}
                  defaultValue="0"
                  onChange={handleRelatedValueChange}
                  isInvalid={errors.multipleFound}
                />
                <Form.Control.Feedback type="invalid">
                  Wartość musi być liczbą większą lub równa 0.
                </Form.Control.Feedback>
              </Form.Group>
            </Col>
            <Col>
              <Form.Group controlId="validation5">
                <Form.Label>Niepoprawna ramka</Form.Label>
                <Form.Control
                  name="incorrectBox"
                  type="number"
                  ref={register({ pattern: onlyNumsPattern, min: 0 })}
                  defaultValue="0"
                  onChange={handleRelatedValueChange}
                  isInvalid={errors.incorrectBox}
                />
                <Form.Control.Feedback type="invalid">
                  Wartość musi być liczbą większą lub równa 0.
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group controlId="validation3">
                <Form.Label>Niewykryte obiekty</Form.Label>
                <Form.Control
                  name="notFound"
                  type="number"
                  ref={register({ pattern: onlyNumsPattern, min: 0 })}
                  defaultValue="0"
                  onChange={handleRelatedValueChange}
                  isInvalid={errors.notFound}
                />
                <Form.Control.Feedback type="invalid">
                  Wartość musi być liczbą większą lub równa 0.
                </Form.Control.Feedback>
              </Form.Group>
              {isNotValid ? (
                <div className="review-form-invalid-feedback">
                  <p>
                    {`Suma wszystkich pól oprócz obiektów nieznalezionych musi być równa liczbie znalezionych obiektów tj. ${foundObjectsNum}.`}
                  </p>
                </div>
              ) : null}
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
