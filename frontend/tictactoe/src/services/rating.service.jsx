import {RATING_BASE_URL} from "../consts/endpoints";

export const ratingService = {
    getPersonalRating,
    getGlobalRating
};

async function getPersonalRating(){
    return await fetch(RATING_BASE_URL + "/personal", {
        method: "GET",
        credentials: "include",
    });
}

async function getGlobalRating(gameSettings){
    return await fetch(RATING_BASE_URLServiceUrl + "/global", {
        method: "GET",
        credentials: "include"
    });
}