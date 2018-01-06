namespace AdventOfCode
  open System

  // Borrowed from: http://www.fssnip.net/7S3/title/Intersperse-a-list
  module List =
    let intersperse sep ls =
      List.foldBack (fun x -> function
        | [] -> [x]
        | xs -> x::sep::xs) ls []

  module Util =
    type Example<'input, 'output> =
      { Input:    'input
        Output:   'output }

    type IPart<'input, 'output> =
      abstract Number      : int
      abstract Examples    : Example<'input, 'output> list
      abstract ActualInput : 'input
      abstract Solve       : 'input -> 'output

    let check<'input, 'output> (part: IPart<'input, 'output>) =
      part.Number |> printfn "Checking Part %i"

      part.Examples
      |> List.iter (fun ex ->
          let output = part.Solve ex.Input
          if (output :> obj) <> (ex.Output :> obj) then
            eprintfn "  ✗ example failed. Expecting %A, but got %A" (ex.Output :> obj) (output :> obj)
      )

      part.ActualInput
      |> part.Solve
      |> printfn "  output: %A"

    let checkParts<'input, 'output> (parts: IPart<'input, 'output> list) =
      parts
      |> List.map (fun p -> (fun () -> check p))
      |> List.intersperse (fun () -> printfn "")
      |> List.iter (fun fn -> fn())
