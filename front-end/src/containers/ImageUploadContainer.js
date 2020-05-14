import React, { useState } from "react";
import { Row, Col, Card } from "react-bootstrap";
import ImageUploadForm from "../components/ImageUploadForm";
import Image from "../components/Image";
import { sendImage } from "../services/api";
import ReviewForm from "../components/ReviewForm";

const ImageUploadContainer = () => {
  const [imagePreviewSrc, setImagePreviewSrc] = useState(null);
  const [isLoadingImg, setIsLoadingImg] = useState(false);
  const [isResultLoaded, setIsResltLoaded] = useState(false);

  const onUploadImage = (imageSrc) => {
    setImagePreviewSrc(imageSrc);
  };

  const handleImageSend = async (imageData) => {
    setIsLoadingImg(true);
    const res = await sendImage(imageData);
    const src = `data:image/png;base64,${res.data.imageOriginal}`;

    setImagePreviewSrc(src);
    setIsLoadingImg(false);
    setIsResltLoaded(true);
  };

  const handleReviewSend = () => {};

  return (
    <div>
      <Row className="justify-content-center">
        <Col>
          <Card className="card-custom card-image">
            <Image src={imagePreviewSrc} isLoading={isLoadingImg} />
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
          {isResultLoaded ? (
            <Card className="card-custom">
              <ReviewForm onSubmit={handleReviewSend} />
            </Card>
          ) : null}
        </Col>
      </Row>
    </div>
  );
};

export default ImageUploadContainer;
