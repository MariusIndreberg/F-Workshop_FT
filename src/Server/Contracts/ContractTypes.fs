module Contracts

type Person = {
    PersonNummer : string 
    Fornavn : string 
    Etternavn : string
} 

type Foretak = {
    OrgNummer : string
    Navn : string
}

type Enhet = 
    | Foretak of Foretak
    | Person of Person 