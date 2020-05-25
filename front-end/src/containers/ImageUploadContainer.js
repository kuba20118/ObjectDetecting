import React, { useState, useEffect, useContext } from "react";
import { Row, Col, Card } from "react-bootstrap";
import ImageUploadForm from "../components/ImageUploadForm";
import Image from "../components/Image";
import { sendImage, addStats } from "../services/api";
import ReviewForm from "../components/ReviewForm";
import { store } from "../state/store.js";

const ImageUploadContainer = () => {
  const globalState = useContext(store);
  const { dispatch } = globalState;

  const [isLoadingImg, setIsLoadingImg] = useState(false);
  const [isLoadingReviewSent, setIsLoadingReviewSent] = useState(false);
  const [isReviewSent, setIsReviewSent] = useState(false);

  const [resultImageData, setResultImageData] = useState({});

  const onUploadImage = (imageSrc) => {
    dispatch({ type: "SET_IMAGE_SRC", payload: imageSrc });
  };

  const handleImageSend = async (imageData) => {
    setIsLoadingImg(true);
    const res = await sendImage(imageData);
    setResultImageData(res.data);

    const src = `data:image/png;base64,${res.data.imageProcessed}`;
    dispatch({ type: "SET_IMAGE_SRC", payload: src });

    setIsLoadingImg(false);
  };

  const handleReviewSend = async (reviewFormData) => {
    setIsLoadingReviewSent(true);
    const reviewData = {
      imageId: resultImageData.id,
      ...reviewFormData,
    };

    const res = await addStats(reviewData);
    dispatch({ type: "SET_CURRENT_STATS", payload: res.data });
    setIsLoadingReviewSent(false);
    setIsReviewSent(true);
  };

  return (
    <div>
      <Row className="justify-content-center">
        <Col>
          <Card className="card-custom card-image">
            <Image
              src={globalState.state.currentImageSrc}
              isLoading={isLoadingImg}
            />
          </Card>
        </Col>
      </Row>
      <Row className="justify-content-end">
        <ImageUploadForm onUpload={onUploadImage} sendImage={handleImageSend} />
      </Row>
      <br />
      <br />
      <Row>
        <Col>
          {Object.keys(resultImageData).length ? (
            <Card className="card-custom review-card">
              <ReviewForm
                onSubmit={handleReviewSend}
                isLoading={isLoadingReviewSent}
                isSent={isReviewSent}
              />
            </Card>
          ) : null}
        </Col>
      </Row>
    </div>
  );
};

export default React.memo(ImageUploadContainer);
