Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports HtmlAgilityPack
Public Class Form1
    Private dragging As Boolean
    Private offset As Point
    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        Me.dragging = True
        Me.offset = e.Location
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If Me.dragging Then
            Dim point As Point = MyBase.PointToScreen(e.Location)
            MyBase.Location = New Point((point.X - Me.offset.X), (point.Y - Me.offset.Y))
            Cursor.Current = Cursors.NoMove2D
        End If
    End Sub

    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        Me.dragging = False
    End Sub
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim CP As CreateParams = MyBase.CreateParams
            CP.Style = &HA0000
            Return CP
        End Get
    End Property
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Me.ListBox1.Items.Clear()
            Dim htmlWeb As HtmlWeb = New HtmlWeb()
            Dim htmlDocument As HtmlAgilityPack.HtmlDocument = New HtmlAgilityPack.HtmlDocument()
            htmlDocument = htmlWeb.Load("https://www.sslproxies.org")
            Dim htmlNode As HtmlNode = htmlDocument.DocumentNode.Descendants("tbody").FirstOrDefault()
            Dim enumerable As IEnumerable(Of HtmlNode) = htmlNode.Descendants("tr")
            For Each htmlNode2 As HtmlNode In enumerable
                Dim text As String = htmlNode2.FirstChild.InnerText
                Dim innerText As String = htmlNode2.SelectSingleNode("td[2]").InnerText
                text = text + ":" + innerText
                Me.ListBox1.Items.Add(text)
            Next
            Me.Label3.Text = Me.ListBox1.Items.Count.ToString()
        Catch ex As Exception

        End Try

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.ListBox1.Items.Clear()
        Dim httpWebRequest As HttpWebRequest = CType(WebRequest.Create("https://www.my-proxy.com/free-socks-5-proxy.html"), HttpWebRequest)
        Dim httpWebResponse As HttpWebResponse = CType(httpWebRequest.GetResponse(), HttpWebResponse)
        Dim streamReader As StreamReader = New StreamReader(httpWebResponse.GetResponseStream())
        Dim input As String = streamReader.ReadToEnd()
        Dim regex As Regex = New Regex("[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}:[0-9]{1,5}")
        Dim matchCollection As MatchCollection = regex.Matches(input)
        Try
            For Each obj As Object In matchCollection
                Dim item As Match = CType(obj, Match)
                Me.ListBox1.Items.Add(item)
            Next
        Finally
        End Try
        Dim httpWebRequest2 As HttpWebRequest = CType(WebRequest.Create("http://www.live-socks.net/feeds/posts/default"), HttpWebRequest)
        Dim httpWebResponse2 As HttpWebResponse = CType(httpWebRequest2.GetResponse(), HttpWebResponse)
        Dim streamReader2 As StreamReader = New StreamReader(httpWebResponse2.GetResponseStream())
        Dim input2 As String = streamReader2.ReadToEnd()
        Dim regex2 As Regex = New Regex("[0-9].{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}:[0-9]{1,5}")
        Dim matchCollection2 As MatchCollection = regex2.Matches(input2)
        Try
            For Each obj2 As Object In matchCollection2
                Dim item2 As Match = CType(obj2, Match)
                Me.ListBox1.Items.Add(item2)
            Next
        Finally
        End Try
        Me.Label3.Text = Me.ListBox1.Items.Count.ToString()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Process.Start("https://www.sslproxies.org")
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Process.Start("https://my-proxy.com/free-socks-5-proxy.html")
    End Sub

    Private Sub LinkLabel5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        Process.Start("http://www.live-socks.net/feeds/posts/default")
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        MsgBox("Samad.Dz!!")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Me.ListBox1.Items.Clear()
            Me.Label3.Text = "Idle..."
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            If ListBox1.Items.Count <= 0 Then
                MsgBox("Grab Proxies first and save", vbInformation, "INfo")
            Else
                Dim array As String() = New String() {Me.ListBox1.Items.ToString()}
                Dim saveFileDialog As SaveFileDialog = New SaveFileDialog()
                saveFileDialog.Filter = "Proxy-List (*.txt)|*.txt|All Files (*.*)|*.*"
                saveFileDialog.CheckPathExists = True
                saveFileDialog.ShowDialog(Me)
                Dim streamWriter As StreamWriter = New StreamWriter(saveFileDialog.FileName)
                Dim num As Integer = Me.ListBox1.Items.Count - 1
                For i As Integer = 0 To num
                    streamWriter.WriteLine(RuntimeHelpers.GetObjectValue(Me.ListBox1.Items(i)))
                Next
                streamWriter.Close()
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
