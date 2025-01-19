<!-- by IxI-Enki -->

# Template Method
### <p align="center"> Class Diagram </p>
```mermaid
classDiagram
  direction RL
    class AbstractClass {
        <<abstract>>
        +TemplateMethod() void
        +PrimitiveOperation1() abstract void
        +PrimitiveOperation2() abstract void
    }

    class ConcreteClassA {
        +PrimitiveOperation1() void
        +PrimitiveOperation2() void
    }

    class ConcreteClassB {
        +PrimitiveOperation1() void
        +PrimitiveOperation2() void
    }

    AbstractClass <|-- ConcreteClassA : extends
    AbstractClass <|-- ConcreteClassB : extends

    note for AbstractClass "Defines skeleton of algorithm"
    note for ConcreteClassA "Implements specific steps"
    note for ConcreteClassB "Implements specific steps"
```
---
### <p align="center"> Sequence Diagram </p>
```mermaid
sequenceDiagram
  autonumber
    participant Client as "Client"
    participant AC as "AbstractClass"
    participant CCA as "ConcreteClassA"
    participant CCB as "ConcreteClassB"

    Client->>AC: Call TemplateMethod()
    AC-->>AC: Start TemplateMethod
    AC->>CCA: PrimitiveOperation1()
    activate CCA
    CCA-->>AC: 
    deactivate CCA
    AC->>CCA: PrimitiveOperation2()
    activate CCA
    CCA-->>AC: 
    deactivate CCA
    AC-->>Client: Return

    Note over Client,AC: First Scenario with ConcreteClassA
    Client->>AC: Call TemplateMethod()
    AC-->>AC: Start TemplateMethod
    AC->>CCB: PrimitiveOperation1()
    activate CCB
    CCB-->>AC: 
    deactivate CCB
    AC->>CCB: PrimitiveOperation2()
    activate CCB
    CCB-->>AC: 
    deactivate CCB
    AC-->>Client: Return
    Note over Client,AC: Second Scenario with ConcreteClassB
```
---
### <p align="center"> Implementation </p>
<div align="left">

```c#
public abstract class AbstractClass
{
    // Template method defines the skeleton of an algorithm
    public void TemplateMethod()
    {
        PrimitiveOperation1();
        PrimitiveOperation2();
        Console.WriteLine("Common implementation");
    }

    // Abstract methods to be implemented by subclasses
    public abstract void PrimitiveOperation1();
    public abstract void PrimitiveOperation2();
}
```
```c#
public class ConcreteClassA : AbstractClass
{
    // Implementation of PrimitiveOperation1 specific to ClassA
    public override void PrimitiveOperation1()
    {
        Console.WriteLine("ConcreteClassA: PrimitiveOperation1");
    }

    // Implementation of PrimitiveOperation2 specific to ClassA
    public override void PrimitiveOperation2()
    {
        Console.WriteLine("ConcreteClassA: PrimitiveOperation2");
    }
}

public class ConcreteClassB : AbstractClass
{
    // Implementation of PrimitiveOperation1 specific to ClassB
    public override void PrimitiveOperation1()
    {
        Console.WriteLine("ConcreteClassB: PrimitiveOperation1");
    }

    // Implementation of PrimitiveOperation2 specific to ClassB
    public override void PrimitiveOperation2()
    {
        Console.WriteLine("ConcreteClassB: PrimitiveOperation2");
    }
}
```
```c#
class Program
{
    static void Main(string[] args)
    {
        AbstractClass aA = new ConcreteClassA();
        aA.TemplateMethod();

        AbstractClass aB = new ConcreteClassB();
        aB.TemplateMethod();
    }
}
```
</div>

<!-- by IxI-Enki -->