// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

let isNumber (c:char) =
    match System.Int32.TryParse (c.ToString()) with
    | true, out -> Some out
    | false, _ -> None

let GetFirstNumber (input:string) =
    input
    |> Seq.toList
    |> List.pick isNumber

let GetLastNumber (input:string) =
    input
    |> Seq.toList
    |> List.rev
    |> List.pick isNumber

let GetNumber (input:string) =
    let n1 = input |> GetFirstNumber
    let n2 = input |> GetLastNumber
    int ($"{n1}{n2}")


let lines = Seq.toList (System.IO.File.ReadLines("/home/fabi/Dokumente/coding/adventofcode23/Day01/puzzle_input.txt"))

//printfn "%d" lines.Length

lines |> Seq.sumBy (fun l -> GetNumber l) |> printf "%d"


type Match =
    { Index : int
      Number : int }

let GetRealNumbers (input:string) =
    input
    |> Seq.mapi (fun i c -> (i,c))
    |> Seq.choose (fun (i, c) -> 
        match isNumber c with
        | Some nbr -> Some { Index = i; Number = nbr } 
        | None -> None)
    |> Seq.toList

let numberStrings = [ "one"; "two"; "three"; "four"; "five"; "six";"seven";"eight";"nine"]
let resolveWrittenNumber writtenNumber = 
    match writtenNumber with
    | "one" ->  1
    | "two" ->  2
    | "three" ->  3
    | "four" ->  4
    | "five" ->  5
    | "six" ->  6
    | "seven" ->  7
    | "eight" ->  8
    | "nine" ->  9
    | _ ->  0

let GetWrittenNumbers (input: string) =
    numberStrings
    |> Seq.collect (fun writtenNumber ->
        Seq.unfold( fun (state:int) ->
            let index = input.IndexOf(writtenNumber, state)
            if index <> -1 then
                Some({ Index = index; Number = resolveWrittenNumber(writtenNumber)}, index+1)
            else
                None
        ) 0
    )
    |> List.ofSeq

        

let getFirstAndLast (matches: Match list)=
    let low = matches |> Seq.minBy (fun m -> m.Index)
    let high = matches |> Seq.maxBy (fun m -> m.Index)
    [low; high]

let matchToNumber (matches: Match list) =
    int ($"{matches.Head.Number}{matches[1].Number}")

let sumOfMatches (matches: Match list) =
    matches
    |> List.sumBy (fun m -> m.Number)

printf "\r\n"

let allNumbers =
    List.map( fun line ->
        let lineReal = GetRealNumbers line
        let linewritten = GetWrittenNumbers line
        List.append lineReal linewritten
        )

allNumbers lines
    |> List.map getFirstAndLast
    |> List.map matchToNumber
    |> List.sum
    |> printf "%d"