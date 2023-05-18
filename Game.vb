'-------------------
'--Made by: Rairii--
'-------------------
' Game Rules:
'collect the correct answers  and shoot the incorrect ones
'collecting cash gives you power blasting through the invaders it also highlights the correct answer
'however be wary as you may also hit the correct answers which makes you lose health
'i made the answer faster than the incorrect answer to make it more noticable
Public Class Game
    Dim player1 As New PictureBox
    Dim moveleft As Boolean = False
    Dim moveright As Boolean = False
    Dim score As New Label
    Dim title As New Label
    Dim highscore As New Label
    Dim question As String
    'bulletsssss
    Dim bala As Integer = 10
    Dim missilearray(bala) As PictureBox
    Dim onscreen(bala) As Boolean
    Dim bullets As Integer = 0
    Dim bulletwidth As Integer = 5
    Dim bulletheight As Integer = 5
    Dim bulletspeed As Integer = 30
    'enemies!!!! wrong answers
    Dim maxenemy As Integer = 3
    Dim enemyarray(maxenemy) As Label
    Dim onscreenenemy(maxenemy) As Boolean
    Dim enemyspeed As Integer = 8

    'health and score
    Dim myhealth As Integer = 100
    Dim myscore As Integer = 0
    Dim myhighscore As Integer = 0

    'uno kuno ganern answers
    Dim maxuno As Integer = 0
    Dim unoarray(maxuno) As Label
    Dim onscreenuno(maxuno) As Boolean

    'power kuno
    Dim power As Boolean = False
    Dim casharray(maxuno) As PictureBox
    Dim onscreencash(maxuno) As Boolean

    'timer and other control variables
    Dim seconds As Double = 0 'timer in seconds
    Dim powertime As Double
    Dim random1 As Single = 1
    Dim random2 As Single = 1
    Dim a As Integer
    Dim b As Integer
    Dim c As Integer
    Dim gamestarted As Boolean = False
    'buttons and timers
    Dim timer As Windows.Forms.Timer
    Dim enemytimer As Windows.Forms.Timer
    Dim unotimer As Windows.Forms.Timer
    Dim startbutton As New Button
    Dim backbutton As New Button
    Dim rules As New Button

    'sounds
    'nosounds yet so...... idunno


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Computer.Audio.Play(Global.ProjTempA.My.Resources.Endless_Cyber_Runner_2, AudioPlayMode.BackgroundLoop)

        Me.Text = "Math Invaders"
        Me.Width = 500
        Me.Height = 600
        Me.MaximizeBox = False
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.BackgroundImage = Global.ProjTempA.My.Resources.Resources.b1bdc1ae539dcbd1a7c33cef3e5f2d9a 'remove comment sign if the file is imported change to the image of choice
        '
        '
        '
        '
        Me.BackColor = Color.Black
        buttonstarts()
        Me.Controls.Add(title)
        With title
            .Width = Me.Width
            .Height = 55
            .Top = startbutton.Top - 100
            .Left = Me.Width / 2 - title.Width / 2
            .Font = New Font("Roboto", 37, FontStyle.Bold)
            .ForeColor = Color.White
            .BackColor = Color.Transparent
            .Text = "MATH INVADERS!"
            .TextAlign = ContentAlignment.MiddleCenter
        End With
        Me.Controls.Add(highscore)
        With highscore
            .Width = Me.Width
            .Height = 30
            .Top = title.Bottom
            .Left = Me.Width / 2 - title.Width / 2
            .Font = New Font("Roboto", 15, FontStyle.Regular)
            .ForeColor = Color.White
            .BackColor = Color.Transparent
            .Text = "Highscore: " & myhighscore
            .TextAlign = ContentAlignment.MiddleCenter
        End With
    End Sub

    Sub gamestart()
        timer = New Timer()
        timer.Interval = 50
        AddHandler timer.Tick, AddressOf timer_Tick
        enemytimer = New Timer()
        enemytimer.Interval = 100
        AddHandler enemytimer.Tick, AddressOf enemytimer_Tick
        unotimer = New Timer()
        unotimer.Interval = 100
        AddHandler unotimer.Tick, AddressOf unotimer_Tick
        Me.Controls.Add(score)
        a = Rnd() * 100
        b = Rnd() * 100
        cashcreation(maxuno)
        gunshot(bala)
        sinkocreation(maxenemy)
        unocreation(maxuno)
        score.Top = 0
        score.Left = 0
        score.Font = New Font("Roboto", 15, FontStyle.Regular)
        score.ForeColor = Color.White
        score.Height = 70
        score.Width = Me.Width
        score.BackColor = Color.Transparent
        score.BringToFront()
        myhealth = 100
        Me.Controls.Add(player1)
        player1.BackgroundImage = Global.ProjTempA.My.Resources.Resources.SeekPng_com_space_ship_png_254127 'remove the comment sign of you imported
        '
        '
        '
        '
        '

        player1.BackgroundImageLayout = ImageLayout.Stretch
        player1.Width = 50
        player1.Height = 50
        player1.Top = Me.Height - 2 * player1.Height
        player1.Left = Me.Width / 2 - player1.Width
        player1.BackColor = Color.Transparent 'change to transparent if png file is available
        player1.BringToFront()
        timer.Enabled = True
        enemytimer.Enabled = True
        unotimer.Enabled = True
        player1.Show()
        score.Show()
        Randomize()
        question = questionmaking(b)
        startbutton.Hide()
        backbutton.Hide()
        rules.Hide()
        title.Hide()
        highscore.Hide()
        Me.Focus()
        gamestarted = True
    End Sub

    Sub rulesclicked()
        MsgBox("SPACEBAR to shoot! || ARROW KEYS to move || ESC to pause" & vbNewLine &
               "- Catch the correct answers and shoot the incorrect ones" & vbNewLine &
               "- Shooting the correct answers gives you minus health" & vbNewLine &
               "- Collecting power-ups highlights the correct answer and gives you power to blast through enemies" & vbNewLine &
               "- Have Fun!", MsgBoxStyle.Information And MsgBoxStyle.MsgBoxRtlReading, "RULES")
    End Sub
    Private Sub timer_Tick(sender As Object, e As EventArgs)
        seconds += 0.05 'to keep track of time
        If moveright = True Then
            player1.Left += 30
        ElseIf moveleft = True Then
            player1.Left -= 30
        End If
        If player1.Left <= 0 Then
            player1.Left += 5
        End If
        If player1.Right >= Me.Width Then
            player1.Left -= 15
        End If
        If power = True Then
            bulletwidth = 10
            bulletheight = 30
            bulletspeed = 50
            enemyspeed = 5
            For i As Integer = 0 To bala
                With missilearray(i)
                    .Width = bulletwidth
                    .Height = bulletheight
                    .BackColor = Color.LightBlue
                End With
            Next
            unoarray(0).BackColor = Color.White
            powertime += 0.05
            If powertime > 10 Then 'remove the powerup after 10 seconds
                powertime = 0
                bulletheight = 5
                bulletwidth = 5
                For i As Integer = 0 To bala
                    With missilearray(i)
                        .Width = bulletwidth
                        .Height = bulletheight
                        .BackColor = Color.Red
                    End With
                Next
                unoarray(0).BackColor = Color.Transparent
                power = False
                bulletspeed = 30
                enemyspeed = 8
            End If
        End If
        For i As Integer = 0 To bala
            missilearray(i).Top -= bulletspeed
            If missilearray(i).Top <= -2 Then
                onscreen(i) = False
            End If
            For j = 0 To maxenemy
                If Collision(missilearray(i), enemyarray(j)) Then 'when bullet and enemy collides
                    If power = False Then
                        missilearray(i).Visible = False
                        myscore += 5
                    Else
                        myscore += 10
                    End If
                    enemyarray(j).Top = 70
                    enemyarray(j).Left = Int(Rnd() * Me.Width)
                    enemyarray(j).Text = Int(Rnd() * 100) + a - b
                End If
            Next
            For j = 0 To maxuno
                If Collision(missilearray(i), unoarray(j)) Then 'when bullet and correct answer collides
                    If power = False Then
                        missilearray(i).Visible = False
                    End If
                    myhealth -= 10
                    unoarray(j).Top = 70
                    unoarray(j).Left = Int(Rnd() * Me.Width)
                    unoarrayredif(j)
                End If
            Next
        Next
        seconds = Math.Round(seconds, 2)
        score.Text = "score: " & myscore & vbNewLine & "Health: " & myhealth & vbNewLine & "Question: " & question
        If seconds Mod 5 > 0 And seconds Mod 5 < 0.1 Then 'randomizes if the correct answer and power up will appear every 5 seconds
            random1 = Rnd()
            random2 = Rnd()
        End If
    End Sub
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Dim count As Integer = 1
        Select Case e.KeyCode
            Case Keys.Left
                moveleft = True
                If player1.Left <= 0 Then
                    moveleft = False
                End If
            Case Keys.Right
                moveright = True
                If player1.Right >= Me.Width Then
                    moveright = False
                End If
            Case Keys.Space
                For i As Integer = 0 To bullets

                    If onscreen(i) = True Then
                        count += 1
                    End If

                Next
                If count < bala Then 'limits the number of bullets to "bala"
                    onscreen(bullets) = True
                    missilearray(bullets).Visible = True
                    missilearray(bullets).Top = player1.Top
                    missilearray(bullets).Left = player1.Left + player1.Width / 2 - bulletwidth + 3
                    bullets += 1
                    If bullets = bala Then
                        bullets = 0
                    End If
                End If
            Case Keys.Escape
                If gamestarted = True Then
                    paused()
                End If
        End Select
    End Sub
    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Select Case e.KeyCode
            Case Keys.Left
                moveleft = False
            Case Keys.Right
                moveright = False

        End Select
    End Sub

    Sub gunshot(ByVal i As Integer)
        For x As Integer = 0 To i
            Dim missile As New PictureBox
            Me.Controls.Add(missile)
            missile.Width = bulletwidth
            missile.Height = bulletheight
            missile.BorderStyle = BorderStyle.FixedSingle
            missile.BackColor = Color.Red
            missile.Top = player1.Top
            missile.Left = player1.Left + player1.Width / 2 - bulletwidth + 10
            missile.BringToFront()
            missilearray(x) = missile
            onscreen(x) = False
            missilearray(x).Visible = False
        Next
    End Sub
    Sub sinkocreation(ByVal i As Integer)
        For x As Integer = 0 To i
            Dim missile As New Label
            Me.Controls.Add(missile)
            missile.Width = 70
            missile.Height = 40
            missile.Font = New Font("Roboto", 20, FontStyle.Bold)
            missile.ForeColor = Color.Yellow
            missile.BackColor = Color.Transparent
            missile.Text = Int(Rnd() * 100) + a - b
            missile.TextAlign = ContentAlignment.MiddleCenter
            missile.Top = 70
            missile.Left = Int(-x * Rnd() * 10 + Me.Width - Rnd() * Me.Width)
            missile.BringToFront()
            enemyarray(x) = missile
            enemyarray(x).Visible = True
            onscreenenemy(x) = True
        Next
    End Sub

    Sub unocreation(ByVal i As Integer)
        For x As Integer = 0 To i
            Dim missile As New Label
            Me.Controls.Add(missile)
            missile.Width = 70
            missile.Height = 40
            missile.Font = New Font("Roboto", 20, FontStyle.Bold)
            missile.ForeColor = Color.Yellow
            missile.BackColor = Color.Transparent
            missile.TextAlign = ContentAlignment.MiddleCenter
            missile.Text = a
            missile.Top = 70
            missile.Left = Int(Me.Width - Rnd() * Me.Width)
            missile.BringToFront()
            unoarray(x) = missile
            onscreenuno(x) = False
            unoarray(x).Visible = True
        Next

    End Sub
    Private Function Collision(ByVal Object1 As Object, ByVal Object2 As Object) As Boolean
        Dim Collided As Boolean = False
        If Object1.Top + Object1.Height >= Object2.Top And
            Object2.Top + Object2.Height >= Object1.Top And
            Object1.Left + Object1.Width >= Object2.Left And
            Object2.Left + Object2.Width >= Object1.Left And Object1.visible = True And Object2.visible = True Then
            Collided = True
        End If
        Return Collided
    End Function

    Private Sub enemytimer_Tick(sender As Object, e As EventArgs)
        Dim random As Single
        For i = 0 To maxenemy
            enemyarray(i).Top += enemyspeed * Rnd()

            If enemyarray(i).Top > Me.Height Then
                myhealth -= 20
                enemyarray(i).Top = 70
            End If
            If myhealth <= 0 Then 'when health is gone gameover
                talona()
                Exit Sub
            End If
            If enemyarray(i).Left < 5 Then
                enemyarray(i).Left += 10
            End If
            If enemyarray(i).Left > (Me.Width - 40) Then
                enemyarray(i).Left -= 10
            End If
            random = Rnd()
            If random > 0.66 Then 'makes the enemy move left and right randomly
                enemyarray(i).Left += enemyspeed - 3
            ElseIf random < 0.66 Then
                enemyarray(i).Left -= enemyspeed - 3
            End If
            If Collision(player1, enemyarray(i)) Then
                enemyarray(i).Top = 70
                enemyarray(i).Left = Int(Me.Width - Rnd() * Me.Width)
                myhealth -= 20
                enemyarray(i).Text = Int(Rnd() * 100) + a - b
            End If
        Next
    End Sub
    Sub cashcreation(ByVal i As Integer)

        For x As Integer = 0 To i
            Dim missile As New PictureBox
            Me.Controls.Add(missile)
            missile.Width = 50
            missile.Height = 50
            missile.BackgroundImage = Global.ProjTempA.My.Resources.Resources.pera
            missile.BackColor = Color.Transparent
            missile.BackgroundImageLayout = ImageLayout.Stretch
            missile.Top = -10
            missile.Left = Int(Me.Width - Rnd() * Me.Width)
            missile.BringToFront()
            casharray(x) = missile
            onscreencash(x) = True
            casharray(x).Visible = True
        Next

    End Sub
    Private Sub unotimer_Tick(sender As Object, e As EventArgs)
        Dim random As Single
        For i = 0 To maxuno
            unoarray(i).Top += enemyspeed
            casharray(i).Top += 5 + 15 * Rnd()

            If unoarray(i).Top > Me.Height Then
                unoarray(i).Top = 70
                unoarray(i).Left = Int(Me.Width - Rnd() * Me.Width)
            End If
            If casharray(i).Top > Me.Height + 100 Then
                casharray(i).Top = -100
                casharray(i).Left = Int(Rnd() * Me.Width)
            End If
            If unoarray(i).Left < 5 Then
                unoarray(i).Left += 10
            End If
            If unoarray(i).Left > (Me.Width - 40) Then
                unoarray(i).Left -= 10
            End If
            random = Rnd()
            If random > 0.66 Then 'makes the answer move left and right randomly
                unoarray(i).Left += 5
            ElseIf random < 0.66 Then
                unoarray(i).Left -= 5
            End If

            If Collision(player1, unoarray(i)) Then ' collision of answer
                unoarray(i).Left = Me.Width + 500
                If myhealth < 100 Then
                    myhealth += 10
                End If
                myscore += 200
                unoarrayredif(i)
            End If
            If Collision(player1, casharray(i)) Then
                casharray(i).Left = Me.Width + 500
                power = True
            End If

            'randomizing yung labas ng powerup and correct answer every 5 seconds
            If random1 > 0.7 And unoarray(i).Top > Me.Height + 100 Then '70% chance of answer popping out
                unoarray(i).Hide()
            Else
                unoarray(i).Visible = True
            End If
            If random2 > 0.3 And casharray(i).Top > Me.Height Or power = True Then
                casharray(i).Hide() '30% chance of power up popping out
                casharray(i).Top = 0
            Else
                casharray(i).Show()
            End If
        Next


    End Sub
    Function questionmaking(ByVal b As Integer) 'function for question making
        If b < 33 And b > 0 Then
            question = a \ b & " * " & b & " + " & a Mod b & " =?"

        ElseIf b > 33 And b < 66 Then
            question = a * b & "/" & b & " =?"
        ElseIf b > 66 Then
            question = a - b & " + " & b & " =?"
        Else
            question = a + b & " - " & b & " =?"
        End If
        Return question
    End Function

    Sub unoarrayredif(ByVal i As Integer) 'redifining uno array and question string for the new questions
        a = 200 * Rnd()
        b = 100 * Rnd()
        unoarray(i).Text = a
        question = questionmaking(b)
    End Sub

    Sub talona()
        If myscore > myhighscore Then
            myhighscore = myscore
        End If
        enemytimer.Stop()
        timer.Stop()
        unotimer.Stop()
        MsgBox("Your score is: " & myscore, MsgBoxStyle.Critical, "GAME OVER")
        myscore = 0
        startbutton.Text = "Retry"
        startbutton.Show()
        backbutton.Show()
        rules.Show()
        For i = 0 To maxenemy
            enemyarray(i).Hide()
        Next
        For i = 0 To maxuno
            unoarray(i).Hide()
            casharray(i).Hide()
        Next
        For i = 0 To bala
            missilearray(i).Hide()
        Next
        player1.Hide()
        score.Hide()
        startbutton.Show()
        backbutton.Show()
        rules.Show()
        title.Show()
        highscore.Show()
        highscore.Text = "Highscore: " & myhighscore
    End Sub
    Sub buttonstarts()

        Me.Controls.Add(startbutton)
        With startbutton
            .ForeColor = Color.White
            .Font = New Font("Roboto", 15, FontStyle.Bold)
            .TextAlign = ContentAlignment.MiddleCenter
            .BackColor = Color.Black
            .Width = 200
            .Height = 30
            .Top = Me.Height / 2 - 70
            .Left = Me.Width / 2 - startbutton.Width / 2
            .Text = "Start"
            .TabStop = False
        End With
        AddHandler startbutton.Click, AddressOf gamestart

        Me.Controls.Add(rules)
        With rules
            .ForeColor = Color.White
            .Font = New Font("Roboto", 15, FontStyle.Bold)
            .TextAlign = ContentAlignment.MiddleCenter
            .BackColor = Color.Black
            .Width = 200
            .Height = 30
            .Top = Me.Height / 2 - 30
            .Left = Me.Width / 2 - startbutton.Width / 2
            .Text = "Rules"
            .TabStop = False
        End With
        AddHandler rules.Click, AddressOf rulesclicked
        Me.Controls.Add(backbutton)
        With backbutton
            .ForeColor = Color.White
            .Font = New Font("Roboto", 15, FontStyle.Bold)
            .TextAlign = ContentAlignment.MiddleCenter
            .BackColor = Color.Black
            .Width = 200
            .Height = 30
            .Top = Me.Height / 2 + 10
            .Left = Me.Width / 2 - startbutton.Width / 2
            .Text = "Back"
            .TabStop = False
        End With
        AddHandler backbutton.Click, AddressOf backbutton_Click
    End Sub

    Sub backbutton_Click()
        'Please input anong mangyayari pag pinindut backbutton!!
        Me.Close()
    End Sub

    Private Sub Game_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If gamestarted = True Then
            unotimer.Stop()
            timer.Stop()
            enemytimer.Stop()
        End If
        Me.Show()
        My.Computer.Audio.Stop()
    End Sub

    Sub paused()
        Dim answer As DialogResult = MsgBox("Paused! Continue??", MsgBoxStyle.YesNo, "Paused")
        If answer = vbYes Then

            unotimer.Start()
            timer.Start()
            enemytimer.Start()
        ElseIf answer = vbNo Then
            talona()
            gamestarted = False
        End If

    End Sub
End Class