import "./styles/RatingBlock.css";
import {useState} from "react";
import {ratingService} from "../../../services/rating.service.jsx";
import Modal from "../../../components/modal/Modal.jsx";

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
        setGlobalRating(null);
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
