Public Class Form1
    Dim r As New Random
    Dim score As Integer
    Dim count As Integer
    Sub Randmove(p As PictureBox)
        Dim x As Integer
        Dim y As Integer
        x = r.Next(-75, 75)
        y = r.Next(-75, 75)
        MoveTo(p, x, y)
    End Sub



    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.R
                crash.Image.RotateFlip(RotateFlipType.Rotate90FlipX)
                Me.Refresh()
            Case Keys.Up
                MoveTo(crash, 0, -8)
            Case Keys.Down
                MoveTo(crash, 0, 8)
            Case Keys.Left
                MoveTo(crash, -8, 0)
            Case Keys.Right
                MoveTo(crash, 8, 0)
            Case Keys.Space
                tnt4.Location = crash.Location
                tnt4.Visible = True
                Timer5.Enabled = True
                tnt1.Location = crash.Location
                tnt1.Visible = True
                Timer2.Enabled = True
                tnt2.Location = crash.Location
                tnt2.Visible = True
                Timer3.Enabled = True
                tnt3.Location = crash.Location
                tnt3.Visible = True
                Timer4.Enabled = True
        End Select
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Randmove(badguy1)
    End Sub



    Sub follow(p As PictureBox)
        Static headstart As Integer
        Static c As New Collection
        c.Add(crash.Location)
        headstart = headstart + 1
        If headstart > 10 Then
            p.Location = c.Item(1)
            c.Remove(1)
        End If
    End Sub

    Public Sub chase(p As PictureBox)
        Dim x, y As Integer
        If p.Location.X > crash.Location.X Then
            x = -2
        Else
            x = 2
        End If
        MoveTo(p, x, 0)
        If p.Location.Y < crash.Location.Y Then
            y = 2
        Else
            y = -2
        End If
        MoveTo(p, x, y)
    End Sub




    Function Collision(p As PictureBox, t As String, Optional ByRef other As Object = vbNull)
        Dim col As Boolean

        For Each c In Controls
            Dim obj As Control
            obj = c
            If obj.Visible AndAlso p.Bounds.IntersectsWith(obj.Bounds) And obj.Name.ToUpper.Contains(t.ToUpper) Then
                col = True
                other = obj
            End If
        Next
        Return col
    End Function
    'Return true or false if moving to the new location is clear of objects ending with t
    Function IsClear(p As PictureBox, distx As Integer, disty As Integer, t As String) As Boolean
        Dim b As Boolean

        p.Location += New Point(distx, disty)
        b = Not Collision(p, t)
        p.Location -= New Point(distx, disty)
        Return b
    End Function
    'Moves and object (won't move onto objects containing  "wall" and shows green if object ends with "win"
    Sub MoveTo(p As PictureBox, distx As Integer, disty As Integer)
        If IsClear(p, distx, disty, "WALL") Then
            p.Location += New Point(distx, disty)
        End If
        Dim other As Object = Nothing
        If p.Name.Contains("tnt1") And Collision(p, "badguy", other) Then

            other.visible = False
            tnt1.Visible = False
            Return
        End If
        If p.Name.Contains("tnt2") And Collision(p, "badguy", other) Then

            other.visible = False
            tnt2.Visible = False
            Return
        End If
        If p.Name.Contains("tnt3") And Collision(p, "badguy", other) Then

            other.visible = False
            tnt3.Visible = False
            Return
        End If
        If p.Name.Contains("tnt4") And Collision(p, "badguy", other) Then

            other.visible = False
            tnt4.Visible = False
            Return
        End If

        If p.Name.Contains("crash") And Collision(p, "underwaterrecipe", other) Then
            underwaterrecipe.Visible = False
            underwatertnt.Visible = True

            Return
        End If

        If p.Name.Contains("crash") And Collision(p, "underwatertnt", other) Then
            forest.Visible = False
            underwatertnt.Visible = False
            Timer1.Interval = Timer1.Interval * 13
            Timer2.Interval = Timer2.Interval * 2
            Timer3.Interval = Timer3.Interval * 2
            Timer4.Interval = Timer4.Interval * 2
            Timer5.Interval = Timer5.Interval * 2

            Return
        End If
        ' explosion1.Location = badguy1.Location
        ' explosion1.Visible = True
        ' Else
        'xplosion1.Location = badguy2.Location
        ' explosion1.Visible = True
        If p.Name.Contains("crash") And Collision(p, "warptntrecipe", other) Then
            warptnt.Visible = True
            warptntrecipe.Visible = False
            Return
        End If


        If p.Name.Contains("crash") And Collision(p, "underwatertnt", other) Then
            score = score + 1
            other.visible = False
            i9ugehwjte.Text = score
        End If

        If p.Name.Contains("crash") And Collision(p, "warptnt", other) Then
            warptnt.Visible = False
            Me.Visible = False
            Dim f As New Form2
            f.ShowDialog()
            Me.Visible = True
        End If


        If p.Name.Contains("badguy") And Collision(p, "crash", other) Then
            crash.Visible = False
            youlose.Visible = True
            youlose2.Visible = True
        End If

    End Sub





    Private Sub tnt1_Click(sender As Object, e As EventArgs) Handles tnt1.Click

    End Sub
    Dim bdir As Integer = 40

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

        MoveTo(tnt1, bdir, 0)
    End Sub
    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        MoveTo(tnt2, 0, 40)
    End Sub
    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        MoveTo(tnt3, -40, 0)
    End Sub

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
        MoveTo(tnt4, 0, -40)
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles tntlabel.Click

    End Sub

    Private Sub warptnt_Click(sender As Object, e As EventArgs) Handles warptnt.Click

    End Sub

    Private Sub underwatertntlabel_Click(sender As Object, e As EventArgs) Handles i9ugehwjte.TextChanged

    End Sub

    Private Sub badguy1_Click(sender As Object, e As EventArgs) Handles badguy1.Click

    End Sub

    Private Sub Timer6_Tick(sender As Object, e As EventArgs) Handles Timer6.Tick
        chase(badguy2)
    End Sub
End Class

'how to make everything turn into a blue water background when touching the underwatertnt and slow everything down
'how to make tnts disappear when hitting the wall but not dissapear when hitting everything else
'how to make a counter for tnts at the bottom left



'if p.name = "picturebox3" and collision(p, "fries", other) then 
'progressbar1.value = progresbar1.value + 10
'score = score + 1 
'other.visible = false
'return
'end if




'me.visible = false
'dim f as new form2
'f.showdialog()
'me.visible = true
'return
'end if