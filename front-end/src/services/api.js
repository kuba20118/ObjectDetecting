import axios from "axios";

export const sendImage = async (imageData) => {
  console.log(imageData);
  //send image
  const res = await axios.post("/images/add", imageData);
  console.log(res);
  // wait for response ...

  // fake return obj
  return new Promise((resolve, reject) => {
    setTimeout(() => resolve({ done: true, fake: true }), 3000);
  });
};
