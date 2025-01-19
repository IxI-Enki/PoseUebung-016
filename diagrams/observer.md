<!-- by IxI-Enki -->

# Observer
### <p align="center"> Class Diagram </p>
```mermaid
classDiagram
  direction RL
    class Subject {
        <<abstract>>
        +List~Observer~ observers
        +Attach(Observer observer)
        +Detach(Observer observer)
        +Notify()
    }
    
    class ConcreteSubject {
        -State state
        +GetState(): State
        +SetState(State state)
    }
    
    class Observer {
        <<abstract>>
        +Update()
    }
    
    class ConcreteObserver {
        -Subject subject
        -State state
        +Update()
    }
    
    Subject <|.. ConcreteSubject : implements
    Observer <|.. ConcreteObserver : implements
    Subject --o Observer : observers
    ConcreteObserver -- ConcreteSubject : observes

    %% Explanation:
    %% - Subject is an abstract class that manages a list of Observer objects.
    %% - ConcreteSubject is a concrete implementation of Subject, which notifies
```
---
### <p align="center"> Sequence Diagram </p>
```mermaid
sequenceDiagram
    participant ConcreteSubject as "ConcreteSubject"
    participant ConcreteObserverA as "ConcreteObserverA"
    participant ConcreteObserverB as "ConcreteObserverB"

    ConcreteSubject->>ConcreteObserverA: Attach()
    ConcreteSubject->>ConcreteObserverB: Attach()
    ConcreteSubject->>ConcreteSubject: SetState(newState)
    ConcreteSubject->>ConcreteSubject: Notify()
    ConcreteSubject-->>ConcreteObserverA: Update()
    activate ConcreteObserverA
    ConcreteObserverA-->>ConcreteSubject: GetState()
    ConcreteSubject-->>ConcreteObserverA: Return State
    deactivate ConcreteObserverA
    ConcreteSubject-->>ConcreteObserverB: Update()
    activate ConcreteObserverB
    ConcreteObserverB-->>ConcreteSubject: GetState()
    ConcreteSubject-->>ConcreteObserverB: Return State
    deactivate ConcreteObserverB

    %% Explanation:
    %% - ConcreteSubject attaches observers to its list.
    %% - When its state changes, it calls Notify() which triggers Update() for each observer.
    %% - Each observer then queries the subject to get the updated state, demonstrating the pull model of the observer pattern where the observer pulls the latest state from the subject.
```
---
### <p align="center"> Implementation </p>
<div align="left">

```c#
// Subject (Abstract Class)
public abstract class Subject
{
    protected List<IObserver> observers = new List<IObserver>();
    
    public void Attach(IObserver observer) => observers.Add(observer);
    public void Detach(IObserver observer) => observers.Remove(observer);
    public void Notify() 
    {
        foreach (var observer in observers) 
        {
            observer.Update();
        }
    }
}
```
```C#
// ConcreteSubject 
public class ConcreteSubject : Subject
{
    private string _state;

    public string State 
    { 
        get => _state; 
        set 
        { 
            _state = value; 
            Notify(); 
        } 
    }
}
```
```C#
// Observer (Interface in C#)
public interface IObserver
{
    void Update();
}
```
```C#
// ConcreteObserver
public class ConcreteObserver : IObserver
{
    private string _observerState;
    private ConcreteSubject _subject;

    public ConcreteObserver(ConcreteSubject subject)
    {
        this._subject = subject;
    }

    public void Update()
    {
        _observerState = _subject.State;
        // Perform actions based on new state
    }
}
```

<!-- by IxI-Enki -->