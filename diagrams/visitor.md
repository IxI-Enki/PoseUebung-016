<!-- by IxI-Enki -->

# Visitor
### <p align="center"> Class Diagram </p>
```mermaid
classDiagram
  direction TB
    class Element {
        <<abstract>>
        +Accept(Visitor): void
    }

    class ConcreteElementA {
        +Accept(Visitor): void
    }

    class ConcreteElementB {
        +Accept(Visitor): void
    }

    class Visitor {
        <<abstract>>
        +VisitConcreteElementA(ConcreteElementA): void
        +VisitConcreteElementB(ConcreteElementB): void
    }

    class ConcreteVisitor1 {
        +VisitConcreteElementA(ConcreteElementA): void
        +VisitConcreteElementB(ConcreteElementB): void
    }

    class ConcreteVisitor2 {
        +VisitConcreteElementA(ConcreteElementA): void
        +VisitConcreteElementB(ConcreteElementB): void
    }

    class ObjectStructure {
        +Attach(Element): void
        +Detach(Element): void
        +Accept(Visitor): void
    }

    Element <|-- ConcreteElementA : extends
    Element <|-- ConcreteElementB : extends
    Visitor <|-- ConcreteVisitor1 : extends
    Visitor <|-- ConcreteVisitor2 : extends

    Element --|> Visitor : uses
    ConcreteElementA --|> Visitor : uses
    ConcreteElementB --|> Visitor : uses
    ObjectStructure o-- Element : contains
```
---
### <p align="center"> Sequence Diagram </p>
```mermaid
sequenceDiagram
  autonumber
    participant OS as ObjectStructure
    participant V as Visitor
    participant CEA as ConcreteElementA
    participant CEB as ConcreteElementB

    OS->>OS: Attach(CEA)
    OS->>OS: Attach(CEB)

    activate OS
    OS->>V: Accept(V)
    V->>CEA: Accept(V)
    activate CEA
    CEA->>V: VisitConcreteElementA(CEA)
    deactivate CEA
    V->>CEB: Accept(V)
    activate CEB
    CEB->>V: VisitConcreteElementB(CEB)
    deactivate CEB
    deactivate OS
```
---
### <p align="center"> Implementation </p>
<div align="left">

```c#
public abstract class Element
{
    public abstract void Accept(Visitor visitor);
}
```
```c#
public class ConcreteElementA : Element
{
    public override void Accept(Visitor visitor)
    {
        // Calls the appropriate Visit method on the visitor
        visitor.VisitConcreteElementA(this);
    }

    public void OperationA()
    {
        // Element specific operation
    }
}

public class ConcreteElementB : Element
{
    public override void Accept(Visitor visitor)
    {
        visitor.VisitConcreteElementB(this);
    }

    public void OperationB()
    {
        // Another element specific operation
    }
}
```
```c#
public abstract class Visitor
{
    public abstract void VisitConcreteElementA(ConcreteElementA elementA);
    public abstract void VisitConcreteElementB(ConcreteElementB elementB);
}
```
```c#
public class ConcreteVisitor1 : Visitor
{
    public override void VisitConcreteElementA(ConcreteElementA element)
    {
        // Perform operation on ConcreteElementA
        Console.WriteLine("ConcreteVisitor1 on ConcreteElementA");
    }

    public override void VisitConcreteElementB(ConcreteElementB element)
    {
        // Perform operation on ConcreteElementB
        Console.WriteLine("ConcreteVisitor1 on ConcreteElementB");
    }
}

public class ConcreteVisitor2 : Visitor
{
    public override void VisitConcreteElementA(ConcreteElementA element)
    {
        // Another operation on ConcreteElementA
        Console.WriteLine("ConcreteVisitor2 on ConcreteElementA");
    }

    public override void VisitConcreteElementB(ConcreteElementB element)
    {
        // Another operation on ConcreteElementB
        Console.WriteLine("ConcreteVisitor2 on ConcreteElementB");
    }
} 
```
```c#
public class ObjectStructure
{
    private List<Element> elements = new List<Element>();

    public void Attach(Element element)
    {
        elements.Add(element);
    }

    public void Detach(Element element)
    {
        elements.Remove(element);
    }

    public void Accept(Visitor visitor)
    {
        foreach (var element in elements)
        {
            element.Accept(visitor);
        }
    }
}
```
</div>

<!-- by IxI-Enki -->

