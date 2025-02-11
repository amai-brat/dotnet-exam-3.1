import {MAIN_BASE_URL} from "../consts/endpoints";

export const gameService = {
    getRooms,
    createGame
};

async function getRooms(page, count){
    return await fetch(MAIN_BASE_URL + `/games?page=${page}&count=${count}`,{
        method: "GET",
        credentials: "include"
    });
}

// TODO: fix
async function createGame(gameSettings){
    return await fetch(MAIN_BASE_URL + "/games", {
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