#load "util.fsx"

open System
open AdventOfCode.Util

checkParts <|
  [ { new IPart<string, string> with
        member this.Number = 1
        member this.ActualInput = System.IO.File.ReadAllText "day_7.input.txt"
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
        member this.Solve input = "TODO"
    }
    { new IPart<string, string> with
        member this.Number = 2
        member this.ActualInput = "TODO"
        member this.Examples = []
        member this.Solve input = "TODO"
    } ]
