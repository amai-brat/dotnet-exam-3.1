import {useState} from "react";
import {validateUserLoginForm} from "./AuthFormValidators.jsx";
import {authServiceUrl} from "../../httpClient/httpUrls.jsx";
import InputWithError from "../../components/input/InputWithError.jsx";
import "./styles/LogRegForm.css";
import {useNavigate} from "react-router-dom";

const LoginForm = () => {
    const navigate = useNavigate();
    const [loginData, setLoginData] = useState({
        login: "",
        password: "",
    })
    const [success, setSuccess] = useState({})
    const [errors, setErrors] = useState({});
    const handleLogin = async (e) => {
        setErrors({});
        const newErrors = {};

        if(!validateUserLoginForm(loginData, newErrors)) {
            setErrors(newErrors);
            return;
        }

        try {
            const response = await fetch(authServiceUrl + "login", {
                method: "POST",
                body: JSON.stringify({
                    Login: loginData.login,
                    Password: loginData.password,
                }),
            })

            if (response.ok) {
                setSuccess({message: "Успешный вход"})
                setTimeout(() => {
                    navigate("/")
                }, 1000);
            }else{
                setErrors({message: response.message});
            }
        }catch(err) {
            setErrors({message: "Что-то пошло не так"});
        }
    }
    
    const handleChange = (e) => {
        setErrors({...errors, [e.target.name]: undefined});
        setLoginData({...loginData, [e.target.name]: e.target.value});
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
                                         value={loginData.login}
                                         onChange={handleChange}
                                         placeholder="Логин"/>}
                    errorMessage={errors.login}
                />
                <InputWithError
                    InputElement={<input type="password"
                                         name="password"
                                         value={loginData.password}
                                         onChange={handleChange}
                                         placeholder="Пароль"/>}
                    errorMessage={errors.password}
                />
            </div>
            <button className="log-reg-form-inputs-btn" onClick={handleLogin}>Вход</button>
        </div>
    )
}

export default LoginForm;