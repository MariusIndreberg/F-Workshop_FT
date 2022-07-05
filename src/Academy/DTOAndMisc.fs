module DTOAndMisc

type Attribute = {
    Name : string 
    Value : string 
}
with 
    static member Create tag  value = {
        Name = tag 
        Value = value
    }

    member this.ToString () = 
        failwith "Not implemented" 

type Element = {
    Name : string 
    Attributes : Attribute list 
    Elements : Html list 
}
with 
    static member Create tag attr elems = 
        let elem = {
            Name = tag 
            Attributes = attr 
            Elements = elems
        } 
        Element elem

    member this.ToString () = 
        failwith "Not implemented" 

and Html = 
    | Element of Element 
    | Text of string

let stringify (root : Html) : string = 
    failwith "Not implemented"

let html = 
    Element.Create "html" [] [
        Element.Create "head" [] [
            Element.Create "meta" [ Attribute.Create "charset" "UTF-8" ] []
        ]
        Element.Create "body" [] [
            Element.Create "a" [ Attribute.Create "href" "https://google.no" ] [ Text "Click me" ]
            Element.Create "div" [] [
                Element.Create "img" [ Attribute.Create "src" "mypic.jpg" ] []
            ]
        ]
    ]