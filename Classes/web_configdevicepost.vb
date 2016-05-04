
Imports System.Text
Imports System.IO
Imports System.Threading
Imports System.Web
Imports System
Imports HomeSeerAPI
Imports Scheduler










Public Class web_configdevicepost

    Shared count As Integer
    Shared dv As Scheduler.Classes.DeviceClass = Nothing

    Shared PED As clsPlugExtraData

    Shared street As String
    Shared city As String
    Shared country As String
    Shared lon As String = ""
    Shared lat As String = ""
    Shared id_index As Integer = 1
    Shared hsref As Integer = -1

    Shared id_name As String = ""
    Shared id_length As Integer = -1
    Shared id As String = "" '' id of postbackcall

    Shared parts As Collections.Specialized.NameValueCollection


    Public Shared Function postconfig(ref As Integer, data As String, user As String, userRights As Integer) As Enums.ConfigDevicePostReturn




        parts = HttpUtility.ParseQueryString(data)

        Dim count As Integer = parts.Count



        Dim PED As clsPlugExtraData
        ''  Shared public ReturnValue As Integer = Enums.ConfigDevicePostReturn.DoneAndCancelAndStay
        Dim ReturnValue As Integer = Enums.ConfigDevicePostReturn.CallbackOnce



        '' choice between submit button and checkbox , textbox doesnt postback


        If parts.GetKey(26) = "id" Then  '' If  checkbox

            Dim v As String = parts(26)
            Dim k As String = parts.GetKey(26)

            Dim p As Integer = postcheckbox(ref, data, user, userRights)


            Dim gg As String = "test"

        ElseIf parts(26) = "Submit" Then  '' if button 

            Dim v1 As String = parts(26)

            Dim k1 As String = parts.GetKey(26)

            Dim p As Integer = postbutton(ref, data, user, userRights)

        End If














        Return ReturnValue



    End Function


    Shared Function postcheckbox(ref As Integer, data As String, user As String, userRights As Integer) As Integer

        Dim parts As Collections.Specialized.NameValueCollection

        parts = HttpUtility.ParseQueryString(data)

        Dim count As Integer = parts.Count

        Dim dv As Scheduler.Classes.DeviceClass = Nothing

        Dim PED As clsPlugExtraData



        Dim tempstring = ""

        Select Case "tempstring"

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
                ''  clsPageBuilder.DeviceUtilityPage.pageCommands.Add("refresh", "true")

        End Select


        Return 1
    End Function

    Shared Function postbutton(ref As Integer, data As String, user As String, userRights As Integer) As Integer

        Dim parts As Collections.Specialized.NameValueCollection

        parts = HttpUtility.ParseQueryString(data)



        Try

            id = parts.GetKey(26)

            If id = "coordinates" Then

            ElseIf id = "save" Then

            Else

                '' get name of button and index
                id_length = id.Length

                id_index = Mid(id, id.Length - 1, 2)
                id_name = Mid(id, 1, id.Length - 2)


            End If




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


            Case "edit"

                '' Scheduler.PageBuilderAndMenu.clsPageBuilder.clsKeyValue()

                ''     BuienRadarConfigPage.postBackProc("BuienRadarConfig", "jil data", "default", "6")

                '' Scheduler.DeviceUtilityPageBuilder.divToUpdate.Add("overlay", web_configdevice.buildoverlay)
                ''Scheduler.PageBuilderAndMenu.clsPageBuilder.DeviceUtilityPage.divToUpdate.Add("overlay123", web_configdevice.buildoverlay)
                ''.divToUpdate.Add("overlay", web_configdevice.buildoverlay)
                ''Scheduler.PageBuilderAndMenu.clsPageBuilder.DeviceUtilityPage.pageCommands.Add("newpage", "\JillesBuienRadarGps\testurl.html")
                '' clsPageBuilder.DeviceUtilityPage.divToUpdate.Add("overlay123", web_configdevice.buildoverlay)




        End Select

        Select Case parts("id")



            Case "brc_tbstreet"

                street = parts("street")

            Case "brc_tbcity"

                city = parts("city")

            Case "brc_tbcountry"

                country = parts("country")





            Case "button"
                '' clsPageBuilder.DeviceUtilityPage.pageCommands.Add("newpage", "\JillesBuienRadarGps\testurl.html")                 ''RedirectPage("\JillesBuienRadarGps\testurl.html")
                'Shared public a As Scheduler.PageBuilderAndMenu.clsPageBuilder = DeviceUtilityPageBuilder.
                'Scheduler.PageBuilderAndMenu.clsPageBuilder.RedirectPage("\JillesBuienRadarGps\testurl.html")
                ''   Scheduler.PageBuilderAndMenu.clsPageBuilder.DeviceUtilityPage.pageCommands.Add("newpage", "\JillesBuienRadarGps\testurl.html")




                ''gets coordinates address from geocode
            Case "brc_bcoordinates"

                ''    clsPageBuilder.DeviceUtilityPage.divToUpdate.Add("geotext", street & " " & city & " " & country)
                ''   clsPageBuilder.DeviceUtilityPage.pageCommands.Add("executefunction", "geocode()")



                '' saves overlay to device
            Case "brc_bsave"


                street = parts("street")
                city = parts("city")
                country = parts("country")
                lat = parts("latitude")
                lon = parts("longitude")

                Create_Hs3_Device(id_index, "rainmonitor", street + " " + city)

                Files.changefilelocations(1, id_index, street, city, country, lon, lat, HsDevice.HsDvRef, "")

                dv = hs.GetDeviceByRef(HsDevice.HsIoDvRef)
                PED = dv.PlugExtraData_Get(hs)
                ''    clsPageBuilder.DeviceUtilityPage.pageCommands.Add("refresh", "true")


        End Select


        Return 1

    End Function






End Class
