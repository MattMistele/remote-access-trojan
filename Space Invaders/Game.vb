' Game Class
' This class calls the following functions that should be defined
' as Public in the Form1.vb source code file, or any convenient source 
' file within the project:
'    * Game_Init
'    * Game_Update(ByVal deltaTime As Single)
'    * Game_Draw()    
'    * Game_End


Public Class Game
    Private p_device As Graphics
    Public p_surface As Bitmap
    Private p_pb As PictureBox
    Private p_frm As Form
    Private p_font As Font
    Private p_gameOver As Boolean

    Protected Overrides Sub Finalize()
        REM free memory
        p_device.Dispose()
        p_surface.Dispose()
        p_pb.Dispose()
        p_font.Dispose()
    End Sub

    Public Sub New(ByRef form As Form, ByVal width As Integer, ByVal height As Integer)
        p_device = Nothing
        p_surface = Nothing
        p_pb = Nothing
        p_frm = Nothing
        p_font = Nothing
        p_gameOver = False

        REM set form properties
        p_frm = form
        p_frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        p_frm.MaximizeBox = False
        REM adjust size for window border
        p_frm.Size = New Point(width + 6, height + 28)
        p_frm.KeyPreview = True

        REM create a picturebox
        p_pb = New PictureBox()
        p_pb.Parent = p_frm
        'p_pb.Dock = DockStyle.Fill
        p_pb.Location = New Point(0, 0)
        p_pb.Size = New Size(width, height)
        p_pb.BackColor = Color.Black


        REM create graphics device
        p_surface = New Bitmap(p_frm.Size.Width, p_frm.Size.Height)
        p_pb.Image = p_surface
        p_device = Graphics.FromImage(p_surface)

        REM set the default font
        SetFont("Arial", 18, FontStyle.Regular)

    End Sub

    Public ReadOnly Property Device() As Graphics
        Get
            Return p_device
        End Get
    End Property

    Public Sub Update()
        REM refresh the drawing surface
        p_pb.Image = p_surface
    End Sub


    REM ********************************************
    REM font support with several Print variations
    REM ********************************************
    Public Sub SetFont(ByVal name As String, ByVal size As Integer, ByVal style As FontStyle)
        p_font = New Font(name, size, style, GraphicsUnit.Pixel)
    End Sub

    Public Sub Print(ByVal x As Integer, ByVal y As Integer, ByVal text As String, ByVal color As Brush)
        p_device.DrawString(text, p_font, color, x, y)
    End Sub

    Public Sub Print(ByVal x As Integer, ByVal y As Integer, ByVal text As String)
        Print(x, y, text, Brushes.White)
    End Sub

    Public Sub Print(ByVal pos As Point, ByVal text As String, ByVal color As Brush)
        Print(pos.X, pos.Y, text, color)
    End Sub

    Public Sub Print(ByVal pos As Point, ByVal text As String)
        Print(pos.X, pos.Y, text)
    End Sub

    REM ***********************************************
    REM Bitmap support functions
    REM ***********************************************
    Public Function LoadBitmap(ByVal filename As String)
        Dim bmp As Bitmap
        Try
            bmp = New Bitmap(filename)
        Catch ex As Exception
            bmp = Nothing
        End Try
        Return bmp
    End Function

    Public Sub DrawBitmap(ByRef bmp As Bitmap, ByVal x As Single, ByVal y As Single)
        p_device.DrawImageUnscaled(bmp, x, y)
    End Sub

    Public Sub DrawBitmap(ByRef bmp As Bitmap, ByVal x As Single, ByVal y As Single, ByVal width As Integer, ByVal height As Integer)
        p_device.DrawImageUnscaled(bmp, x, y, width, height)
    End Sub

    Public Sub DrawBitmap(ByRef bmp As Bitmap, ByVal pos As Point)
        p_device.DrawImageUnscaled(bmp, pos)
    End Sub

    Public Sub DrawBitmap(ByRef bmp As Bitmap, ByVal pos As Point, ByVal size As Size)
        p_device.DrawImageUnscaled(bmp, pos.X, pos.Y, size.Width, size.Height)
    End Sub

    REM ***************************************************
    REM Input support functions
    REM ***************************************************

    'Public ReadOnly Property MousePosition() As Point
    '    Get
    '        Return Form1.MousePosition
    '    End Get
    'End Property



End Class

