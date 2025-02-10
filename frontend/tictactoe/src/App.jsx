import './App.css'
import {Route, Routes} from "react-router-dom";
import AuthPage from "./pages/Auth/AuthPage.jsx";
import RoomPage from "./pages/Room/RoomPage.jsx";

const App = () => {
    return (
        <>
          <Routes>
            <Route path="" element={<RoomPage/>}/>
            <Route path="login" element={<AuthPage formType="login"/>}/>
            <Route path="register" element={<AuthPage formType="register"/>}/>
          </Routes>
        </>
    )
}

export default App
