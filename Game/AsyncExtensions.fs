module GamePlay.AsyncExtensions

#nowarn "40"
open System
open System.Windows.Forms
   
type Microsoft.FSharp.Control.Async with 

  static member AwaitObservable(event1:IObservable<'a>) = 
    Async.FromContinuations((fun (cont, econt, ccont) -> 
      let rec callback = (fun value ->
        remover.Dispose()
        cont(value) )
      and remover : IDisposable = event1.Subscribe(callback) 
      () ))
  
  static member AwaitObservable(event1:IObservable<'a>, event2:IObservable<'b>) = 
    Async.FromContinuations((fun (cont, econt, ccont) -> 
      let rec callback1 = (fun value ->
        remover1.Dispose()
        remover2.Dispose()
        cont(Choice1Of2(value)) )
      and callback2 = (fun value ->
        remover1.Dispose()
        remover2.Dispose()
        cont(Choice2Of2(value)) )
      and remover1 : IDisposable = event1.Subscribe(callback1)
      and remover2 : IDisposable = event2.Subscribe(callback2)
      () ))

  static member AwaitObservable(event1:IObservable<'a>, event2:IObservable<'b>, event3:IObservable<'c>) = 
    Async.FromContinuations((fun (cont, econt, ccont) -> 
      let rec callback1 = (fun value ->
        dispose()
        cont(Choice1Of3(value)) )
      and callback2 = (fun value ->
        dispose()
        cont(Choice2Of3(value)) )
      and callback3 = (fun value ->
        dispose()
        cont(Choice3Of3(value)) )
      and dispose() =
          remover1.Dispose()
          remover2.Dispose()
          remover3.Dispose()
      and remover1 : IDisposable = event1.Subscribe(callback1) 
      and remover2 : IDisposable = event2.Subscribe(callback2) 
      and remover3 : IDisposable = event3.Subscribe(callback3)
      () ))