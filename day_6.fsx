open System

let input = @"10	3	15	10	5	15	5	15	9	2	5	8	5	2	3	6"
let exampleInput = @"0	2	7	0"

let splitOnTab (tabbed: string) = tabbed.Split '\t'

let toBanks = splitOnTab >> Array.map Int32.Parse

let redistribute oldBanks =
  let startingIndex =
    oldBanks
    |> Array.max
    |> (fun value -> oldBanks |> Array.findIndex ((=) value))

  oldBanks
  |> Array.copy
  |> (fun banks ->
        let banksLength = banks |> Array.length
        let getNextIndex index = (index + 1) % banksLength
        let fullest = (startingIndex, banks.[startingIndex])
        Array.set banks startingIndex 0

        fullest
        |> (fun (index, blocks) -> (index |> getNextIndex, blocks))
        |> Seq.unfold (fun state ->
            let (lastIndex, blocks) = state

            if blocks = 0
            then
              None
            else
              let newState = (lastIndex |> getNextIndex, blocks - 1)

              Some(state, newState))
        |> Seq.map fst
        |> Seq.iter (fun index ->
            let currentValue = banks.[index]
            Array.set banks index (currentValue + 1))

        banks)

module Seq =
  let unfoldBy fn = Seq.unfold (fun item -> Some(item, (fn item)))

module Pt1 =
  let solve() =
    input
    |> toBanks
    |> Seq.unfoldBy redistribute
    |> Seq.mapi (fun idx banks -> (idx, banks))
    |> Seq.scan
        (fun (set: Set<int[]>, _) (idx, banks) ->
          if set.Contains banks
          then
            printfn "loop detected: %A" banks
            (set, Some(idx))
          else
            (set.Add(banks), None))
        (Set.empty, None)
    |> Seq.pick snd

module Pt2 =
  let solve() =
    input
    |> toBanks
    |> Seq.unfoldBy redistribute
    |> Seq.mapi (fun idx banks -> (idx, banks))
    |> Seq.scan
        (fun (map: Map<int[], int>, _) (idx, banks) ->
          if map.ContainsKey banks
          then
            printfn "Loop detected: %A" banks
            let loopSize = idx - map.Item(banks)

            (map, Some(loopSize))
          else
            (map.Add(banks, idx), None))
        (Map.empty, None)
    |> Seq.pick snd

let solutions: obj list = [Pt1.solve(); Pt2.solve()]
printfn "Solutions:"
solutions
|> Seq.iteri (fun i -> printfn "  part %i: %A" i)
