<!-- by IxI-Enki -->

# Strategy
### <p align="center"> Class Diagram </p>
```mermaid
classDiagram
    class Context {
        <<class>>
        -strategy: IStrategy
        +Context(IStrategy strategy)
        +ExecuteStrategy()
    }

    class IStrategy {
        <<interface>>
        +Execute()
    }

    class ConcreteStrategyA {
        <<class>>
        +Execute()
    }

    class ConcreteStrategyB {
        <<class>>
        +Execute()
    }

    Context --> IStrategy : uses
    ConcreteStrategyA ..|> IStrategy : implements
    IStrategy <|.. ConcreteStrategyB : implements

    note for Context "Holds a reference to a Strategy object."
    note for IStrategy "Declares an interface common to all supported algorithms."
    note for ConcreteStrategyA "Implements the algorithm using the Strategy interface."
    note for ConcreteStrategyB "Another implementation of the Strategy interface."
```
---
### <p align="center"> Sequence Diagram </p>
```mermaid
sequenceDiagram
  autonumber
    participant C as Client
    participant CT as Context
    participant S as IStrategy
    participant CSA as ConcreteStrategyA
    participant CSB as ConcreteStrategyB

    C->>CT: Create Context with ConcreteStrategyA
    activate CT
    CT->>CSA: Set strategy to ConcreteStrategyA
    deactivate CT

    C->>CT: ExecuteStrategy()
    activate CT
    CT->>S: Execute()
    S->>CSA: Execute()
    activate CSA
    CSA-->>S: Return result
    S-->>CT: Return result
    CT-->>C: Return result
    deactivate CT
    deactivate CSA

    Note over C,CT: Time passes, strategy changes

    C->>CT: Change strategy to ConcreteStrategyB
    activate CT
    CT->>CSB: Set strategy to ConcreteStrategyB
    deactivate CT

    C->>CT: ExecuteStrategy()
    activate CT
    CT->>S: Execute()
    S->>CSB: Execute()
    activate CSB
    CSB-->>S: Return result
    S-->>CT: Return result
    CT-->>C: Return result
    deactivate CT
    deactivate CSB
```
---
### <p align="center"> Implementation </p>
<div align="left">

```c#
 public interface IStrategy
{
    void Execute();
}
```
```c#
public class ConcreteStrategyA : IStrategy
{
    public void Execute()
    {
        // Implement strategy A
    }
}

public class ConcreteStrategyB : IStrategy
{
    public void Execute()
    {
        // Implement strategy B
    }
}
```
```c#
public class Context
{
    private IStrategy strategy;

    public Context(IStrategy strategy)
    {
        this.strategy = strategy;
    }

    public void ExecuteStrategy()
    {
        strategy.Execute();
    }

    public void SetStrategy(IStrategy strategy)
    {
        this.strategy = strategy;
    }
}
```
</div>

<!-- by IxI-Enki -->