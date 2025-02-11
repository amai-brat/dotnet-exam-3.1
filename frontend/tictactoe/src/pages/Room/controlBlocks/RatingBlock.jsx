import "./styles/RatingBlock.css";
import {useState} from "react";
import {ratingService} from "../../../services/rating.service.jsx";
import Modal from "../../../components/modal/Modal.jsx";

const testGlobalRatings = [
    { username: "PlayerOne", rating: 2500 },
    { username: "PlayerTwo", rating: 2400 },
    { username: "PlayerThree", rating: 2300 },
    { username: "PlayerFour", rating: 2200 },
    { username: "PlayerFive", rating: 2100 },
    { username: "PlayerSix", rating: 2050 },
    { username: "PlayerSeven", rating: 2000 },
    { username: "PlayerEight", rating: 1950 },
    { username: "PlayerNine", rating: 1900 },
    { username: "PlayerTen", rating: 1850 },
    { username: "PlayerEleven", rating: 1800 },
    { username: "PlayerTwelve", rating: 1750 },
    { username: "PlayerThirteen", rating: 1700 },
    { username: "PlayerFourteen", rating: 1650 },
    { username: "PlayerFifteen", rating: 1600 },
    { username: "PlayerSixteen", rating: 1550 },
    { username: "PlayerSeventeen", rating: 1500 },
    { username: "PlayerEighteen", rating: 1450 },
    { username: "PlayerNineteen", rating: 1400 },
    { username: "PlayerTwenty", rating: 1350 },
];


const RatingBlock = ({ user }) => {
    const [showModal, setShowModal] = useState(false);
    const [globalRating, setGlobalRating] = useState([]);

    const getGlobalRating = async () => {
        try{
            const response = await ratingService.getGlobalRating();
            if (response.ok) {
                const rating = await response.json();
                setGlobalRating(rating);
            }   
        }catch(err){
            console.error(err);
        }
        setGlobalRating(testGlobalRatings);
    }

    const handleShowModal = () => {
        getGlobalRating();
        setShowModal(true);
    }

    return (
        <div className="rating-block">
            {user ? (
                <div className="rating-block-personal">
                    <h3>Личный рейтинг: {user.rating}</h3>
                </div>
            ) : null}
            <div className="rating-block-rate">
                <button onClick={handleShowModal}>Рейтинг</button>
            </div>
            {showModal && (
                <Modal onClose={() => setShowModal(false)}>
                    <h2>Глобальный рейтинг</h2>
                    <div className="rating-block-modal">
                        <table>
                            <thead>
                            <tr>
                                <th>Игрок</th>
                                <th>Рейтинг</th>
                            </tr>
                            </thead>
                            <tbody>
                            {globalRating.map((item, index) => (
                                <tr key={index}>
                                    <td>{item.username}</td>
                                    <td>{item.rating}</td>
                                </tr>
                            ))}
                            </tbody>
                        </table>
                    </div>
                </Modal>
            )}
        </div>
    );
}

export default RatingBlock;
