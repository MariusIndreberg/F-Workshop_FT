module Types
open System 
open Contracts
[<AutoOpen>]
module ResultExtensions = 

    let apply fRes xRes = 
        match fRes, xRes with 
        | Ok f, Ok x -> Ok (f x)
        | Error fe, Ok x -> Error fe 
        | Ok f, Error fx -> Error fx 
        | Error fe, Error fx -> Error (fx + ", " + fe)

    let (<*>) = apply
    let (<!>) = Result.map

[<AutoOpen>]
module ValidatorFunctions = 

    let isNotEmpty (str : string) = 
        if not <| String.IsNullOrWhiteSpace str then 
            Ok str 
        else 
            Error $"string was expected to null be empty or null"

    let noDigits (str : string) = 
        if str |> String.forall (Char.IsLetter) then 
            Ok str 
        else 
            Error $"Expected no numbers got {str}"

    let isNumerical (str : string) = 
        if (str |> String.forall Char.IsDigit) then 
            Ok str 
        else 
            Error $"Expected all numbers got {str}"

    let isLength (n : int) (str : string) = 
        if (str.Length <= n) then 
            Ok str 
        else 
            Error $"Expected {str} to be less length than {n}"
    


type StringLength = private StringLength of string 
module StringLength = 
    let private isValid str n = 
        (isNotEmpty >> Result.bind noDigits >> Result.bind (isLength n)) str

    let create (str : string) (n : int) = 
        match isValid str n with
        | Ok str -> Ok (StringLength str) 
        | Error err -> Error err

    let value (StringLength p) = 
        p

type PersonNummer = private PersonNummer of string
module PersonNummer = 

    let private isValid str = 
        (isNotEmpty >> Result.bind isNumerical >> Result.bind (isLength 11)) str

    let create (str : string) = 
        match isValid str with
        | Ok str -> Ok (PersonNummer str) 
        | Error err -> Error err

    let value (PersonNummer p) = 
        p

type OrgNummer = private OrgNummer of string 
module OrgNummer = 

    let private isValid str = 
        (isNumerical >> Result.bind (isLength 9)) str 
       
    let Create (str : string) = 
        isValid str |> Result.map OrgNummer
    
    let CreateOpt (str : string option) = 
        match str with 
        | Some s -> Create (s) |> Result.map Some 
        | None -> Ok None

    let value (OrgNummer p) = 
        p

type Person = private {
    PersonNummer : PersonNummer
    Fornavn : StringLength
    Etternavn : StringLength 
}
module Person =
    let private make p f e = {
        PersonNummer = p 
        Fornavn = f 
        Etternavn = e
    }
    let Create (p : Contracts.Person) = 
        make 
        <!> PersonNummer.create p.PersonNummer
        <*> StringLength.create p.Fornavn 64
        <*> StringLength.create p.Etternavn 64
    
type Foretak = private {
    Orgnummer : OrgNummer option
    Navn : StringLength
}
module Foretak = 
    open DbTypes
    let private make o n = {
        Orgnummer = o 
        Navn = n
    }

    let Create (p : Contracts.Foretak) = 
        make 
        <!> (OrgNummer.Create p.OrgNummer |> Result.map Some)
        <*> StringLength.create p.Navn 64

    let ToDBType (f : Foretak) : ForetakDB = 
        {
            Id = 0
            Navn = StringLength.value f.Navn 
            Orgnummer = f.Orgnummer |> Option.map (OrgNummer.value)
        }

    let FromDBtype (f : ForetakDB)  = 
        make 
        <!> ( OrgNummer.CreateOpt f.Orgnummer)
        <*> StringLength.create f.Navn 64

type Enhet = 
    | Person of Person 
    | Foretak of Foretak
module Enhet = 
    let Create (e : Contracts.Enhet) = 
        match e with 
        | Contracts.Foretak f -> Foretak.Create f |> Result.map Foretak
        | Contracts.Person p -> Person.Create p |> Result.map Person

    

