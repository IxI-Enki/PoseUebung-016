<!-- by IxI-Enki -->

# Chain
### <p align="center"> Class Diagram </p>
```mermaid
classDiagram
  direction BT
    class Handler {
        <<abstract>>
        +Handler nextHandler
        +SetNext(Handler handler): void
        +HandleRequest(Request request): void
    }

    class ConcreteHandlerA {
        +HandleRequest(Request request): void
    }

    class Request {
        <<struct>>
        +string requestType
        +string requestContent
    }

    class Client {
        +MakeRequest(): void
    }

    Handler <|-- ConcreteHandlerA : extends
    Handler "1" o-- "0..1" Handler : nextHandler
    Client --> Handler : uses
    ConcreteHandlerA --> Request : handles

    note for Handler "Base handler class. Defines interface for handling requests."
    note for ConcreteHandlerA "Handles or delegates specific type of requests."
    note for Request "Represents the request data."
    note for Client "Initiates the chain by sending a request."
```
---
### <p align="center"> Sequence Diagram </p>
autonumber
```mermaid
sequenceDiagram
    participant Client as Client
    participant HandlerA as ConcreteHandlerA
    participant HandlerB as ConcreteHandlerB

    Client->>HandlerA: MakeRequest(request)
    alt can handle request
        HandlerA->>HandlerA: HandleRequest(request)
    else cannot handle request
        HandlerA->>HandlerB: HandleRequest(request)
        alt can handle request
            HandlerB->>HandlerB: HandleRequest(request)
        else cannot handle request
            HandlerB->>Client: Return unhandled request
        end
    end
```
---
### <p align="center"> Implementation </p>
<div align="left">

```c#
public abstract class Handler
{
    protected Handler successor;

    public void SetNext(Handler next)
    {
        this.successor = next;
    }

    public void HandleRequest(Request request)
    {
        if (CanHandle(request))
        {
            Handle(request);
        }
        else if (successor != null)
        {
            successor.HandleRequest(request); // Delegate to next handler
        }
    }

    protected abstract bool CanHandle(Request request);
    protected abstract void Handle(Request request);
}
```
```c#
public class ConcreteHandlerA : Handler
{
    protected override bool CanHandle(Request request)
    {
        return request.RequestType == "TypeA"; // Handles requests of TypeA
    }

    protected override void Handle(Request request)
    {
        Console.WriteLine("ConcreteHandlerA handled the request.");
    }
}
```
```c#
public struct Request
{
    public string RequestType { get; set; }
    public string RequestContent { get; set; }

    public Request(string type, string content)
    {
        RequestType = type;
        RequestContent = content;
    }
}
```
```c#
public class Client
{
    public void MakeRequest(Handler handler, string requestType, string content)
    {
        Request request = new Request(requestType, content);
        handler.HandleRequest(request);
    }
}

// Usage example in Main or another method:
public static void Main()
{
    Handler h1 = new ConcreteHandlerA();
    Handler h2 = new ConcreteHandlerB();
    h1.SetNext(h2);

    Client client = new Client();
    client.MakeRequest(h1, "TypeA", "Request for TypeA"); // Should be handled by ConcreteHandlerA
    client.MakeRequest(h1, "TypeB", "Request for TypeB"); // Should be handled by ConcreteHandlerB
}
```
</div>

<!-- by IxI-Enki -->