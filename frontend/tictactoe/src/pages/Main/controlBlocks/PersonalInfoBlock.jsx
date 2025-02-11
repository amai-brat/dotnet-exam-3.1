import "./styles/PersonalInfoBlock.css";
import {useNavigate} from "react-router-dom";
import {authService} from "../../../services/auth.service.jsx";

const PersonalInfoBlock = ({ user, setUser }) => {
    const navigate = useNavigate();
    
    const handleClick = (e) => {
        navigate(`/${e.target.name}`);
    }
    
    const handleExit = (e) => {
        authService.exit();
        setUser(undefined);
    }
    
    return (
        <div className="personal-info-block">
            {user ? (
                <div className="personal-info-block-personal">
                    <h3>{user.username}</h3>
                    <button onClick={handleExit}>Выйти</button>
                </div>
            ) : (
                <div className="personal-info-block-log-reg">
                    <button name="login" onClick={handleClick}>Войти</button>
                    <button name="register" onClick={handleClick}>Зарегистрироваться</button>
                </div>
            )}
        </div>
    );
}

export default PersonalInfoBlock;
