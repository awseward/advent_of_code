open System

type Example<'input, 'answer> =
  { Input:    'input
    Answer:   'answer }

module Pt1 =
  let examples = [ { Input  = @"0	2	7	0"; Answer = 5 } ]

  let solve input =
    (*  _____ ___  ____   ___
       |_   _/ _ \|  _ \ / _ \
         | || | | | | | | | | |
         | || |_| | |_| | |_| |
         |_| \___/|____/ \___/  *)
    None

module Pt2 =
  let examples = [ { Input  = @"0	2	7	0"; Answer = 4 } ]

  let solve input =
    (*  _____ ___  ____   ___
       |_   _/ _ \|  _ \ / _ \
         | || | | | | | | | | |
         | || |_| | |_| | |_| |
         |_| \___/|____/ \___/  *)
    None

let checkExample solveFn example =
  let answer = solveFn example.Input
  if (answer :> obj) <> (example.Answer :> obj) then
    eprintfn "Nope! Was expecting %A, but got %A" (example.Answer :> obj) answer

[ (Pt1.solve, Pt1.examples)
  (Pt2.solve, Pt2.examples) ]
|> List.iter (fun (solve, examples) -> examples |> List.iter (checkExample solve))
