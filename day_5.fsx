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

module Pt1 =
  let solve() =
    let jumps =
      input.Split '\n'
      |> Seq.map Int32.Parse
      |> Array.ofSeq
    let jumpsCount = jumps |> Array.length
    let isInBounds index =
      0 <= index && index < jumpsCount

    let mutable currentIndex = 0
    let mutable stepCount = 0

    while currentIndex |> isInBounds do
      let currentStepValue = jumps.[currentIndex]
      let nextIndex = currentIndex + currentStepValue

      Array.set jumps currentIndex (currentStepValue + 1)
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
    let isInBounds index =
      0 <= index && index < jumpsCount

    let mutable currentIndex = 0
    let mutable stepCount = 0

    while currentIndex |> isInBounds do
      let currentStepValue = jumps.[currentIndex]
      let nextIndex = currentIndex + currentStepValue

      if currentStepValue >= 3
      then currentStepValue - 1
      else currentStepValue + 1
      |> Array.set jumps currentIndex

      currentIndex <- nextIndex
      stepCount <- stepCount + 1

    stepCount

let solutions: obj list = [Pt1.solve(); Pt2.solve()]
printfn "Solutions:"
solutions
|> Seq.iteri (fun i -> printfn "  part %i: %A" i)
