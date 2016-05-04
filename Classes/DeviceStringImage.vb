Imports HomeSeerAPI
Imports Scheduler
Imports System.Reflection
Imports System.Text


Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters

Imports System.Xml
Imports System.Data
Imports System.Linq
Imports System.Xml.Linq

Imports System.Xml.Serialization
Imports SplineFormCpp2
Imports System.Collections


Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.ComponentModel



''' <summary>
''' 'class to create a dvicestring and 
''' </summary>
''' <remarks></remarks>
Public Class DeviceStringImage

    Property index As Integer ''hsdevref of string
    Property devstring As StringBuilder
    Property AllZero As Boolean = False '' boolean , if true then no new graphic has to be made lateron only graphic with times

    Dim small As String
    Dim big As String

    Dim size As Boolean  '' boolean if 




    Sub New(ByVal ref As Integer)

        index = ref

    End Sub


    ''' <summary>
    ''' 'to set devicestring of buienradar gps big  = devicevalue 100 or small = devicivalue 0
    ''' 
    ''' 
    ''' 
    ''' 
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub setdevicestringbuienradargps(ByVal raindata As AllRainData)

        Dim cs As New ClassesAndStructures

        Dim arr As New List(Of KeyValuePair(Of String, Double))

        Dim str As String = raindata.str

        arr = cs.result(str)

        '' arr = makekeyvaluepairlistfromstring(str)

        '' give rdevicereference
        Dim query1 = (From el In Files.LocationsXdoc...<location>
        Where (el.<location_index>.Value = index)
                        Select el.<location_hsref>.Value).FirstOrDefault()

        '' give place 
        Dim query2 = (From el In Files.LocationsXdoc...<location>
        Where (el.<location_index>.Value = index)
                        Select el.<location_city>.Value).FirstOrDefault()

        raindata.City = query2
        '' Dim g As System.Drawing.Graphics = Drawing.Graphics.FromImage(bm)


        Dim go As New GraphObj


        Dim bitmapspline As JillesSpline

        bitmapspline = New JillesSpline(go.g, go.Canvas.Size, 15)


        bitmapspline.DrawStart(arr)

        Dim BigBm As Bitmap = go.Canvas

        Dim BigMs As New MemoryStream

        BigBm.Save(BigMs, System.Drawing.Imaging.ImageFormat.Bmp)

        Dim Bigbase64 As String = Convert.ToBase64String(BigMs.ToArray)

        'Bigbase64 = "data:image/x-ms-bmp;base64," + Bigbase64
        Bigbase64 = "data:image/x-ms-bmp;base64," + Bigbase64

        ''http://stackoverflow.com/questions/11244752/resizing-compressing-and-smoothing-images-with-vb-net

        go.g.DrawImage(go.Canvas, 0, 0, 140, 60)

        Dim SmallBm As Bitmap = go.Canvas

        Dim SmallMs As New MemoryStream

        SmallBm.Save(SmallMs, System.Drawing.Imaging.ImageFormat.Bmp)

        ''  http://filext.com/file-extension/BMP

        Dim Smallbase64 As String = Convert.ToBase64String(SmallMs.ToArray)

        '' Smallbase64 = "data:image/x-ms-bmp;base64," + Smallbase64
        Smallbase64 = "data:image/x-ms-bmp;base64," + Smallbase64




        '        small =
        '           "<div id='buienradardevgifsmall" & query1 & "' style='position: relative;top: 0px; left: 0px; width: 140px; height: 60px;background-color:#e0ffff ; z-index: 900; " &
        '" background-image: url(" & Smallbase64 & " '>"


        small =
                 "<div id='buienradardevgifsmall" & query1 & "' style='position: relative;top: 0px; left: 0px; width: 140px; height: 60px; z-index: 900; " &
      " background-image: url(" & Smallbase64 & ")  '>"




        big =
        "<div id='buienradardevmaster" & query1 & "'>" &
        small &
          "</div>" &
     "<div id='buienradardevgifbig" & query1 & "' style='position: absolute ;top: 0px; left: 0px; width: 1200px ; height: 400px ; background-color:#e0ffff ;  z-index: 900; " &
     " background-image: url(" & Bigbase64 & ") '>" &
    "<button onclick='setvisibilityhidden(" & query1 & ")'>Back</button>" &
                   "<script type='text/javascript'>" &
         " setvisibilityhidden = function (index) { " &
        " var div1 = document.getElementById('buienradardevgifbig' + index); " &
        " div1.style.visibility = 'hidden'  ; }" &
        "</script>" &
        "</div>"



        'If hs.DeviceValue(query1) = 0 Then

        '    hs.SetDeviceString(query1, small, True)


        'ElseIf hs.DeviceValue(query1) = 100 Then

        '    hs.SetDeviceString(query1, big, True)

        'End If

        If GetNamedEdoHsDevice(query1, "size") = "True" Then

            hs.SetDeviceString(query1, small, True)

        ElseIf GetNamedEdoHsDevice(query1, "size") = "False" Then

            hs.SetDeviceString(query1, big, True)

        End If






        hs.SetDeviceLastChange(query1, Now)

        Dim v As Integer = 5



        setvirtualdevicestring(GetDeviceRefChildFromParent(query1), raindata)







    End Sub


    Sub SetDeviceValue(ByVal devref As Integer, ByVal devval As Double)

        If devval = 0 Then

            hs.SetDeviceString(devref, small, True)

        ElseIf devval = 100 Then

            hs.SetDeviceString(devref, big, True)

        End If

        hs.SetDeviceLastChange(devref, Now)
    End Sub

    Sub SetDeviceGraphSize(ByVal devref As Integer, ByVal size As Boolean)

        If size = True Then

            hs.SetDeviceString(devref, small, True)

        ElseIf size = False Then

            hs.SetDeviceString(devref, big, True)

        End If

        '' hs.SetDeviceLastChange(devref, Now)
    End Sub

    Sub setvirtualdevicestring(ByVal RefDevChild As Integer, ByVal raindata As AllRainData)



        Dim NoRain As String = "No Rain"
        Dim Rain As String = "Rain Expected"

        Dim dvstring As String
        Dim countofindextorain As Integer = 25  '' startvalue
        Dim strb As New StringBuilder



        If raindata.LocalCalculatedRainData.CountOfZero = 25 Then


            strb.Append(NoRain)

            strb.Append("<br>")

            Dim rainintensity As String = "intensity: " & [Enum].GetName(GetType(RainIntensity), raindata.LocalCalculatedRainData.IntensityRain)

            strb.Append(rainintensity)






            dvstring = strb.ToString


        Else



            strb.Append(Rain)

            strb.Append("<br>")

            Dim indexoffirstrain As String = "index:  " & raindata.LocalCalculatedRainData.IndexOfFirstRainNotZero

            strb.Append(indexoffirstrain)

            strb.Append("<br>")


            countofindextorain = raindata.LocalCalculatedRainData.IndexOfFirstRainNotZero - raindata.LocalCalculatedRainData.IndexFirstTimeafterNow


            Dim test As Integer = 5

            If countofindextorain < 0 Then

                countofindextorain = 0

            End If

            Dim minutes As Integer = countofindextorain * 5

            Dim interval As TimeSpan = TimeSpan.FromMinutes(minutes)

            Dim timetorain As String = "time to rain: " & interval.ToString

            strb.Append(timetorain)

            strb.Append("<br>")

            Dim maxrain As String = "max: " & raindata.LocalCalculatedRainData.MaxRainValue.ToString

            strb.Append(maxrain)

            strb.Append("<br>")

            Dim IndexOfFirstOfLastPeriodOfNoRain As Integer = raindata.LocalCalculatedRainData.IndexOfFirstOfLastPeriodOfNoRain

            interval = TimeSpan.FromMinutes(IndexOfFirstOfLastPeriodOfNoRain * 5)

            Dim timetodry As String = "time to dry: " & interval.ToString

            strb.Append(timetodry)

            strb.Append("<br>")
            ''[Enum].GetName(GetType(Colors), 3))




            ''http://stackoverflow.com/questions/18888519/get-vb-net-enum-description-from-value

            ''  Dim rainintensity As String = "intensity: " & GetEnumDescription(raindata.LocalCalculatedRainData.IntensityRain)

            '' http://msdn.microsoft.com/en-us/library/system.enum.getname(v=vs.110).aspx

            Dim rainintensity As String = "intensity: " & [Enum].GetName(GetType(RainIntensity), raindata.LocalCalculatedRainData.IntensityRain)

            strb.Append(rainintensity)

            strb.Append("<br>")



            Dim averegerain As String = "average: " & raindata.LocalCalculatedRainData.RainValueAverage.ToString

            strb.Append(averegerain)



            dvstring = strb.ToString
        End If



        hs.SetDeviceLastChange(RefDevChild, Now)
        hs.SetDeviceValueByRef(RefDevChild, countofindextorain, True)
        hs.SetDeviceString(RefDevChild, dvstring, True)

        ''hs.Speak(hs.GetAppPath & "\Wave\problem.wav", True)
     

        JillesSpeak(raindata.City, RefDevChild, countofindextorain)


    End Sub

    Sub JillesSpeak(ByVal city As String, ref As Integer, ByVal value As Integer)
        ''  hs.Speak("rain in $$DSR:" & ref.ToString & ":  minutes ", True)



        Try

            city.Replace("ij", "i")

        Catch ex As Exception

        End Try






        Select Case value
            Case 25

                hs.Speak("no rain expected in " & city & "   the next 2 hours ", True)

                ''    hs.Speak("does ingrid want coffee ", True)

            Case 2 To 24

                hs.Speak(" rain expected in " & city & "   over " & FiveMinutesPartsToTimeString(value), True)

                'Case 23

                '    hs.Speak(" rain expected in " & city & "   over 1 hour and 55 minutes ", True)

                'Case 22

                '    hs.Speak(" rain expected in " & city & "   over 1 hour and 50 minutes ", True)

                'Case 21

                '    hs.Speak(" rain expected in " & city & "   over 1 hour and 45 minutes ", True)

                'Case 20

                '    hs.Speak(" rain expected in " & city & "   over 1 hour and 40 minutes ", True)

                'Case 19

                '    hs.Speak(" rain expected in " & city & "   over 1 hour and 35 minutes ", True)

                'Case 18

                '    hs.Speak(" rain expected in " & city & "   over 1 hour and 30 minutes ", True)

                'Case 17

                '    hs.Speak(" rain expected in " & city & "   over 1 hour and 25 minutes ", True)

                'Case 16

                '    hs.Speak(" rain expected in " & city & "   over 1 hour and 20 minutes ", True)

                'Case 15

                '    hs.Speak(" rain expected in " & city & "   over 1 hour and 15 minutes ", True)

                'Case 14

                '    hs.Speak(" rain expected in " & city & "   over 1 hour and 10 minutes ", True)

                'Case 13

                '    hs.Speak(" rain expected in " & city & "   over 1 hour and 5 minutes ", True)

                'Case 12

                '    hs.Speak(" rain expected in " & city & "   over 1 hour  ", True)


                'Case 11

                '    hs.Speak(" rain expected in " & city & "   over  55 minutes ", True)


                'Case 10

                '    hs.Speak(" rain expected in " & city & "  over  45 minutes ", True)

                'Case 9

                '    hs.Speak(" rain expected in " & city & "  over 40 minutes ", True)

                'Case 8

                '    hs.Speak(" rain expected in " & city & "   35 minutes ", True)


                'Case 7

                '    hs.Speak(" rain expected in " & city & "   30 minutes ", True)

                'Case 6

                '    hs.Speak(" rain expected in " & city & "   25 minutes ", True)

                'Case 5

                '    hs.Speak(" rain expected in " & city & "   20 minutes ", True)

                'Case 4

                '    hs.Speak(" rain expected in " & city & "   15 minutes ", True)

                'Case 3

                '    hs.Speak(" rain expected in " & city & "   10 minutes ", True)

                'Case 2

                '    hs.Speak(" rain expected in " & city & "   5 minutes ", True)

            Case 1

                hs.Speak(" it is raining  in " & city & "    ", True)

            Case 0

                hs.Speak(" it is raining  in " & city & "    ", True)


        End Select






    End Sub


    Public Shared Function GetEnumDescription(ByVal EnumConstant As [Enum]) As String
        Dim fi As FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())
        Dim attr() As DescriptionAttribute = _
                      DirectCast(fi.GetCustomAttributes(GetType(DescriptionAttribute), _
                      False), DescriptionAttribute())

        If attr.Length > 0 Then
            Return attr(0).Description
        Else
            Return EnumConstant.ToString()
        End If
    End Function









    Function makekeyvaluepairlistfromstring(ByVal str As String) As List(Of KeyValuePair(Of String, Double))

        Dim b As Integer = 77

        Dim c As Object = 10 ^ ((b - 109) / 32)

        Dim arg() As String = {vbCrLf, vbLf}

        Dim raintimepairs As String() = str.Split(arg, StringSplitOptions.None)

        Dim datum As Date = Now.ToShortTimeString

        Dim raintimepair As KeyValuePair(Of String, Double)

        Dim raintimepairlist As New List(Of KeyValuePair(Of String, Double))


        Dim raintimemepairarray As Array
        raintimemepairarray = {}


        ''get location from index
        Dim query2 = (From el In Files.RainXdoc...<Location>
                                Where (el.<IndexLocation>.Value = index)
                                Select el).FirstOrDefault()




        For i As Integer = 0 To raintimepairs.Length - 2


            raintimepair = New KeyValuePair(Of String, Double)(raintimepairs(i).Split("|")(1), _
                      Math.Round((10 ^ ((((CInt(raintimepairs(i).Split("|")(0)))) - 109) / 32)), 2))


            raintimepairlist.Add(raintimepair)

            '' get pair from index
            Dim r As Integer = i
            Dim query3 = (From el In query2...<Pair>
                                  Where (el.<IndexPair>.Value = r)
                                  Select el).FirstOrDefault()

            query3.<Time>.Value = raintimepair.Key
            query3.<Rain>.Value = raintimepair.Value



        Next



        Return raintimepairlist





    End Function


    Function FiveMinutesPartsToTimeString(ByVal index) As String

        Dim strb As New StringBuilder


        Dim ts5 As New TimeSpan(0, 5, 0)

        Dim ts As TimeSpan = ts5

        For i = 0 To (index - 1)

            ts = ts.Add(ts5)

        Next

        Dim strh As String = CStr(IIf(ts.Hours > 0, String.Format("{0:0} hours and ", ts.Hours), ""))
        Dim strm As String = CStr(IIf(ts.Minutes > 0, String.Format("{0:0} minutes  ", ts.Minutes), ""))

        strb.Append(strh)
        strb.Append(strm)

        Return strb.ToString

    End Function

End Class
