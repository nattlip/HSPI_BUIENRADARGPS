Imports System.Text
Imports System.Web
Imports Scheduler
Imports HomeSeerAPI
Imports System.Threading








Public Class web_BuienRadarConfig


    Inherits clsPageBuilder


    Dim street As String
    Dim city As String
    Dim country As String
    Dim lon As String = ""
    Dim lat As String = ""
    Public Shared id_index As Integer = -1
    Dim hsref As Integer = -1

    Public Sub New(ByVal pagename As String)
        MyBase.New(pagename)
    End Sub

    Public Overrides Function postBackProc(page As String, data As String, user As String, userRights As Integer) As String

        Dim parts As Collections.Specialized.NameValueCollection
        Dim PED As clsPlugExtraData
        Dim ReturnValue As Integer = Enums.ConfigDevicePostReturn.DoneAndCancelAndStay
        Dim dv As Scheduler.Classes.DeviceClass = Nothing

       

        Dim id_name As String = ""
        Dim id_length As Integer = -1
        Dim id As String = "" '' id of postbackcall



        parts = HttpUtility.ParseQueryString(data)

        Try

            id = parts("id")
            id_length = id.Length

            id_index = Mid(id, id.Length - 1, 2)
            id_name = Mid(id, 1, id.Length - 2)
        Catch

        End Try










        Select Case id_name
            ''ref=8415&plugin=BuienRadarGps&instance=&action=pluginpost&name1=&lon1=&lat1=&id=dvc_bsave1&save1=Submit
            'Case "brc_bsave"

            '    Me.divToUpdate.Add("geotext", "corona53 spijkenisse  nederland")
            '    Me.pageCommands.Add("executefunction", "geocode()")













            '    Files.changefilelocations(1, id_index, name, lon, lat)


            '    dv = hs.GetDeviceByRef(HsDevice.HsIoDvRef)
            '    PED = dv.PlugExtraData_Get(hs)


            Case "brc_bedit"


                Me.divToUpdate.Add("overlay", BuienRadarConfig.buildoverlay)


            Case "brc_bgraph"

                Me.pageCommands.Add("newpage", "buienradargps?index=" & id_index)

            Case "brc_cbmon"

                Dim monitor As String = ""

                If parts(0) = "checked" Then

                    monitor = "true"

                ElseIf parts(0) = "unchecked" Then

                    monitor = "false"

                End If

                Files.changefilelocations(3, id_index, "", "", "", "", "", 0, monitor)

                dv = hs.GetDeviceByRef(HsDevice.HsIoDvRef)
                PED = dv.PlugExtraData_Get(hs)
                Me.pageCommands.Add("refresh", "true")

        End Select

       Select parts("id")

        

            Case "brc_tbstreet"

                street = parts("street")

            Case "brc_tbcity"

                city = parts("city")

            Case "brc_tbcountry"

                country = parts("country")





            Case "button"
                'clsPageBuilder.DeviceUtilityPage.pageCommands.Add("newpage", "\JillesBuienRadarGps\testurl.html")                 ''RedirectPage("\JillesBuienRadarGps\testurl.html")
                ''Dim a As Scheduler.PageBuilderAndMenu.clsPageBuilder = DeviceUtilityPageBuilder.
                ''Scheduler.PageBuilderAndMenu.clsPageBuilder.RedirectPage("\JillesBuienRadarGps\testurl.html")
                'Scheduler.PageBuilderAndMenu.clsPageBuilder.DeviceUtilityPage.pageCommands.Add("newpage", "\JillesBuienRadarGps\testurl.html")




                ''gets coordinates address from geocode
            Case "brc_bcoordinates"

                Me.divToUpdate.Add("geotext", street & " " & city & " " & country)
                Me.pageCommands.Add("executefunction", "geocode()")




                '' saves overlay to device
            Case "brc_bsave"


                street = parts("street")
                city = parts("city")
                country = parts("country")
                lat = parts("latitude")
                lon = parts("longitude")


                Dim exists As Integer = Files.raindevicereref(id_index)

                If exists = -1 Then

                    Create_Hs3_Parent_Device(id_index, "rainmonitor ", street + " " + city)
                    BuienRadarGps.checkraincount = 0
                    Files.changefilelocations(1, id_index, street, city, country, lon, lat, HsDeviceExtended.HsDvParentRef, "")
                    BuienRadarGps.checkraincount = 0
                Else

                    dv = hs.GetDeviceByRef(exists)
                    dv.Name(hs) = street + " " + city
                    Dim childref As Integer = CInt(dv.AssociatedDevices_List(hs))
                    dv = hs.GetDeviceByRef(childref)
                    dv.Name(hs) = street + " " + city
                    Files.changefilelocations(1, id_index, street, city, country, lon, lat, exists, "")


                End If





                dv = hs.GetDeviceByRef(HsDevice.HsIoDvRef)
                PED = dv.PlugExtraData_Get(hs)
                Me.pageCommands.Add("refresh", "true")


            Case "brc_bdelete"


                street = parts("")
                city = parts("")
                country = parts("")
                lat = parts("")
                lon = parts("")




                Try

                    Dim s As Integer = Files.raindevicereref(id_index)

                    Dim device As Scheduler.Classes.DeviceClass = hs.GetDeviceByRef(s)

                    device.AssociatedDevice_ClearAll(hs)

                    hs.DeleteDevice(s)

                Catch ex As Exception

                    Dim test As Integer = 5

                End Try

                Files.changefilelocations(2, id_index, street, city, country, lon, lat, 0, "false")   '' after delete device hs otherwise no location available to query for devref

                dv = hs.GetDeviceByRef(HsDevice.HsIoDvRef)
                PED = dv.PlugExtraData_Get(hs)
                Me.pageCommands.Add("refresh", "true")





        End Select





        Return MyBase.postBackProc(page, data, user, userRights)
    End Function











    Public Function GetPagePlugin(ByVal pageName As String, ByVal user As String, ByVal userRights As Integer, ByVal queryString As String) As String
        Dim stb As New StringBuilder
        Dim instancetext As String = ""
        Try

            Me.reset()

            Me.AddHeader("  <script type='text/javascript' src='http://maps.google.com/maps/api/js?sensor=false'></script>")
            Me.AddHeader(" <script src='/js/buienradargps/Jilles.Geocode.js'></script>")
            ' handle any queries like mode=something
            Dim parts As Collections.Specialized.NameValueCollection = Nothing
            If (queryString <> "") Then
                parts = HttpUtility.ParseQueryString(queryString)
            End If
            If Instance <> "" Then instancetext = " - " & Instance
            stb.Append(hs.GetPageHeader(pageName, "BuienRadarGpsConfig" & instancetext, "", "", True, False))

            stb.Append(clsPageBuilder.DivStart("pluginpage", ""))

            ' a message area for error messages from jquery ajax postback (optional, only needed if using AJAX calls to get data)
            stb.Append(clsPageBuilder.DivStart("errormessage", "class='errormessage'"))
            stb.Append(clsPageBuilder.DivEnd)

            'Me.RefreshIntervalMilliSeconds = 3000
            'stb.Append(Me.AddAjaxHandlerPost("id=timer", pageName))

            ' specific page starts here
            stb.Append(clsPageBuilder.DivEnd)
            stb.Append(BuienRadarConfig.buildwebpage)



            ' add the body html to the page
            Me.AddBody(stb.ToString)

            ' return the full page
            Return Me.BuildPage()
        Catch ex As Exception
            'WriteMon("Error", "Building page: " & ex.Message)
            Return "error - " & Err.Description
        End Try
    End Function









End Class
