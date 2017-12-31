open System

let input = () // FIXME

module Pt1 =
  let solve() =
    ()

module Pt2 =
  let solve() =
    ()

let solutions: obj list = [Pt1.solve(); Pt2.solve()]
printfn "Solutions:"
solutions
|> Seq.iteri (fun i -> printfn "  part %i: %A" i)
