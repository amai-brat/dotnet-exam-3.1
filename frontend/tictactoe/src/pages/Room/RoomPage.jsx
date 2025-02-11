import {useEffect, useState} from "react";
import {userService} from "../../services/user.service.jsx";
import PersonalInfoBlock from "./controlBlocks/PersonalInfoBlock.jsx";
import RatingBlock from "./controlBlocks/RatingBlock.jsx";
import CreateGameBlock from "./controlBlocks/CreateGameBlock.jsx";
import RoomsList from "./roomsList/RoomsList.jsx";
import {ratingService} from "../../services/rating.service.jsx";
import "./styles/RoomPage.css"; 

const RoomPage = () => {
    const [user, setUser] = useState(undefined);
    
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
        setUser({name: "DIMADOMA", rating: 10});
        return false;
    }
    
    const getPersonalRating = async () => {
        try {
            const response = await ratingService.getPersonalRating();
            if (response.ok) {
                const rating = await response.json();
                setUser({...user, rating: rating});
                return;
            }
        }catch(err){
            console.error(err);
        }
        setUser({...user, rating: null});
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
                <PersonalInfoBlock user={user}/>
                <RatingBlock user={user}/>
                <CreateGameBlock user={user}/>
            </div>
            <div id="room-page-rooms">
                <RoomsList user={user}/>
            </div>
        </div>
    )
}

export default RoomPage;