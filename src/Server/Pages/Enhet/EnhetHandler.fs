module EnhetHandler
open Giraffe
open Microsoft.AspNetCore.Http
open Types
open EnhetRepository
open System.Threading.Tasks
open  Microsoft.FSharp.Control

let getEnhet next ctx = 
    task {
        try 
            
            return! Successful.OK " " next ctx
        with 
            e -> return! ServerErrors.INTERNAL_ERROR e.Message next ctx
    }

let getForetak next ctx = 
    task {
        try 
            let! foretak = getForetak ()
            let resp = 
                foretak 
                |> Seq.map Foretak.FromDBtype
                |> Seq.toList 
                |> sequence 
                |> function 
                    | Ok res -> Successful.OK res 
                    | Error err -> RequestErrors.BAD_REQUEST err
            return! resp next ctx
        with 
            e -> return! ServerErrors.INTERNAL_ERROR e.Message next ctx
    }


let saveEnhet (next : HttpFunc) (ctx : HttpContext) = 
    task {
        try 
            let! e = ctx.BindJsonAsync<Contracts.Enhet>()      
            let resp =
                e
                |> Enhet.Create
                |> Result.map (saveEnhet) 
                |> function 
                    | Ok res -> Successful.OK (res |> Async.AwaitTask |> Async.RunSynchronously)
                    | Error err -> RequestErrors.BAD_REQUEST err
            return! resp next ctx
        with 
            e -> return! ServerErrors.INTERNAL_ERROR e.Message next ctx
    }

let webApi<'a> : HttpHandler = 
    choose [
        GET >=> choose [
            route "/" >=> getEnhet
            route "/foretak" >=> getForetak
        ]
        POST >=> choose [
            route "/" >=> saveEnhet 
        ]
    ]