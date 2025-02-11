import './App.css'
import { Link, Route, Routes } from "react-router"
import { RoomPage } from './pages/Room/RoomPage'
import { MainPage } from './pages/Main/MainPage'

function App() {
  return (
    <>
      <Routes>
        <Route path='/' element={<MainPage/>}></Route>
        <Route path='room/:id' element={<RoomPage/>}></Route>
      </Routes>
    </>
  )
}

export default App
