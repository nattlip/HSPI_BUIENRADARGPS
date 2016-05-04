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


Imports HSPI_BUIENRADARGPS


Module HsDevice

    Public NoDevice As Boolean = True '' indicating no iodevice so plugin hasnt run yet
    Public HsIoDvRef As Integer = -1
    Public HsDvRef As Integer = -1

    ''' <summary>
    ''' 'creates a  normal hs3 device in homeseer
    ''' </summary>
    ''' <param name="device_Type"></param>
    ''' <param name="device_Name"></param>
    ''' <returns>true if deviceref > 0 , false if not </returns>
    ''' <remarks></remarks>
    Friend Function Create_Hs3_Device(ByVal deviceindex As Integer, ByVal device_Type As String, ByVal device_Name As String) As Boolean



        Dim MyDeviceRef As Integer = -1
        Dim col As New Collections.Generic.List(Of Scheduler.Classes.DeviceClass)
        Dim dv As Scheduler.Classes.DeviceClass
        Dim Found As Boolean = False

        Try
            Dim EN As Scheduler.Classes.clsDeviceEnumeration
            EN = hs.GetDeviceEnumerator
            If EN Is Nothing Then Throw New Exception(IFACE_NAME & " failed to get a device enumerator from HomeSeer.")
            Do
                dv = EN.GetNext
                If dv Is Nothing Then Continue Do
                If dv.Interface(Nothing) IsNot Nothing Then
                    If dv.Interface(Nothing).Trim = IFACE_NAME Then
                        col.Add(dv)
                    End If
                End If
            Loop Until EN.Finished
        Catch ex As Exception
            hs.WriteLog(IFACE_NAME & " Error", "Exception in Find_Create_Devices/Enumerator: " & ex.Message)
        End Try

        Try
            If col IsNot Nothing AndAlso col.Count > 0 Then
                For Each dv In col
                    If dv Is Nothing Then Continue For
                    If dv.DeviceType_Get(Nothing) IsNot Nothing Then
                        If dv.DeviceType_Get(Nothing).Device_Type = 70 Then
                            'Found = True
                            MyDeviceRef = dv.Ref(hs)

                            ' Now (mostly for demonstration purposes) - work with the PlugExtraData object.
                            Dim EDO As HomeSeerAPI.clsPlugExtraData = Nothing
                            EDO = dv.PlugExtraData_Get(hs)
                            If EDO IsNot Nothing Then
                                Dim obj As Object = Nothing
                                Dim obj2 As Object = Nothing

                                obj2 = EDO.GetNamed("size")
                                If obj2 IsNot Nothing Then
                                    Log("Plug-In Extra Data Object Retrieved: Size= " & obj2.ToString, LogLevel.Normal)
                                End If

                                If obj2 Is Nothing Then

                                    If Not EDO.AddNamed("size", True) Then
                                        Log("Error adding named data object Size to plug-in sample device!", LogLevel.Normal)
                                        Exit For
                                    End If
                                    dv.PlugExtraData_Set(hs) = EDO
                                    hs.SaveEventsDevices()

                                End If



                                obj = EDO.GetNamed("My Special Object")

                                If obj IsNot Nothing Then
                                    Log("Plug-In Extra Data Object Retrieved = " & obj.ToString, LogLevel.Normal)
                                End If
                                obj = EDO.GetNamed("My Count")
                                Dim MC As Integer = 1
                                If obj Is Nothing Then
                                    If Not EDO.AddNamed("My Count", MC) Then
                                        Log("Error adding named data object to plug-in sample device!", LogLevel.Normal)
                                        Exit For
                                    End If
                                    dv.PlugExtraData_Set(hs) = EDO
                                    hs.SaveEventsDevices()
                                Else
                                    Try
                                        MC = CInt(obj)
                                    Catch ex As Exception
                                        MC = -1
                                    End Try
                                    If MC < 0 Then Exit For
                                    Log("Retrieved count from plug-in sample device is: " & MC.ToString, LogLevel.Normal)
                                    MC += 1
                                    ' Now put it back - need to remove the old one first.
                                    EDO.RemoveNamed("My Count")
                                    EDO.AddNamed("My Count", MC)
                                    dv.PlugExtraData_Set(hs) = EDO
                                    hs.SaveEventsDevices()
                                End If
                            End If


                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            hs.WriteLog(IFACE_NAME & " Error", "Exception in Find_Create_Devices/Find: " & ex.Message)
        End Try

        Try  '' to create a device
            If Not Found Then
                Dim ref As Integer
                ref = hs.NewDeviceRef(device_Name)
                HsDvRef = ref
                If ref > 0 Then
                    MyDeviceRef = ref
                    dv = hs.GetDeviceByRef(ref)
                    Dim disp(1) As String
                    disp(0) = "This is a test of the"
                    disp(1) = "Emergency Broadcast Display Data system"
                    dv.AdditionalDisplayData(hs) = disp
                    dv.Address(hs) = "BuienRadarGps " & deviceindex
                    'dv.Can_Dim(hs) = True
                    dv.Device_Type_String(hs) = device_Type


                    Dim DT As New DeviceTypeInfo


                    DT.Device_API = DeviceTypeInfo.eDeviceAPI.Plug_In
                    DT.Device_Type = 71 '' DeviceTypeInfo.eDeviceAPI.Plug_In   somfy devoce
                    DT.Device_SubType = 1
                    Dim obj As Object = DT.Device_Type_Description
                    dv.DeviceType_Set(hs) = DT
                    dv.Interface(hs) = IFACE_NAME
                    dv.InterfaceInstance(hs) = ""
                    dv.Last_Change(hs) = #5/21/1929 11:00:00 AM#
                    dv.Location(hs) = IFACE_NAME
                    dv.Location2(hs) = "Rain"

                    Dim EDO As New HomeSeerAPI.clsPlugExtraData
                    dv.PlugExtraData_Set(hs) = EDO
                    ' Now just for grins, let's modify it.
                    Dim HW As String = "Hello World"
                    If EDO.GetNamed("My Special Object") IsNot Nothing Then
                        EDO.RemoveNamed("My Special Object")
                    End If
                    EDO.AddNamed("My Special Object", HW)
                    ' Need to re-save it.

                    Dim size As Boolean

                    If EDO.GetNamed("size") IsNot Nothing Then

                        EDO.RemoveNamed("size")

                    End If

                    EDO.AddNamed("size", True)  ' graph is mall

                    dv.PlugExtraData_Set(hs) = EDO








                    ' add an Up button and value
                    Dim Pair As VSPair
                    Pair = New VSPair(HomeSeerAPI.ePairStatusControl.Both)
                    Pair.PairType = VSVGPairType.SingleValue
                    'Pair.ProtectionSet = ePairProtection.Do_Not_Delete
                    Pair.Value = 100
                    Pair.Status = "Big"
                    Pair.Render = Enums.CAPIControlType.Button
                    hs.DeviceVSP_AddPair(ref, Pair)




                    ' add an Down button and value
                    Pair = New VSPair(HomeSeerAPI.ePairStatusControl.Both)
                    Pair.PairType = VSVGPairType.SingleValue
                    Pair.Value = 0
                    Pair.Status = "Small"
                    Pair.Render = Enums.CAPIControlType.Button
                    hs.DeviceVSP_AddPair(ref, Pair)


                    Pair = New VSPair(HomeSeerAPI.ePairStatusControl.Status)
                    Pair.PairType = VSVGPairType.Range
                    Pair.RangeStart = 1
                    Pair.RangeEnd = 25
                    '' Pair.RangeStatusPrefix = ""

                    hs.DeviceVSP_AddPair(ref, Pair)





                    'Dim pstr As New StringBuilder

                    'pstr.Clear()

                    'pstr.Append("<div class='Rollershutter'><div class='Down'></div></div><script type='text/javascript'>")
                    'pstr.Append("innerboxdown(); function innerboxdown() { ")
                    'pstr.Append("if (parseInt($('.Down').css('top')) < -12 ) {")

                    'pstr.Append("$('.Down').animate({ top: '+=32' }, 10000, 'swing', function () { }); } }</script>")
                    'Pair.Status = pstr.ToString




                    'place images  \HTML\Images\(appname)

                    'dv.graphics = "Jilles/rollershutterup.gif" & Chr(2) & "0" & Chr(1) & "Jilles/favorite.gif" & Chr(2) & "50" & Chr(1) & "Jilles/rollershutterdown.gif" & Chr(2) & "100" & Chr(1) & "Jilles/stop.gif" & Chr(2) & "25"

                    Dim vgPair As VGPair

                    vgPair = New VGPair


                    ''vgPair.Graphic = "/images/JillesSomfy/rollershutterup.gif"
                    'vgPair.Graphic = "/images/JillesBuienRadarGps/rain-icon.png"
                    'vgPair.Set_Value = "100"
                    'vgPair.PairType = VSVGPairType.SingleValue
                    ''vgPair.ProtectionSet = ePairProtection.Do_Not_Delete


                    'hs.DeviceVGP_AddPair(ref, vgPair)

                    'vgPair = New VGPair


                    ''vgPair.Graphic = "/images/JillesSomfy/rollershutterdown.gif"
                    'vgPair.Graphic = "/images/JillesBuienRadarGps/partly-cloudy-day-icon.png"
                    'vgPair.Set_Value = "0"
                    'vgPair.PairType = VSVGPairType.SingleValue
                    ''vgPair.ProtectionSet = ePairProtection.Do_Not_Delete


                    'hs.DeviceVGP_AddPair(ref, vgPair)






                    '' add DIM values
                    'hs.DeviceVSP_AddRange(ref, "Dim ", 1, 99, ePairStatusControl.Both, True, 0, "", "%", True, 0)
                    dv.MISC_Set(hs, Enums.dvMISC.SHOW_VALUES)
                    dv.MISC_Set(hs, Enums.dvMISC.NO_LOG)
                    ''dv.MISC_Set(hs, Enums.dvMISC.CONTROL_POPUP)
                    '' dv.MISC_Set(hs, Enums.dvMISC.CONTROL_POPUP)      ' set this for a status only device, no controls, and do not include the DeviceVSP calls above
                    '' dv.MISC_Set(hs, Enums.dvMISC.NO_STATUS_TRIGGER)  '' if this is activated no valuechange of device is processed



                    Dim PED As clsPlugExtraData = dv.PlugExtraData_Get(hs)

                    If PED Is Nothing Then PED = New clsPlugExtraData
                    PED.AddNamed("Index", "1")
                    dv.PlugExtraData_Set(hs) = PED
                    dv.Status_Support(hs) = True
                    dv.UserNote(hs) = "This is my user note - how do you like it? This device is version " & dv.Version.ToString
                    ''  Execute.setdevicestring(ref, "Up")  ' this will override the name/value pairs
                End If
                hs.SaveEventsDevices()



            End If
        Catch ex As Exception
            hs.WriteLog(IFACE_NAME & " Error", "Exception in Find_Create_Devices/Create: " & ex.Message)



        End Try

    End Function

    'Friend Function Create_Virtual_Somfy_Device(ByVal device_Type As String, ByVal device_Name As String, index As Integer) As Boolean

    '    Dim col As New Collections.Generic.List(Of Scheduler.Classes.DeviceClass)
    '    Dim dv As Scheduler.Classes.DeviceClass
    '    Dim Found As Boolean = False

    '    Try
    '        Dim EN As Scheduler.Classes.clsDeviceEnumeration
    '        EN = hs.GetDeviceEnumerator
    '        If EN Is Nothing Then Throw New Exception(IFACE_NAME & " failed to get a device enumerator from HomeSeer.")
    '        Do
    '            dv = EN.GetNext
    '            If dv Is Nothing Then Continue Do
    '            If dv.Interface(Nothing) IsNot Nothing Then
    '                If dv.Interface(Nothing).Trim = IFACE_NAME Then
    '                    col.Add(dv)
    '                End If
    '            End If
    '        Loop Until EN.Finished
    '    Catch ex As Exception
    '        hs.WriteLog(IFACE_NAME & " Error", "Exception in Find_Create_Devices/Enumerator: " & ex.Message)
    '    End Try

    '    'Try
    '    '    If col IsNot Nothing AndAlso col.Count > 0 Then
    '    '        For Each dv In col
    '    '            If dv Is Nothing Then Continue For
    '    '            If dv.DeviceType_Get(Nothing) IsNot Nothing Then
    '    '                If dv.DeviceType_Get(Nothing).Device_Type = 70 Then
    '    '                    'Found = True
    '    '                    MyDevice = dv.Ref(hs)

    '    '                    ' Now (mostly for demonstration purposes) - work with the PlugExtraData object.
    '    '                    Dim EDO As HomeSeerAPI.clsPlugExtraData = Nothing
    '    '                    EDO = dv.PlugExtraData_Get(hs)
    '    '                    If EDO IsNot Nothing Then
    '    '                        Dim obj As Object = Nothing
    '    '                        obj = EDO.GetNamed("My Special Object")
    '    '                        If obj IsNot Nothing Then
    '    '                            Log("Plug-In Extra Data Object Retrieved = " & obj.ToString, LogType.LOG_TYPE_INFO)
    '    '                        End If
    '    '                        obj = EDO.GetNamed("My Count")
    '    '                        Dim MC As Integer = 1
    '    '                        If obj Is Nothing Then
    '    '                            If Not EDO.AddNamed("My Count", MC) Then
    '    '                                Log("Error adding named data object to plug-in sample device!", LogType.LOG_TYPE_ERROR)
    '    '                                Exit For
    '    '                            End If
    '    '                            dv.PlugExtraData_Set(hs) = EDO
    '    '                            hs.SaveEventsDevices()
    '    '                        Else
    '    '                            Try
    '    '                                MC = CInt(obj)
    '    '                            Catch ex As Exception
    '    '                                MC = -1
    '    '                            End Try
    '    '                            If MC < 0 Then Exit For
    '    '                            Log("Retrieved count from plug-in sample device is: " & MC.ToString, LogType.LOG_TYPE_INFO)
    '    '                            MC += 1
    '    '                            ' Now put it back - need to remove the old one first.
    '    '                            EDO.RemoveNamed("My Count")
    '    '                            EDO.AddNamed("My Count", MC)
    '    '                            dv.PlugExtraData_Set(hs) = EDO
    '    '                            hs.SaveEventsDevices()
    '    '                        End If
    '    '                    End If


    '    '                End If
    '    '            End If
    '    '        Next
    '    '    End If
    '    'Catch ex As Exception
    '    '    hs.WriteLog(IFACE_NAME & " Error", "Exception in Find_Create_Devices/Find: " & ex.Message)
    '    'End Try

    '    Try
    '        If Not Found Then
    '            Dim ref As Integer
    '            ref = hs.NewDeviceRef(device_Name)
    '            If ref > 0 Then
    '                MyDevice = ref
    '                dv = hs.GetDeviceByRef(ref)
    '                Dim disp(1) As String
    '                disp(0) = "This is a test of the"
    '                disp(1) = "Emergency Broadcast Display Data system"
    '                dv.AdditionalDisplayData(hs) = disp
    '                dv.Address(hs) = "SOMFYTRANS_00_V" & index.ToString
    '                dv.Device_Type_String(hs) = device_Type


    '                Dim DT As New DeviceTypeInfo


    '                DT.Device_API = DeviceTypeInfo.eDeviceAPI.Plug_In
    '                DT.Device_Type = 72 '' virtual somfy device
    '                DT.Device_SubType = 1
    '                Dim obj As Object = DT.Device_Type_Description
    '                dv.DeviceType_Set(hs) = DT
    '                dv.Interface(hs) = IFACE_NAME
    '                dv.InterfaceInstance(hs) = ""
    '                dv.Last_Change(hs) = #5/21/1929 11:00:00 AM#
    '                dv.Location(hs) = IFACE_NAME
    '                dv.Location2(hs) = "Sample Devices"

    '                Dim EDO As New HomeSeerAPI.clsPlugExtraData
    '                dv.PlugExtraData_Set(hs) = EDO
    '                ' Now just for grins, let's modify it.
    '                Dim HW As String = "Hello World"
    '                If EDO.GetNamed("My Special Object") IsNot Nothing Then
    '                    EDO.RemoveNamed("My Special Object")
    '                End If
    '                EDO.AddNamed("My Special Object", HW)
    '                ' Need to re-save it.
    '                dv.PlugExtraData_Set(hs) = EDO

    '                ' add an Up button and value
    '                Dim Pair As VSPair
    '                Pair = New VSPair(HomeSeerAPI.ePairStatusControl.Both)
    '                Pair.PairType = VSVGPairType.SingleValue
    '                'Pair.ProtectionSet = ePairProtection.Do_Not_Delete
    '                Pair.Value = 100
    '                Pair.Status = "Up"
    '                Pair.Render = Enums.CAPIControlType.Button
    '                hs.DeviceVSP_AddPair(ref, Pair)

    '                ' add an Down button and value
    '                Pair = New VSPair(HomeSeerAPI.ePairStatusControl.Both)
    '                Pair.PairType = VSVGPairType.SingleValue
    '                Pair.Value = 0



    '                'Dim pstr As New StringBuilder

    '                'pstr.Clear()

    '                'pstr.Append("<div class='Rollershutter'><div class='Down'></div></div><script type='text/javascript'>")
    '                'pstr.Append("innerboxdown(); function innerboxdown() { ")
    '                'pstr.Append("if (parseInt($('.Down').css('top')) < -12 ) {")

    '                'pstr.Append("$('.Down').animate({ top: '+=32' }, 10000, 'swing', function () { }); } }</script>")
    '                'Pair.Status = pstr.ToString



    '                Pair.Status = "Down"
    '                Pair.Render = Enums.CAPIControlType.Button




    '                hs.DeviceVSP_AddPair(ref, Pair)




    '                'place images  \HTML\Images\(appname)

    '                'dv.graphics = "Jilles/rollershutterup.gif" & Chr(2) & "0" & Chr(1) & "Jilles/favorite.gif" & Chr(2) & "50" & Chr(1) & "Jilles/rollershutterdown.gif" & Chr(2) & "100" & Chr(1) & "Jilles/stop.gif" & Chr(2) & "25"

    '                Dim vgPair As VGPair

    '                vgPair = New VGPair


    '                'vgPair.Graphic = "/images/JillesSomfy/rollershutterup.gif"
    '                vgPair.Graphic = "/images/JillesSomfy/rollersshutterup.gif"
    '                vgPair.Set_Value = "100"
    '                vgPair.PairType = VSVGPairType.SingleValue
    '                'vgPair.ProtectionSet = ePairProtection.Do_Not_Delete


    '                'hs.DeviceVGP_AddPair(ref, vgPair)

    '                vgPair = New VGPair


    '                'vgPair.Graphic = "/images/JillesSomfy/rollershutterdown.gif"
    '                vgPair.Graphic = "/images/JillesSomfy/rollersshutterdown.gif"
    '                vgPair.Set_Value = "0"
    '                vgPair.PairType = VSVGPairType.SingleValue
    '                'vgPair.ProtectionSet = ePairProtection.Do_Not_Delete


    '                'hs.DeviceVGP_AddPair(ref, vgPair)






    '                '' add DIM values
    '                'hs.DeviceVSP_AddRange(ref, "Dim ", 1, 99, ePairStatusControl.Both, True, 0, "", "%", True, 0)
    '                dv.MISC_Set(hs, Enums.dvMISC.SHOW_VALUES)
    '                dv.MISC_Set(hs, Enums.dvMISC.NO_LOG)
    '                ''dv.MISC_Set(hs, Enums.dvMISC.STATUS_ONLY)      ' set this for a status only device, no controls, and do not include the DeviceVSP calls above

    '                Dim PED As clsPlugExtraData = dv.PlugExtraData_Get(hs)

    '                If PED Is Nothing Then PED = New clsPlugExtraData
    '                PED.AddNamed("Index", index)
    '                dv.PlugExtraData_Set(hs) = PED
    '                dv.Status_Support(hs) = True
    '                dv.UserNote(hs) = "This is my user note - how do you like it? This device is version " & dv.Version.ToString
    '                Execute.setdevicestring(ref, "Up")
    '            End If
    '            hs.SaveEventsDevices()

    '            Files.ChangeFileVirualInstalledDevices(index, "Virtual Somfy Device", device_Name, ref, 4)


    '        End If
    '    Catch ex As Exception
    '        hs.WriteLog(IFACE_NAME & " Error", "Exception in Find_Create_Devices/Create: " & ex.Message)



    '    End Try

    'End Function







    Friend Sub Create_Hs3_IO_Device(Optional ByVal plugin_name As String = IFACE_NAME, Optional ByVal instance_name As String = "", _
                                Optional ByVal Hs3_io_device_type As String = "BuienRadarGps")


        Dim device_name As String = IFACE_NAME & "Device"
        Dim device_type As String = IFACE_NAME & "IO Device"
        Dim MyDevice As Integer = -1
        Dim colio As New Collections.Generic.List(Of Scheduler.Classes.DeviceClass) '' collection of io devices ie rts485 transmitter
        Dim dv As Scheduler.Classes.DeviceClass
        Dim Found As Boolean = False
        Dim EN As Object ''Scheduler.Classes.clsDeviceEnumeration
        Log("HSPI Started", LogLevel.Normal)


        Try
            ''Dim EN As Scheduler.Classes.clsDeviceEnumeration
         If hs IsNot Nothing Then
                Try
                    EN = hs.GetDeviceEnumerator

                Catch ex As Exception

                End Try

            End If

            EN = hs.GetDeviceEnumerator()
            If EN Is Nothing Then Throw New Exception(IFACE_NAME & " failed to get a device enumerator from HomeSeer.")
            Do
                dv = EN.GetNext
                If dv Is Nothing Then Continue Do
                If dv.Interface(Nothing) IsNot Nothing Then

                    If dv.Interface(Nothing).Trim = IFACE_NAME Then

                        If dv.InterfaceInstance(hs) = Instance Then

                            If dv.DeviceType_Get(Nothing).Device_Type = 72 Then '' ie io device in my plugin 


                                colio.Add(dv)

                            End If

                        End If

                    End If
                End If
            Loop Until EN.Finished
        Catch ex As Exception
            '' hs.WriteLog(IFACE_NAME & " Error", "Exception in Find_Create_Devices/Enumerator: " & ex.Message)
        End Try

        Try
            If colio IsNot Nothing AndAlso colio.Count > 0 Then
                For Each dv In colio


                    '' if no io devoce exists
                    If dv Is Nothing Then Continue For
                    If dv.DeviceType_Get(Nothing) IsNot Nothing Then
                        If dv.DeviceType_Get(Nothing).Device_Type = 72 Then
                            Found = True
                            NoDevice = False  '' flag if xdoxs should be created in files
                            MyDevice = dv.Ref(hs)
                            HsIoDvRef = dv.Ref(hs)





                            ' Now (mostly for demonstration purposes) - work with the PlugExtraData object.
                            Dim EDO As HomeSeerAPI.clsPlugExtraData = Nothing
                            EDO = dv.PlugExtraData_Get(hs)
                            If EDO IsNot Nothing Then

                                Files.CreateXdocuments(True)

                                Dim oLocationsXdoc As Object = EDO.GetNamed("Locations")

                                Files.LocationsXdoc = XDocument.Parse(oLocationsXdoc)

                            




                                        If oLocationsXdoc IsNot Nothing Then

                                            Log("Plug-In Extra Data Object Retrieved = " & oLocationsXdoc.ToString, LogLevel.Normal)


                                        End If


                                        ''  CheckDeviceValueIO()

                                        Dim obj As Object
                                        obj = EDO.GetNamed("My Count")
                                        Dim MC As Integer = 1
                                        If obj Is Nothing Then
                                            If Not EDO.AddNamed("My Count", MC) Then
                                                Log("Error adding named data object to plug-in sample device!", LogLevel.Normal)
                                                Exit For
                                            End If
                                            dv.PlugExtraData_Set(hs) = EDO
                                            hs.SaveEventsDevices()
                                        Else
                                            Try
                                                MC = CInt(obj)
                                            Catch ex As Exception
                                                MC = -1
                                            End Try
                                            If MC < 0 Then Exit For
                                            Log("Retrieved count from plug-in sample device is: " & MC.ToString, LogLevel.Normal)
                                            MC += 1
                                            ' Now put it back - need to remove the old one first.
                                            EDO.RemoveNamed("My Count")
                                            EDO.AddNamed("My Count", MC)
                                            dv.PlugExtraData_Set(hs) = EDO
                                            hs.SaveEventsDevices()
                                        End If
                            End If


                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            hs.WriteLog(IFACE_NAME & " Error", "Exception in Find_Create_Devices/Find: " & ex.Message)
        End Try

        Try                                             ''  to create a io device
            If Not Found Then
                Dim ref As Integer
                ref = hs.NewDeviceRef(device_name)
                If ref > 0 Then
                    MyDevice = ref
                    HsIoDvRef = ref
                    dv = hs.GetDeviceByRef(ref)
                    Dim disp(1) As String
                    disp(0) = "This is the IO Device of BuienradarGps"
                    disp(1) = "Holds in EDO the devices and checks connectivity"
                    dv.AdditionalDisplayData(hs) = disp
                    dv.Address(hs) = "BuRarGps_IO_0"
                    'dv.Can_Dim(hs) = True
                    dv.Device_Type_String(hs) = "IO panel"


                    Dim DT As New DeviceTypeInfo

                    DT.Device_API = DeviceTypeInfo.eDeviceAPI.Plug_In
                    DT.Device_Type = 72 '' DeviceTypeInfo.eDeviceAPI.Plug_In  BuienRadarGps io device
                    DT.Device_SubType = 1
                    Dim obj As Object = DT.Device_Type_Description
                    dv.DeviceType_Set(hs) = DT
                    dv.Interface(hs) = IFACE_NAME


                    dv.InterfaceInstance(hs) = instance_name
                    dv.Last_Change(hs) = Date.Now
                    dv.Location(hs) = IFACE_NAME
                    dv.Location2(hs) = "Buiten"

                    'Dim EDO As New HomeSeerAPI.clsPlugExtraData
                    'dv.PlugExtraData_Set(hs) = EDO
                    '' Now just for grins, let's modify it.
                    'Dim HW As String = "Hello World"
                    'If EDO.GetNamed("My Special Object") IsNot Nothing Then
                    '    EDO.RemoveNamed("My Special Object")
                    'End If
                    'EDO.AddNamed("My Special Object", HW)
                    '' Need to re-save it.
                    'dv.PlugExtraData_Set(hs) = EDO



                    'place images  \HTML\Images\(appname)

                    'dv.graphics = "Jilles/rollershutterup.gif" & Chr(2) & "0" & Chr(1) & "Jilles/favorite.gif" & Chr(2) & "50" & Chr(1) & "Jilles/rollershutterdown.gif" & Chr(2) & "100" & Chr(1) & "Jilles/stop.gif" & Chr(2) & "25"
                    Dim Pair As VSPair
                    Pair = New VSPair(HomeSeerAPI.ePairStatusControl.Status)
                    Pair.PairType = VSVGPairType.SingleValue
                    'Pair.ProtectionSet = ePairProtection.Do_Not_Delete
                    Pair.Value = 100
                    Pair.Status = "Connected"
                    Pair.Render = Enums.CAPIControlType.Button
                    hs.DeviceVSP_AddPair(ref, Pair)


                    ' add an OFF button and value
                    Pair = New VSPair(HomeSeerAPI.ePairStatusControl.Status)
                    Pair.PairType = VSVGPairType.SingleValue
                    'Pair.ProtectionSet = ePairProtection.Do_Not_Delete
                    Pair.Value = 0
                    Pair.Status = "DisConnected"
                    Pair.Render = Enums.CAPIControlType.Button
                    hs.DeviceVSP_AddPair(ref, Pair)














                    Dim vgPair As VGPair

                    vgPair = New VGPair


                    vgPair.Graphic = "/images/BuienRadarGps/On-Line.gif"
                    vgPair.Set_Value = "100"
                    vgPair.PairType = VSVGPairType.SingleValue
                    'vgPair.ProtectionSet = ePairProtection.Do_Not_Delete


                    hs.DeviceVGP_AddPair(ref, vgPair)

                    vgPair = New VGPair


                    vgPair.Graphic = "/images/BuienRadarGps/Off-Line.gif"
                    vgPair.Set_Value = "0"
                    vgPair.PairType = VSVGPairType.SingleValue
                    'vgPair.ProtectionSet = ePairProtection.Do_Not_Delete


                    hs.DeviceVGP_AddPair(ref, vgPair)


                    hs.SetDeviceValue("BuienRadarGps", 0)



                    '' add DIM values
                    ''hs.DeviceVSP_AddRange(ref, "Dim ", 1, 99, ePairStatusControl.Both, True, 0, "", "%", True, 0)
                    dv.MISC_Set(hs, Enums.dvMISC.SHOW_VALUES)
                    dv.MISC_Set(hs, Enums.dvMISC.NO_LOG)
                    dv.MISC_Set(hs, Enums.dvMISC.STATUS_ONLY)
                    dv.MISC_Set(hs, Enums.dvMISC.NO_STATUS_TRIGGER)

                    ' deletes the buttons and the extra plugin config page
                    ''dv.MISC_Set(hs, Enums.dvMISC.STATUS_ONLY)      ' set this for a status only device, no controls, and do not include the DeviceVSP calls above
                    Dim PED As clsPlugExtraData = dv.PlugExtraData_Get(hs)
                    If PED Is Nothing Then PED = New clsPlugExtraData

                    If NoDevice = True Then

                        Files.CreateXdocuments(False)


                    End If





                    Dim sLocationsXdoc As String = Files.LocationsXdoc.ToString
                   



                    PED.AddNamed("Locations", sLocationsXdoc)   ''the old rts485settings.xml file
              


                    dv.PlugExtraData_Set(hs) = PED
                    dv.Status_Support(hs) = False
                    dv.UserNote(hs) = "This is my user note - how do you like it? This device is version " & dv.Version.ToString
                    'hs.SetDeviceString(ref, "Not Set", False)  ' this will override the name/value pairs
                End If
                hs.SaveEventsDevices()


            End If
        Catch ex As Exception
            hs.WriteLog(IFACE_NAME & " Error", "Exception in Find_Create_Devices/Create: " & ex.Message)
        End Try

    End Sub

    Public Sub CheckDeviceValueIO(ByVal connected As Boolean)



        If connected = True Then

            hs.SetDeviceValueByRef(HsIoDvRef, 100, True)

        Else

            hs.SetDeviceValueByRef(HsIoDvRef, 0, True)

        End If

        hs.SetDeviceLastChange(HsIoDvRef, Now)


    End Sub



    ''' <summary>
    ''' 'change the size of the grapic form small to big and vice versa : small = true
    ''' </summary>
    ''' <param name="ref"></param>
    ''' <param name="size"></param>
    ''' <remarks></remarks>
    Sub ChangeNamedEdoHsDevice(ByVal ref As Integer, ByRef size As Boolean)

        Dim name As String = "size"

        Dim dv As Scheduler.Classes.DeviceClass
        Dim EDO As New HomeSeerAPI.clsPlugExtraData


        '' first getdevice
        dv = hs.GetDeviceByRef(ref)


        '' get and change edo 


        dv.PlugExtraData_Set(hs) = EDO


        If EDO.GetNamed(name) IsNot Nothing Then
            EDO.RemoveNamed(name)
        End If
        EDO.AddNamed(name, size)
        ' Need to re-save it.
        dv.PlugExtraData_Set(hs) = EDO





    End Sub





    Function GetNamedEdoHsDevice(ByVal ref As Integer, ByRef name As String) As String





        Dim dv As Scheduler.Classes.DeviceClass
        Dim EDO As New HomeSeerAPI.clsPlugExtraData


        '' first getdevice
        dv = hs.GetDeviceByRef(ref)


        '' get and change edo 


        EDO = dv.PlugExtraData_Get(hs)


        If EDO.GetNamed(name) IsNot Nothing Then

            Return EDO.GetNamed(name)

        Else

            Return "No Edo with name " & name

        End If





    End Function





End Module
