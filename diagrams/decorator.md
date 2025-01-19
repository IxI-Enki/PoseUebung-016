<!-- by IxI-Enki -->

# Decorator
### <p align="center"> Class Diagram </p>
```mermaid
classDiagram
  direction RL
    class Component {
      <<Interface>>
      +operation()$
    }
    note for Component "Base interface"

    class ConcreteComponent {
      +operation()$
    }
    note for ConcreteComponent "Basic implementation"

    class Decorator {
      -component: Component
      +operation()$
    }
    note for Decorator "Wraps component"

    class ConcreteDecoratorA {
      -addedState: String
      +operation()$
      +addedBehavior()$
    }
    note for ConcreteDecoratorA "Adds behavior A"

    class ConcreteDecoratorB {
      +operation()$
      +addedBehavior()$
    }
    note for ConcreteDecoratorB "Adds behavior B"

    ConcreteComponent ..|>  Component : implements
    Decorator <|-- ConcreteDecoratorA : extends
    ConcreteDecoratorB --|> Decorator : extends
    Component <|.. Decorator : implements
    Decorator --> "1" Component : has
```
---
### <p align="center"> Sequence Diagram </p>
```mermaid
sequenceDiagram
  autonumber
    participant Client
    participant ConcreteComponent as "ConcreteComponent"
    participant DecoratorA as "ConcreteDecoratorA"
    participant DecoratorB as "ConcreteDecoratorB"

    Client->>ConcreteComponent: operation()
    activate ConcreteComponent
    ConcreteComponent-->>Client: result
    deactivate ConcreteComponent

    Client->>DecoratorA: operation()
    activate DecoratorA
    DecoratorA->>ConcreteComponent: operation()
    activate ConcreteComponent
    ConcreteComponent-->>DecoratorA: result
    deactivate ConcreteComponent
    DecoratorA-->>Client: modified result
    deactivate DecoratorA

    Client->>DecoratorB: operation()
    activate DecoratorB
    DecoratorB->>DecoratorA: operation()
    activate DecoratorA
    DecoratorA->>ConcreteComponent: operation()
    activate ConcreteComponent
    ConcreteComponent-->>DecoratorA: result
    deactivate ConcreteComponent
    DecoratorA-->>DecoratorB: modified result
    deactivate DecoratorA
    DecoratorB-->>Client: further modified result
    deactivate DecoratorB
```
---
### <p align="center"> Implementation </p>
<div align="left">

```c#
// Interface for components
public interface IComponent
{
    string Operation();
}
```
```c#
// Concrete component
public class ConcreteComponent : IComponent
{
    public string Operation() => "ConcreteComponent.Operation()";
}
```
```c#
// Base Decorator class
public abstract class Decorator : IComponent
{
    protected IComponent component;
    public Decorator(IComponent component) => this.component = component;
    public virtual string Operation() => component?.Operation() ?? string.Empty;
}
```
```c#
// Concrete Decorator A
public class ConcreteDecoratorA : Decorator
{
    private string addedState;
    public ConcreteDecoratorA(IComponent component) : base(component) => addedState = "New State";
    public override string Operation() => $"{base.Operation()} + Added Behavior A";
    public string AddedBehavior() => $"DecoratorA.AddedBehavior() with state: {addedState}";
}
```
```c#
// Concrete Decorator B
public class ConcreteDecoratorB : Decorator
{
    public ConcreteDecoratorB(IComponent component) : base(component) { }
    public override string Operation() => $"{base.Operation()} + Added Behavior B";
    public string AddedBehavior() => "DecoratorB.AddedBehavior()";
}
```
```c#
// Usage example
class Program
{
    static void Main()
    {
        IComponent simple = new ConcreteComponent();
        Console.WriteLine(simple.Operation());

        IComponent decoratorA = new ConcreteDecoratorA(simple);
        Console.WriteLine(decoratorA.Operation());
        Console.WriteLine(((ConcreteDecoratorA)decoratorA).AddedBehavior());

        IComponent decoratorB = new ConcreteDecoratorB(decoratorA);
        Console.WriteLine(decoratorB.Operation());
        Console.WriteLine(((ConcreteDecoratorB)decoratorB).AddedBehavior());
    }
}
```
</div>

<!-- by IxI-Enki -->