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

type ForetakKonsesjon = 
    | Bank 
    | VerdipapirForetak

type PersonKonsesjon = 
    | Revisor 
    | Regnskapsforer 
type Land = 
    | Norge 
    | Sverige 

type Foretak = {
    EnhetNummer : string option 
    Navn : string 
    Konsesjoner : ForetakKonsesjon list 
    Land : Land 
} with 
    static member Create enr navn la = {
        EnhetNummer = enr
        Navn = navn
        Land = la
        Konsesjoner = []
    }

    member this.AddKonsesjon fk = 
        { this with Konsesjoner = fk :: this.Konsesjoner }

type Person = {
    PersonNummer : string 
    Fornavn : string 
    Etternavn : string 
    Land : Land
    Konsesjoner : PersonKonsesjon list
} with 
    static member Create pnr fnavn enavn la = {
        PersonNummer = pnr 
        Fornavn = fnavn 
        Etternavn = enavn 
        Land = la
        Konsesjoner = []
    }

    member this.AddKonsesjon pk = 
        { this with Konsesjoner = pk :: this.Konsesjoner }

type Enhet = 
    | Person of Person 
    | Foretak of Foretak

    
type Sfag = Sfag of Enhet list 

let emptySfag = Sfag []

let sfag = Sfag [
    Person.Create "24219451" "Marius" "Indreberg" Norge |> fun x -> x.AddKonsesjon Revisor |> Person
    Person.Create "24219441" "tom" "cruise" Sverige |> fun x -> x.AddKonsesjon Regnskapsforer |> Person
    Person.Create "24232941" "val" "kilmer" Sverige |> fun x -> x.AddKonsesjon Regnskapsforer |> Person
    Foretak.Create (Some "123154") "Bekk Consulting" Norge |> fun x -> x.AddKonsesjon Bank |> Foretak
    Foretak.Create None "Legit Accounting A/S" Sverige |> fun x -> x.AddKonsesjon VerdipapirForetak |> Foretak
] 


let leggTilEnhetSfag (enhet : Enhet) (sfag : Sfag) = 
    failwith ""

let leggTilKonsesjon<'a> (konsesjon : 'a) (enhet : Enhet) = 
    failwith ""

let finnFraLand (l : Land) (sFag : Sfag ) = 
    failwith ""

