import {authServiceUrl} from "../httpClient/httpUrls.jsx";

export const authService = {
    login,
    register,
    exit
};

async function login(loginData){
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

async function register(registerData){
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

function exit(){
    
}