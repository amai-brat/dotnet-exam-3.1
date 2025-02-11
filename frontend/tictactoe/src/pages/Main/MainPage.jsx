import {useEffect, useState} from "react";
import {userService} from "../../services/user.service.jsx";
import PersonalInfoBlock from "./controlBlocks/PersonalInfoBlock.jsx";
import RatingBlock from "./controlBlocks/RatingBlock.jsx";
import CreateGameBlock from "./controlBlocks/CreateGameBlock.jsx";
import RoomsList from "./roomsList/RoomsList.jsx";
import {ratingService} from "../../services/rating.service.jsx";
import "./styles/MainPage.css"; 

const MainPage = () => {
    const [user, setUser] = useState(undefined);
    const [rating, setRating] = useState(undefined);

    const getPersonalInfo = async () => {
        try {
            const response = await userService.getPersonalInfo();
            if (response.ok) {
                setUser(await response.json());
                return true;
            }
        }catch(err){
            console.error(err);
        }
        setUser(null);
        return false;
    }
    
    const getPersonalRating = async () => {
        try {
            const response = await ratingService.getPersonalRating();
            if (response.ok) {
                const rating = await response.json();
                setRating(rating.rating)
                return;
            }
        }catch(err){
            console.error(err);
        }
        setRating(null);
    }

    useEffect(() => {
        getPersonalInfo().then((isUserOk) => {
            if (isUserOk) {
                getPersonalRating().then();    
            }
        });
    }, []);
    
    return (
        <div id="room-page">
            <div id="room-page-control-block">
                <PersonalInfoBlock user={user} setUser={setUser}/>
                <RatingBlock rating={rating}/>
                <CreateGameBlock user={user}/>
            </div>
            <div id="room-page-rooms">
                <RoomsList user={user}/>
            </div>
        </div>
    )
}

export default MainPage;