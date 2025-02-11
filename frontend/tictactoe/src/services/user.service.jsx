import {MAIN_BASE_URL} from "../consts/endpoints";

export const userService = {
    getPersonalInfo
};

async function getPersonalInfo(){
    return await fetch(MAIN_BASE_URL + "/personal",{
        method: "GET",
        credentials: "include"
    });
}