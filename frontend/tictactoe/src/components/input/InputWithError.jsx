import React, { useState } from 'react';
import "./styles/InputWithError.css";

const InputWithError = ({ InputElement, errorMessage }) => {
    const isError = errorMessage !== null && errorMessage !== undefined && errorMessage.trim() !== "";

    return (
        <div className={"input-with-error"}>
            {React.cloneElement(InputElement, { className: isError ? "input-error" : "input-not-error" })}
            {isError && <label className={isError ? "input-error" : ""}>{errorMessage}</label>}
        </div>
    );
};

export default InputWithError;