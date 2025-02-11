import {AUTH_BASE_URL} from "../consts/endpoints";

export const authService = {
    login,
    register,
    exit
};

async function login(loginData){
    return await fetch(AUTH_BASE_URL + "/login", {
        method: "POST",
        credentials: "include",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            Login: loginData.login,
            Password: loginData.password,
        }),
    })
}

async function register(registerData){
    return await fetch(AUTH_BASE_URL + "/register", {
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

async function exit(){
    return await fetch(AUTH_BASE_URL + "/signout", {
        method: "POST",
        credentials: "include"
    });
}