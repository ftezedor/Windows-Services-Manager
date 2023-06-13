Imports System.Threading
Imports Windows.Service.Worker
Imports Windows.Service.Logging


Namespace Service.Worker3
    Public Class Worker3
        Inherits ServiceWorker

        Private [exit] As Boolean = False

        Public Overrides ReadOnly Property Title As String
            Get
                Return "Worker Tester 3"
            End Get
        End Property

        Public Overrides Sub Run()
            Logger.LogInfo("starting working")

            While Not [exit]
                Logger.LogInfo("performing some work...")

                Try
                    'System.Threading.Thread.Sleep(Integer.MaxValue)
                    Thread.Sleep(Timeout.Infinite)

                Catch ex As System.Threading.ThreadInterruptedException
                End Try
            End While

            Logger.LogInfo("work is done")
        End Sub

        Public Overrides Sub [Stop]()
            [exit] = True
        End Sub
    End Class
End Namespace
