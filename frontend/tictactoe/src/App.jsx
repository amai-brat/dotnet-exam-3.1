import "./App.css";
import AuthPage from "./pages/Auth/AuthPage.jsx";
import MainPage from "./pages/Main/MainPage.jsx";
import {RoomPage} from "./pages/Room/RoomPage.jsx";
import {Route, Routes} from "react-router-dom";

const App = () => {
    return (
        <>
          <Routes>
            <Route path="/" element={<MainPage/>}></Route>
            <Route path="room/:id" element={<RoomPage/>}></Route>
            <Route path="login" element={<AuthPage formType="login"/>}/>
            <Route path="register" element={<AuthPage formType="register"/>}/>
          </Routes>
        </>
    )
}
export default App