Public Class Highscores

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub Highscores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'ScoresDataSet.SpaceInvadersScores' table. You can move, or remove it, as needed.
        Me.SpaceInvadersScoresTableAdapter.Fill(Me.ScoresDataSet.SpaceInvadersScores)
        Label2.Text = ("Congradulations! Your score was " & score & "!")
    End Sub

    Private Sub SubmitButton_Click(sender As Object, e As EventArgs) Handles SubmitButton.Click
        If TextBox1.Text = "" Then
            MsgBox("You must enter in a name before posting your score.")
        Else
            Me.SpaceInvadersScoresTableAdapter.Insert(Microsoft.VisualBasic.Left(TextBox1.Text, 19), score)
            Me.SpaceInvadersScoresTableAdapter.Fill(Me.ScoresDataSet.SpaceInvadersScores)
            SubmitButton.Enabled = False
        End If
    End Sub
End Class