import './App.css'
import { Link, Route, Routes } from "react-router"
import { RoomPage } from './pages/Room/RoomPage'
import { MainPage } from './pages/Main/MainPage'
import AuthPage from "./pages/Auth/AuthPage.jsx";

const App = () => {
    return (
        <>
          <Routes>
            <Route path='/' element={<MainPage/>}></Route>
            <Route path='room/:id' element={<RoomPage/>}></Route>
            <Route path="" element={<RoomPage/>}/>
            <Route path="login" element={<AuthPage formType="login"/>}/>
            <Route path="register" element={<AuthPage formType="register"/>}/>
          </Routes>
        </>
    )
}
export default App