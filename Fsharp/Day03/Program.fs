// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

type Number = {
    StartIndex :int
    EndIndex :int
    Value : int
}

let lines = System.IO.File.ReadAllLines("./../../../../../Day03/puzzle_input.txt") |> Array.map (fun x -> x.ToCharArray())

let InvalidPostion(x,y):bool =
    x = -1 || y = -1 || y > lines.Length-1 || x > lines[y].Length-1

let IsSymbol (x,y, symbol: char option) :bool =
    let c = lines[y][x]
    if symbol.IsNone then
        c <> '.' && not(System.Char.IsDigit(c))
    else
        c = symbol.Value

let IsSymbolAdjacent (xPos:int,yPos:int, symbol) :bool * int * int =
    let mutable found = (false,-1,-1)
    for x = xPos - 1 to xPos + 1 do
        for y = yPos - 1 to yPos + 1 do
            if not(InvalidPostion(x,y)) then
                if IsSymbol(x,y, symbol) then
                    found <- (true,x,y)
    found

let IsPartNumber (number : Number, y :int) : bool =
    let mutable res = false
    for x = number.StartIndex to number.EndIndex do
        let matched (b, _,_ ) = b
        
        if IsSymbolAdjacent(x,y, None) |> matched then
            res <- true
    res

let GetValue (line: char[], start, finish) =
    let mutable res = ""
    for i = start to finish do
        res <- res + string line[i]
    int res

let FindNumbers( line:char[]) =
    let mutable x = 0
    let numbers = new System.Collections.Generic.List<Number>()
    while x < line.Length do
        if System.Char.IsDigit(line[x]) then
            let start = x
            while x + 1 < line.Length && System.Char.IsDigit(line[x+1]) do
                x <- x + 1
            numbers.Add({ StartIndex = start; EndIndex = x; Value = GetValue(line, start, x)})
        x <- x + 1
    numbers

let GetEngineParts =
    let parts = new System.Collections.Generic.List<int>()
    for y = 0 to lines.Length - 1 do
        let line = lines[y]
        let numbers = FindNumbers line
        for number in numbers do
            if IsPartNumber(number, y) then
                parts.Add(number.Value)
    parts

printfn "engine parts:"
GetEngineParts |> Seq.sum |> printfn "%d"

let IsGearNumber (number : Number, y :int) =
    let mutable res = (false,-1,-1)
    for x = number.StartIndex to number.EndIndex do
        match IsSymbolAdjacent(x,y, Some('*')) with
        | (b, x, y) ->
            if b then
                res <- (true, x, y)
    res

type GearPart = {
    Number : Number
    Connection : int * int
    mutable Linked : bool
}

let GetGearParts =
    let gearParts = new System.Collections.Generic.List<GearPart>()
    for y = 0 to lines.Length - 1 do
        let line = lines[y]
        let numbers = FindNumbers line
        for number in numbers do
            match IsGearNumber(number, y) with
            | (b, x, y) ->
                if b && x <> -1 then
                    gearParts.Add({Number = number; Connection = (x,y); Linked =false})
    gearParts

type Gear = {
    P1 : Number
    P2 : Number
}

let GetGears (GearParts: GearPart seq) =
    let gears = new System.Collections.Generic.List<Gear>();
    for gearPart in GearParts do
        if not(gearPart.Linked) then
            let matched = GearParts |> Seq.where (fun x -> x.Connection = gearPart.Connection)
            if matched |> Seq.length > 1 then
                let m1 = Seq.item 0 matched
                let m2 = Seq.item 1 matched
                //modify?
                m1.Linked <- true
                m2.Linked <- true
                gears.Add({ P1 = m1.Number; P2 = m2.Number })
    gears

let CalcGearRatio g =
    g.P1.Value * g.P2.Value

printfn "gears:"
GetGearParts |> GetGears |> Seq.sumBy CalcGearRatio |>  printfn "%d"