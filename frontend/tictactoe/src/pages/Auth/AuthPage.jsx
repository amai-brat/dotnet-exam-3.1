import LoginForm from "./LoginForm.jsx";
import RegisterForm from "./RegisterForm.jsx";
import "./styles/AuthPage.css";

const AuthPage = ({formType}) => {
    return (
        <div id="auth-page">
            {formType === "login" ? 
                <LoginForm /> :
                formType === "register" ?
                    <RegisterForm /> :
                    <label>Ошибка типа формы</label>
            }
        </div>
    )
}

export default AuthPage;