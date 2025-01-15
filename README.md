##### <p align="center"> PoseUebung-016 </p>

# <p align="center"> Card Game Simulation </p>

<div align="center">


```mermaid

---
title: UNO
---

flowchart TB

    strt[         S T A R T           ]@{ shape: circle   }
    crDck[        Create Deck         ]@{ shape: rounded  }
    dfPlyr[       Define Players      ]@{ shape: rounded  }
    fxPlyr[       Stored Players      ]@{ shape: h-cyl    }
    dfPlyrOrdr[   Define PLayer Order ]@{ shape: fr-rect  }
    fxStrtCndtns[ Start Conditions    ]@{ shape: win-pane }
    shDck[        Schuffle Deck       ]@{ shape: hex      }
    rvCrd[        Reveal First Card   ]@{ shape: fr-rect  }
    fxDck[        Stored Deck         ]@{ shape: h-cyl    }
    hndPlyr[      Hand Out Cards      ]@{ shape: st-rect  }
    bgnGame[      Game Loop           ]@{ shape: dbl-circ }

    strt         ---     crDck
    crDck        ---     shDck
    shDck        ---     fxDck
    strt         ---     dfPlyr 
    hndPlyr      ---     fxPlyr
    dfPlyr       ------  fxPlyr 
    fxDck        ---     rvCrd
    fxDck        ---     hndPlyr
    fxPlyr       ---     dfPlyrOrdr 
    rvCrd        -.-o   fxStrtCndtns
    fxDck        -.-o   fxStrtCndtns
    fxPlyr       -.-o    fxStrtCndtns 
    dfPlyrOrdr   -.-o   fxStrtCndtns 
    fxStrtCndtns ---     bgnGame

```



</div>