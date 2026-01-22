import React from "react";

const button = (props) =>{
const { onClick,children} = props;

    return <button>
        {children}
          </button>
};

export default button;