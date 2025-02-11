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
<<<<<<< HEAD
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
=======
import {useEffect, useState} from "react";
import {userService} from "../../services/user.service.jsx";
import PersonalInfoBlock from "./controlBlocks/PersonalInfoBlock.jsx";
import RatingBlock from "./controlBlocks/RatingBlock.jsx";
import CreateGameBlock from "./controlBlocks/CreateGameBlock.jsx";
import RoomsList from "./roomsList/RoomsList.jsx";
import {ratingService} from "../../services/rating.service.jsx";
import "./styles/RoomPage.css"; 

const RoomPage = () => {
    const [user, setUser] = useState(undefined);
    
    const getPersonalInfo = async () => {
        try {
            const response = await userService.getPersonalInfo();
            if (response.ok) {
                setUser(await response.json());
                return true;
            }
        }catch(err){
            console.error(err);
        }
        setUser(null);
        return false;
    }
    
    const getPersonalRating = async () => {
        try {
            const response = await ratingService.getPersonalRating();
            if (response.ok) {
                const rating = await response.json();
                setUser({...user, rating: rating.rating});
                return;
            }
        }catch(err){
            console.error(err);
        }
        setUser({...user, rating: null});
    }

    useEffect(() => {
        getPersonalInfo().then((isUserOk) => {
            if (isUserOk) {
                getPersonalRating().then();    
            }
        });
    }, []);
    
    return (
        <div id="room-page">
            <div id="room-page-control-block">
                <PersonalInfoBlock user={user}/>
                <RatingBlock user={user}/>
                <CreateGameBlock user={user}/>
            </div>
            <div id="room-page-rooms">
                <RoomsList user={user}/>
            </div>
        </div>
    )
}

export default RoomPage;
>>>>>>> dev

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