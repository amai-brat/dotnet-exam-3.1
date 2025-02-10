import {authServiceUrl} from "../httpClient/httpUrls.jsx";

export const authService = {
    login,
    register
};

const login = async (loginData) => {
    return await fetch(authServiceUrl + "login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            Login: loginData.login,
            Password: loginData.password,
        }),
    })
}

const register = async (registerData) => {
    return await fetch(authServiceUrl + "register", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            Login: registerData.login,
            Password: registerData.password,
            PasswordConfirm: registerData.passwordConfirm
        }),
    })
}