import React from "react";
import PacmanLoader from "react-spinners/PacmanLoader";
// import { css } from "@emotion/core";

// const override = css`
//   display: block;
//   margin: 0 auto;
//   border-color: red;
// `;

const withLoading = (Component) => ({ isLoading, ...props }) => {
  return isLoading ? (
    <PacmanLoader size={40} color={"rgb(54, 215, 183)"} loading={isLoading} />
  ) : (
    <Component {...props} />
  );
};

export default withLoading;
