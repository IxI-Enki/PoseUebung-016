##### <p align="center"> PoseUebung-016 </p>

# <p align="center"> Card Game Simulation </p>

<div align="center">


```mermaid
 
flowchart TB

  %% NODES:
    fxStrtCndtns[ Game Conditions     ]@{ shape: win-pane }
    dfPlyrOrdr[   Define Order        ]@{ shape: hex      }
    bgnGame[      Game Loop           ]@{ shape: dbl-circ }
    hndPlyr[      Hand Out            ]@{ shape: st-rect  }
    dfPlyr[       set Players         ]@{ shape: rounded  }
    fxPlyr[       Stored Players      ]@{ shape: h-cyl    }
    crDck[        Create Deck         ]@{ shape: rounded  }
    shDck[        Schuffle Deck       ]@{ shape: hex      }
    rvCrd[        Start Card          ]@{ shape: win-pane }
    fxDck[        Stored Deck         ]@{ shape: h-cyl    }
    strt[         *App-Start*         ]@{ shape: circle   }
    init[         i                   ]@{ shape: fork     }
    e[            Initialize          ]@{ shape: flip-tri }
    ip[           i                   ]@{ shape: fork     }
                                     id@{ shape: tri, label: "Split\n Deck" }
    d[            Deck                ]@{ shape: win-pane }
    
  %% CLASS STYLES:
    style strt color:#0f0f, fill:#f1f4f230, stroke:#001f0090, stroke-width:33px;
  
    style dfPlyr color:#000, fill:#319230, stroke:#001f00, stroke-width:3px;
    style crDck  color:#000, fill:#516192, stroke:#00001f, stroke-width:3px;
    
    style dfPlyrOrdr color:#000, fill:#319230, stroke:#001f00, stroke-width:3px;
    style shDck      color:#000, fill:#516192, stroke:#00001f, stroke-width:3px;
    
    style fxPlyr color:#000, fill:#319230, stroke:#000, stroke-width:2px;
    style fxDck  color:#000, fill:#516192, stroke:#000, stroke-width:2px;
    
    style Player-Registration color:#000, fill:#7a7, stroke:#001f0070, stroke-width:9px;
    style Deck-Building       color:#000, fill:#477, stroke:#00001f70, stroke-width:9px;
    style Game-Setup          color:#000, fill:#794, stroke:#ffff0170, stroke-width:10px;
  

  %%_BUILDING: ________________________________________________________________
    strt --- Deck-Building
    strt --- Player-Registration 
      linkStyle 0,1 color:#000, stroke:#af6, stroke-width:8px;

    %%___PLAYERS: _____________________
    subgraph Player-Registration
      direction TB
      dfPlyr --> | throw coins | dfPlyrOrdr 
        linkStyle 2 color:#fff, stroke:#000, stroke-width:4px;
    end
    Player-Registration -.-o | send list of Players | Game-Setup
      linkStyle 3 color:#ffff00, stroke:#a0f06090, stroke-width:13px;

    %%___DECK: ________________________
    subgraph Deck-Building
      direction TB
      crDck --> | as singelton | shDck
        linkStyle 4 color:#fff, stroke:#000, stroke-width:4px;
    end
    Deck-Building -.-o | send shuffled Deck   | Game-Setup
      linkStyle 5 color:#cef, stroke:#a0f06090, stroke-width:13px;
   
  %%_GAME-SETUP: ______________________________________________________________
    subgraph Game-Setup
      direction TB 

    %% PLAYER INIT:
      fxPlyr --> ip
      fxDck  --> id
        linkStyle 6,7 stroke:#e0f0407f, stroke-width:8px;

      ip -..-> | Player  | e
      id -..-> | 5 Cards | e 
      %%  linkStyle 6,7 stroke:#e0f0407f, stroke-width:8px;

        subgraph Player-Initialization
          note[ each ]@{shape: text} ~~~ e
            style note color:#000, fill:#d7d9d409, stroke:#ffffff30, stroke-width:2px;
        
          e --> hndPlyr
          %%  linkStyle 6,7 stroke:#e0f0407f, stroke-width:8px;
        end

      Player-Initialization -..-> | register Players | init
      %%  linkStyle 6,7 stroke:#e0f0407f, stroke-width:8px;
      
      id    ---> | first Card     | rvCrd
      rvCrd ---> | register Card  | init
      id    ---> | remaining Deck | d
      d     ---> | register Deck  | init
      %%  linkStyle 6,7 stroke:#e0f0407f, stroke-width:8px;
      
    end
    Game-Setup -.-o  | send Setup status | fxStrtCndtns 
    %%  linkStyle 6,7 stroke:#e0f0407f, stroke-width:8px;

  %% GAME-LOOP: 
    fxStrtCndtns ==> | Run-Game-Loop     | bgnGame
    %%  linkStyle 6,7 stroke:#e0f0407f, stroke-width:8px;

```
</div>