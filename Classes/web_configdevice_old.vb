Imports System.Text
Imports Scheduler

''' <summary>
''' 'class forconfigdevicetab in hspi , makes webpagetab for function configdevice
''' </summary>
''' <remarks></remarks>
Public Class web_configdevice

    ''' <summary>
    ''' 'the name of the page of deviceconfig to use in clsjquery
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Property pagename As String = "deviceutility"

    Public Shared Function buildwebpage() As String





        Dim cstbcdio As New StringBuilder
        cstbcdio.Append("<div id='geotext'></div>")
        cstbcdio.Append("<div id='overlay'></div>")
        cstbcdio.Append("<table id='Table1' style='width: 50%;'   >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<th class='tablecolumnheader'  style='width: 25%;' >")
        cstbcdio.Append("<div>action</div>")
        cstbcdio.Append("</th>")
        cstbcdio.Append("<th class='tablecolumnheader'  style='width: 25%;' >")
        cstbcdio.Append("<div>place</div>")
        cstbcdio.Append("</th>")
        cstbcdio.Append("<th class='tablecolumnheader'  style='width: 25%;' >")
        cstbcdio.Append("<div>longitude</div>")
        cstbcdio.Append("</th>")
        cstbcdio.Append("<th class='tablecolumnheader'  style='width: 25%;' >")
        cstbcdio.Append("<div>lattitude</div>")
        cstbcdio.Append("</th>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior1c1'>" & BuildButton("save0", "save") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior1c2'>" & BuildTextBox("name0", 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior1c3'>" & BuildTextBox("lon0", 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior1c4'>" & BuildTextBox("lat0", 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior2c1'>" & BuildButton("geo1", "geo") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior2c2'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior2c3'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior2c4'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior3c1'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior3c2'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior3c3'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior3c4'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior4c1'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior4c2'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior4c3'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior4c4'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior5c1'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior5c2'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior5c3'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior5c4'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior6c1'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior6c2'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior6c3'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior6c4'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior7c1'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior7c2'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior7c3'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior7c4'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior8c1'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior8c2'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior8c3'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior8c4'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior9c1'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior9c2'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior9c3'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior9c4'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior10c1'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior10c2'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior10c3'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior10c4'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior11c1'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior11c2'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior11c3'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior11c4'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior12c1'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior12c2'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior12c3'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior12c4'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior13c1'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior13c2'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior13c3'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior13c4'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior14c1'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior14c2'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior14c3'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior14c4'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior15c1'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior15c2'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior15c3'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior15c4'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior16c1'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior16c2'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior16c3'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior16c4'>divhtml</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("</table>")


        Return cstbcdio.ToString

    End Function

    Public Shared Function BuildButton(ByVal Name As String, ByVal buttontext As String, Optional ByVal Rebuilding As Boolean = False) As String
        Dim Content As String = ""
        Dim Button As String
        Dim b As New clsJQuery.jqButton(Name, "", pagename, True)

        Select Case Name
            Case "Button1"
                buttontext = "Go To Status Page"
                b.submitForm = False
        End Select

        b.id = "brc_b" & Name
        b.label = buttontext

        Button = b.Build


        Return Button
    End Function

    Public Shared Function BuildTextBox(ByVal Name As String, ByVal cell As Integer, Optional ByVal Rebuilding As Boolean = False, Optional ByVal Text As String = "") As String

        Dim index As Integer = -1 '' rowindex


        Try
            index = StripIndexFromString(Name, 5)
        Catch

        End Try

        Dim tb As New clsJQuery.jqTextBox(Name, "", Text, pagename, 20, False)
        Dim TextBox As String = ""

        tb.id = "brc_tb" & Name
        tb.defaultText = filldeviceproperties(index, cell)
        TextBox = tb.Build



        Return TextBox
    End Function

    Public Shared Function filldeviceproperties(ByVal row As Integer, ByVal cell As Integer) As String

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




        Dim xmlfile1 As XDocument = Files.LocationsXdoc

        '' if hsref > 0 then device can be deleted and all other buttons accept add can be enabled 


        Dim query1 = (From el In xmlfile1...<location>
        Where (el.<location_index>.Value = row)
        Select el).FirstOrDefault()

        Select Case cell

            Case 1       ''<location_name>

                Return query1.<location_name>.Value


            Case 2     ''<device_Channel>


                Return query1.<location_longitude>.Value

            Case 3 ''<device_Type>

                Return query1.<location_lattitude>.Value


        End Select



    End Function


    ''' <summary>
    ''' 'returns an index as integer lgth = string.length with one number as index
    ''' </summary>
    ''' <param name="str"></param>
    ''' <param name="lgth"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function StripIndexFromString(ByVal str As String, ByVal lgth As Integer) As Integer

        Dim returnindex As Integer

        If str.Length = lgth Then

            returnindex = Mid(str, lgth, 1)

        ElseIf str.Length = lgth + 1 Then

            returnindex = Mid(str, lgth + 1, 2)

        End If


        Return returnindex
    End Function

    Public Shared Function buildoverlay() As String

        Dim overlaygps As New StringBuilder


        overlaygps.Append("<table id='Table1' style='width: 50%;'   >")
        overlaygps.Append("<tr>")
        overlaygps.Append("<th class='tablecolumnheader'  style='width: 50%;' >")
        overlaygps.Append("<div>header</div>")
        overlaygps.Append("</th>")
        overlaygps.Append("<th class='tablecolumnheader'  style='width: 50%;' >")
        overlaygps.Append("<div>header</div>")
        overlaygps.Append("</th>")
        overlaygps.Append("</tr >")
        overlaygps.Append("<tr>")
        overlaygps.Append("<td class='tablecolumn'  style='width: 50%;' >")
        overlaygps.Append("<div id='overlaygpsr1c1'>street and housenumber</div>")
        overlaygps.Append("</td>")
        overlaygps.Append("<td class='tablecolumn'  style='width: 50%;' >")

        Dim tb1 As New clsJQuery.jqTextBox("street", "text", "", pagename, 20, False)

        tb1.defaultText = "corona 52"

        tb1.id = "brc_tb" & "street"


        '' tb.defaultText = filldeviceproperties(index, cell)

        overlaygps.Append("<div id='overlaygpsr1c2'>" & tb1.Build & "</div>")
        overlaygps.Append("</td>")
        overlaygps.Append("</tr >")
        overlaygps.Append("<tr>")
        overlaygps.Append("<td class='tablecolumn'  style='width: 50%;' >")
        overlaygps.Append("<div id='overlaygpsr2c1'>city</div>")
        overlaygps.Append("</td>")
        overlaygps.Append("<td class='tablecolumn'  style='width: 50%;' >")


        Dim tb2 As New clsJQuery.jqTextBox("city", "text", "", pagename, 20, False)


        tb2.id = "brc_tb" & "city"

        tb2.defaultText = "spijkenisse"

        overlaygps.Append("<div id='overlaygpsr2c2'>" & tb2.Build & "</div>")
        overlaygps.Append("</td>")
        overlaygps.Append("</tr >")
        overlaygps.Append("<tr>")
        overlaygps.Append("<td class='tablecolumn'  style='width: 50%;' >")
        overlaygps.Append("<div id='overlaygpsr3c1'>country</div>")
        overlaygps.Append("</td>")
        overlaygps.Append("<td class='tablecolumn'  style='width: 50%;' >")

        Dim tb3 As New clsJQuery.jqTextBox("country", "text", "", pagename, 20, False)


        tb3.id = "brc_tb" & "country"
        tb3.defaultText = "nederland"


        overlaygps.Append("<div id='overlaygpsr3c2'>" & tb3.Build & "</div>")
        overlaygps.Append("</td>")
        overlaygps.Append("</tr >")
        overlaygps.Append("<tr>")
        overlaygps.Append("<td class='tablecolumn'  style='width: 50%;' >")
        overlaygps.Append("<div id='overlaygpsr4c1'>divhtml</div>")
        overlaygps.Append("</td>")
        overlaygps.Append("<td class='tablecolumn'  style='width: 50%;' >")
        overlaygps.Append("<div id='overlaygpsr4c2'>divhtml</div>")
        overlaygps.Append("</td>")
        overlaygps.Append("</tr >")
        overlaygps.Append("</table>")
        overlaygps.Append(BuildButton("save", "save"))




        Return overlaygps.ToString

    End Function





End Class
