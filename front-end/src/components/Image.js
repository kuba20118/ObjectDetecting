import React, { PureComponent } from "react";
import withLoading from "../hoc/withLoading";

export class Image extends PureComponent {
  render() {
    const { src } = this.props;
    return (
      <div className="image-wrapper">
        {src ? <img className="img" src={src} alt="Zdjęcie" /> : null}
      </div>
    );
  }
}

export default withLoading(Image);
