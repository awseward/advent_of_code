open System

let input = () // FIXME

module Pt1 =
  let solve() =
    (*  _____ ___  ____   ___
       |_   _/ _ \|  _ \ / _ \
         | || | | | | | | | | |
         | || |_| | |_| | |_| |
         |_| \___/|____/ \___/  *)
    ()

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
