<!-- by IxI-Enki -->

# Memento
### <p align="center"> Class Diagram </p>
```mermaid
classDiagram
  direction RL
    class Originator {
        -state: string
        +CreateMemento() Memento
        +SetMemento(memento: Memento)
        +GetState() string
        +SetState(state: string)
    }

    class Memento {
        -state: string
        +GetState() string
    }

    class Caretaker {
        -memento: Memento
        +GetMemento() Memento
        +SetMemento(memento: Memento)
    }

    Originator -- Memento : creates and sets
    Caretaker o-- Memento : manages

    note for Originator "Responsible for creating a memento with internal state and restoring from it."
    note for Memento "Stores internal state of Originator. Immutable."
    note for Caretaker "Manages the Memento, doesn't know about the state details."
```
---
### <p align="center"> Sequence Diagram </p>
```mermaid
sequenceDiagram
  autonumber

    participant O as Originator
    participant M as Memento
    participant C as Caretaker

    O->>O: SetState("State1")
    O->>M: CreateMemento()
    M-->>C: Memento
    C->>C: SaveMemento(Memento)
    
    Note over O: State changes
    O->>O: SetState("State2")
    
    C->>O: GetMemento()
    O->>M: SetMemento(Memento)
    O-->>O: Restore from Memento
```
---
### <p align="center"> Implementation </p>
<div align="left">

```c#
public interface IMemento
{
    string GetState();
}
```
```c#
public class ConcreteMemento : IMemento
{
    private string _state;
    public ConcreteMemento(string state) => _state = state;

    public string GetState() => _state;
}
```

<!-- by IxI-Enki -->