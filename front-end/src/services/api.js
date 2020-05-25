import axios from "axios";

export const sendImage = async (imageData) => {
  const res = await axios.post("images/add", imageData);

  // return delayed response
  return new Promise((resolve, reject) =>
    res.data
      ? setTimeout(() => resolve(res), 1500)
      : reject(new Error("Something went wrong"))
  );
};

export const getAllStatsData = async () => await axios.get("/stats/all");

export const getSummaryStatsData = async () =>
  await axios.get("/stats/summary");

export const getStatsById = async (id) => await axios.get(`/stats/${id}`);

export const addStats = async (reviewData) => {
  const res = await axios.post("stats/add", reviewData);
  return new Promise((resolve, reject) =>
    res.data
      ? setTimeout(() => resolve(res), 1500)
      : reject(new Error("Something went wrong"))
  );
};
