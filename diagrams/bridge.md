<!-- by IxI-Enki -->

# Bridge
### <p align="center"> Class Diagram </p>
```mermaid
classDiagram
    class Implementor {
        <<abstract>>
        +OperationImplementor()
    }
    
    class ConcreteImplementorA {
        +OperationImplementor()
    }
    
    class ConcreteImplementorB {
        +OperationImplementor()
    }

    class Abstraction {
        <<abstract>>
        -implementor: Implementor
        +Abstraction(implementor: Implementor)
        +Operation()
    }
    
    class RefinedAbstraction {
        +Operation()
    }

    Implementor <|-- ConcreteImplementorA : extends
    Implementor <|-- ConcreteImplementorB : extends
    Abstraction <|-- RefinedAbstraction : extends
    Abstraction o-- Implementor : uses

    %% Class Diagram Comments
    Implementor : "Defines interface for implementation classes"
    ConcreteImplementorA : "Concrete implementation of the interface"
    ConcreteImplementorB : "Another concrete implementation"
    Abstraction : "Maintains reference to Implementor"
    RefinedAbstraction : "Extends the interface of Abstraction"
```
---
### <p align="center"> Sequence Diagram </p>
autonumber
```mermaid
sequenceDiagram
  autonumber
    participant Client as "Client"
    participant Abstraction as "Abstraction"
    participant RefinedAbstraction as "RefinedAbstraction"
    participant Implementor as "Implementor"
    participant ConcreteImplementorA as "ConcreteImplementorA"

    Client->>Abstraction: Create with Implementor
    Abstraction->>Implementor: Set Implementor
    Client->>RefinedAbstraction: Create with Implementor
    RefinedAbstraction->>Implementor: Set Implementor
    
    Client->>RefinedAbstraction: Call Operation()
    RefinedAbstraction->>Implementor: Call OperationImplementor()
    Implementor->>ConcreteImplementorA: Delegate to actual implementation
    ConcreteImplementorA-->>Implementor: Return result
    Implementor-->>RefinedAbstraction: Return result
    RefinedAbstraction-->>Client: Return result

    %% Sequence Diagram Comments
    note over Client,ConcreteImplementorA: "Here, the client uses refined abstraction to interact with different implementors without changing the abstraction."
```
---
### <p align="center"> Implementation </p>
<div align="left">

```c#
// Implementor interface
public abstract class Implementor
{
    public abstract void OperationImplementor();
}
```
```c#
// Concrete Implementors
public class ConcreteImplementorA : Implementor
{
    public override void OperationImplementor()
    {
        Console.WriteLine("ConcreteImplementorA performs operation.");
    }
}

public class ConcreteImplementorB : Implementor
{
    public override void OperationImplementor()
    {
        Console.WriteLine("ConcreteImplementorB performs operation.");
    }
}
```
```c#
// Abstraction
public abstract class Abstraction
{
    protected Implementor implementor;

    protected Abstraction(Implementor implementor)
    {
        this.implementor = implementor;
    }

    public abstract void Operation();
}
```
```c#
// Refined Abstraction
public class RefinedAbstraction : Abstraction
{
    public RefinedAbstraction(Implementor implementor) : base(implementor) { }

    public override void Operation()
    {
        // Refined behavior can be added here
        implementor.OperationImplementor();
    }
}
```
```c#
// Client code
public class Program
{
    public static void Main(string[] args)
    {
        // Create implementor objects
        Implementor implementorA = new ConcreteImplementorA();
        Implementor implementorB = new ConcreteImplementorB();

        // Create abstraction with different implementors
        Abstraction abstractionA = new RefinedAbstraction(implementorA);
        Abstraction abstractionB = new RefinedAbstraction(implementorB);

        // Call operations on abstractions
        abstractionA.Operation(); // Output: ConcreteImplementorA performs operation.
        abstractionB.Operation(); // Output: ConcreteImplementorB performs operation.
    }
} 
```
</div>

<!-- by IxI-Enki -->classDiagram
    