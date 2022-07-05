module RecordsAndUnions

type Person = {
    Name : string 
    Age : int
}
with 
    static member Default = {
        Name = "Tom Cruise"
        Age = 29
    } 

let changeAge (person : Person) (age : int) : Person = 
    failwith "not implemented"


type Node<'a> = {
    Value : 'a 
    Next : Node<'a> option
}
with 
    static member Default value next = {
        Value = value 
        Next = next
    }

    member this.SetNext (nextNode : Node<'a>) = 
        failwith "not implemented"

    
type LinkedList<'a> =
    | Head of Node<'a>
    | Empty

module LinkedList = 
    
    let Add<'a> node list = 
        failwith "Not Implemented"

    let Remove<'a> node list = 
        failwith "Not implemented"

    let Find elem list = 
        failwith "Not Implemented"
    
    let Sum list = 
        failwith "Not Implemented"



    