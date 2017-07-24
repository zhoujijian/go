module FmtSGF
    #if INTERACTIVE
    #load "FParsec"
    #endif

    open FParsec

    let ws = spaces
    let str s = pstring s
    let strws s = str s .>> ws
    let wsstr s = ws >>. str s

    let Run parser text =
        match run parser text with
        | Success(result, _, _) -> printfn "Success: %A" result
        | Failure(error, _, _)  -> printfn "Failure: %A" error
        System.Console.ReadLine() |> ignore

    let PComment =
        let norm = satisfy (fun c -> c <> '\\' && c <> ']')
        let left = strws "C" >>. str "["
        let riht = str "]"
        between left riht (manyChars norm)

    let PMove =
        let az = satisfy (fun c -> c >= 'a' && c <= 'z')
        let left = strws "["
        let riht = wsstr "]"
        between left riht (pipe2 az az (fun a b -> string a + string b))

    let PNode =
        let semicolon = str ";"
        let node = sepBy PMove semicolon
        node

    Run PComment "C[this is simple comment text.]"
    Run PMove "[ad]"
    Run PNode ";[ad];[cf]"

    type Move = { Row : int; Col : int }

    type Node = { Move:float32 }
    type SGF =
        | Node
        | Tree of Node list