module EnhetRepository 
open Types
open Dapper.FSharp.PostgreSQL
open Dapper.FSharp
open System.Data
open DbTypes
open System.Threading.Tasks

let conn = new Npgsql.NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=mysecretpassword;") :> IDbConnection
let foretakTable = table'<ForetakDB> "foretak" |> inSchema "dbo" 

let saveForetak (f : Foretak) = 
    insert {
        into foretakTable
        value (Foretak.ToDBType f)
    } |> conn.InsertAsync 

let getForetak () = 
    select {
        for f in foretakTable do 
        selectAll
    } |> conn.SelectAsync<ForetakDB>

let saveEnhet (e : Enhet) = 
    match e with 
    | Foretak f -> saveForetak f  
    | _ -> Task.Factory.StartNew ( fun x ->  1)

let getEnheter (e : Enhet) = 
    match e with 
    | Foretak f -> getForetak ()
    | _ -> Task.Factory.StartNew (fun x -> Seq.empty)
