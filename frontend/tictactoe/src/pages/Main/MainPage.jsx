import { ENDPOINTS } from '../../consts/endpoints'
import { useEffect, useState } from 'react'
import { Link } from "react-router"

const PAGE_SIZE = 20;

export const MainPage = () => {
  const [games, setGames] = useState([]);
  useEffect(() => {
    (async() => {
      const params = new URLSearchParams({count: PAGE_SIZE, page: 1});
      const response = await fetch(`${ENDPOINTS.ROOMS}?${params.toString()}`);
      if (response.ok) {
        const dto = await response.json();
        setGames(dto.games);
      }
    })();
  }, []);

  return (
    <>
      {games.map((game) => (<div>
          <Link key={game.id} to={`room/${game.id}`}>Game {game.id}</Link>
        </div>))}
    </>
  )
}