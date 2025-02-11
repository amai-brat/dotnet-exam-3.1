import {useState} from "react";
import {validateUserRegisterForm} from "./AuthFormValidators.jsx";
import InputWithError from "../../components/input/InputWithError.jsx";
import {useNavigate} from "react-router-dom";
import {authService} from "../../services/auth.service.jsx";

const RegisterForm = () => {
    const navigate = useNavigate();
    const [registerData, setRegisterData] = useState({
        login: "",
        password: "",
        passwordConfirm: ""
    })
    const [success, setSuccess] = useState({})
    const [errors, setErrors] = useState({});
    const handleRegister = async (e) => {
        setErrors({});
        const newErrors = {};
        
        if(!validateUserRegisterForm(registerData, newErrors)) {
            setErrors(newErrors);
            return;
        }
        
        try {
            const response = await authService.register(registerData);
            
            if (response.ok) {
                setSuccess({message: "Успешная регистрация"})
                setTimeout(() => {
                    navigate("/login")
                }, 1000);
            }else{
                setErrors({message: await response.text()});
            }
        }catch(err) {
            setErrors({message: "Что-то пошло не так"});
        }
    }

    const handleChange = (e) => {
        setErrors({...errors, [e.target.name]: undefined});
        setRegisterData({...registerData, [e.target.name]: e.target.value});
    }

    const colorMessage = success.message ? "log-reg-form-message-success" :
        errors.message ? "log-reg-form-message-error" : "";
    
    return (
        <div className="log-reg-form">
            <div className={"log-reg-form-message " + colorMessage}>
                {success.message && <label>{success.message}</label>}
                {errors.message && <label>{errors.message}</label>}
            </div>
            <div className="log-reg-form-inputs">
                <InputWithError
                    InputElement={<input type="text"
                                         name="login"
                                         value={registerData.login}
                                         onChange={handleChange}
                                         placeholder="Логин"/>}
                    errorMessage={errors.login}
                />
                <InputWithError
                    InputElement={<input type="password"
                                         name="password"
                                         value={registerData.password}
                                         onChange={handleChange}
                                         placeholder="Пароль"/>}
                    errorMessage={errors.password}
                />
                <InputWithError
                    InputElement={<input type="password"
                                         name="passwordConfirm"
                                         value={registerData.passwordConfirm}
                                         onChange={handleChange}
                                         placeholder="Повтор пароля"/>}
                    errorMessage={errors.passwordConfirm}
                />
            </div>
            <button className="log-reg-form-inputs-btn" onClick={handleRegister}>Регистрация</button>
        </div>
    )
}

export default RegisterForm;