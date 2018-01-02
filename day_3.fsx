open System

(*
       5  4  3   2   1  60   9  8  7
       6  7  6   5   4   3   2  1  6
       7  8 17  16  15  14  13 30  5
       8  9 18   5   4   3  12 29  4
       9 40 19   6   1   2  11 28  3
      70  1 20   7   8   9  10 27  2
       1  2 21  22  23  24  25 26  1
       2  3  4   5   6   7   8  9 50
       3  4  5   6   7   8   9 80  1


  NOTES:
    1. Bottom right corners are { 1, 3, 5, ... }^2
    2. ...

btmLft                   265149                      btmRt
|________________________________________________________|
    |
    length = sqrt(btmRt)

  btmRt  = 265225
  length = 515

*)

let input = 265149

let oddInts = 1 |> Seq.unfold (fun i -> Some(i, i + 2))

module Pt1 =
  let solve() =
    let sideLength =
      oddInts
      |> Seq.skipWhile (fun i -> i * i < input)
      |> Seq.take 1
      |> Seq.head

    let nextSquare = sideLength * sideLength

    printfn "%i² = %i" sideLength (nextSquare)
    printfn "input: %i" input
    printfn "%i - %i = %i" nextSquare input (nextSquare - input)

    let stepsToMidpointFromBtmRt = (sideLength / 2)
    let stepsFromInputToMidpoint = stepsToMidpointFromBtmRt - (nextSquare - input)

    stepsFromInputToMidpoint
    |> printfn "%A"

    stepsFromInputToMidpoint + stepsToMidpointFromBtmRt

module Pt2 =
  type Cell =
    { Index: int
      Value: int option }

  type OuterRing =
    { Rank:       int
      SideLength: int
      StartsAt:   int
      Cells:      Cell list list }

  type Ring =
  | Core of Cell
  | Outer of OuterRing

  let emptyCell index = { Index = index; Value = None }
  let square x = x * x

  let withRank rank =
    if rank < 0 then
      failwith "Cannot have negative rank"
    else if rank < 1 then
      Core <| { Index = 1; Value = None }
    else
      let sideLength = oddInts |> Seq.item rank
      let startsAt = oddInts |> Seq.item (rank - 1) |> square |> (+) 1
      let cells =
        let cellsSize = sideLength - 1
        let endsAt = (startsAt + cellsSize * 4 - 1)

        [startsAt..endsAt]
        |> List.map emptyCell
        |> List.chunkBySize cellsSize

      { Rank       = rank
        SideLength = sideLength
        StartsAt   = startsAt
        Cells      = cells } |> Outer

  let withSideLength sideLength =
    oddInts
    |> Seq.findIndex ((=) sideLength)
    |> withRank

  let withIndex index =
    oddInts
    |> Seq.find (square >> (<=) index)
    (* NOTE: Above line a bit confusing, but amounts to this:
    |> Seq.find (fun n -> index <= (square n)) *)
    |> withSideLength

  withRank 0 |> printfn "withRank 0: %A"
  withSideLength 7 |> printfn "withSideLength 7: %A"
  withIndex 20 |> printfn "withIndex 20: %A"

  let solve() =
    (*  _____ ___  ____   ___
       |_   _/ _ \|  _ \ / _ \
         | || | | | | | | | | |
         | || |_| | |_| | |_| |
         |_| \___/|____/ \___/  *)
    ()

let solutions: obj list = [Pt1.solve(); Pt2.solve()]
printfn "Solutions:"
solutions
|> Seq.iteri (fun i -> printfn "  part %i: %A" i)
