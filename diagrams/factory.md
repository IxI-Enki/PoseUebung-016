<!-- by IxI-Enki -->

# Factory
### <p align="center"> Class Diagram </p>
```mermaid
classDiagram
    direction TB
    class Product {
        <<interface>>
        +void Operation()
    }
    
    class ConcreteProductA {
        +void Operation()
    }
    class ConcreteProductB {
        +void Operation()
    }
    
    ConcreteProductA --|> Product : implements
    ConcreteProductB --|> Product : implements

    class Creator {
        <<abstract>>
        +Product FactoryMethod()
        +void SomeOperation()
    }

    class ConcreteCreatorA {
        +Product FactoryMethod()
    }
    class ConcreteCreatorB {
        +Product FactoryMethod()
    }

    ConcreteCreatorA --|> Creator : extends
    ConcreteCreatorB --|> Creator : extends

    Creator --> Product : uses

    note for Product "Defines the interface for objects the factory method creates."
    note for ConcreteProductA "Implements Product interface."
    note for ConcreteProductB "Another implementation of the Product interface."
    note for Creator "Declares the factory method which returns an object of type Product."
    note for ConcreteCreatorA "Overrides the factory method to return an instance of ConcreteProductA."
    note for ConcreteCreatorB "Overrides the factory method to return an instance of ConcreteProductB."
```
---
### <p align="center"> Sequence Diagram </p>
```mermaid
sequenceDiagram
    participant Client as Client
    participant Creator as ConcreteCreator
    participant Product as ConcreteProduct

    Client->>Creator: Create a product
    Creator->>Product: new()
    Product-->>Creator: 
    Creator-->>Client: Return Product

    Note over Client,Product: Here, the Client doesn't know which concrete product it gets.
```
---
### <p align="center"> Implementation </p>
<div align="left">

```c#
public interface IProduct 
{ 
  void Operation(); 
}
```
```c#
public abstract class Creator
{
    public abstract IProduct FactoryMethod();
    public void SomeOperation()
    {
        var product = FactoryMethod();
        // Use product...
    }
}
```
```c#
public class ConcreteCreatorA : Creator
{
    public override IProduct FactoryMethod() => new ConcreteProductA();
}
```
</div>

<!-- by IxI-Enki -->
