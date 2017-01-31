#load "game.fs"

module GoGame =
    open GoWin
    open Game
    open Kifu
    open GamePlay.AsyncExtensions

    open System
    open System.Windows.Forms
    open System.Drawing

    let startx = 15.0f
    let starty = 15.0f
    let rad1 = 15.0f
    let rad2 = rad1*2.0f
    let RESEARCH        = "Research"
    let CANCEL_RESEARCH = "Cancel"

    let main() =
        let form = new GoWindow()
        let opendlg = new OpenFileDialog()
        let savedlg = new SaveFileDialog()
        opendlg.Filter <- "kifu(*.json)|*.json"
        savedlg.Filter <- "kifu(*.json)|*.json"

        let (putClick : IDisposable ref) = ref null
        let play = ref (GameData.Create {
            Path  = String.Empty
            Count = 19
            Root  = []
        })

        let releasePutClick() =
            if (!putClick) <> null then
                (!putClick).Dispose()
                putClick := null

        let onDraw (e:PaintEventArgs) =
            let extend = startx+30.0f*18.0f
            for i in 0..18 do
                let x = startx+30.0f*(float32 i)
                let y = starty+30.0f*(float32 i)
                e.Graphics.DrawLine(Pens.Gray, new PointF(startx, y), new PointF(extend, y))
                e.Graphics.DrawLine(Pens.Gray, new PointF(x, starty), new PointF(x, extend))

            let getColor cs =
                match cs.seq with
                | q when q>=0 ->
                    let number = cs.seq.ToString()
                    match cs.side with
                    | 0 -> Some(Brushes.White, Brushes.Black, Pens.Black, number)
                    | 1 -> Some(Brushes.Black, Brushes.White, Pens.White, number)
                    | _ -> None
                | _ -> None

            let font = new Font("Consolas", 10.0f)
            let drawBoard (board:CrossPoint[,]) numVisible =
                for r=0 to 18 do
                    for c=0 to 18 do
                        let x = startx+30.0f*(float32 r)-rad1
                        let y = starty+30.0f*(float32 c)-rad1
                        match getColor board.[r,c] with
                        | Some(brush0, brush1, pen, number) ->
                            e.Graphics.FillEllipse(brush0, x, y, rad2, rad2)
                            e.Graphics.DrawEllipse(pen, x, y, rad2, rad2)

                            let x = x+rad1
                            let y = y+rad1
                            let rect = e.Graphics.MeasureString(number, font)
                            if numVisible then
                                e.Graphics.DrawString(number, font, brush1, x-rect.Width/2.0f, y-rect.Height/2.0f)
                        | None -> ()
            
            drawBoard ((!play).Current.Board) true
            drawBoard ((!play).Restore.Board) false // restore board overwrite current board
        
        let showBranch() =
            form.lstVariety.Items.Clear()
            for i in 0..(!play).NextBranchCount-1 do
                form.lstVariety.Items.Add(i.ToString()) |> ignore

            if form.lstVariety.Items.Count > 0 then
                form.lstVariety.SelectedIndex <- 0

        let onPut (e:MouseEventArgs) =
            let r = e.X/30
            let c = e.Y/30
            if r>=0 && r<=18 && c>=0 && c<=18 then
                play := (!play).Put r c
                showBranch()
                form.panelDraw.Invalidate()

        form.panelDraw.Paint.Add(onDraw)

        form.butNext.Click.Add(fun e ->
            if form.lstVariety.SelectedIndex >= 0 then
                play := (!play).Next form.lstVariety.SelectedIndex
                showBranch()
                form.panelDraw.Invalidate() )

        form.butPrev.Click.Add(fun e ->
            play := (!play).Prev()
            showBranch()
            form.panelDraw.Invalidate() )

        (*form.OnClosing.Add(fun e ->
            if form.butResearch.Text = "Cancel" then
                MessageBox.Show("Stop Research First!")
                e.Cancel <- true ) *)

        form.saveKifu.Click.Add(fun e ->
            savedlg.FileName <- ""
            let kifu = KifuJson()
            let data = { (!play).Round with Root = (!play).Current.KfNext }
            match data.Path with
            | "" ->
                match savedlg.ShowDialog() with
                | DialogResult.OK ->
                    kifu.SaveKifu data savedlg.FileName
                | _ -> ()
            | path ->
                kifu.SaveKifu data path )

        form.openKifu.Click.Add(fun e ->
            opendlg.FileName <- ""
            match opendlg.ShowDialog() with
            | DialogResult.OK ->
                let kifu = KifuJson()
                let root = kifu.OpenKifu opendlg.FileName
                play := GameData.Create {
                    Path  = opendlg.FileName
                    Count = 19
                    Root  = root
                }
                showBranch()

                form.panelRoot.Enabled       <- true
                form.panelVariety.Enabled    <- true
                form.butResearch.Enabled     <- true
                form.butSaveResearch.Enabled <- true
                form.lstVariety.Enabled      <- true
                form.panelDraw.Invalidate()

                releasePutClick()

                let rec loopHandle() =
                    async {
                        let! click = Async.AwaitObservable(form.butResearch.Click, form.butSaveResearch.Click)
                        match click with
                        | Choice1Of2(_) ->
                            match form.butResearch.Text with
                            | "Research" ->
                                form.butResearch.Text <- CANCEL_RESEARCH
                                form.butSaveResearch.Enabled <- true
                                form.lstVariety.Enabled <- false
                                putClick := form.panelDraw.MouseClick.Subscribe(onPut)
                                play := (!play).BeginVariety()
                            | text ->
                                assert (text = "Cancel")
                                form.butResearch.Text <- RESEARCH
                                form.butSaveResearch.Enabled <- false
                                form.lstVariety.Enabled <- true
                                releasePutClick()
                                play := (!play).CancelVariety()
                                showBranch()
                        | _ ->
                            form.butResearch.Text <- RESEARCH
                            form.butSaveResearch.Enabled <- false
                            form.lstVariety.Enabled <- true
                            releasePutClick()
                            play := (!play).SaveVariety()
                            showBranch()

                        form.panelDraw.Invalidate()
                        return! loopHandle()
                    }
                Async.StartImmediate(loopHandle())
            | _ -> () )

        form.newKifu.Click.Add(fun _ ->
            form.panelRoot.Enabled       <- true
            form.panelVariety.Enabled    <- false
            form.butResearch.Enabled     <- false
            form.butSaveResearch.Enabled <- false
            form.lstVariety.Enabled      <- false
            form.panelDraw.Invalidate()

            releasePutClick()
            putClick := form.panelDraw.MouseClick.Subscribe onPut

            play := GameData.Create {
                Path  = String.Empty
                Count = 19
                Root  = []
            }
            showBranch() )

        Application.Run(form)

    [<System.STAThread>]
    main()