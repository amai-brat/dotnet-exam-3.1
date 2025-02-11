import {useEffect, useRef, useState} from "react";
import RoomCard from "./RoomCard.jsx";
import "./styles/RoomsList.css";
import {gameService} from "../../../services/game.service.jsx";

const RoomsPerPage = 5;

const testRooms = [
    {
        ratingUp: 1500,
        gameMaster: "PlayerOne",
        status: "active",
        gamersCount: 5,
        viewersCount: 2
    },
    {
        ratingUp: 1200,
        gameMaster: "PlayerTwo",
        status: "active",
        gamersCount: 3,
        viewersCount: 1
    },
    {
        ratingUp: 1800,
        gameMaster: "PlayerThree",
        status: "inactive",
        gamersCount: 0,
        viewersCount: 0
    },
    {
        ratingUp: 1600,
        gameMaster: "PlayerFour",
        status: "active",
        gamersCount: 4,
        viewersCount: 3
    },
    {
        ratingUp: 1400,
        gameMaster: "PlayerFive",
        status: "active",
        gamersCount: 2,
        viewersCount: 1
    }
];

const RoomsList = ({ user }) => {
    const [rooms, setRooms] = useState([]);
    const [page, setPage] = useState(1);
    const [loading, setLoading] = useState(false);
    const [noMoreRooms, setNoMoreRooms] = useState(false);
    const observerRef = useRef();
    const getRooms = async () => {
        setLoading(true);
        try {
            const response = await gameService.getRooms(page, RoomsPerPage);
            if (response.ok) {
                const rooms = await response.json();
                if(rooms.length === 0){
                    setNoMoreRooms(true);
                }else{
                    setNoMoreRooms(false);
                }
                setRooms(prevRooms => [...prevRooms, ...rooms]);
            }   
        }catch (err){
            console.error(err);
        }
        setRooms(prevRooms => [...prevRooms, ...testRooms])
        if(page === 3){
            setNoMoreRooms(true);
        }
        setLoading(false);
    }

    useEffect(() => {
        getRooms().then();
    }, [page]);

    const loadMoreRooms = (entries) => {
        const [entry] = entries;
        if (entry.isIntersecting && !loading && !noMoreRooms) {
            setPage(prevPage => prevPage + 1);
        }
    }

    useEffect(() => {
        const observer = new IntersectionObserver(loadMoreRooms, {
            rootMargin: '100px',
        });

        if (observerRef.current) {
            observer.observe(observerRef.current);
        }

        return () => {
            if (observerRef.current) {
                observer.unobserve(observerRef.current);
            }
        };
    }, [loading]);

    return (
        <div className="rooms-list">
            {rooms.map((room, index) => (
                <RoomCard key={index} room={room} userRating={user?.rating} />
            ))}
            {loading && <label className="rooms-list-info-label">Загрузка...</label>}
            {noMoreRooms && <label className="rooms-list-info-label">Комнат больше нет</label>}
            <div ref={observerRef}/>
        </div>
    );
}

export default RoomsList;
