Imports System.IO

Public Class uxMain
    Private Sub uxMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Public Sub SortLines(sender As Object, e As EventArgs)
        uxIPTVList.HideSelection = False
        'for showing selection
        'Saving current selection
        Dim selectedText As String = uxIPTVList.SelectedText
        'Saving curr line

        Dim firstCharInLineIndex As Integer = uxIPTVList.GetFirstCharIndexOfCurrentLine()
        Dim currLineIndex As Integer = uxIPTVList.Text.Substring(0, firstCharInLineIndex).Count(Function(c) c = ControlChars.Lf)
        Dim currLine As String = uxIPTVList.Lines(currLineIndex)
        Dim offset As Integer = uxIPTVList.SelectionStart - firstCharInLineIndex


        'Sorting
        Dim lines As String() = uxIPTVList.Lines
        Array.Sort(lines, Function(str1 As String, str2 As String) str1.CompareTo(str2))
        uxIPTVList.Lines = lines

        If Not [String].IsNullOrEmpty((selectedText)) Then
            'restoring selection
            Dim newIndex As Integer = uxIPTVList.Text.IndexOf(selectedText)
            uxIPTVList.[Select](newIndex, selectedText.Length)
        Else
            'Restoring the cursor

            'location of the current line
            Dim lineIdx As Integer = Array.IndexOf(uxIPTVList.Lines, currLine)
            Dim textIndex As Integer = uxIPTVList.Text.IndexOf(currLine)
            Dim fullIndex As Integer = textIndex + offset
            uxIPTVList.SelectionStart = fullIndex
            uxIPTVList.SelectionLength = 0
        End If
    End Sub

    Private Sub ClearList()
        uxClassName.Select()
        uxContentName.Text = ""
        uxContentURL.Text = ""
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim load As New OpenFileDialog
        load.Title = "Load file as CSV Files |*.csv |"
        load.Filter = "CSV Files|*.csv|All Files|*"

        If (load.ShowDialog() = DialogResult.OK) Then
            uxIPTVList.Clear()
            Dim streamReader As StreamReader

            Dim textLine As String
            If File.Exists(load.FileName) Then
                streamReader = New StreamReader(load.FileName)
                textLine = streamReader.ReadToEnd 'ReadLine

                While textLine <> Nothing
                    uxIPTVList.AppendText(textLine & vbCrLf)
                    textLine = streamReader.ReadLine
                    SortLines(sender, e)
                End While

                SortLines(sender, e)
                streamReader.Close()
            Else
                MessageBox.Show("File not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Dim saveFileDialog As New SaveFileDialog()
        Dim result As DialogResult
        Dim streamWriter As StreamWriter
        Dim fileName As String

        saveFileDialog.CheckFileExists = False
        saveFileDialog.Title = "Save file as CSV Files |*.csv |"
        saveFileDialog.Filter = "CSV Files|*.csv|All Files|*"
        result = saveFileDialog.ShowDialog
        fileName = saveFileDialog.FileName

        If fileName = Nothing Then
            MessageBox.Show("Invalid File", "Save As", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        streamWriter = New StreamWriter(fileName)
        streamWriter.Write(uxIPTVList.Text)
        streamWriter.Close()
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Environment.Exit(0)
    End Sub


    Private Sub uxAddToList_Click(sender As Object, e As EventArgs) Handles uxAddToList.Click
        If uxClassName.Text = String.Empty Or uxContentName.Text = String.Empty Or uxContentURL.Text = String.Empty Then
            MessageBox.Show("You can not use empty values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        ElseIf uxIPTVList.Text.Contains(uxContentURL.Text) Then
            MessageBox.Show("URL Already in list!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ClearList()
            Return
        Else
            If uxIPTVList.Text = String.Empty Then
                uxIPTVList.AppendText("Class,Name,URL,Picture")
                uxIPTVList.AppendText(vbCrLf & uxClassName.Text.ToUpper + "," + uxContentName.Text + "," + uxContentURL.Text)
                ClearList()
                SortLines(sender, e)
            Else
                uxIPTVList.AppendText(vbCrLf & uxClassName.Text.ToUpper + "," + uxContentName.Text + "," + uxContentURL.Text)
                ClearList()
                SortLines(sender, e)
            End If
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)
        Environment.Exit(0)
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        MessageBox.Show("Tocombox IPTV created by Tryller", "About", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
