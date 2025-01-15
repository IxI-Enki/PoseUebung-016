##### <p align="center"> PoseUebung-016 </p>

# <p align="center"> Card Game Simulation </p>

<div align="center">


```mermaid
 
flowchart TB

  %% NODES:
    fxStrtCndtns[ Game Conditions     ]@{ shape: win-pane }
    dfPlyrOrdr[   Define Order      ]@{ shape: hex      }
    bgnGame[      Game Loop           ]@{ shape: dbl-circ }
    hndPlyr[      Hand Out            ]@{ shape: flip-tri }
    dfPlyr[       set Players         ]@{ shape: rounded  }
    fxPlyr[       Stored Players      ]@{ shape: h-cyl    }
    crDck[        Create Deck         ]@{ shape: rounded  }
    shDck[        Schuffle Deck       ]@{ shape: hex      }
    rvCrd[        Start Card          ]@{ shape: hex      }
    fxDck[        Stored Deck         ]@{ shape: h-cyl    }
    strt[         *App-Start*         ]@{ shape: circle   }
    init[         i                   ]@{ shape: fork     }
    p[            Players             ]@{ shape: st-rect  }

  %% CLASS STYLES:
    style dfPlyr color:#000, fill:#319230,stroke:#001f00,stroke-width:3px;
    style dfPlyrOrdr color:#000, fill:#319230,stroke:#001f00,stroke-width:3px;
    style strt color:#02af08, fill:#142,stroke:#001f00,stroke-width:3px;
    style Player-Registration color:#000, fill:#7a7,stroke:#001f00,stroke-width:3px;
    style Deck-Building color:#000, fill:#477;

  %% BUILDING:
    strt --- Deck-Building
    strt --- Player-Registration 

    subgraph Player-Registration
      direction TB
      dfPlyr --> | throw 
                   coin  | dfPlyrOrdr 
    end


    subgraph Deck-Building
      crDck --- shDck
    end
 

    Player-Registration -.-o | send list of Players | fxPlyr
   
  %% GAME SETUP:
    subgraph *Game Setup* 
      shDck      -.-o | send shuffled Deck   | fxDck
      fxPlyr     -->  | each Player          | hndPlyr
      fxDck      -->  | take first
                        Card                 | rvCrd
    %% PLAYER INIT:
        subgraph *Player Initialization*
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

  %% GAME-LOOP: 
    fxStrtCndtns ====> | Run-Game-Loop     | bgnGame


    %% link-styles:
      linkStyle 1 color:#000, stroke:#040, stroke-width:3px;
      linkStyle 3,5 color:#000, stroke:#007f00, stroke-width:3px;
```
</div>