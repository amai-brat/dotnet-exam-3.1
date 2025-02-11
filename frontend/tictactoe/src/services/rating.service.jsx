import {ratingServiceUrl} from "../httpClient/httpUrls.jsx";

export const ratingService = {
    getPersonalRating,
    getGlobalRating
};

async function getPersonalRating(){
    return await fetch(ratingServiceUrl + "personal", {
        method: "GET",
        credentials: "include",
    });
}

async function getGlobalRating(gameSettings){
    return await fetch(ratingServiceUrl + "global", {
        method: "GET",
        credentials: "include"
    });
}