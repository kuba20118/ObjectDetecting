import axios from "axios";

export const sendImage = async (imageData) => {
  //send image
  // const res = axios.post("url", imageData);

  // wait for response ...

  // fake return obj
  return new Promise((resolve, reject) => {
    setTimeout(() => resolve({ done: true, fake: true }), 3000);
  });
};
