// store.js
import React, { createContext, useReducer } from "react";

const initialState = {
  currentStats: {},
  allStats: {},
  imageSrc: "",
};
const store = createContext(initialState);
const { Provider } = store;

const StateProvider = ({ children }) => {
  const [state, dispatch] = useReducer((state, action) => {
    switch (action.type) {
      case "SET_CURRENT_STATS":
        return { ...state, currentStats: action.payload };
      case "SET_IMAGE_SRC":
        return { ...state, currentImageSrc: action.payload };
      default:
        throw new Error();
    }
  }, initialState);
  return <Provider value={{ state, dispatch }}>{children}</Provider>;
};

export { store, StateProvider };
