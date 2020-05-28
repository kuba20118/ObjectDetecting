import React, { useState } from "react";
import { Form, Button, Col, Row } from "react-bootstrap";
import { Link } from "react-router-dom";
import withLoading from "../hoc/withLoading";
import { useForm } from "react-hook-form";
import ReactTooltip from "react-tooltip";

const onlyNumsPattern = /^\d+$/;

const ReviewForm = ({ onSubmit, isSent, foundObjectsNum = 0 }) => {
  const [isNotValid, setIsNotValid] = useState(false);
  const [isZero, setIsZero] = useState(true);
  const { register, errors, handleSubmit, getValues } = useForm();

  const onSubmitForm = (data) => {
    if (isAllZero()) {
      setIsZero(true);
      return;
    }
    if (!isRelatedValuesValid()) {
      setIsNotValid(true);
      return;
    }
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

  const isRelatedValuesValid = () => {
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

    return relatedValuesSum === foundObjectsNum;
  };

  const handleRelatedValueChange = (e) => {
    if (isRelatedValuesValid()) {
      setIsZero(false);
      setIsNotValid(false);
    } else setIsNotValid(true);
  };

  const isAllZero = () => {
    return (
      Object.keys(getValues())
        .map((key) => parseInt(getValues()[key], 10))
        .reduce((sum, val) => sum + val) === 0
    );
  };

  return (
    <>
      {!isSent ? (
        <Form noValidate onSubmit={handleSubmit(onSubmitForm)}>
          <Row className="review-row">
            <Col sm={7}>
              <Row className="review-form-row">
                <Form.Group
                  className="review-form-group"
                  controlId="validation1"
                >
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
                <a
                  className="btn-tooltip"
                  data-tip="Obiekt został poprawnie wykryty i zaznaczony."
                  data-for="correctInfo"
                >
                  i
                </a>
                <ReactTooltip id="correctInfo" type="info" multiline={true} />
              </Row>
              <Row className="review-form-row">
                <Form.Group
                  className="review-form-group"
                  controlId="validation2"
                >
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
                <a
                  className="btn-tooltip"
                  data-tip="Obiekt został niepoprawnie sklasyfikowany, np. <br />pies na zdjęciu został oznaczony jako kot."
                  data-for="incorrectInfo"
                >
                  i
                </a>
                <ReactTooltip id="incorrectInfo" type="info" multiline={true} />
              </Row>
              <Row className="review-form-row">
                <Form.Group
                  className="review-form-group"
                  controlId="validation4"
                >
                  <Form.Label>
                    Kilkukrotne zaznaczenie wykrytego obiektu
                  </Form.Label>
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
                <a
                  className="btn-tooltip"
                  data-tip="Obiekt został poprawnie wykryty, ale zaznaczony<br />więcej niż jeden raz. np. na zdjęciu z jedną krową<br /> naniesione są 2 ramki."
                  data-for="multipleFoundInfo"
                >
                  i
                </a>
                <ReactTooltip
                  id="multipleFoundInfo"
                  type="info"
                  multiline={true}
                />
              </Row>
            </Col>
            <Col sm={5}>
              <Row className="review-form-row">
                <Form.Group
                  className="review-form-group"
                  controlId="validation5"
                >
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
                <a
                  className="btn-tooltip"
                  data-tip="Obiekt został poprawnie wykryty, ale ramka jest<br /> niedokładna, np. na zdjęciu kota zaznaczone jest jego ucho."
                  data-for="incorrectBoxInfo"
                >
                  i
                </a>
                <ReactTooltip
                  id="incorrectBoxInfo"
                  type="info"
                  multiline={true}
                />
              </Row>
              <Row className="review-form-row">
                <Form.Group
                  className="review-form-group"
                  controlId="validation3"
                >
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
                <a
                  className="btn-tooltip"
                  data-tip="Obiekt z listy, dobrze widoczny. <br />Na zdjęciu nie został wykryty i zaznaczony, np. <br />na zdjęciu z 3 kotami, zaznaczone są tylko 2 z nich."
                  data-for="notFoundInfo"
                >
                  i
                </a>
                <ReactTooltip id="notFoundInfo" type="info" multiline={true} />
              </Row>
              <Row className="review-form-row">
                {isNotValid ? (
                  <div className="review-form-invalid-feedback">
                    <p>
                      {`Suma wszystkich pól oprócz obiektów nieznalezionych musi być równa liczbie znalezionych obiektów tj. ${foundObjectsNum}.`}
                    </p>
                  </div>
                ) : null}
              </Row>
            </Col>
          </Row>
          <Row className="justify-content-end">
            <Button
              variant={isZero || isNotValid ? "light" : "primary"}
              disabled={isZero || isNotValid}
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
