open System

let input =
  @"
abcde fghij
abcde xyz ecdab
a ab abc abd abf abj
iiii oiii ooii oooi oooo
oiii ioii iioi iiio
  ".Trim()

module Pt1 =
  let solve() =
    input.Split '\n'
    |> Seq.filter (fun row ->
        let words = row.Split ' '
        let distinct = words |> Seq.distinct

        (Seq.length words) = (Seq.length distinct)
    )
    |> Seq.length

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
