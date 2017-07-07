Imports System.IO

Public Class uxMain
    Private Sub uxMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim load As New OpenFileDialog
        load.Title = "Load file as CSV Files |*.csv |"
        load.Filter = "CSV Files|*.csv|All Files|*"

        If (load.ShowDialog() = DialogResult.OK) Then
            uxIPTVList.Clear()
            Dim fluxoTexto As IO.StreamReader

            Dim linhaTexto As String
            If IO.File.Exists(load.FileName) Then
                fluxoTexto = New IO.StreamReader(load.FileName)
                uxLabelList.Text = "Loaded List:" + load.FileName
                linhaTexto = fluxoTexto.ReadToEnd 'ReadLine

                While linhaTexto <> Nothing
                    uxIPTVList.AppendText(linhaTexto & vbCrLf)
                    linhaTexto = fluxoTexto.ReadLine
                End While

                fluxoTexto.Close()
            Else
                MessageBox.Show("Arquivo não existe")
            End If
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Dim salvarComo As SaveFileDialog = New SaveFileDialog()
        Dim caminho As DialogResult
        Dim fluxoTexto As IO.StreamWriter
        Dim Arquivo As String

        salvarComo.CheckFileExists = False
        salvarComo.Title = "Save file as CSV Files |*.csv |"
        caminho = salvarComo.ShowDialog
        Arquivo = salvarComo.FileName + ".csv"

        If Arquivo = Nothing Then
            MessageBox.Show("Arquivo Invalido", "Salvar Como", MessageBoxButtons.OK)
        Else
            fluxoTexto = New IO.StreamWriter(Arquivo)
            fluxoTexto.Write(uxIPTVList.Text)
            fluxoTexto.Close()
        End If
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Environment.Exit(0)
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles uxContentName.TextChanged

    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub uxAddToList_Click(sender As Object, e As EventArgs) Handles uxAddToList.Click
        If uxClassName.Text = String.Empty Or uxContentName.Text = String.Empty Or uxContentURL.Text = String.Empty Then
            MessageBox.Show("Não pode usar valores em branco", "Erro", MessageBoxButtons.OK)
            Return
        Else
            If uxIPTVList.Text = String.Empty Then
                uxIPTVList.AppendText("Class,Name,URL,Picture" & vbCrLf)
                uxIPTVList.AppendText(uxClassName.Text.ToLower + "," + uxContentName.Text + "," + uxContentURL.Text & vbCrLf)
                uxIPTVList.Refresh()
                uxContentName.Text = ""
                uxContentURL.Text = ""
                uxIPTVList.Refresh()
            Else
                uxIPTVList.AppendText(uxClassName.Text.ToLower + "," + uxContentName.Text + "," + uxContentURL.Text & vbCrLf)
                uxIPTVList.Refresh()
                uxContentName.Text = ""
                uxContentURL.Text = ""
                uxIPTVList.Refresh()
            End If
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Environment.Exit(0)
    End Sub
End Class
