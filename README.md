##### <p align="center"> PoseUebung-016 </p>

# <p align="center"> Card Game Simulation </p>

<div align="center">


```mermaid

---
title: UNO
---

flowchart TB

    strt[         App-Start           ]@{ shape: circle   }
    crDck[        Create Deck         ]@{ shape: rounded  }
    dfPlyr[       Define Players      ]@{ shape: rounded  }
    fxPlyr[       Stored Players      ]@{ shape: h-cyl    }
    dfPlyrOrdr[   Define Player Order ]@{ shape: hex      }
    fxStrtCndtns[ Game Conditions     ]@{ shape: win-pane }
    shDck[        Schuffle Deck       ]@{ shape: hex      }
    rvCrd[        Start Card          ]@{ shape: hex      }
    fxDck[        Stored Deck         ]@{ shape: h-cyl    }
    hndPlyr[      Hand Out            ]@{ shape: flip-tri }
    bgnGame[      Game Loop           ]@{ shape: dbl-circ }
    p[            Players             ]@{ shape: st-rect  }
    init[         i                   ]@{ shape: fork     }

    strt         ---     crDck
    strt         ---     dfPlyr 
    subgraph Deck Building
      crDck      ---     shDck
    end
    subgraph Player Registration 
      dfPlyr     ---  dfPlyrOrdr 
    end

    subgraph Game Setup 
      shDck      -.-o | send shuffled Deck   | fxDck
      dfPlyrOrdr -.-o | send list of Players | fxPlyr
      fxPlyr     -->  | each Player          | hndPlyr
      fxDck      -->  | take first
                        Card                 | rvCrd
        subgraph Player Initialization 
          hndPlyr --> p
        end

      fxDck -->  | 5 Cards         | hndPlyr 
      p     -.-> | register Player | init
      rvCrd -.-> | register Card   | init
      fxDck -.-> | register
                   remaining
                   Cards
                   as 
                   Deck            | init
    end

    init         -.-o  | send Setup status | fxStrtCndtns 
    fxStrtCndtns ====> | Run-Game-Loop     | bgnGame


    style strt color:#02af08, fill:#142,stroke:#001f00,stroke-width:3px

```



</div>