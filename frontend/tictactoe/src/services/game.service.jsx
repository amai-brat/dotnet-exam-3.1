import {authServiceUrl, mainServiceUrl} from "../httpClient/httpUrls.jsx";

export const gameService = {
    getRooms,
    createGame
};

async function getRooms(page, count){
    return await fetch(mainServiceUrl + `rooms?page=${page}&count=${count}`,{
        method: "GET",
        credentials: "include"
    });
}

async function createGame(gameSettings){
    return await fetch(mainServiceUrl + "/game/new", {
        method: "POST",
        credentials: "include",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            RatingUp: gameSettings.ratingUp
        })
    })
}