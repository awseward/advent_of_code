#load "util.fsx"

open System
open System.Text.RegularExpressions
open AdventOfCode.Util

let strSplit (sep: string) (input: string) =
  input.Split([|sep|], StringSplitOptions.RemoveEmptyEntries)

module RecursiveCircus =
  type Program =
    { Name: string
      Weight: int
      ChildNames: string list option }

  let parseLine (line: string) =
    let nameAndWeightMatch = Regex.Match(line.Trim(), "^([a-zA-z]+)\ \((\d+)\)")
    let name = nameAndWeightMatch.Groups.[1].Value
    let weight = nameAndWeightMatch.Groups.[2].Value |> Int32.Parse

    let childrenMatch = Regex.Match(line.Trim(), " -> (.+)$")
    let childNames =
      if not childrenMatch.Success then
        None
      else
        childrenMatch.Groups.[1].Value
        |> strSplit ", "
        |> List.ofArray
        |> Some

    { Name = name
      Weight = weight
      ChildNames = childNames }

checkParts <|
  [ { new IPart<string, string> with
        member this.Number = 1
        member this.ActualInput = System.IO.File.ReadAllText("day_7.input.txt").Trim()
        member this.Examples =
          [ { Input =
                @"
pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)
                ".Trim()
              Output = "tknk" } ]

        member this.Solve input =
          let programs =
            input.Split '\n'
            |> Array.map RecursiveCircus.parseLine

          let allNames =
            programs
            |> Array.map (fun p -> p.Name)
            |> Set.ofArray

          programs
          |> Seq.choose (fun p -> p.ChildNames)
          |> Seq.concat
          |> Set.ofSeq
          |> Set.difference allNames
          |> Set.minElement
    }
    { new IPart<string, string> with
        member this.Number = 2
        member this.ActualInput = "TODO"
        member this.Examples = []
        member this.Solve input = "TODO"
    } ]
