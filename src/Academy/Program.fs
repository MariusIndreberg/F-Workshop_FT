open Expecto
open System 
open ValuesAndFunctions
open RecordsAndUnions
open Collections
open DTOAndMisc
let tests = 
    testList "tests" [   
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

        testList "Records And Unions" [
            test "ChangeAge" {
                Expect.equal (changeAge { Name = "Hans"; Age = 18 } 22) { Name = "Hans"; Age = 22 } "Expecting age to be 22"
                Expect.equal (changeAge { Name = "Hans"; Age = 18 } 24) { Name = "Hans"; Age = 24 } "Expecting age to be 22"
            }
            let node = Node.Default<int> 2 None
            let node2 = Node.Default<int> 5 (Some node)
            test "SetNext" {
              
                Expect.equal (node.SetNext node2) { Value = 2; Next = Some node2 } "Next node should be 5"
                Expect.equal (node2.SetNext node) { Value = 5; Next = Some node } "Next node should be 2"
            }
            let list = Head (node2)
            let emptyList = Empty
            test "LinkedList - Add" {
                let newNode = Node.Default<int> 8 None
                Expect.equal (LinkedList.Add newNode list) (Head newNode) "Head node should be"
                Expect.equal (LinkedList.Add  node emptyList) (Head node) "Head node should be"
            }
            test "LinkedList - Remove" {
                let nodeToRemove = node
                Expect.equal (LinkedList.Remove nodeToRemove list ) (Head node2) "Head node should be"
                Expect.equal (LinkedList.Remove  node emptyList) (Empty) "Head node should be"
            }
            test "LinkedList - Find" {
                let newNode = 8 
                Expect.equal (LinkedList.Find newNode list ) (None) "Head node should be"
                Expect.equal (LinkedList.Find  node.Value emptyList) (Some node) "Head node should be"
            }
            test "LinkedList - Sum" {
                Expect.equal (LinkedList.Sum list) 7 "Should be 7"
                Expect.equal (LinkedList.Sum emptyList) 0 "Head node should be"
            }

        ]

        testList "Collections" [
            test "FindlargestFibNumberOverN" {
                Expect.equal (Sequences.findFirstFibNumberLargerThanN 100000 ) 121393 ""
                Expect.equal (Sequences.findFirstFibNumberLargerThanN 1000) 1597  ""
            }
        ]

        testList "DTOAndMisc" [
            test "Stringify - HTML" {
                let htmlStr = "<html><head><meta charset=UTF-8/></head><body><a href=https://google.no>Click me</a><div><img src=mypic.jpg></img></div></body></html>"
                Expect.equal (DTOAndMisc.stringify html) htmlStr "html in string format"
            }
        ]
    ]
    

[<EntryPoint>]
let main argv =
    runTestsWithCLIArgs [] argv tests |> ignore
    0
