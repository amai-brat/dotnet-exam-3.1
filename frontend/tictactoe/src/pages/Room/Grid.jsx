import blankImg from "../../assets/blank.svg";
import crossImg from "../../assets/cross.svg";
import circleImg from "../../assets/circle.svg";
import styles from './styles/Grid.module.css';

export const Grid = (props) => {
  const { grid, onCellClick } = props;
  return (
    <div className={styles.grid}>
      {grid.map((row, idx) => (
        <div key={idx} className={styles.gridRow}>
          {row.map((cell, jdx) => (
            <div key={jdx} onClick={() => onCellClick(idx, jdx)} className={styles.gridCell} style={{cursor: cell == 0 ? "pointer" : ""}}>
              {switchCell(cell)}
            </div>
          ))}
        </div>
      ))}
    </div>
  )
}

const switchCell = (cellType) => {
  switch (cellType)
  {
    case 0:
      return <img src={blankImg} width={100} height={100}></img>
    case 1:
      return <img src={crossImg} width={100} height={100}></img>
    case 2:
      return <img src={circleImg} width={100} height={100}></img>
  }
}