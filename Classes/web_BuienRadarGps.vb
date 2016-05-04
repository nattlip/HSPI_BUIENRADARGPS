Imports System.Text
Imports System.Web
Imports Scheduler
Imports HomeSeerAPI
Imports System.Threading
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging


Public Class web_BuienRadarGps
    Inherits clsPageBuilder
    Dim TimerEnabled As Boolean



    Public lat As New String("")
    Public lon As New String("")
    Public location As New String("")
    Public index As String = ("0") '' index of location

    Public buienradardevgifstringSmall As String
    Public buienradardevgifstringBig As String


    Public Sub New(ByVal pagename As String)
        MyBase.New(pagename)
    End Sub

    Public Overrides Function postBackProc(page As String, data As String, user As String, userRights As Integer) As String

        Dim parts As Collections.Specialized.NameValueCollection
        parts = HttpUtility.ParseQueryString(data)


        Select Case parts("id")
            Case "oTextbox1"
                PostMessage(parts("Textbox1"))
                BuildTextBox("Textbox1", True, "tested")
            Case "button1"
                Me.pageCommands.Add("newpage", "\JillesBuienRadarGps\testurl.html")
                Me.divToUpdate.Add("hiddencontainer", HSPI_BUIENRADARGPS.getrain(index))
                Me.pageCommands.Add("executefunction", "alert('1');")
                Me.pageCommands.Add("executefunction", "drawspline();")
                Me.pageCommands.Add("executefunction", "alert('2');")
                Me.divToUpdate.Add("current_time", DateTime.Now.ToString)

            Case "timer" 'this stops the timer and clears the message
                ''  Me.pageCommands.Add("executefunction", "createelements()")
                ''  Me.divToUpdate.Add("hiddencontainer", HSPI_BUIENRADARGPS.getrain(index))
                ''  Me.pageCommands.Add("executefunction", "drawspline()")
                If TimerEnabled Then 'this handles the initial timer post that occurs immediately upon enabling the timer.
                    TimerEnabled = False
                Else
                    Me.pageCommands.Add("stoptimer", "")
                    Me.divToUpdate.Add("message", "&nbsp;")
                End If



        End Select


        If parts("action") = "updatetime" Then
            ' ajax timer has expired and posted back to us, update the time




            ''  Me.divToUpdate.Add("hiddencontainer2", hsip & ":" & hsport)
            Me.divToUpdate.Add("hiddencontainer", HSPI_BUIENRADARGPS.getrain(index))


            Me.pageCommands.Add("executefunction", "drawspline() ; postgif() ; ")

            Me.divToUpdate.Add("current_time", DateTime.Now.ToString)
            'Me.pageCommands.Add("executefunction", "postgif()")
            ''  Me.pageCommands.Add("executefunction", "alertgps()")
        End If

        If Mid(data, 1, 4) = "data" Then '' if is image small



            Dim urlstring As String = data

            Dim withoutdata As String = data.Replace("data:image/png;base64,", "")


            'For an image on disk
            'Dim path As String = hs.GetAppPath & "\html\images\BuienRadarGps\"
            'Dim filnamewithpath = path & "brgps.png"
            'Dim fs As FileStream = File.Open(filnamewithpath, FileMode.Open, FileAccess.ReadWrite)

            'Dim bw As BinaryWriter = New BinaryWriter(fs)
            'bw.Close()


            Dim imagedata As Byte() = Convert.FromBase64String(withoutdata)
            Dim ms As MemoryStream = New MemoryStream(imagedata)
            'bw.Write(imagedata)
            'make it a drawing image
            Dim img As Image = Image.FromStream(ms)
            Dim img2 As Image


            


            Dim j As Integer = img.Width
            Dim bm As Bitmap = New Bitmap(img, 140, 60)  ''

            Dim ms2 As New MemoryStream

            bm.Save(ms2, ImageFormat.Png)


            Dim base64 As String = Convert.ToBase64String(ms2.ToArray)

            base64 = "data:image/png;base64," + base64

            img2 = bm


            Dim jil As Integer = 5





            '' get devref of device

            Dim query1 = (From el In Files.LocationsXdoc...<location>
                            Where (el.<location_index>.Value = index)
                            Select el.<location_hsref>.Value).FirstOrDefault()



            buienradardevgifstringSmall = _
         "<div id='buienradardevgifsmall" & query1 & "' style='position: relative;top: 0px; left: 0px; width: 140px; height: 60px;background-color:#e0ffff ; z-index: 900; " & _
         " background-image: url(" & base64 & " '>"


            'If hs.DeviceValue(query1) = 0 Then



            hs.SetDeviceString(query1, buienradardevgifstringSmall, True)
            hs.SetDeviceLastChange(query1, Now)
        End If




        'End If


        If Mid(data, 1, 6) = "jilles" Then '' if is image big

            Dim urlstring As String = data.Replace("jilles", "")
            Dim withoutdata As String = data.Replace("jillesdata:image/png;base64,", "")
            Dim imagedata As Byte() = Convert.FromBase64String(withoutdata)
            Dim ms As MemoryStream = New MemoryStream(imagedata)
            'bw.Write(imagedata)
            'make it a drawing image
            Dim img As Image = Image.FromStream(ms)
            Dim img2 As Image

            Dim j As Integer = img.Width
            Dim bm As Bitmap = New Bitmap(img, 140, 60)  ''

            Dim ms2 As New MemoryStream

            bm.Save(ms2, ImageFormat.Png)


            Dim base64 As String = Convert.ToBase64String(ms2.ToArray)

            base64 = "data:image/png;base64," + base64      ''litle

            img2 = bm


            Dim jil As Integer = 5






            '' get devref of device

            Dim query1 = (From el In Files.LocationsXdoc...<location>
                            Where (el.<location_index>.Value = index)
                            Select el.<location_hsref>.Value).FirstOrDefault()

            '' buienradardevgifstringBig = "<script src='/js/buienradargps/PostGif.js'></script>" & _


            buienradardevgifstringSmall =
               "<div id='buienradardevgifsmall" & query1 & "' style='position: relative;top: 0px; left: 0px; width: 140px; height: 60px;background-color:#e0ffff ; z-index: 900; " &
    " background-image: url(" & base64 & " '>"
   





            buienradardevgifstringBig =
            "<div id='buienradardevmaster" & query1 & "'>" &
             buienradardevgifstringSmall &
              "</div>" &
         "<div id='buienradardevgifbig" & query1 & "' style='position: absolute ;top: 0px; left: 0px; width: 80% ; height: 80% ; background-color:#e0ffff ;  z-index: 900; " &
         " background-image: url(" & urlstring & " '>" &
        "<button onclick='setvisibilityhidden(" & query1 & ")'>Try it</button>" &
                       "<script type='text/javascript'>" &
             " setvisibilityhidden = function (index) { " &
            " var div1 = document.getElementById('buienradardevgifbig' + index); " &
            " div1.style.visibility = 'hidden'  ; }" &
            "</script>" &
            "</div>"


            If hs.DeviceValue(query1) = 0 Then

                hs.SetDeviceString(query1, buienradardevgifstringSmall, True)


            ElseIf hs.DeviceValue(query1) = 100 Then

                hs.SetDeviceString(query1, buienradardevgifstringBig, True)

            End If

            hs.SetDeviceLastChange(query1, Now)

            Dim v As Integer = 5


        End If











        Return MyBase.postBackProc(page, data, user, userRights)
    End Function

    Public Function GetPagePlugin(ByVal pageName As String, ByVal user As String, ByVal userRights As Integer, ByVal queryString As String) As String
        Dim stb As New StringBuilder
        Dim instancetext As String = ""
        Try

            Me.reset()

            '' Me.AddHeader(" <script src='/js/jquery-1.10.1.js'></script>")
            Me.AddHeader(" <script src='/js/buienradargps/jilles.spline0.min.js'></script>")
            Me.AddHeader(" <script src='/js/buienradargps/jilles.createelements.js'></script>")
            Me.AddHeader(" <script src='/js/buienradargps/Spline1.js'></script>")
            Me.AddHeader(" <script src='/js/buienradargps/PostGif.js'></script>")


            CurrentPage = Me

            ' handle any queries like mode=something
            Dim parts As Collections.Specialized.NameValueCollection = Nothing
            If (queryString <> "") Then
                parts = HttpUtility.ParseQueryString(queryString)

                If (queryString <> "") Then
                    parts = HttpUtility.ParseQueryString(queryString)
                    index = parts("index")
                Else
                    index = 0
                End If

                getlocationparameters(index)

            End If
            If Instance <> "" Then instancetext = " - " & Instance
            ''  stb.Append(hs.GetPageHeader(pageName, "Sample" & instancetext, "", "", True, False, ))

            stb.Append(clsPageBuilder.DivStart("pluginpage", ""))




            ' a message area for error messages from jquery ajax postback (optional, only needed if using AJAX calls to get data)
            stb.Append(clsPageBuilder.DivStart("errormessage", "class='errormessage'"))
            stb.Append(clsPageBuilder.DivEnd)



            Dim HomeseerWebServerIp As String = hs.GetIPAddress
            Dim HomeseerWebServerPort As String = hs.WebServerPort





            Me.RefreshIntervalMilliSeconds = 30000 '' 5 minutes
            stb.Append(stb.Append(Me.AddAjaxHandlerPost("action=updatetime&jilles=gek", "BuienRadarGps")))

            ''  stb.Append(Me.AddAjaxHandlerPost("id=timer", pageName))
            'Me.pageCommands.Add("refresh", "true")
            ' specific page starts here
            '' stb.Append("<div id= 'hiddencontainer'   style='display: none;'>" & HSPI_BUIENRADARGPS.getrain() & "</div>")

            stb.Append(clsPageBuilder.DivEnd)


            stb.Append(clsPageBuilder.DivStart("hiddencontainer3", "style='display: none;'"))
            stb.Append(HomeseerWebServerIp & ":" & HomeseerWebServerPort)
            stb.Append(clsPageBuilder.DivEnd)




            stb.Append("<div id='current_time'>" & DateTime.Now.ToString & "</div>" & vbCrLf)
            stb.Append("<div id='place'>  place: " & location & "</div>" & vbCrLf)
            stb.Append("<div id='lon'>  longitude:  " & lon & "</div>" & vbCrLf)
            stb.Append("<div id='lat'>  lattitude:  " & lat & "</div>" & vbCrLf)
            stb.Append(BuildContent)

            Dim b2 As New clsJQuery.jqButton("button1", "Button for form2", "BuienRadarGps", False)
            '' b2.functionToCallOnClick = "createrandomspline()"
            stb.Append(b2.Build)

            ''   stb.Append(BuildButton("Button1"))

            ' add the body html to the page
            Me.AddBody(stb.ToString)

            ' return the full page
            Return Me.BuildPage()
        Catch ex As Exception
            'WriteMon("Error", "Building page: " & ex.Message)
            Return "error - " & Err.Description
        End Try
    End Function

    Function BuildContent() As String
        Dim pstr As New StringBuilder

        pstr.Clear()


        pstr.Append("<script type='text/javascript'>")
        pstr.Append("createelements()")
        '' pstr.Append("createparentandbardivsasgif(""parent"")")
        pstr.Append("</script>")



        Return pstr.ToString
    End Function

    Public Function BuildTextBox(ByVal Name As String, Optional ByVal Rebuilding As Boolean = False, Optional ByVal Text As String = "") As String
        Dim tb As New clsJQuery.jqTextBox(Name, "", Text, Me.PageName, 20, False)
        Dim TextBox As String = ""
        tb.id = "o" & Name

        TextBox = tb.Build

        If Rebuilding Then
            Me.divToUpdate.Add(Name & "_div", TextBox)
        Else
            TextBox = "<div id='" & Name & "_div'>" & tb.Build & "</div>"
        End If

        Return TextBox
    End Function

    Function BuildButton(ByVal Name As String, Optional ByVal Rebuilding As Boolean = False) As String
        Dim Content As String = ""
        Dim ButtonText As String = "Submit"
        Dim Button As String
        Dim b As New clsJQuery.jqButton(Name, "", Me.PageName, True)

        Select Case Name
            Case "Button1"
                ButtonText = "Go To Status Page"
                b.submitForm = True
        End Select

        b.id = "o" & Name
        b.label = ButtonText

        Button = b.Build

        If Rebuilding Then
            Me.divToUpdate.Add(Name & "_div", Button)
        Else
            Button = "<div id='" & Name & "_div'>" & Button & "</div>"
        End If
        Return Button
    End Function

    Sub PostMessage(ByVal sMessage As String)
        Me.divToUpdate.Add("message", sMessage)
        Me.pageCommands.Add("starttimer", "")
        TimerEnabled = True
    End Sub

    Sub getlocationparameters(index)

        Dim query1 = (From el In Files.LocationsXdoc...<location>
                          Where (el.<location_index>.Value = index)
                          Select el).FirstOrDefault()



        If String.IsNullOrEmpty(query1.<location_street>.Value) Then

        Else

            lon = query1.<location_longitude>.Value
            lat = query1.<location_lattitude>.Value
            location = query1.<location_street>.Value & " " & query1.<location_city>.Value & " " & query1.<location_city>.Value & " " & query1.<location_country>.Value
        End If



    End Sub





End Class

