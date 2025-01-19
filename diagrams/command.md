<!-- by IxI-Enki -->

# Command
### <p align="center"> Class Diagram </p>
```mermaid
classDiagram
  direction RL
    class Command {
        <<interface>>
        +Execute()
    }
    
    class ConcreteCommand {
        -Receiver receiver
        +Execute()
    }

    class Invoker {
        -Command command
        +SetCommand(Command cmd)
        +ExecuteCommand()
    }

    class Receiver {
        +Action()
    }

    class Client {
        +CreateCommand()
    }

    Command <|.. ConcreteCommand : implements
    Invoker o-- Command : uses
    ConcreteCommand o-- Receiver : has
    Client ..> ConcreteCommand : creates
    Client ..> Invoker : sets
```
---
### <p align="center"> Sequence Diagram </p>
```mermaid
sequenceDiagram
  autonumber
    participant Client
    participant Invoker
    participant ConcreteCommand
    participant Receiver

    Client->>ConcreteCommand: Create with Receiver
    Client->>Invoker: Set command
    Invoker->>ConcreteCommand: Execute()
    ConcreteCommand->>Receiver: Action()
    Receiver-->>ConcreteCommand: .
    ConcreteCommand-->>Invoker: .
```
---
### <p align="center"> Implementation </p>
<div align="left">

```c#
// Command interface
public interface ICommand
{
    void Execute();
}
```
```c#
// Concrete Command
public class ConcreteCommand : ICommand
{
    private Receiver receiver;

    public ConcreteCommand(Receiver receiver)
    {
        this.receiver = receiver;
    }

    public void Execute()
    {
        // Call receiver's Action method
        this.receiver.Action();
    }
}
```
```c#
// Receiver
public class Receiver
{
    public void Action()
    {
        // Perform some action
        Console.WriteLine("Receiver does something");
    }
}
```
```c#
// Invoker
public class Invoker
{
    private ICommand command;

    public void SetCommand(ICommand command)
    {
        this.command = command;
    }

    public void ExecuteCommand()
    {
        // Execute the command
        this.command?.Execute();
    }
}
```
```c#
// Client code
public class Program
{
    public static void Main()
    {
        Receiver receiver = new Receiver();
        ConcreteCommand concreteCommand = new ConcreteCommand(receiver);
        Invoker invoker = new Invoker();

        invoker.SetCommand(concreteCommand);
        invoker.ExecuteCommand(); // Outputs: Receiver does something
    }
}
```

</div>

<!-- by IxI-Enki -->