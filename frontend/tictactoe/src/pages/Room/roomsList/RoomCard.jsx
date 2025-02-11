import "./styles/RoomCard.css";
import {useNavigate} from "react-router-dom";

const RoomCard = ({ room, userRating }) => {
    const navigate = useNavigate();
    const canJoin = userRating <= room.ratingUp && room.status === "active";
    const handleJoin = () => {
        navigate(`/game/${room.id}`);
    }

    return (
        <div className="room-card">
            <div className="room-card-info">
                <h3>Игра: {room.gameMaster}</h3>
                <label>Рейтинг: {room.ratingUp}</label>
                <label>Статус: {room.status}</label>
                <label>Игроков: {room.gamersCount}</label>
                <label>Зрителей: {room.viewersCount}</label>
            </div>
            <div className="room-card-control">
                {canJoin && <button onClick={handleJoin}>Присоединиться</button>}
            </div>
        </div>
    );
}

export default RoomCard;
