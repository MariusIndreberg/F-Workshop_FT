module EnhetRepository 
open Types
open Dapper.FSharp.PostgreSQL
open Dapper.FSharp
open System.Data
open DbTypes
open System.Threading.Tasks

let conn = new Npgsql.NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=mysecretpassword;") :> IDbConnection
let foretakTable = table<ForetakDB>

let saveForetak (f : Foretak) = 
    insert {
        into foretakTable
        value (Foretak.ToDBType f)
    } |> conn.InsertAsync

let getForetak (id : int) = 
    select {
        for f in foretakTable do 
        where (f.Id = id)
    } |> conn.SelectAsync<Foretak>

let saveEnhet (e : Enhet) = 
    match e with 
    | Foretak f -> 
        try 
            Ok <| saveForetak f 
        with
            e -> Error e.Message 
    | _ -> Ok <| Task.Factory.StartNew (fun x -> 1)

let getEnhet (e : Enhet) = 
    match e with 
    | Foretak f -> getForetak 0
    | _ -> Task.Factory.StartNew (fun x -> Seq.empty)
