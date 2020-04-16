import React, { useState } from "react";
import { Form, Button } from "react-bootstrap";

const ImageUploadForm = ({ onUpload, sendImage }) => {
  const [image, setImage] = useState(null);
  const [isSubmitable, setIsSubmitable] = useState(false);

  const handleSelectedHandler = (event) => {
    if (!event.target.files[0]) {
      return;
    }
    const reader = new FileReader();
    const file = event.target.files[0];
    setImage(file);
    reader.readAsDataURL(file);

    reader.onloadend = () => {
      onUpload(reader.result);
      setIsSubmitable(true);
    };
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    if (!isSubmitable) {
      return;
    }

    const fd = new FormData();
    fd.append("image", image, image.name);
    sendImage(fd);
  };

  return (
    <Form onSubmit={handleSubmit} className="image-form">
      <div className="input-group">
        <div className="input-group-prepend">
          <Button
            className="input-group-text"
            type="submit"
            disabled={!isSubmitable}
          >
            Wyślij
          </Button>
        </div>
        <div className="custom-file">
          <input
            type="file"
            accept=".jpg, .jpeg, .png"
            multiple={false}
            onChange={handleSelectedHandler}
            className="custom-file-input"
            id="inputGroupFile01"
          />
          <label className="custom-file-label" htmlFor="inputGroupFile01">
            Wybierz zdjęcie
          </label>
        </div>
      </div>
    </Form>
  );
};

export default ImageUploadForm;
