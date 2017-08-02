module FmtSGF
    open FParsec
    open Game

    let UpperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    let LowerCaseChars = "abcdefghijklmnopqrs"

    type IndentValue = { Indent : string; Values : string list }
    type Property =
        | Comment of string
        | Move of int * int
        | Unknown of IndentValue
    type Node     = { Properties : Property list }
    type Sequence = { Nodes : Node list }
    type GameTree = { VartMain : Sequence; VartChildren : GameTree list }

    let ws = spaces
    let str s = pstring s
    let strws s = str s .>> ws
    let wsstr s = ws >>. str s

    let Run parser text =
        match run parser text with
        | Success(result, _, _) -> printfn "Success: %A" result
        | Failure(error, _, _)  -> printfn "Failure: %A" error

    let PL = str "["
    let PR = str "]"
    let PPropertyIndent = many1Satisfy (fun c -> UpperCaseChars.Contains(string c))
    let PPropertyValue  = between PL PR (manyChars (satisfy (fun c -> c <> '\\' && c <> ']')))
    let PProperty       = pipe2 PPropertyIndent (many1 PPropertyValue) (fun indent values ->
        match indent with
        | "C" -> match values with | h::t -> Comment(h) | _ -> Comment("")
        | side when indent = "B" || indent = "W" ->
            let position = values |> List.exactlyOne
            let chars = "abcdefghijklmnopqrs"
            let row = chars.IndexOf(string position.[0])
            let col = chars.IndexOf(string position.[1])
            Move(row, col)
        | _ -> Unknown({ Indent = indent; Values = values }) )

    let PNode     = str ";" >>. (many PProperty) |>> fun (properties:Property list) -> { Properties = properties }
    let PSequence = many1 PNode |>> fun (nodes:Node list) -> { Nodes = nodes }
    let PGameTree, PGameTreeRef = createParserForwardedToRef()
    do PGameTreeRef :=
        let root = pipe2 PSequence (many PGameTree) (fun sequence trees ->
            { VartMain = sequence; VartChildren = trees } )
        root |> between (str "(") (str ")")

    (*
    let PComment =
        let norm = satisfy (fun c -> c <> '\\' && c <> ']')
        let left = str "C" >>. PL
        let riht = PR
        between left riht (manyChars norm)

    let PMove =
        let chars = "abcdefghijklmnopqrs"
        let check = satisfy (fun c -> chars.Contains(string c))
        let side = anyOf "BW" |>> (fun c -> match c with | 'B' -> 0 | 'W' -> 1 | _ -> failwith "unknown property")
        let left = str "["
        let riht = str "]"
        pipe3 side (PL >>. check) (check .>> PR) (fun seq a b ->
            let row = chars.IndexOf(a)
            let col = chars.IndexOf(b)
            (row, col, seq))

    let PNode =
        pipe2 PMove (opt PComment) (fun (row, col, seq) note ->
            { Row=row; Col=col; Seq=seq; Note=note } )

    let PNodes =
        let semicolon = str ";"
        semicolon >>. (sepBy PNode semicolon) |>> fun (crosses:Cross list) ->
            let rec chain (crosses:Cross list) =
                match crosses with
                | [] -> None
                | h::t ->
                    let next = chain t
                    let step = { Curr=h; Next=match next with | None -> [] | Some t -> [t] }
                    Some(step)
            crosses |> chain

    let PTree, PTreeRef = createParserForwardedToRef()
    do PTreeRef :=
        let root = pipe2 PNodes (many PTree) (fun root children ->
            let nexts =
                children |> List.filter (fun c -> match c with | Some _ -> true | _ -> false)
                         |> List.map (fun (Some t) -> t)
            match root with
            | None   -> None
            | Some t ->
                let rec chain (curr:KifuStep) =
                    match curr.Next with
                    | []    -> { curr with Next = nexts }
                    | h::[] -> { curr with Next = [chain h] }
                    | _     -> failwith "error: next more than 1"
                t |> chain |> Some )
        root |> between (str "(") (str ")")
    
    Run PMove "B[ad]"
    Run PNodes ";B[ad]C[not too bad];W[cf]"
    Run PTree "(;B[ad];W[cf](;W[hh])(;W[ij]))"
    *)

    Run PGameTree "(;B[ad];W[cf](;W[hh])(;W[ij]))"
    System.Console.ReadLine() |> ignore