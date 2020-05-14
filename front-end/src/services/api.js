import axios from "axios";

export const sendImage = async (imageData) => {
  //send image
  const res = await axios.post("/images/add", imageData);
  // log response
  console.log(res);

  // return delayed response
  return new Promise((resolve, reject) =>
    res.data
      ? setTimeout(
          () =>
            resolve({
              done: true,
              data: res.data,
            }),
          3000
        )
      : reject(new Error("Something went wrong"))
  );
};
