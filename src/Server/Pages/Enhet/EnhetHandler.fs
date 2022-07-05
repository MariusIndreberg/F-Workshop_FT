module EnhetHandler
open Giraffe
open Microsoft.AspNetCore.Http
open Types
open EnhetRepository

let getEnhet next ctx = 
    task {
        try 

            return! Successful.OK "get" next ctx
        with 
            e -> return! ServerErrors.INTERNAL_ERROR e.Message next ctx
    }

let foo next ctx = 
    Successful.OK "" next ctx

let saveEnhet (next : HttpFunc) (ctx : HttpContext) = 
    task {
        try 

            let! e = ctx.BindJsonAsync<Contracts.Enhet>()
            return! 
                e |> 
                (Enhet.Create
                >> Result.bind (saveEnhet >> Result.map Async.AwaitTask))
                |> function 
                | Ok res -> 
                    Successful.OK res next ctx
                | Error err ->  
                    RequestErrors.BAD_REQUEST err next ctx
        with 
            e -> return! ServerErrors.INTERNAL_ERROR e.Message next ctx
    }

let webApi<'a> : HttpHandler = 
    choose [
        GET >=> choose [
            route "/" >=> getEnhet
        ]
        POST >=> choose [
            route "/" >=> saveEnhet 
        ]
    ]