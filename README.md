##### <p align="center"> PoseUebung-016 </p>

# <p align="center"> Card Game Simulation </p>

<div align="center">


```mermaid

---
title: UNO
---

flowchart TB
    strt@{ shape: start } --- crDck[Create Deck]@{shape: rounded}
    strt --- dfPlyr["Define Players"]@{shape: rounded}

    dfPlyr --- fxPlyr["Stored Players"]@{shape: win-pane	}
    fxPlyr --- dfPlyrOrdr["Define PLayer Order"]@{shape: hex}
    fxPlyr --- fxStrtCndtns
    fxDck --- fxStrtCndtns

    crDck --- shDck["Schuffle Deck"]@{ shape: hex	}
    shDck --- fxDck["Stored Deck"]@{shape: win-pane	}

    fxStrtCndtns -.- rvCrd["Reveal First Card"]@{ shape: fr-rect}

    dfPlyrOrdr --- fxStrtCndtns["Start Condition"]@{shape: dbl-circ}
    rvCrd --- bgnGame

    fxStrtCndtns --- hndPlyr["Hand Out Cards"]@{shape: st-rect}
    hndPlyr --- bgnGame["Start Game Loop"]@{shape: stadium}

```



</div>