module Collections


module Lists = 
    let emptyList = []

    let addToList (list : List<'a>) (elem : 'a) = failwith "what"

    let removeFromList (list : List<'a>) (elem : 'a) = failwith "what"

    let find (list: List<'a>) (elem) = failwith "what"

    let sumBy (list : List<'a>) func = failwith "what"

module Sequences = 
    let emptySequence = Seq.empty

    let findFirstFibNumberLargerThanN (n: int) =
        failwith ""

module Maps = 
    let emptyMap = Map.empty

    let addToMap (map : Map<int, int>) (key : int) (value : int) = 
        failwith ""

    let findValue (map : Map<int, int>) (key : int) = 
        failwith ""

    let updateMap (map : Map<int, int>) (key : int) (value : int) = 
        failwith ""

    let updateAllValues (map : Map<int, int>) (func : int -> int) = 
        failwith ""

module Arrays = 

    let emptyArray = Array.init (20) (fun idx -> 0)

    let updateSpecifiedIndex arr idx  =
        failwith ""


module Sets = 
    let emptySet = Set.empty

    let intersect setA setB = 
        failwith ""

    let difference setA setB = 
        failwith ""

    let union setA setB = 
        failwith ""

    let makeListDistinct (list : List<int>) : List<int> =
        failwith ""



