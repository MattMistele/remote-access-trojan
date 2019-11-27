Imports System.Drawing

Public Class Sprite

    Public Enum AnimateDir
        NONE = 0
        FORWARD = 1
        BACKWARD = -1
    End Enum

    Public Enum AnimateWrap
        WRAP = 0
        BOUNCE = 1
    End Enum

    Private p_game As Game
    Private p_position As System.Drawing.PointF
    Private p_velocity As System.Drawing.PointF
    Private p_size As System.Drawing.Size
    Private p_bitmap As System.Drawing.Bitmap
    Private p_alive As Boolean
    Private p_columns As Integer
    Private p_totalFrames As Integer
    Private p_currentFrame As Integer
    Private p_animationDir As AnimateDir
    Private p_animationWrap As AnimateWrap
    Private p_lastTime As Integer
    Private p_animationRate As Integer


    Public Sub New(ByRef game As Game)
        REM keep reference to Game object
        p_game = game

        p_position = New PointF(0.0, 0.0)
        p_size = New Size(0, 0)
        p_bitmap = Nothing
        p_alive = True

        REM set animation to 1 frame by default
        p_columns = 1
        p_totalFrames = 1
        p_currentFrame = 0
        p_animationDir = AnimateDir.FORWARD
        p_animationWrap = AnimateWrap.WRAP
        p_lastTime = 0
        p_animationRate = 30

    End Sub

    Protected Overrides Sub Finalize()
        REM we don't have a base class here but be
        REM sure to always call this if you do!
        MyBase.Finalize()


    End Sub

    Public Property Alive() As Boolean
        Get
            Return p_alive
        End Get
        Set(ByVal value As Boolean)
            p_alive = value
        End Set
    End Property

    REM the sprite's source bitmap should be loaded separately
    REM and used here by reference--but do not destroy p_bitmap
    Public Property Image() As System.Drawing.Bitmap
        Get
            Return p_bitmap
        End Get
        Set(ByVal value As Bitmap)
            p_bitmap = value
            p_size = value.Size
        End Set
    End Property

    REM we can spell out the full type for clarity
    Public Property Position() As System.Drawing.PointF
        Get
            Return p_position
        End Get
        Set(ByVal value As PointF)
            p_position = value
        End Set
    End Property

    REM optional way to change X position
    Public Property X() As Single
        Get
            Return p_position.X
        End Get
        Set(ByVal value As Single)
            p_position.X = value
        End Set
    End Property

    REM optional way to change Y position 
    Public Property Y() As Single
        Get
            Return p_position.Y
        End Get
        Set(ByVal value As Single)
            p_position.Y = value
        End Set
    End Property

    Public Property Size() As System.Drawing.Size
        Get
            Return p_size
        End Get
        Set(ByVal value As System.Drawing.Size)
            p_size = value
        End Set
    End Property

    REM optional way to change size 
    Public Property Width() As Integer
        Get
            Return p_size.Width
        End Get
        Set(ByVal value As Integer)
            p_size.Width = value
        End Set
    End Property

    REM optional way to change size 
    Public Property Height() As Integer
        Get
            Return p_size.Height
        End Get
        Set(ByVal value As Integer)
            p_size.Height = value
        End Set
    End Property

    Public Property Columns() As Integer
        Get
            Return p_columns
        End Get
        Set(ByVal value As Integer)
            p_columns = value
        End Set
    End Property

    Public Property TotalFrames() As Integer
        Get
            Return p_totalFrames
        End Get
        Set(ByVal value As Integer)
            p_totalFrames = value
        End Set
    End Property

    Public Property AnimateDirection() As AnimateDir
        Get
            Return p_animationDir
        End Get
        Set(ByVal value As AnimateDir)
            p_animationDir = value
        End Set
    End Property

    Public Property AnimateWrapMode() As AnimateWrap
        Get
            Return p_animationWrap
        End Get
        Set(ByVal value As AnimateWrap)
            p_animationWrap = value
        End Set
    End Property

    Public Property AnimationRate() As Integer
        Get
            Return 1000 / p_animationRate
        End Get
        Set(ByVal value As Integer)
            If value = 0 Then value = 1
            p_animationRate = 1000 / value
        End Set
    End Property

    REM cycle the sprite's animation frame 
    Public Sub Animate(ByVal startFrame As Integer, ByVal endFrame As Integer)

        REM do we even need to animate?
        If p_totalFrames > 0 Then
            If (startFrame = 0 And endFrame = 0) Then
                p_currentFrame = 0
            Else



                REM check animation timing
                Dim time As Integer = My.Computer.Clock.TickCount()
                If time > p_lastTime + p_animationRate Then
                    p_lastTime = time

                    REM go to next frame
                    p_currentFrame += p_animationDir

                    If p_animationWrap = AnimateWrap.WRAP Then
                        REM need to wrap animation?
                        If p_currentFrame < startFrame Then
                            p_currentFrame = endFrame
                        ElseIf p_currentFrame > endFrame Then
                            p_currentFrame = startFrame
                        End If

                    ElseIf p_animationWrap = AnimateWrap.BOUNCE Then
                        REM need to bounce animation?
                        If p_currentFrame < startFrame Then
                            p_currentFrame = startFrame
                            REM reverse direction 
                            p_animationDir *= -1
                        ElseIf p_currentFrame > endFrame Then
                            p_currentFrame = endFrame
                            REM reverse direction 
                            p_animationDir *= -1
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub Draw()
        Dim frame As New Rectangle
        frame.X = (p_currentFrame Mod p_columns) * p_size.Width
        frame.Y = (p_currentFrame \ p_columns) * p_size.Height
        frame.Width = p_size.Width
        frame.Height = p_size.Height
        p_game.Device.DrawImage(p_bitmap, Bounds(), frame, GraphicsUnit.Pixel)
    End Sub

    REM returns bounding rectangle around sprite 
    Public ReadOnly Property Bounds() As Rectangle
        Get
            Dim rect As Rectangle
            rect = New Rectangle(p_position.X, p_position.Y, p_size.Width, p_size.Height)
            Return rect
        End Get
    End Property

    Public Function IsColliding(ByRef other As Sprite) As Boolean
        REM test for bounding rectangle collision
        Dim collision As Boolean
        collision = Me.Bounds.IntersectsWith(other.Bounds)
        Return collision
    End Function


    Public Property Velocity() As PointF
        Get
            Return p_velocity
        End Get
        Set(ByVal value As PointF)
            p_velocity = value
        End Set
    End Property

    Public Property CurrentFrame() As Integer
        Get
            Return p_currentFrame
        End Get
        Set(ByVal value As Integer)
            p_currentFrame = value
        End Set
    End Property

    Public Sub KeepInBounds(ByVal boundary As Rectangle)
        If p_position.X < boundary.Left Then
            p_velocity.X *= -1
            p_position.X = boundary.Left
        End If
        If p_position.X > boundary.Right Then
            p_velocity.X *= -1
            p_position.X = boundary.Right
        End If

        If p_position.Y < boundary.Top Then
            p_velocity.Y *= -1
            p_position.Y = boundary.Top
        End If
        If p_position.Y > boundary.Bottom Then
            p_velocity.Y *= -1
            p_position.Y = boundary.Bottom
        End If
    End Sub

End Class

