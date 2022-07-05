module Server
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open System.Text.Json
open System.Text.Json.Serialization

let webApp =
    choose [
        route "/ping"   >=> text "pong"
        subRoute "/enhet" EnhetHandler.webApi
    ]

let configureApp (app : IApplicationBuilder) =
    app.UseStaticFiles()
        .UseGiraffe webApp

let configureServices (services : IServiceCollection) =
    services.AddGiraffe() |> ignore
    let jsonOptions = JsonSerializerOptions()
    jsonOptions.Converters.Add(JsonFSharpConverter())
    services.AddSingleton(jsonOptions) |> ignore
    services.AddSingleton<Json.ISerializer, SystemTextJson.Serializer>() |> ignore 

[<EntryPoint>]
let main _ =
    Dapper.FSharp.OptionTypes.register()
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
                    .Configure(configureApp)
                    .ConfigureServices(configureServices)
                    |> ignore)
        .Build()
        .Run()
    0