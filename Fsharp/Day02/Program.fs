// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

type Color =
    | Red = 0
    | Green = 1
    | Blue = 2

type Draw = {
    Amount : int
    Color: Color
}

type GameRecord = {
    Id: int
    Draws: Draw seq
}

let ParseColor (color:string) =
    match color with
    | "red" -> Color.Red
    | "green" -> Color.Green
    | "blue" -> Color.Blue

let ParseDraw (text:string) =
    let drawArr = text.Trim().Split(" ")
    { Amount = int (drawArr[0]); Color = ParseColor drawArr[1]}

let ParseRound (text:string) : Draw seq =
    let round = text.Split("; ")
    round
    |> Seq.collect (fun x -> x.Split(", "))
    |> Seq.map ParseDraw

let ParseGameId (gameSubstring:string)=
    int (gameSubstring.Replace("Game ", ""))

let ParseGameRecord(line:string) =
    let game = line.Split(":")[0]
    let draws = line.Split(":")[1]
    { Id = ParseGameId(game); Draws = ParseRound draws}

let lines = System.IO.File.ReadAllLines("./../../../../../Day02/puzzle_input.txt")

//print test parsing
let printDraws (input:Draw seq)=
    input
    |> Seq.map (fun (x:Draw) -> 
        match x.Color with
        | Color.Red -> $"red {x.Amount}"
        | Color.Green -> $"green {x.Amount}"
        | Color.Blue -> $"blue {x.Amount}"
        | _ -> ""
        )
    |> Seq.iter (printf "%s")
// for line in lines do
//     let record = ParseGameRecord line
//     printfn "id: %d draws:" record.Id
//     printDraws (record.Draws)

let gameBag = seq { { Amount = 12; Color = Color.Red };{ Amount = 13; Color = Color.Green };{ Amount = 14; Color = Color.Blue }; }

let IsPossible (bag: Draw seq, round: Draw seq) :bool =
    let mutable result = true
    for draw in round do
        let bagElement = bag |> Seq.filter (fun x -> draw.Color = x.Color)
        if draw.Amount > (Seq.item 0 bagElement).Amount then
            result <- false
    result

let possibleSum =
    let mutable sum = 0
    for line in lines do
        let record = ParseGameRecord line
        if IsPossible ( gameBag, record.Draws) then
            sum <- sum + record.Id
    sum

printfn "sum of ids: %d" possibleSum

// let mutable i = 1
// for line in lines do
//     printfn "line %d is possible: %b" i (IsPossible (gameBag, (ParseGameRecord line).Draws))
//     i <- i + 1


let calcPower (draws: Draw seq) =
    (Seq.item 0 draws).Amount * (Seq.item 1 draws).Amount *(Seq.item 2 draws).Amount

let AtLeastNecessary(round: Draw seq)=
    let reds = round |> Seq.filter (fun x -> x.Color = Color.Red) |> Seq.maxBy (fun x -> x.Amount)
    let greens = round |> Seq.filter (fun x -> x.Color = Color.Green) |> Seq.maxBy (fun x -> x.Amount)
    let blues = round |> Seq.filter (fun x -> x.Color = Color.Blue) |> Seq.maxBy (fun x -> x.Amount)
    seq{ reds; greens; blues }

let powerSum =
    let mutable sum = 0
    for line in lines do
        let record = ParseGameRecord line
        sum <- sum + calcPower(AtLeastNecessary (record.Draws))
    sum

printfn "power of at least necessary: %d" powerSum