Imports System.Runtime.CompilerServices
Imports HomeSeerAPI






Module HsDeviceExtended
    Public HsDvParentRef As Integer = -1
    Public HsDvChildRef As Integer = -1

    Friend Function Create_Hs3_Parent_Device(ByVal deviceindex As Integer, ByVal device_Type As String, ByVal device_Name As String) As Boolean

        Dim Found As Boolean = False
        Dim dv As Scheduler.Classes.DeviceClass
      

      
   
        Try  '' to create a device
            If Not Found Then
                Dim ref As Integer
                ref = hs.NewDeviceRef(device_Name)
                HsDvParentRef = ref
                If ref > 0 Then
                    dv = hs.GetDeviceByRef(ref)
                    Dim disp(1) As String
                    disp(0) = "This is a Rain Parent Device"
                    disp(1) = "DeviceGraphic displays Rain Graph"
                    dv.AdditionalDisplayData(hs) = disp
                    dv.Address(hs) = "BuRaGps " & deviceindex & "_1"
                    dv.Device_Type_String(hs) = device_Type & "graph"
                    dv.Relationship(hs) = Enums.eRelationship.Parent_Root

                    Dim DT As New DeviceTypeInfo
                    DT.Device_API = DeviceTypeInfo.eDeviceAPI.Plug_In
                    DT.Device_Type = DeviceTypeInfo.eDeviceType_Plugin.Root '' DeviceTypeInfo.eDeviceAPI.Plug_In   somfy devoce
                    DT.Device_SubType = 1
                    Dim obj As Object = DT.Device_Type_Description
                    dv.DeviceType_Set(hs) = DT
                    dv.Interface(hs) = IFACE_NAME
                    dv.InterfaceInstance(hs) = ""
                    dv.Last_Change(hs) = Now
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

                    EDO.AddNamed("size", size)  ' graph is mall

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

                Create_Hs3_child_Device(HsDvParentRef, deviceindex, device_Type, device_Name)

            End If
        Catch ex As Exception
            hs.WriteLog(IFACE_NAME & " Error", "Exception in Find_Create_Devices/Create: " & ex.Message)



        End Try

    End Function

    Function GetDeviceRefChildFromParent(ByVal ParentDevRef As Integer) As Integer


        Dim dv As Scheduler.Classes.DeviceClass
        dv = hs.GetDeviceByRef(ParentDevRef)
        Dim childref As Integer = CInt(dv.AssociatedDevices_List(hs))

        Return childref

    End Function
























    Friend Function Create_Hs3_child_Device(ByVal RefParent As Integer, ByVal DeviceIndexParent As Integer, ByVal device_Type As String, ByVal device_Name As String) As Boolean





        Dim dv, DvParent As Scheduler.Classes.DeviceClass
        Dim Found As Boolean = False


        Try  '' to create a device
            If Not Found Then
                Dim ref As Integer
                ref = hs.NewDeviceRef(device_Name)
                HsDvChildRef = ref
                If ref > 0 Then
                    dv = hs.GetDeviceByRef(ref)
                    Dim disp(1) As String
                    disp(0) = "This is a Rain Child Device"
                    disp(1) = "DeviceValue Displays Rain paramaters"
                    dv.AdditionalDisplayData(hs) = disp
                    dv.Address(hs) = "BuRaGps " & DeviceIndexParent & "_2"
                    'dv.Can_Dim(hs) = True
                    dv.Device_Type_String(hs) = device_Type & "parameters"

                    dv.Relationship(hs) = Enums.eRelationship.Child
                    dv.AssociatedDevice_ClearAll(hs)  ' There can be only one parent, so make sure by wiping these out.
                    dv.AssociatedDevice_Add(hs, RefParent)

                    '' associate parent with child

                    DvParent = hs.GetDeviceByRef(RefParent)

                    If DvParent.AssociatedDevices_Count(hs) < 1 Then
                        ' There are none added, so it is OK to add this one.
                        DvParent.AssociatedDevice_Add(hs, HsDvChildRef)
                    End If

                    Dim DT As New DeviceTypeInfo

                    DT.Device_API = DeviceTypeInfo.eDeviceAPI.Plug_In
                    DT.Device_Type = 71
                    DT.Device_SubType = 2

                    Dim obj As Object = DT.Device_Type_Description
                    dv.DeviceType_Set(hs) = DT
                    dv.Interface(hs) = IFACE_NAME
                    dv.InterfaceInstance(hs) = ""
                    dv.Last_Change(hs) = Now
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




                    '' clsValueRange()   





                    Dim pair As VSPair

                    '' add range for rainexpected
                    pair = New VSPair(ePairStatusControl.Status)
                    pair.PairType = VSVGPairType.Range
                    pair.RangeStart = 0
                    pair.RangeEnd = 25
                    pair.RangeStatusPrefix = "Rain in  "
                    pair.RangeStatusSuffix = ""
                    hs.DeviceVSP_AddPair(ref, pair)











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























End Module
