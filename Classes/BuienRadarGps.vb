Imports System
Imports Scheduler
Imports HomeSeerAPI
Imports HSCF.Communication.Scs.Communication.EndPoints.Tcp
Imports HSCF.Communication.ScsServices.Client
Imports HSCF.Communication.ScsServices.Service
Imports System.Reflection
Imports System.Xml
Imports System.Xml.Linq

''' <summary>
''' 'in this module raindata are retreived from buienradar put in a list and str and time and raindata are analysed
''' </summary>
''' <remarks></remarks>
Public Module BuienRadarGps


    Public Arima(15) As DeviceStringImage

    Dim Rainxml As XElement = Files.RainXdoc
    Public BuienRadarWebSiteConnected As Boolean = False


    Public Structure AllRainData
        Public str As String
        Public timerainlist As List(Of KeyValuePair(Of String, Double))
        'Public RainValueAverage As Decimal
        'Public IndexOfFirstRainZero As Integer
        'Public IndexOfFirstRainNotZero As Integer
        Public LocalCalculatedRainData As CalculatedRainData
        Public City As String
    End Structure


    Public Structure CalculatedRainData
        Public RainValueAverage As Decimal
        Public IndexOfFirstRainZero As Integer
        Public IndexOfFirstRainNotZero As Integer
        Public CountOfZero As Integer
        Public CountOfNotZero As Integer
        Public MaxRainValue As Decimal
        Public FirstTimeAfterNow As DateTime
        Public IndexFirstTimeafterNow As Integer
        Public IntensityRain As RainIntensity
        ''' <summary>
        ''' 'test
        ''' </summary>
        ''' <remarks></remarks>
        Public IndexOfFirstOfLastPeriodOfNoRain As Integer
    End Structure

    Public Structure FirstTimeAfterNow
        Public Index As Integer
        Public Time As DateTime
    End Structure

    Public Enum RainIntensity
        None = 0
        Light = 1
        Moderate = 2
        Heavy = 3
        Violent = 4
    End Enum







    '    Intensity

    ''  http://en.wikipedia.org/wiki/Rain




    'Heavy rain in Glenshaw, PA








    'The sound of a heavy rain fall in suburban neighborhood


    'Problems playing this file? See media help. 

    'Rainfall intensity is classified according to the rate of precipitation:
    'Light rain — when the precipitation rate is < 2.5 mm (0.098 in) per hour
    'Moderate rain — when the precipitation rate is between 2.5 mm (0.098 in) - 7.6 mm (0.30 in) or 10 mm (0.39 in) per hour[103][104]
    'Heavy rain — when the precipitation rate is > 7.6 mm (0.30 in) per hour,[103] or between 10 mm (0.39 in) and 50 mm (2.0 in) per hour[104]
    'Violent rain — when the precipitation rate is > 50 mm (2.0 in) per hour[104]

    'Euphemisms for a heavy or violent rain include gully washer, trash-mover and toad-strangler.[







    Public checkraincount As Integer = 0




    Public Function CheckConnectionBuiennRadarGps() As Boolean



        Dim lat As String = "53.197718900000012"
        Dim lon As String = "5.789775100000043"
        Dim a As String
        Try
            a = hs.GetURL("gps.buienradar.nl", "/getrr.php?lat=" & lat & "&lon=" & lon, False, 80, True)
            BuienRadarWebSiteConnected = True

        Catch ex As Exception
            BuienRadarWebSiteConnected = False
        End Try

        If Mid(a, 1, 5) = "ERROR" Then

            BuienRadarWebSiteConnected = False

        Else
            BuienRadarWebSiteConnected = True
        End If
        HsDevice.CheckDeviceValueIO(BuienRadarWebSiteConnected)
        Return BuienRadarWebSiteConnected

    End Function

    Public Function getrain(ByVal index As String) As AllRainData

        Dim LocalRainData As New AllRainData



        Dim gpswebsiteerror As Boolean = True
        Dim lat As New String("")
        Dim lon As New String("")
        Dim location As New String("")
        '' Public index As String = ("0") '' index of location

        '' get location from index
        Dim query1 = (From el In Files.LocationsXdoc...<location>
                             Where (el.<location_index>.Value = index)
                             Select el).FirstOrDefault()



        If String.IsNullOrEmpty(query1.<location_street>.Value) Then

        Else

            lon = query1.<location_longitude>.Value
            lat = query1.<location_lattitude>.Value
            location = query1.<location_street>.Value & " " & query1.<location_city>.Value & " " & query1.<location_city>.Value & " " & query1.<location_country>.Value
        End If



        Dim a As String = ""




        Try

            a = hs.GetURL("gps.buienradar.nl", "/getrr.php?lat=" & lat & "&lon=" & lon, False, 80, True)


        Catch ex As Exception

            a = "ERROR"

        End Try



        If Mid(a, 1, 5) = "ERROR" Then

            gpswebsiteerror = True
            BuienRadarWebSiteConnected = False

            buienradar_timer.Interval = 30000

        Else

            gpswebsiteerror = False

            BuienRadarWebSiteConnected = True



        End If

        HsDevice.CheckDeviceValueIO(BuienRadarWebSiteConnected)


        Dim b As Integer = 77

        Dim c As Object = 10 ^ ((b - 109) / 32)

        Dim arg() As String = {vbLf} '' vbLf  vbCrLf

        Dim raintimepairs As String() = a.Split(arg, StringSplitOptions.None)

        Dim datum As Date = Now.ToShortTimeString

        Dim raintimepair As KeyValuePair(Of String, Double)

        Dim raintimepairlist As New List(Of KeyValuePair(Of String, Double))


        Dim raintimemepairarray As Array
        raintimemepairarray = {}


        ''get location from index
        Dim query2 As XElement = (From el In Files.RainXdoc...<Location>
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



        Console.Write(a)

        LocalRainData.timerainlist = raintimepairlist

        LocalRainData.LocalCalculatedRainData = CheckRain(index)

        LocalRainData.str = a


        raintimemepairarray = raintimepairlist.ToArray


        Return LocalRainData



    End Function





    Public WithEvents buienradar_timer As New System.Timers.Timer

    Public buienradartimerVal As Double

    Public Sub buienradar_timer_Elapsed(sender As Object, e As System.Timers.ElapsedEventArgs) Handles buienradar_timer.Elapsed

        buienradartimeraction()


    End Sub


    Sub buienradartimeraction()





        Dim xmlfile1 As XDocument = Files.LocationsXdoc

        '' if hsref > 0 then device can be deleted and all other buttons accept add can be enabled 


        Dim query1 = From el In xmlfile1...<location>
        Where (el.<location_monitor>.Value = "true")
        Select el.<location_index>.Value

        'Dim indexes = From el In query1
        'Select el.<location_index>

        If BuienRadarGps.BuienRadarWebSiteConnected = True Then

            buienradar_timer.Interval = 300000                        ''300000 ''5 minutes






            For Each i In query1


                Dim index As Integer = CInt(i)


                '' get live data
                Dim raindata As AllRainData = HSPI_BUIENRADARGPS.getrain(index)



                If checkraincount = 0 Then ''Or (raindata.LocalCalculatedRainData.MaxRainValue <> 0 And Arima(index).AllZero = True) Then  '' if it isnt the first time after starting plugin



                    Arima(index) = New DeviceStringImage(index)

                    Arima(index).setdevicestringbuienradargps(raindata)

                    If raindata.LocalCalculatedRainData.MaxRainValue = 0 Then

                        Arima(index).AllZero = True

                    End If


                End If



                '' if not first pass then
                If checkraincount > 0 Then

                    '' if rain isnt 0
                    If raindata.LocalCalculatedRainData.MaxRainValue <> 0 Then

                        Arima(index).setdevicestringbuienradargps(raindata)

                        Arima(index).AllZero = False


                    ElseIf raindata.LocalCalculatedRainData.MaxRainValue = 0 Then

                        If Arima(index).AllZero = True Then

                            Dim nonewrainstring As Boolean = False

                        ElseIf Arima(index).AllZero = False Then

                            Arima(index).setdevicestringbuienradargps(raindata)

                            Arima(index).AllZero = True

                        End If

                    End If

                End If
                '' if it isnt the first time after starting pluginThen 






                '' check if raindata is zero , and if this is the first time to check 

                'Arima(index) = New DeviceStringImage(index)

                'Arima(index).setdevicestringbuienradargps(raindata)

                '' checkrain(index)
            Next

        Else

            BuienRadarGps.BuienRadarWebSiteConnected = CheckConnectionBuiennRadarGps()
            buienradar_timer.Interval = 30000
        End If

        checkraincount += 1  '' to check if it is the first time , for checking 0 rain

    End Sub



    ''' <summary>
    ''' 'gets  rain data on checked locations
    ''' </summary>
    ''' <remarks></remarks>
    Public Function CheckRain(ByVal index As Integer) As CalculatedRainData

        Dim LocalCalculatedRainData As New CalculatedRainData


        ''http://msdn.microsoft.com/en-us/vstudio/bb688088

        '' get checked locationas which should be monitored 
        Dim querylocations1 = (From el In Files.LocationsXdoc...<location>
                             Where (el.<location_monitor>.Value = "true")
                             Select el.<location_index>.Value)

        Dim querydevref1 = (From el In Files.LocationsXdoc...<location>
                             Where (el.<location_monitor>.Value = "true")
                             Select el.<location_hsref>.Value)

        '' get rainpairlist in xml of location index
        Dim queryraintimepair2 = (From el In Files.RainXdoc...<Location>
                               Where (el.<IndexLocation>.Value = index)
                               Select el).FirstOrDefault()
        '' get all rain values not the xml around it.
        Dim queryrain3 = (From el In queryraintimepair2...<Pair>
                               Select Double.Parse(el.<Rain>.Value))

        Dim querytime3 = (From el In queryraintimepair2...<Pair>
                               Select el.<Time>.Value).ToList  '' was collection which isnt indexed

        '' rain

        Dim query3rainlist As New List(Of Double)

        For Each rain In queryrain3

            rain = CDbl(rain)
            query3rainlist.Add(rain)
        Next


        Dim averagerain4 = Aggregate rain In queryrain3
                           Into Average()



        Dim averagerain3 = query3rainlist.Average()

        LocalCalculatedRainData.RainValueAverage = averagerain3

        Dim MaxRain = Aggregate rain In queryrain3
                           Into Max()

        LocalCalculatedRainData.MaxRainValue = MaxRain

        Select Case MaxRain

            Case 0
                LocalCalculatedRainData.IntensityRain = RainIntensity.None
            Case 0 To 2.5
                LocalCalculatedRainData.IntensityRain = RainIntensity.Light
            Case 2.5 To 10
                LocalCalculatedRainData.IntensityRain = RainIntensity.Moderate
            Case 10 To 50
                LocalCalculatedRainData.IntensityRain = RainIntensity.Heavy
            Case Is > 50
                LocalCalculatedRainData.IntensityRain = RainIntensity.Heavy

        End Select















        Dim MinRain = Aggregate rain In queryrain3
                           Into Min()

        Dim SumRain = Aggregate rain In queryrain3
                           Into Sum()

        '' get  count of qyery3 with zero rain
        Dim RainCountZero = Aggregate rn In queryrain3
                          Where rn = "0"
                          Into Count()

        LocalCalculatedRainData.CountOfZero = RainCountZero

        Dim RainCountNotZero = queryrain3.Count - RainCountZero

        '' get index of first rain not zero

        LocalCalculatedRainData.CountOfNotZero = RainCountNotZero

        Dim FirstRainNotZero As Double

        Try
            FirstRainNotZero = (From el In queryrain3
                                   Where el <> "0"
                                   Select el).FirstOrDefault
        Catch ex As Exception

            FirstRainNotZero = -1

        End Try

        'Dim s
        'Const server_url = "http://someserver.com/datapost/hereitis.html"
        'Const headers = "Content-Type: application/x-www-form-urlencoded"

        's = hs.URLAction(server_url, "POST", Data, headers)

        'Dim s As Object

        's = hs.URLAction("http://10.10.10.4/BuienRadarGps", "POST", "posted string", "Content-Type: application/x-www-form-urlencoded")


        Dim IndexOfFirstOfLastPeriodOfNoRain As Integer = 25


        Dim zerolist As New List(Of Double)  '' to hold first list of zeros
        
        For i As Integer = 0 To queryrain3.Count - 1

            Dim f As Integer = i
            Dim d As Double

            Dim list1 As New List(Of Double)

            For h As Integer = i To queryrain3.Count - 1

                list1.Add(queryrain3.ToList(h))

            Next


            Dim MaxRain1 = Aggregate rain In list1
                          Into Max()

            If MaxRain1 = 0 And zerolist.Count = 0 Then

                zerolist = list1
                IndexOfFirstOfLastPeriodOfNoRain = i

            End If


        Next


        LocalCalculatedRainData.IndexOfFirstOfLastPeriodOfNoRain = IndexOfFirstOfLastPeriodOfNoRain















        Dim FirstRainZero As Double


        Try




            FirstRainZero = (From el In queryrain3
        Where el = "0"
Select el
).First

            'gives fault ifthere is no 0

        Catch ex As Exception

            FirstRainZero = -1

        End Try


        Dim IndexOfFirstRainZero = queryrain3.ToList.IndexOf(FirstRainZero)

        LocalCalculatedRainData.IndexOfFirstRainZero = IndexOfFirstRainZero

        ''
        Dim IndexOfFirstRainNotZero = queryrain3.ToList.IndexOf(FirstRainNotZero)

        LocalCalculatedRainData.IndexOfFirstRainNotZero = IndexOfFirstRainNotZero


        ''  a = hs.GetURL("gps.buienradar.nl", "/getrr.php?lat=" & lat & "&lon=" & lon, False, 80, True)

        LocalCalculatedRainData.FirstTimeAfterNow = checktime(querytime3).Time
        LocalCalculatedRainData.IndexFirstTimeafterNow = checktime(querytime3).Index



        '' hs.Speak(hs.GetAppPath & "\Wave\problem.wav", True)
        ''  hs.Speak("..\Wave\problem.wav", True)

        Return LocalCalculatedRainData








        Dim j As Integer = 5
    End Function

   


    Function checktime(ByVal timequery As List(Of String)) As FirstTimeAfterNow

        Dim yearnow As Integer = Now.Year
        Dim monthnow As Integer = Now.Month
        Dim daynow As Integer = Now.Day
        Dim hournow As Integer = Now.Hour
        Dim minutenow As Integer = Now.Minute
        Dim secondnow As Integer = 0
        Dim timelist As New List(Of Date)

        Dim TimeSpanBuienRadarGpsTotal = New TimeSpan(1, 55, 0) ''hour  time now = app third time real time is 1 hour 50 minutes


        '' it is about 0 indexed elemnt 2 or 3 if they are 00.00 then it goes wrong


        Dim arg As Char = vbLf
        Dim arg1 As Char = vbCrLf
        Dim arg2 As Char = Chr(13)
        Dim arg3 As Char = Chr(10)

        For i = 0 To timequery.Count - 1

            Dim s = timequery(i)

            's = s.Replace(arg1, "")
            's = s.Replace(arg2, "")
            's = s.Replace(arg3, "")
            s = s.Trim()

            timequery(i) = s
        Next

        Dim str As String = "00:00"

        Dim ZeroIndex = timequery.IndexOf(str)





        For Each Time As String In timequery

            Dim hour As Integer = CInt(Mid(Time, 1, 2))
            Dim minute As Integer = CInt(Mid(Time, 4, 2))

            Dim dt = New Date(yearnow, monthnow, daynow, hour, minute, 0)    '' to check if dt is bigger as now 

            If ZeroIndex = -1 Then

                timelist.Add(dt)

            ElseIf ZeroIndex > 1 And (Now.Hour = 23 Or Now.Hour = 22) Then     '' if zeroindex = 1  and now is the old date then there shouldnt by added a day heppens with index 2,3,1 

                If timequery.IndexOf(Time) < ZeroIndex Then '' if index is <zero index

                    timelist.Add(dt)

                Else  '' if index >=  zeorindex  add a day


                    timelist.Add(New Date(yearnow, monthnow, daynow + 1, hour, minute, 0))


                End If
               

            ElseIf ZeroIndex >= 0 And (Now.Hour = 0 Or Now.Hour = 1) Then


                If timequery.IndexOf(Time) < ZeroIndex Then '' if index is <zero index 

                    timelist.Add(New Date(yearnow, monthnow, daynow - 1, hour, minute, 0))

                Else  '' if index >=  zeorindex  this day


                    timelist.Add(dt)


                End If


            End If

        Next

        '' Dim FirstTimeAfterNow As DateTime

        'Try

        Dim FirstTimeAfterNow = (From el As Date In timelist
                           Where el > Now
                           Select el).First


        'Catch ex As Exception
        '' if times after el are all smaller



        'End Try







        ''http://www.tutorialspoint.com/vb.net/vb.net_date_time.htm
        ''http://social.msdn.microsoft.com/Forums/vstudio/en-US/9bbb28f9-3bdc-45fe-af24-b5dcc4a299e9/why-is-only-the-time-fraction-of-datetime-structure-compared-?forum=vbgeneral





        Dim ArrayTimequery = timequery.ToArray

        Dim ZeroIndexArray As Integer

        Dim str1 = timequery(5)
        Dim str1index = timequery.IndexOf(str1)

        ZeroIndexArray = Array.IndexOf(ArrayTimequery, str)



        Dim timelist1 As New List(Of DateTime)

        For i = 0 To (timequery.Count) - 1
            Dim t As String
            Dim d As DateTime


            t = timequery(i)


            'If ZeroIndex < 1 Or ZeroIndex > 3 Then '' <1 = -1 , 0

            If DateTime.TryParse(t, d) Then
                If i < ZeroIndex Then
                    Dim da As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                    'Dim dan As New DateTime
                    'dan = da.Add(d.TimeOfDay)
                    timelist1.Add(da.Add(d.TimeOfDay))
                Else
                    Dim da As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1)
                    'Dim dan As New DateTime
                    'dan = da.Add(d.TimeOfDay)
                    timelist1.Add(da.Add(d.TimeOfDay))
                    Dim g As Integer = 5
                End If
            End If

            'End If
            ''  ElseIf (ZeroIndex = 1 Or ZeroIndex = 2 Or ZeroIndex = 3) Then

            'If DateTime.TryParse(t, d) Then
            '    If i < ZeroIndex Then
            '        Dim da As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
            '        da.Add(d.TimeOfDay)
            '        timelist1.Add(d)
            '    Else
            '        Dim da As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1)
            '        da.Add(d.TimeOfDay)
            '        timelist1.Add(d)

            '    End If
            ''    End If



            'ElseIf ZeroIndex = 3 Then

            '    If DateTime.TryParse(t, d) Then
            '        Dim da As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
            '        da.Add(d.TimeOfDay)
            '        timelist1.Add(d)
            '    End If












        Next

        Dim FirstTimeAfternow1 As DateTime = (From el In timelist1
        Where el > DateTime.Now
                                                            Select el).First


        Dim j As Integer = 5


        Dim localfirstimeafternow As FirstTimeAfterNow

        localfirstimeafternow.Time = FirstTimeAfterNow
        localfirstimeafternow.Index = timelist.IndexOf(FirstTimeAfterNow)

        Return localfirstimeafternow










        ''Dim FirstTimeAfterNow1 As DateTime

        'FirstTimeAfterNow1 = timelist
        '.Where f(i) > Now




        '        var query = dataset.Tables[0].AsEnumerable()
        '           .Where (i=> i.Field<string>("Project").Contains("070932.01"))
        '//         .Select(i =>
        '//         {return i;}
        '//           )
        '           .Select (i=>i.Field<string>("City"));

        'Dim IndexOfFirstTimeAfterNow = timelist.IndexOf(FirstTimeAfterNow)

        'Dim span As TimeSpan = FirstTimeAfterNow - Now





    End Function
End Module


