module Kifu

open Game
open MiniJson.JsonModule
open MiniJson.DynamicJsonModule
open System.IO

type KifuJson() =
    member x.SaveKifu kifu path =
        let rec mapNext (nexts:KifuStep list) =
            nexts
            |> List.map(fun next -> mapStep next)
            |> Array.ofList
            |> JsonArray
        and mapStep (step:KifuStep) =
            seq {
                yield ("Row", JsonNumber(float step.Curr.Row))
                yield ("Col", JsonNumber(float step.Curr.Col))
                match step.Curr.Note with
                | None -> ()
                | Some(note) -> yield ("Note", JsonString(note))
                if step.Next.Length > 0 then
                    yield ("Next", mapNext step.Next)
            }
            |> Array.ofSeq
            |> JsonObject

        let root = JsonObject [|("kifu", mapNext kifu.Root)|]
        let stream = new FileStream(path, FileMode.Create)
        let writer = new StreamWriter(stream)
        writer.Write(root.ToString true)
        writer.Close()
        stream.Close()

    member x.OpenKifu path =
        let stream = new FileStream(path, FileMode.Open)
        let reader = new StreamReader(stream)
        let text   = reader.ReadToEnd()
        reader.Close()
        stream.Close()

        match parse true text with
        | Failure(msg, pos) -> failwith "open kifu.json error"
        | Success json ->
            let root = (json.Query)?kifu
            match root with
            | PathOk _ ->
                let rec ofJson (emt:JsonPath) : KifuStep =
                    let next = ofNext (emt?Next)
                    let curr =
                        {
                            Seq  = 0
                            Row  = int (emt?Row).AsFloat
                            Col  = int (emt?Col).AsFloat
                            Note =
                                match (emt?Note) with
                                | PathOk(emtNote, _) -> Some((emt?Note).AsString)
                                | _ -> None
                        }
                    { Curr=curr; Next=next }
                and ofNext emtNext =
                    match emtNext with
                    | PathOk _ ->
                        [ for i in 0..(emtNext.Length-1) -> emtNext.[i] |> ofJson ]
                    | _ -> []
                ofNext root
            | _ -> []