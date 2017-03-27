Imports System.Text.RegularExpressions
Imports Microsoft.Win32

Public Class ComPortFinder
    Public Function ComPortNames(ByVal VID As String, ByVal PID As String, Optional ByVal MI As String = "") As List(Of String)
        Dim pattern As String
        If String.IsNullOrEmpty(MI) Then
            pattern = String.Format("^VID_{0}.PID_{1}", VID, PID)
        Else
            pattern = String.Format("^VID_{0}.PID_{1}.MI_{2}", VID, PID, MI)
        End If

        Dim _rx As New Regex(pattern, RegexOptions.IgnoreCase)
        Dim comports As New List(Of String)()
        Dim rk1 As RegistryKey = Registry.LocalMachine
        Dim rk2 As RegistryKey = rk1.OpenSubKey("SYSTEM\CurrentControlSet\Enum")
        For Each s3 As String In rk2.GetSubKeyNames()
            Dim rk3 As RegistryKey = rk2.OpenSubKey(s3)
            For Each s As String In rk3.GetSubKeyNames()
                If _rx.Match(s).Success Then
                    Dim rk4 As RegistryKey = rk3.OpenSubKey(s)
                    For Each s2 As String In rk4.GetSubKeyNames()
                        Dim rk5 As RegistryKey = rk4.OpenSubKey(s2)
                        Dim rk6 As RegistryKey = rk5.OpenSubKey("Device Parameters")
                        Try
                            comports.Add(DirectCast(rk6.GetValue("PortName"), String))
                        Catch ex As Exception
                        End Try
                    Next
                End If
            Next
        Next
        Return comports
    End Function

End Class