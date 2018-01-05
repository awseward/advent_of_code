open System

let input = @"10	3	15	10	5	15	5	15	9	2	5	8	5	2	3	6"
let exampleInput = @"0	2	7	0"

let toBanks (tabbed: string) = tabbed.Split '\t' |> Seq.map Int32.Parse |> Array.ofSeq

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
    |> Seq.scan
        (fun (banksSet: Set<int[]>, foundDuplicate) banks ->
          if banksSet.Contains banks
          then
            printfn "loop detected: %A" banks
            (banksSet, true)
          else
            (banksSet.Add(banks), false)
        )
        (Set.empty, false)
    |> Seq.skip 1
    |> Seq.mapi (fun idx (_, foundDuplicate) -> (idx, foundDuplicate))
    |> Seq.find snd
    |> fst

module Pt2 =
  let solve() =
    input
    |> toBanks
    |> Seq.unfoldBy redistribute
    |> Seq.mapi (fun idx banks -> (idx, banks))
    |> Seq.scan
        (fun (banksMap: Map<int[], int>, duplicate) (idx, banks) ->
          if banksMap.ContainsKey banks
          then
            printfn "Loop detected: %A" banks
            (banksMap, Some(idx, banks))
          else
            (banksMap.Add(banks, idx), None))
        (Map.empty, None)
    |> Seq.find (snd >> Option.isSome)
    |> fun (banksMap, dupOption) ->
        let (dupIdx, dupBanks) = dupOption.Value

        dupIdx - banksMap.Item(dupBanks)

let solutions: obj list = [Pt1.solve(); Pt2.solve()]
printfn "Solutions:"
solutions
|> Seq.iteri (fun i -> printfn "  part %i: %A" i)
