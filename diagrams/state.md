### <p align="center"> Class Diagram </p>
```mermaid
classDiagram
    class Context {
        -IState state
        +Request()
    }

    class IState {
        <<interface>>
        +Handle(Context context)
    }

    class ConcreteStateA {
        +Handle(Context context)
    }

    class ConcreteStateB {
        +Handle(Context context)
    }

    Context o-- IState : state
    IState <|.. ConcreteStateA : implements
    IState <|.. ConcreteStateB : implements

 %%   note for Context "Context holds a reference to some State object.\nThe Context delegates to the State object for handling requests."
 %%   note for IState "State interface declares a common interface for all concrete states."
 %%   note for ConcreteStateA "ConcreteStateA implements the State interface and defines a behavior associated with this state."
 %%   note for ConcreteStateB "ConcreteStateB implements the State interface and defines a behavior associated with this state."
```
---
### <p align="center"> Sequence Diagram </p>
```mermaid
sequenceDiagram
    participant C as Context
    participant CSA as ConcreteStateA
    participant CSB as ConcreteStateB

    C->>CSA: Request()
    CSA->>C: Handle()
    Note right of CSA: ConcreteStateA handles the request

    C->>C: Change state to ConcreteStateB
    C->>CSB: Request()
    CSB->>C: Handle()
    Note right of CSB: ConcreteStateB now handles the request
```
---
### <p align="center"> Implementation </p>
<div align="left">

```c#
public interface IState
{
    void Handle(Context context);
}

public class Context
{
    private IState state;

    public Context(IState initialState)
    {
        this.state = initialState;
    }

    public void Request()
    {
        state.Handle(this);
    }

    // Method to change state
    public void ChangeState(IState newState)
    {
        this.state = newState;
    }
}
```
```c#
public class ConcreteStateA : IState
{
    public void Handle(Context context)
    {
        // Handle logic specific to State A
        Console.WriteLine("ConcreteStateA handles request.");
        // Optionally change state here
        // context.ChangeState(new ConcreteStateB());
    }
}
```
```c#
public class ConcreteStateB : IState
{
    public void Handle(Context context)
    {
        // Handle logic specific to State B
        Console.WriteLine("ConcreteStateB handles request.");
        // Optionally change state here
        // context.ChangeState(new ConcreteStateA());
    }
}
```
</div>

<!-- by IxI-Enki -->
