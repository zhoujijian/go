module FmtSGF
    open FParsec

    type GameNode = { Row:int; Col:int; Note:Option<string> }

    let ws = spaces
    let str s = pstring s
    let strws s = str s .>> ws
    let wsstr s = ws >>. str s

    let Run parser text =
        match run parser text with
        | Success(result, _, _) -> printfn "Success: %A" result
        | Failure(error, _, _)  -> printfn "Failure: %A" error

    let PComment =
        let norm = satisfy (fun c -> c <> '\\' && c <> ']')
        let left = strws "C" >>. str "["
        let riht = str "]"
        between left riht (manyChars norm)

    let PMove =
        let chars = "abcdefghijklmnopqrs"
        let check = satisfy (fun c -> chars.Contains(string c))
        let left = strws "["
        let riht = wsstr "]"        
        between left riht (pipe2 check check (fun a b ->
            let row = chars.IndexOf(a)
            let col = chars.IndexOf(b)
            (row, col) ))

    let PNode =
        pipe2 PMove (opt PComment) (fun (r, c) note ->
            { Row=r; Col=c; Note=note } )

    let PNodes =
        let semicolon = str ";"
        semicolon >>. (sepBy PNode semicolon)

    let PTree, PTreeRef = createParserForwardedToRef()
    do PTreeRef :=
        let left = strws "("
        let riht = wsstr ")"
        between left riht (PNodes .>> (many PTree))

    Run PComment "C[comment text.]"
    Run PMove "[ad]"
    Run PNodes ";[ad]C[not too bad];[cf]"
    // Run PTree "(;[ad];[cf](;[hh]))"
    System.Console.ReadLine() |> ignore