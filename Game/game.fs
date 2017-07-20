module Game

open System
open System.IO
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Control

type FallCoord = {
    row:int
    col:int
}

type CrossPoint = {
    side:int
    seq:int
}

type KiFu = {
    path :string
    count:int
    steps:FallCoord list
}

let inBoard (board:CrossPoint[,]) (row, col) =
    let R = Array2D.length1 board
    let C = Array2D.length2 board
    if R<>C then failwith("board R!=C")
    (row>=0 && row<R && col>=0 && col<C)

let isEmpty board (row, col) =
    let e = Array2D.get board row col
    e=(-1)

let rivalSide side = 1-side

let findBlock board (row, col, side) block =
    let findSame r c enq deq =
        let notRepeat r c =
            let check (kr, kc) = (kr=r && kc=c)
            (enq   |> List.tryFind check) = None &&
            (deq   |> List.tryFind check) = None &&
            (block |> List.tryFind check) = None
        if (inBoard board (r, c)) then
            let cs = Array2D.get board r c
            match cs.side with
            | -1 -> None
            | sd when (sd=side) && (notRepeat r c) -> Some(enq@[(r, c)])
            | _ -> Some(enq)
        else
            Some(enq)

    let rec scan enq deq =
        match enq with
        | (r, c)::t ->
            let deq = deq@[(r, c)]
            Some(t) |>
            Option.bind(fun t -> findSame (r+1) c t deq) |>
            Option.bind(fun t -> findSame (r-1) c t deq) |>
            Option.bind(fun t -> findSame r (c+1) t deq) |>
            Option.bind(fun t -> findSame r (c-1) t deq) |>
            Option.bind(fun t ->
                match (scan t deq) with
                | None -> None
                | Some(v) -> Some((r, c)::v)
            )
        | _ -> Some([])

    scan [(row, col)] []

let put board (row, col, seq) =
    let side = seq%2
    let isRival (r, c) =
        (inBoard board (r, c)) && ((Array2D.get board r c).side=(rivalSide side))

    let board = Array2D.copy board
    Array2D.set board row col { side=side; seq=seq }

    let r, c = row, col
    [(r+1,c); (r-1,c); (r,c+1); (r,c-1)]
        |> List.fold(fun acc (r, c) ->
            if isRival(r, c) then
                let block = findBlock board (r, c, (rivalSide side)) acc
                match block with
                | Some(value) -> acc@value
                | None -> acc
            else
                acc
        ) []
        |> List.iter(fun (r, c) ->
            Array2D.set board r c { side=(-1); seq=(-1) })
    board

let makeBoard cnt steps =
    let empty = Array2D.create cnt cnt { side=(-1); seq=(-1) }
    steps
        |> List.fold(fun (board, i) fall ->
            let board = put board (fall.row, fall.col, i)
            (board, i+1)) (empty, 0)
        |> fun (board, _) -> board

let applySteps init steps initMaxSeq =
    steps
        |> List.fold(fun (board, i) fall ->
            let board = put board (fall.row, fall.col, (i+initMaxSeq+1))
            (board, i+1)) (init, 0)
        |> fun (board, _) -> board

type Cross = {
    Seq  : int
    Row  : int
    Col  : int
    Note : Option<string>
}

type KifuStep = {
    Curr : Cross
    Next : KifuStep list
}

type KifuRoot = KifuStep list

type KifuCursor = {
    Track : int list
    Branch : int
    KVart : int  // research variety K (index on track)
}

type GameRound = {
    Path  : string
    Count : int
    Root  : KifuRoot
}

type KifuState = {
    KfNext : KifuStep list
    Cursor : KifuCursor
    Board  : CrossPoint[,]
}

let rec split index track =
    match index with
    | 0 -> []
    | _ ->
        match track with
        | x::t -> x::(split (index-1) t)
        | [] -> failwith "index out track range"

let trackSteps (kfNext:KifuStep list) track =
    List.fold(fun acc nextk ->
        let (kfNext:KifuStep list, steps) = acc
        let kfStep = kfNext.[nextk]
        let curr = { row=kfStep.Curr.Row; col=kfStep.Curr.Col }
        (kfStep.Next, steps@[curr])
    ) (kfNext, []) track

let iterNext (func:KifuStep list -> KifuStep list) (kfNext:KifuStep list) (track:int list) =
    let rec iter kfNext k =
        match k=track.Length with
        | true -> func kfNext
        | false ->
            assert(kfNext.Length>0)
            kfNext
            |> List.mapi(fun i kfStep ->
                match i<>track.[k] with
                | true  -> kfStep
                | false ->
                    let next = iter kfStep.Next (k+1)
                    { kfStep with Next = next })
    iter kfNext 0

let appendVariety (kfRoot:KifuStep list) (track:int list) kfStepVart =
    iterNext (fun kfNext -> kfNext@[kfStepVart]) kfRoot track
    
let removeVariety kfRoot (track:int list) remk =
    iterNext (fun kfNext ->
        kfNext
        |> List.fold(fun acc next ->
            let (i, kfSteps) = acc
            let lnext = if i=remk then [] else [next]
            ((i+1), kfSteps@lnext)) (0, [])
        |> fun (_, lnext) -> lnext) kfRoot track

let initSeq (board : CrossPoint[,]) =
    let mutable max = 0
    for r = 0 to 18 do
        for c = 0 to 18 do
            let seq = board.[r, c].seq
            if max < seq then max <- seq
    max

let nextStep nextk state init =
    let cursor = state.Cursor
    match state.KfNext with
    | [] -> state
    | variety ->
        let (kf, steps) = trackSteps variety cursor.Track
        match kf with
        | [] -> state
        | _ ->
            let track = cursor.Track@[nextk]
            let (kf, steps) = trackSteps variety track
            let board = applySteps init steps (initSeq init)
            { state with Board = board; Cursor = { cursor with Track = track } }
    
let prevStep state init =
    let cursor = state.Cursor
    let track  = cursor.Track
    match state.KfNext with
    | [] -> state
    | variety ->
        match track with
        | [] -> state
        | tk ->
            let track = List.init (tk.Length-1) (fun i -> tk.[i])
            let (kf, steps) = trackSteps variety track
            let board = applySteps init steps (initSeq init)
            { state with
                Board  = board
                Cursor = { cursor with Track = track; Branch = 0 }
            }

let putSimple (r:int) (c:int) (tk:int) (state:KifuState) (init:CrossPoint [,]) : KifuState =
    let rec genNext (kfNext:KifuStep list) (nextk:int) : KifuStep =
        assert (kfNext.Length <= 1)
        assert (nextk <= tk)
        match nextk = tk with
        | true ->
            { Curr = { Seq = 0; Row = r; Col = c; Note = None }; Next = [] }
        | _ ->
            match kfNext with
            | [] -> failwith ("target index:" + (string tk) + "out range")
            | head::_ ->
                let next = genNext head.Next (nextk+1)
                { head with Next = [next] }
    
    let kfRoot = [genNext state.KfNext 0]
    let track = state.Cursor.Track @ [0]
    let (_, steps) = trackSteps kfRoot track
    let board = applySteps init steps (initSeq init)
    { state with
        KfNext = kfRoot
        Board  = board
        Cursor = { state.Cursor with Track = track; Branch = 0; }
    }

let rec applyStep (root:KifuRoot) (track:int list) (map:KifuStep->KifuStep) : KifuRoot =
    match track with
    | [] -> root
    | k::t ->
        if k >= (List.length root) then failwith "parameter out range"
        root |> List.mapi (fun (i:int) (step:KifuStep) ->
            match i<>k with
            | true -> step
            | _ ->
                match t with
                | [] -> map step
                | t  ->
                    let next = applyStep step.Next t map
                    { step with Next = next } )

let putStep waycnt r c tk (state:KifuState) =
    let track = state.Cursor.Track
    let rec genNext kfNext nextk =
        kfNext
        |> List.mapi(fun i t ->
            match i with
            | i when i<>track.[nextk] -> t
            | _ ->
                match nextk+1 with
                | k when k<tk ->
                    let next = genNext t.Next k
                    { t with Curr = t.Curr; Next = next }
                | k ->
                    let variety = { Curr = { Seq=0; Row=r; Col=c; Note=None }; Next = [] }
                    { t with Next = t.Next@[variety] })
    let kfRoot =
        match tk with
        | 0 -> state.KfNext@[{ Curr = { Seq=0; Row=r; Col=c; Note=None }; Next = [] }]
        | _ ->
            genNext state.KfNext 0
    
    let (nextVart, steps) = trackSteps kfRoot track
    let vk = nextVart.Length - 1
    let vart = nextVart.[vk].Curr
    let board = makeBoard waycnt (steps@[{ row=vart.Row; col=vart.Col }])
    { state with
        KfNext = kfRoot
        Board  = board
        Cursor = { state.Cursor with Track = track@[vk]; Branch = 0; }
    }

type GameData = {
    Round : GameRound
    Restore : KifuState
    Current : KifuState
}
with
    static member private Init kfRoot board =
        {
            KfNext = kfRoot
            Cursor = { Track = []; Branch = 0; KVart = 0 }
            Board  = board
        }

    static member private Empty =
        let board = Array2D.create 19 19 { side = (-1); seq = (-1) }
        GameData.Init [] board

    static member Create round =
        let board = Array2D.create 19 19 { side = (-1); seq = (-1) }
        {
            Round   = round
            Restore = GameData.Empty
            Current = GameData.Init round.Root board
        }
        
    member x.Next branch =
        assert (branch >= 0)
        let current = nextStep branch x.Current x.Restore.Board
        let cursor  = { current.Cursor with Branch = branch }
        let current = { current with Cursor = cursor }
        { x with Current = current }

    member x.NextBranchCount =
        let (kf, steps) = trackSteps x.Current.KfNext x.Current.Cursor.Track
        kf.Length

    member x.Prev() =
        let current = prevStep x.Current x.Restore.Board
        { x with Current = current }

    member x.Put r c =
        match x.Current.Board.[r, c].seq with
        | num when num >= 0 -> x
        | _ ->
            let tk = x.Current.Cursor.Track.Length
            let current = putSimple r c tk x.Current x.Restore.Board
            { x with Current = current }

    member x.SetNote (note:string) =
        let root = applyStep x.Current.KfNext x.Current.Cursor.Track (fun (step:KifuStep) ->
            let curr = { step.Curr with Note = Some(note) }
            { step with Curr = curr } )
        { x with Current = { x.Current with KfNext = root } }

    member x.BeginVariety() =
        let current = GameData.Init [] x.Current.Board
        { x with Restore = x.Current; Current = current }

    member x.CancelVariety() =
        { x with Current = x.Restore; Restore = GameData.Empty }

    member x.SaveVariety() =
        let kfRoot, track = x.Restore.KfNext, x.Restore.Cursor.Track
        match x.Current.KfNext with
        | [] -> x
        | vart::_ ->
            let kfRoot = appendVariety kfRoot track vart
            { x with
                Current = { x.Restore with KfNext = kfRoot }
                Restore = GameData.Empty }