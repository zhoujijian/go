module FmtSGF
    open FParsec
    let ws = spaces
    let str s = pstring s

    type Node = { Move:float32 }

    type SGF =
        | Node
        | Tree of Node list