open Expecto
open System 
open ValuesAndFunctions
open RecordsAndUnions
open Collections
let tests = 
    testList "Tests values and functions" [
        test "Largest Number" {
            Expect.equal (returnNum 5) 5 "Expecting to get 5"
            Expect.notEqual (returnNum 5) 4 "Expecting to not get 4"
        }
        test "Find largest Integer" {
            Expect.equal (FindLargestInt 4 5) 5 "Expecting to get 5"
            Expect.notEqual (FindLargestInt 4 5) 4 "Expecting to not get 4"
        }
        test "Connect functions" {
            Expect.equal (ConnectFunction (fun x -> x.ToString()) (fun y -> Int32.Parse y) 2) 2 "Expecting to get 2"
            Expect.notEqual (ConnectFunction (fun x -> x.ToString()) (fun y -> Int32.Parse y) 5) 3 "Expecting not to get 3"
        }
        test "Double me" {
            Expect.equal (DoubleMe 4 5) 1024 "Expecting to get 1024"
            Expect.notEqual (FindLargestInt 5 5) 256 "Expecting to not get 256"
        }
    ]

[<EntryPoint>]
let main argv =
    let x = runTestsWithCLIArgs [] argv tests
    printfn "%A" x

    Sequences.findFirstFibNumberLargerThanN 100000 |> printfn "hello: %A"
    0
