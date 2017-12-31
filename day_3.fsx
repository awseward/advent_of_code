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
