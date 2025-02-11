import {mainServiceUrl} from "../httpClient/httpUrls.jsx";

export const userService = {
    getPersonalInfo
};

async function getPersonalInfo(){
    return await fetch(mainServiceUrl + "personal",{
        method: "GET",
        credentials: "include"
    });
}