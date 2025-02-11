import { useState } from "react";
import "./styles/CreateGameBlock.css";
import {gameService} from "../../../services/game.service.jsx";
import {useNavigate} from "react-router-dom";
import Modal from "../../../components/modal/Modal.jsx";

const CreateGameBlock = ({ user }) => {
    const navigate = useNavigate();
    const [showModal, setShowModal] = useState(false);
    const [gameSettings, setGameSettings] = useState({
        ratingUp: 0
    });

    const handleCreateGame = async () => {
        try {
            const response = await gameService.createGame(gameSettings);
            if (response.ok) {
                const roomId = await response.json();
                navigate(`/game/${roomId}`);
            }else if(response.status === 401){
                navigate(`/login`);
            }
        }catch (err) {
            console.error(err);
        }
    }
    
    const handleInputChange = (e) => {
        setGameSettings({...gameSettings, [e.target.name]: e.target.value});
    }

    return (
        <div className="create-game-block">
            <div className="create-game-block-control">
                <button onClick={() => setShowModal(true)}>Создать игру</button>
            </div>
            {showModal && (
                <Modal onClose={() => setShowModal(false)}>
                    <h2>Создание игры</h2>
                    <div className="create-game-block-modal">
                        <div className="create-game-block-modal-settings">
                            <label>
                                Допустимый рейтинг:
                                <input
                                    name="ratingUp"
                                    type="number"
                                    value={gameSettings.ratingUp}
                                    onChange={handleInputChange}
                                />
                            </label>
                        </div>
                        <div className="create-game-block-modal-control">
                            <button onClick={handleCreateGame}>Создать</button>
                        </div>
                    </div>
                </Modal>
            )}
        </div>
    );
}

export default CreateGameBlock;
