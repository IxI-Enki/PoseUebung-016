### <p align="center"> Class Diagram </p>
```mermaid
classDiagram
    class Singleton {
      <<singleton>>
      +Instance() Singleton
      -Singleton()
      +SomeOperation() void
    }

    class ISingletonOperation {
      <<interface>>
      +SomeOperation() void
    }

    class AbstractSingleton {
      <<abstract>>
      #AbstractSingleton()
      +Instance() AbstractSingleton
      +SomeOperation() void
    }

    Singleton --|> ISingletonOperation : implements
    AbstractSingleton --|> ISingletonOperation : implements
    Singleton --|> AbstractSingleton : extends

    note for Singleton "Singleton ensures only one instance of the class is created."
    note for ISingletonOperation "Interface defining operations for Singleton and AbstractSingleton."
    note for AbstractSingleton "Abstract base class for Singleton pattern, managing instance creation."
```
---
### <p align="center"> Sequence Diagram </p>
```mermaid
sequenceDiagram
    participant Client
    participant Singleton

    Client->>Singleton: Get Instance()
    alt Instance does not exist
        Singleton->>Singleton: Create new Singleton
        Singleton->>Singleton: Return new instance
    else Instance exists
        Singleton->>Singleton: Return existing instance
    end
    Singleton->>Client: Return Singleton instance
    Client->>Singleton: Call SomeOperation()
    Singleton->>Client: Operation result
```
---
### <p align="center"> Implementation </p>
<div align="left">

```c#
  public sealed class Singleton : ISingletonOperation
  {
      private static readonly Singleton _instance = new Singleton();
      private Singleton() { } // Private constructor

      public static Singleton Instance => _instance;

      public void SomeOperation()
      {
          // Implementation
      }
  }
```
```c#
public interface ISingletonOperation
{
    void SomeOperation();
}
```
```c#
  public abstract class AbstractSingleton : ISingletonOperation
  {
      protected AbstractSingleton() { } // Protected constructor
      public abstract void SomeOperation();
  }
```
</div>

<!-- by IxI-Enki -->