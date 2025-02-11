import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ENDPOINTS } from "../../consts/endpoints";
import * as signalR from "@microsoft/signalr";
import { Grid } from "./Grid";

export const RoomPage = () => {
  const { id } = useParams();
  const [connection, setConnection] = useState();
  const [grid, setGrid] = useState([[0,0,0], [0,0,0], [0,0,0]]);
  const [isMyTurn, setIsMyTurn] = useState(false);
  const [game, setGame] = useState();
  const [canJoin, setCanJoin] = useState(true);
  
  useEffect(() => {
    (async() => {
      const resp = await fetch(`${ENDPOINTS.ROOM}${id}`,
        {
         headers: {
          "Authorization": ""
         } 
        });
      
      if (resp.ok) {
        setGame(await resp.json());
      }
    })()
  }, []);

  useEffect(() => {
    const con = new signalR.HubConnectionBuilder()
      .withUrl(ENDPOINTS.GAME_HUB)
      .withAutomaticReconnect()
      .build();

    setConnection(con);
  }, []);

  useEffect(() => {
    if (connection) {
      try {
        connection.start().then(() => {
          connection.on('Error', (message) => {
            console.error('Error:', message);
          });
          connection.on('RefreshGrid', (grid) => {
            console.log("Grid update")
            setGrid(_ => grid);
          });
          connection.on('Turn', () => {
            console.log("My turn")
            setIsMyTurn(true);
          });
          connection.on("GameStarted", (isStarted) => {
            setCanJoin(!isStarted);
          })
          connection.on('ResultAnnouncement', (message) => {
            console.log(message)
          })
          }).catch((err) => {
            console.error(err);
          })
      } catch (err) {
        console.error(err);
      }
    }
  
    function joinRoom() {
      if (connection?.state === signalR.HubConnectionState.Connected) {
        connection.invoke('JoinRoom', +id).catch((err) => {
          console.error(err.toString())
        });
      } else {
        setTimeout(joinRoom, 200);
      }
    }

    joinRoom();

    return () => {
      connection?.stop();
    };
  }, [connection]);

  const onCellClick = (idx, jdx) => {
    console.log("click", idx, jdx);

    if (isMyTurn) {
      connection.invoke('SendTurn', idx, jdx);
      setIsMyTurn(prev => !prev);
    }
  }

  const onJoinClick = () => {
    connection.invoke('JoinGame', +id).catch((err) => {
      console.error(err.toString())
    });

    setCanJoin(false);
  }

  return (
    <div style={{'display': 'flex', 'flexDirection': 'column'}}>
      <Grid grid={grid} onCellClick={onCellClick}/>
      <button onClick={onJoinClick} disabled={!canJoin}>Присоединиться</button>
    </div>
  );
}