#if INTERACTIVE
#load "Suave.fs"
#else
module MiniSuaveIO
#endif
open Suave.Http
open Suave.Console
open Suave.Successful
open Suave.Combinators
open Suave.Filters

let private main argv = 
    let request = {Route = ""; Type = Suave.Http.GET}
    let response = {Content = ""; StatusCode = 200}
    let context = {Request = request; Response = response}

    let app = Choose [
                GET >=> Path "/hello" >=> OK "Hello GET"
                POST >=> Path "/hello" >=> OK "Hello POST"
                Path "/foo" >=> Choose [
                                    GET >=> OK "Foo GET"
                                    POST >=> OK "Foo POST"
                                ]
                ]

    executeInLoop context app
    System.Console.ReadKey() |> ignore
    0

#if INTERACTIVE
fsi.CommandLineArgs |> Array.toList |> List.tail |> List.toArray |> main
#else
[<EntryPoint>]
let entryPoint args = main args
#endif