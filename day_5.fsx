open System

let input = System.IO.File.ReadAllText("day_5.input.txt").Trim()
let exampleInput =
  @"
0
3
0
1
-3
  ".Trim()

let flip fn a b = fn b a

module Pt1 =
  let solve() =
    let jumps =
      input.Split '\n'
      |> Seq.map Int32.Parse
      |> Array.ofSeq
    let jumpsCount = jumps |> Array.length

    let mutable currentIndex = 0
    let mutable stepCount = 0

    while 0 <= currentIndex && currentIndex < jumpsCount do
      let currentStepValue = jumps.[currentIndex]
      let nextIndex = currentIndex + currentStepValue

      currentStepValue
      |> (+) 1
      |> Array.set jumps currentIndex

      currentIndex <- nextIndex
      stepCount <- stepCount + 1

    stepCount

module Pt2 =
  let solve() =
    let jumps =
      input.Split '\n'
      |> Seq.map Int32.Parse
      |> Array.ofSeq
    let jumpsCount = jumps |> Array.length

    let mutable currentIndex = 0
    let mutable stepCount = 0

    while 0 <= currentIndex && currentIndex < jumpsCount do
      let currentStepValue = jumps.[currentIndex]
      let nextIndex = currentIndex + currentStepValue

      currentStepValue
      |> if currentStepValue >= 3 then (flip (-) 1) else ((+) 1)
      |> Array.set jumps currentIndex

      currentIndex <- nextIndex
      stepCount <- stepCount + 1

    stepCount

let solutions: obj list = [Pt1.solve(); Pt2.solve()]
printfn "Solutions:"
solutions
|> Seq.iteri (fun i -> printfn "  part %i: %A" i)
