

Imports System.Xml
Imports System.Xml.Linq
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.IO


Imports HomeSeerAPI
Imports Scheduler
Imports System.Reflection
Imports System.Text

Imports HSPI_BUIENRADARGPS
''' <summary>
''' 'class to return create or modify xdocuments needed to store information
''' ' these are stored in the EDO of the io device of plugin instance
''' ' 3 xdoc are settings rts485 settings, di vdi  , 2 are constants dt rts485 nodes
''' </summary>
''' <remarks></remarks>
Public Class Files

#Region "properties"


    ''' <summary>
    ''' devicetype xdocument
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared dtXdoc As New XDocument



    ''' <summary>
    ''' device installed xdocument
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared diXdoc As New XDocument


    ''' <summary>
    ''' virtual device xdocument
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared VdsXdoc As New XDocument


    ''' <summary>
    ''' RTS485 settings xdocument
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Rts485SettingsXdoc As New XDocument

    ''' <summary>
    ''' RTS485 Node Types
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Rts485NodeTypeXdoc As New XDocument


    Public Shared LocationsXdoc As New XDocument


    Public Shared RainXdoc As XElement

    Public Shared OneLocatonKnown As Boolean = False


    Public Shared _deviceissettochannel As Boolean = False
    'Public Property dtds As New DataSet("devicetypes")
    'Public Property dtdt As New DataTable("devicetypes")
    'Public Property rts485dt As New DataTable ''devicesettings rts485 dt
    'Public Property rts485ds As New DataSet ''devicesettings rts485 dt
    'Public Property dids As New DataSet
    'Public Property didt As New DataTable("devicesinstalled")      ''deviceinstalled di dt

    Public Shared dctlock As String()
    'Public txtsi As String    '' name given to device in config2
    'Public ddlsi As String    '' type given to device in config 2
    Public Shared selectedid As Byte '' id chos dev
    'Public devcho As devicechosen

#End Region
   
    ''' <summary>
    ''' 'creates the xdoxuments needed for storing config info. if there is a io device found is true and these 
    ''' </summary>
    ''' <param name="found"></param>
    ''' <remarks></remarks>
    ''' <test></test>
    Public Shared Sub CreateXdocuments(ByVal found As Boolean)

        If found = False Then

         
            CreateLocXdox()
        End If

      

    End Sub





#Region "create empty or modify  xdocument locations settings"

    ''' <summary>
    ''' 'returns emty locations xdocument
    ''' locations have property name lat lon
    ''' </summary>
    ''' <returns>Xdocument locations</returns>
    ''' <remarks></remarks>
    Public Shared Function CreateLocXdox() As XDocument

        '' http://msdn.microsoft.com/en-us/library/bb344365.aspx




        LocationsXdoc.Add(New XElement("Locations"))






        Dim i As Integer



        For i = 0 To 15



            Dim rcxel As XElement = New XElement("location")




            rcxel.Add(New XElement("location_street", ""))
            rcxel.Add(New XElement("location_city", ""))
            rcxel.Add(New XElement("location_country", ""))
            rcxel.Add(New XElement("location_lattitude", ""))
            rcxel.Add(New XElement("location_longitude", ""))
            rcxel.Add(New XElement("location_index", i))
            rcxel.Add(New XElement("location_hsref", ""))
            rcxel.Add(New XElement("location_monitor", "false"))
            LocationsXdoc.Root.Add(rcxel)




        Next

        Return LocationsXdoc



    End Function

    ''' <summary>
    ''' '
    ''' </summary>
    ''' <param name="func"></param>
    ''' <param name="selectedid"></param>
    ''' <param name="street"></param>
    ''' <param name="city"></param>
    ''' <param name="country"></param>
    ''' <param name="lon"></param>
    ''' <param name="lat"></param>
    ''' <param name="ref"></param>
    ''' <param name="monitor"></param>
    ''' <remarks></remarks>
    Public Shared Sub changefilelocations(ByVal func As Integer, ByVal selectedid As Integer, ByVal street As String, _
                                          ByVal city As String, ByVal country As String, ByVal lon As String, ByVal lat As String, ByVal ref As Integer, ByVal monitor As String)

        ''Dim el As XElement

        '         or you can use the XmlDocument to save xml document to specific stream:
        'MemoryStream stream = new MemoryStream();
        'XmlDocument xDocument = new XmlDocument();
        'xDocument.Save(stream);
        'http://msdn2.microsoft.com/en-us/library/aa335927(vs.71).aspx

        'Read the xml document from tream:
        'MemoryStream stream = new MemoryStream();
        'XmlDocument xDocument = new XmlDocument();
        'xDocument.Load(stream);
        'http://msdn2.microsoft.com/en-us/library/aa335923(VS.71).aspx



        '' Dim xmlfile1 As XDocument = XDocument.Load(xmlFile)      ''xmlfile = xmlreader

        Dim Xdoctemp As XDocument = LocationsXdoc  '' temporary file to change and read back to dixdoc


        Dim query1 = (From el In Xdoctemp...<location>
        Where (el.<location_index>.Value = selectedid)
        Select el).FirstOrDefault()

        Select Case func


            Case 1     '' edit device

                If String.IsNullOrEmpty(country) Then

                    query1.<location_street>.Value = ""
                    query1.<location_city>.Value = ""
                    query1.<location_country>.Value = ""
                    query1.<location_longitude>.Value = ""
                    query1.<location_lattitude>.Value = ""
                    query1.<location_hsref>.Value = ref



                Else

                    query1.<location_street>.Value = street
                    query1.<location_city>.Value = city
                    query1.<location_country>.Value = country
                    query1.<location_longitude>.Value = lon
                    query1.<location_lattitude>.Value = lat
                    query1.<location_hsref>.Value = ref

                End If





            Case 2     '' clear device

                query1.<location_street>.Value = ""
                query1.<location_city>.Value = ""
                query1.<location_country>.Value = ""
                query1.<location_longitude>.Value = ""
                query1.<location_lattitude>.Value = ""
                query1.<location_hsref>.Value = ""
                query1.<location_monitor>.Value = monitor
            Case 3 ''monitor device

                query1.<location_monitor>.Value = monitor

                Dim test As Integer = 5


        End Select
        ' Dim query1 = (From elm In readxlm...<node_type>
        'Where (readxlm...<node_type>.Value = "03")
        'Select elm.Parent).FirstOrDefault()



        LocationsXdoc = Xdoctemp
        '' save file in edo
        UdateIoDeviceEDO("Locations")

    End Sub






#End Region

#Region "create  xdocument rainvalues "

    ''' <summary>
    ''' 'returns emty rain xdocument
    ''' locations have property for al times with their rain value
    ''' </summary>
    ''' <returns>Xdocument Rain</returns>
    ''' <remarks></remarks>
    Public Shared Function CreateRainXdox() As XElement

        '' http://msdn.microsoft.com/en-us/library/bb344365.aspx




        ''  RainXdoc.Add(New XElement("Locations"))
        Dim root As New XElement("Locations")





        Dim i As Integer



        For i = 0 To 15  ''16 locations



            Dim rcxel As XElement = New XElement("Location")

            rcxel.Add(New XElement("IndexLocation", i.ToString))


            For ii = 0 To 24  '' count = 25 rainpairvalues first value time each 5 minutes



                Dim pair As XElement = New XElement("Pair")

                pair.Add(New XElement("IndexPair", ii.ToString))
                pair.Add(New XElement("Time"))
                pair.Add(New XElement("Rain"))


                rcxel.Add(pair)

            Next

            root.Add(rcxel)


            RainXdoc = root

        Next

        Return RainXdoc



    End Function












#End Region

#Region "Edo"

    Public Shared Sub UdateIoDeviceEDO(ByVal xdoc As String)

        Dim dv As Scheduler.Classes.DeviceClass

        dv = hs.GetDeviceByRef(HsIoDvRef)


        Dim EDO As HomeSeerAPI.clsPlugExtraData = Nothing

        EDO = dv.PlugExtraData_Get(hs)

        If EDO IsNot Nothing Then

            diXdoc.ToString()


            EDO.RemoveNamed(xdoc)

            If xdoc = "Locations" Then

                EDO.AddNamed(xdoc, LocationsXdoc.ToString)
                dv.PlugExtraData_Set(hs) = EDO
                hs.SaveEventsDevices()

            End If


        End If



    End Sub


#End Region

#Region "return empty xdocument rts485tranmitter settings "



    ''' <summary>
    ''' 'returns empty to be filled Rtssettingsdox
    ''' </summary>
    ''' <returns>Rtssettingsdox</returns>
    ''' <remarks></remarks>
    Public Shared Function CreateSetXdox() As XDocument




        Rts485SettingsXdoc = <?xml version="1.0" encoding="utf-8" standalone="yes"?>
                             <settings>
                                 <comset>Nothing</comset>
                                 <rts485set>False</rts485set>
                                 <nodeid>00 00 00</nodeid>
                             </settings>

        Return Rts485SettingsXdoc



    End Function

    ''' <summary>
    ''' 'writes comport nodeadress and isset to Rts485SettingsXdoc and calls updates EDO
    ''' </summary>
    ''' <remarks></remarks>
    ''' <test></test>
    'Public Shared Sub WriteComAndNaToRts485SettingsXdoc() ''  write to Rts485SettingsXdoc
    '    '' write to settings.xml

    '    Dim comset = HSPI.trans.comport
    '    Dim naset = HSPI.trans.nodeaddress
    '    Dim IsSet = HSPI.trans.ComAndNaSet

    '    Rts485SettingsXdoc.<settings>.<comset>.Value = comset
    '    Rts485SettingsXdoc.<settings>.<nodeid>.Value = naset
    '    Rts485SettingsXdoc.<settings>.<rts485set>.Value = IsSet

    '    UdateIoDeviceEDO("RTSsettings")

    'End Sub

    ''' <summary>
    ''' ''updates EDO with Rts485SettingsXdoc tostring with xdoc as edo part
    ''' </summary>
    ''' <param name="xdoc"></param>
    ''' <remarks></remarks>
    ''' <test></test>























#End Region

#Region "create   xdocument rts485 nodetypes "

    ''' <summary>
    ''' 'returns 
    ''' </summary>
    ''' <returns>rts485NodeTypedox</returns>
    ''' <remarks></remarks>
    ''' 
    Public Shared Function CreateRts485TypesXdox() As XDocument

        Rts485NodeTypeXdoc = <?xml version="1.0" encoding="utf-8" standalone="yes"?>
                             <rts485devices xlmns="urn:jilles:rts485device">
                                 <rts485device>
                                     <node_name>Nothing</node_name>
                                     <node_type>00</node_type>
                                     <node_id></node_id>
                                 </rts485device>
                                 <rts485device>
                                     <node_name>RS485 RTS transmitter</node_name>
                                     <node_type>05</node_type>
                                     <node_id></node_id>
                                 </rts485device>
                                 <rts485device>
                                     <node_name>RS485 4ILT interface</node_name>
                                     <node_type>04</node_type>
                                     <node_id></node_id>
                                 </rts485device>
                                 <rts485device>
                                     <node_name>MOCO RS485</node_name>
                                     <node_type>03</node_type>
                                     <node_id></node_id>
                                 </rts485device>
                                 <rts485device>
                                     <node_name>ST30 RS485</node_name>
                                     <node_type>02</node_type>
                                     <node_id></node_id>
                                 </rts485device>
                             </rts485devices>

        Return Rts485NodeTypeXdoc


    End Function









#End Region

#Region "create  xdocument devicetypes"




    ''' <summary>
    ''' 'create  xdocument devicetypes is not to be modified by proga
    ''' </summary>
    ''' <returns>dtxdoc</returns>
    ''' <remarks></remarks>
    Public Shared Function CreateDtXdox() As XDocument    ''writedevicetypexmlfile

        dtXdoc.Add(New XElement("devicetypes"))


        dtXdoc = fillxelvalue("Roller Shutter")
        dtXdoc = fillxelvalue("Venetian Blind")
        dtXdoc = fillxelvalue("Roller blind")
        dtXdoc = fillxelvalue("Awning")
        dtXdoc = fillxelvalue("Garage Door Opener")
        dtXdoc = fillxelvalue("Curtain")
        dtXdoc = fillxelvalue("Projektion Screen")
        dtXdoc = fillxelvalue("Lightning")


        Return dtXdoc


    End Function



    ''' <summary>
    ''' 'fills dtxdox.root with xelements devicetype which has value value
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function fillxelvalue(ByVal value As String) As XDocument   ' fill xelementvalue 



        dtXdoc.Root.Add(New XElement("devicetype", value))



        Return dtXdoc



    End Function

#End Region

#Region "create empty or modify  xdocument installed devices settings"



    ''' <summary>
    ''' 'returns emty device installed xdocument
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateDiXdox() As XDocument

        '' http://msdn.microsoft.com/en-us/library/bb344365.aspx




        diXdoc.Add(New XElement("InstalledDevices"))






        Dim i As Integer



        For i = 0 To 15



            Dim rcxel As XElement = New XElement("installeddevice")




            rcxel.Add(New XElement("device_ID", i + 1))
            rcxel.Add(New XElement("device_Channel", i))
            rcxel.Add(New XElement("device_Type"), "")
            rcxel.Add(New XElement("device_Name"), "")
            rcxel.Add(New XElement("device_Isset", False))
            rcxel.Add(New XElement("device_HsRef", ""))




            diXdoc.Root.Add(rcxel)




        Next

        Return diXdoc



    End Function



    'Public Sub filltab2table()

    '    ''Dim el As XElement

    '    '         or you can use the XmlDocument to save xml document to specific stream:
    '    'MemoryStream stream = new MemoryStream();
    '    'XmlDocument xDocument = new XmlDocument();
    '    'xDocument.Save(stream);
    '    'http://msdn2.microsoft.com/en-us/library/aa335927(vs.71).aspx

    '    'Read the xml document from tream:
    '    'MemoryStream stream = new MemoryStream();
    '    'XmlDocument xDocument = new XmlDocument();
    '    'xDocument.Load(stream);
    '    'http://msdn2.microsoft.com/en-us/library/aa335923(VS.71).aspx



    '    '' Dim xmlfile1 As XDocument = XDocument.Load(xmlFile)      ''xmlfile = xmlreader

    '    Dim xmlfile1 As XDocument = idxmlmem


    '    Dim query1 = (From el In xmlfile1...<installeddevice>
    '    Where (el.<device_ID>.Value = (selectedid + 1))
    '    Select el).FirstOrDefault()

    '    Select Case func


    '        Case 1     '' edit device

    '            If String.IsNullOrEmpty(textnamedevice) Then

    '            Else

    '                query1.<device_Name>.Value = textnamedevice
    '                query1.<device_Type>.Value = cbsitem


    '            End If





    '        Case 2     '' clear device

    '            If String.IsNullOrEmpty(textnamedevice) Then
    '                textnamedevice = ""
    '            End If

    '            If String.IsNullOrEmpty(cbsitem) Then
    '                cbsitem = ""
    '            End If

    '            query1.<device_Name>.Value = textnamedevice
    '            query1.<device_Type>.Value = cbsitem
    '            query1.<device_Isset>.Value = "False"
    '            query1.<device_HsRef>.Value = ""


    '        Case 3      '' channel is set

    '            query1.<device_Isset>.Value = devisset

    '        Case 4   '' add homeseer absolute dev reference 

    '            query1.<device_HsRef>.Value = hsref



    '    End Select
    '    ' Dim query1 = (From elm In readxlm...<node_type>
    '    'Where (readxlm...<node_type>.Value = "03")
    '    'Select elm.Parent).FirstOrDefault()

    '    xmlfile1.Save(id_xmlf)

    '    dixdoc = xmlfile1
    '    idxmlmem = xmlfile1
    'End Sub


    Public Shared Sub changefileinstalleddevices(ByVal selectedid As Integer, ByVal cbsitem As String, ByVal textnamedevice As String, ByVal devisset As Boolean, ByVal hsref As Integer, ByVal func As Byte)

        ''Dim el As XElement

        '         or you can use the XmlDocument to save xml document to specific stream:
        'MemoryStream stream = new MemoryStream();
        'XmlDocument xDocument = new XmlDocument();
        'xDocument.Save(stream);
        'http://msdn2.microsoft.com/en-us/library/aa335927(vs.71).aspx

        'Read the xml document from tream:
        'MemoryStream stream = new MemoryStream();
        'XmlDocument xDocument = new XmlDocument();
        'xDocument.Load(stream);
        'http://msdn2.microsoft.com/en-us/library/aa335923(VS.71).aspx



        '' Dim xmlfile1 As XDocument = XDocument.Load(xmlFile)      ''xmlfile = xmlreader

        Dim xmlfile1 As XDocument = diXdoc  '' temporary file to change and read back to dixdoc


        Dim query1 = (From el In xmlfile1...<installeddevice>
        Where (el.<device_ID>.Value = (selectedid + 1))
        Select el).FirstOrDefault()

        Select Case func


            Case 1     '' edit device

                If String.IsNullOrEmpty(textnamedevice) Then

                Else

                    query1.<device_Name>.Value = textnamedevice
                    query1.<device_Type>.Value = cbsitem


                End If





            Case 2     '' clear device

                If String.IsNullOrEmpty(textnamedevice) Then
                    textnamedevice = ""
                End If

                If String.IsNullOrEmpty(cbsitem) Then
                    cbsitem = ""
                End If

                query1.<device_Name>.Value = textnamedevice
                query1.<device_Type>.Value = cbsitem
                query1.<device_Isset>.Value = "False"
                query1.<device_HsRef>.Value = ""


            Case 3      '' channel is set

                query1.<device_Isset>.Value = devisset

            Case 4   '' add homeseer absolute dev reference 







                query1.<device_Name>.Value = textnamedevice
                query1.<device_Type>.Value = cbsitem
                query1.<device_Isset>.Value = devisset
                query1.<device_HsRef>.Value = hsref



        End Select
        ' Dim query1 = (From elm In readxlm...<node_type>
        'Where (readxlm...<node_type>.Value = "03")
        'Select elm.Parent).FirstOrDefault()



        diXdoc = xmlfile1

        '' save file in edo
        UdateIoDeviceEDO("InstalledDevices")

    End Sub















#End Region

#Region "create  empty or modify  xdocument virtual devices "









    '''http://msdn.microsoft.com/en-us/magazine/dd722812.aspx
    ''' <summary>
    ''' create a xdocument file from xdocument and xelement virtualdevices
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Dim a As Integer







    ''' <summary>
    ''' 'create a xdocument  from xdocument and xelement virtualdevices
    ''' </summary>
    ''' <returns> VdsXdox</returns>
    ''' <remarks></remarks>
    Public Shared Function CreateVdsdox() As XDocument


        Dim rxel As XElement = <virtualdevices/>  ' rootelement
        Dim val As Byte = 0



        VdsXdoc.Add(rxel)


        rxel = VdsXdoc.Root

        For i = 0 To 15



            Dim vdxel As XElement = <virtualdevice/>





            'vdxel.SetAttributeValue("exists", val)
            'vdxel.SetAttributeValue("index", i)
            'vdxel.SetAttributeValue("name", "")


            Dim vdindexel As XElement = <virtualdeviceindex/>
            Dim vdnamexel As XElement = <virtualdevicename/>
            Dim vdhrefexel As XElement = <virtualdevice_HsRef/>
            Dim vdtypexel As XElement = <virtualdevicetype/>
            vdindexel.Value = i

            vdxel.Add(vdindexel)

            vdxel.Add(vdnamexel)

            vdxel.Add(vdtypexel)

            vdxel.Add(vdhrefexel)


            For x = 1 To 16

                'http://msdn.microsoft.com/en-us/library/bb387100.aspx note the double <<>>

                Dim dxel As XElement
                Dim s As String

                s = "device" & x

                dxel = <<%= s %>/>
                dxel.Value = val
                'dxel.SetAttributeValue("index", x)

                vdxel.Add(dxel)


            Next







            rxel.Add(vdxel)


        Next

        Return VdsXdoc


    End Function






    Public Shared Sub clearvirtualdevicefromfile(ByVal selectedid As Integer)

        Dim val As Byte = 0
        Dim x As Byte
        Dim s As String
        Dim qel As XElement

        Dim query1 = (From el In VdsXdoc...<virtualdevice>
        Where (el.<virtualdeviceindex>.Value = selectedid)
        Select el).FirstOrDefault()

        qel = query1


        query1.<virtualdevicename>.Value = ""
        query1.<virtualdevice_HsRef>.Value = ""
        query1.<virtualdevicetype>.Value = ""





        For x = 1 To 16

            'http://msdn.microsoft.com/en-us/library/bb387100.aspx note the double <<>>
            'http://msdn.microsoft.com/en-us/library/bb384964.aspx
            'http://msdn.microsoft.com/en-us/library/bb384752.aspx

            s = "device" & x


            Dim query2 = (From el In query1.Descendants
         Where (el.Name = s)
         Select el).FirstOrDefault

            query2.Value = 0















        Next





    End Sub

    Public Shared Sub ChangeFileVirualInstalledDevices(ByVal rowindex As Integer, ByVal cbsitem As String, ByVal textnamedevice As String, ByVal hsref As Integer, ByVal func As Byte)

        ''Dim el As XElement

        '         or you can use the XmlDocument to save xml document to specific stream:
        'MemoryStream stream = new MemoryStream();
        'XmlDocument xDocument = new XmlDocument();
        'xDocument.Save(stream);
        'http://msdn2.microsoft.com/en-us/library/aa335927(vs.71).aspx

        'Read the xml document from tream:
        'MemoryStream stream = new MemoryStream();
        'XmlDocument xDocument = new XmlDocument();
        'xDocument.Load(stream);
        'http://msdn2.microsoft.com/en-us/library/aa335923(VS.71).aspx



        '' Dim xmlfile1 As XDocument = XDocument.Load(xmlFile)      ''xmlfile = xmlreader

        Dim xmlfile1 As XDocument = VdsXdoc  '' temporary file to change and read back to dixdoc


        Dim query1 = (From el In xmlfile1...<virtualdevice>
           Where (el.<virtualdeviceindex>.Value = rowindex - 1)
           Select el).FirstOrDefault()

        Select Case func


            Case 1     '' edit device

                If String.IsNullOrEmpty(textnamedevice) Then

                Else

                    query1.<virtualdevicename>.Value = textnamedevice
                    query1.<device_Type>.Value = cbsitem


                End If





            Case 2     '' clear device

                Dim val As Byte = 0
                Dim x As Byte
                Dim s As String





                query1.<virtualdevicename>.Value = ""


                query1.<virtualdevice_HsRef>.Value = ""





                For x = 1 To 16

                    'http://msdn.microsoft.com/en-us/library/bb387100.aspx note the double <<>>
                    'http://msdn.microsoft.com/en-us/library/bb384964.aspx
                    'http://msdn.microsoft.com/en-us/library/bb384752.aspx

                    s = "device" & x


                    Dim query2 = (From el In query1.Descendants
                 Where (el.Name = s)
                 Select el).FirstOrDefault

                    query2.Value = 0

                Next


            Case 4   '' add homeseer absolute dev reference 







                query1.<virtualdevicename>.Value = textnamedevice
                'query1.<device_Type>.Value = cbsitem

                query1.<virtualdevice_HsRef>.Value = hsref



        End Select
        ' Dim query1 = (From elm In readxlm...<node_type>
        'Where (readxlm...<node_type>.Value = "03")
        'Select elm.Parent).FirstOrDefault()



        VdsXdoc = xmlfile1

        '' save file in edo
        UdateIoDeviceEDO("VirtualInstalledDevices")

    End Sub






#End Region

    Public Shared Function raindevicereref(ByVal selectedid As Integer) As Integer


        Dim Xdoctemp As XDocument = LocationsXdoc  '' temporary file to change and read back to dixdoc


        Dim query1 = (From el In Xdoctemp...<location>
        Where (el.<location_index>.Value = selectedid)
        Select el).FirstOrDefault()


        Try


       

            Dim s As Integer = CInt(query1.<location_hsref>.Value)

        Catch ex As Exception

            Return -1  '' indicating no value is stored so no device is defined

        End Try



        Return CInt(query1.<location_hsref>.Value)


    End Function








End Class



