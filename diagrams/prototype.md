<!-- by IxI-Enki -->

# Prototype
### <p align="center"> Class Diagram </p>
```mermaid
classDiagram
  direction RL
    class Prototype {
        <<abstract>>
        +Clone() Prototype
    }
    
    class ConcretePrototype1 {
        -field1: string
        +Clone() ConcretePrototype1
    }
    
    class ConcretePrototype2 {
        -field2: int
        +Clone() ConcretePrototype2
    }
    
    class Client {
        -prototype: Prototype
        +operation()
    }

    Prototype <|-- ConcretePrototype1 : Implements
    Prototype <|-- ConcretePrototype2 : Implements
    Client o-- Prototype : Uses

    note for Prototype "Abstract base class for cloning"
    note for ConcretePrototype1 "Concrete type 1 with specific fields"
    note for ConcretePrototype2 "Concrete type 2 with different fields"
    note for Client "Uses prototype to create clones"
```
---
### <p align="center"> Sequence Diagram </p>
```mermaid
sequenceDiagram
  autonumber
    participant C as Client
    participant P as Prototype
    participant CP1 as ConcretePrototype1
    participant CP2 as ConcretePrototype2

    C->>P: operation()
    P->>CP1: Clone()
    CP1-->>P: ConcretePrototype1*
    P-->>C: ConcretePrototype1

    C->>P: operation()
    P->>CP2: Clone()
    CP2-->>P: ConcretePrototype2*
    P-->>C: ConcretePrototype2

    note over C,P: Client requests a clone
    note over P,CP1: Clone method is called on a concrete prototype
    note over P,CP2: Cloning another type of prototype
```
---
### <p align="center"> Implementation </p>  
<div align="left">

```c#
// Abstract base class for cloning
public abstract class Prototype
{
    public abstract Prototype Clone();
}
```
```c#
// Concrete prototype with string field
public class ConcretePrototype1 : Prototype
{
    private string field1;

    public ConcretePrototype1(string field1)
    {
        this.field1 = field1;
    }

    // Override clone method for deep copy if necessary
    public override Prototype Clone()
    {
        return new ConcretePrototype1(this.field1);
    }
}
```
```c#
// Concrete prototype 2
// Another concrete prototype with integer field
public class ConcretePrototype2 : Prototype
{
    private int field2;

    public ConcretePrototype2(int field2)
    {
        this.field2 = field2;
    }

    // Override clone method for deep copy if necessary
    public override Prototype Clone()
    {
        return new ConcretePrototype2(this.field2);
    }
}
```
```c#
// Client class using prototypes
public class Client
{
    private Prototype prototype;

    public Client(Prototype prototype)
    {
        this.prototype = prototype;
    }

    public void Operation()
    {
        // Cloning the prototype
        Prototype newPrototype = prototype.Clone();
        // Use newPrototype for further operations
    }
}
```
</div>

<!-- by IxI-Enki -->