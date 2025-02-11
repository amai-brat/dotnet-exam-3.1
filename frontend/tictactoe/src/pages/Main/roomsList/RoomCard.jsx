import "./styles/RoomCard.css";
import {useNavigate} from "react-router-dom";

const RoomCard = ({ room, userRating }) => {
    const navigate = useNavigate();
    const canJoin = room.status === 0;
    const handleJoin = () => {
        navigate(`/room/${room.id}`);
    }

    return (
        <div className="room-card">
            <div className="room-card-info">
                <h3>Игра: {room.createdBy.username}</h3>
                <label>Рейтинг: {room.maxRating}</label>
                <label>Статус: {room.status == 0 ? "Started" : "Closed"}</label>
                <label>Создано: {new Date(room.createdAt).toLocaleString()}</label>
            </div>
            <div className="room-card-control">
                {canJoin && <button onClick={handleJoin}>Присоединиться</button>}
            </div>
        </div>
    );
}

export default RoomCard;
