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

module Pt1 =
  let solve() =
    (Set.empty, input |> toBanks)
    |> Seq.unfold(fun state ->
        let (encountered, currentBanks) = state
        let redistributed = redistribute currentBanks

        if encountered.Contains(redistributed)
        then
          printfn "loop detected: %A" redistributed
          None
        else
          let newState = (encountered.Add(redistributed), redistributed)

          Some <| (state, newState))
    |> Array.ofSeq
    |> Seq.length
    |> ((+) 1)

module Pt2 =
  let solve() =
    let mutable loopSize = 0 // Egh...

    (Map.empty, 1, input |> toBanks)
    |> Seq.unfold (fun state ->
        let (encountered, currentIter, currentBanks) = state
        let redistributed = redistribute currentBanks

        if encountered.ContainsKey(redistributed)
        then
          printfn "loop detected: %A" redistributed
          let originalIter = encountered |> Map.find(redistributed)

          loopSize <- currentIter - originalIter // Egh...

          None
        else
          let newState = (encountered.Add(redistributed, currentIter), currentIter + 1, redistributed)

          Some <| (state, newState))
    |> Array.ofSeq
    |> ignore

    loopSize

let solutions: obj list = [Pt1.solve(); Pt2.solve()]
printfn "Solutions:"
solutions
|> Seq.iteri (fun i -> printfn "  part %i: %A" i)
