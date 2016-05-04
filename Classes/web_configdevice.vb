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

        Dim ol As clsJQuery.jqOverlay = New clsJQuery.jqOverlay("ov1", pagename, False, "events_overlay")
        ol.toolTip = "This will display the overlay"
        ol.label = "Display Overlay"
        Dim tbov1 As New clsJQuery.jqTextBox("tbov1", "text", "hello", "test", 20, False)
        Dim tbut1 As New clsJQuery.jqButton("tbut1", "submit", "test", True)
        ol.overlayHTML = "<div>This is the overlay text<br><br>" & tbov1.Build & tbut1.Build & "</div>"
        cstbcdio.Append(ol.Build)














        cstbcdio.Append("<div id='latitude'></div>")
        cstbcdio.Append("<div id='longitude'></div>")
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
        cstbcdio.Append("<div id='cstbcdior1c1'>" & BuildButton("edit00", "edit", True) & BuildButton("graph00", "graph", True) & BuildCheckbox("mon00", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior1c2'>" & filldeviceproperties(0, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior1c3'>" & filldeviceproperties(0, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior1c4'>" & filldeviceproperties(0, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior2c1'>" & BuildButton("edit01", "edit", True) & BuildButton("graph01", "graph", True) & BuildCheckbox("mon01", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior2c2'>" & filldeviceproperties(1, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior2c3'>" & filldeviceproperties(1, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior2c4'>" & filldeviceproperties(1, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior3c1'>" & BuildButton("edit02", "edit", True) & BuildButton("graph02", "graph", True) & BuildCheckbox("mon02", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior3c2'>" & filldeviceproperties(2, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior3c3'>" & filldeviceproperties(2, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior3c4'>" & filldeviceproperties(2, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior4c1'>" & BuildButton("edit03", "edit", True) & BuildButton("graph03", "graph", True) & BuildCheckbox("mon03", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior4c2'>" & filldeviceproperties(3, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior4c3'>" & filldeviceproperties(3, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior4c4'>" & filldeviceproperties(3, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior5c1'>" & BuildButton("edit04", "edit", True) & BuildButton("graph04", "graph", True) & BuildCheckbox("mon04", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior5c2'>" & filldeviceproperties(4, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior5c3'>" & filldeviceproperties(4, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior5c4'>" & filldeviceproperties(4, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior6c1'>" & BuildButton("edit05", "edit", True) & BuildButton("graph05", "graph", True) & BuildCheckbox("mon05", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior6c2'>" & filldeviceproperties(5, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior6c3'>" & filldeviceproperties(5, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior6c4'>" & filldeviceproperties(5, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior7c1'>" & BuildButton("edit06", "edit", True) & BuildButton("graph06", "graph", True) & BuildCheckbox("mon06", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior7c2'>" & filldeviceproperties(6, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior7c3'>" & filldeviceproperties(6, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior7c4'>" & filldeviceproperties(6, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior8c1'>" & BuildButton("edit07", "edit", True) & BuildButton("graph07", "graph", True) & BuildCheckbox("mon07", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior8c2'>" & filldeviceproperties(7, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior8c3'>" & filldeviceproperties(7, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior8c4'>" & filldeviceproperties(7, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior9c1'>" & BuildButton("edit08", "edit", True) & BuildButton("graph08", "graph", True) & BuildCheckbox("mon08", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior9c2'>" & filldeviceproperties(8, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior9c3'>" & filldeviceproperties(8, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior9c4'>" & filldeviceproperties(8, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior10c1'>" & BuildButton("edit09", "edit", True) & BuildButton("graph09", "graph", True) & BuildCheckbox("mon09", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior10c2'>" & filldeviceproperties(9, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior10c3'>" & filldeviceproperties(9, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior10c4'>" & filldeviceproperties(9, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior11c1'>" & BuildButton("edit10", "edit", True) & BuildButton("graph10", "graph", True) & BuildCheckbox("mon10", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior11c2'>" & filldeviceproperties(10, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior11c3'>" & filldeviceproperties(10, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior11c4'>" & filldeviceproperties(10, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior12c1'>" & BuildButton("edit11", "edit", True) & BuildButton("graph11", "graph", True) & BuildCheckbox("mon11", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior12c2'>" & filldeviceproperties(11, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior12c3'>" & filldeviceproperties(11, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior12c4'>" & filldeviceproperties(11, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior13c1'>" & BuildButton("edit12", "edit", True) & BuildButton("graph12", "graph", True) & BuildCheckbox("mon12", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior13c2'>" & filldeviceproperties(12, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior13c3'>" & filldeviceproperties(12, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior13c4'>" & filldeviceproperties(12, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior14c1'>" & BuildButton("edit13", "edit", True) & BuildButton("graph13", "graph", True) & BuildCheckbox("mon13", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior14c2'>" & filldeviceproperties(13, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior14c3'>" & filldeviceproperties(13, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior14c4'>" & filldeviceproperties(13, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior15c1'>" & BuildButton("edit14", "edit", True) & BuildButton("graph14", "graph", True) & BuildCheckbox("mon14", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior15c2'>" & filldeviceproperties(14, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior15c3'>" & filldeviceproperties(14, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior15c4'>" & filldeviceproperties(14, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("<tr>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior16c1'>" & BuildButton("edit15", "edit", True) & BuildButton("graph15", "graph", True) & BuildCheckbox("mon15", "mon") & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior16c2'>" & filldeviceproperties(15, 1) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior16c3'>" & filldeviceproperties(15, 2) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("<td class='tablecolumn'  style='width: 25%;' >")
        cstbcdio.Append("<div id='cstbcdior16c4'>" & filldeviceproperties(15, 3) & "</div>")
        cstbcdio.Append("</td>")
        cstbcdio.Append("</tr >")
        cstbcdio.Append("</table>")
        cstbcdio.Append(clsPageBuilder.FormEnd())

        Return cstbcdio.ToString

    End Function



    Public Shared Function buildedittable()


        Dim editfieldstab As New StringBuilder

        editfieldstab.Append(clsPageBuilder.FormStart("myform", pagename, "post"))
        editfieldstab.Append("<table id='Table1' style='width: 50%;'   >")
        editfieldstab.Append("<tr>")
        editfieldstab.Append("<th class='tablecolumnheader'  style='width: 50%;' >")
        editfieldstab.Append("<div>header</div>")
        editfieldstab.Append("</th>")
        editfieldstab.Append("<th class='tablecolumnheader'  style='width: 50%;' >")
        editfieldstab.Append("<div>header</div>")
        editfieldstab.Append("</th>")
        editfieldstab.Append("</tr >")
        editfieldstab.Append("<tr>")









        editfieldstab.Append("<td class='tablecolumn'  style='width: 50%;' >")
        editfieldstab.Append("<div id='editfieldstabr1c1'>street and housenumber</div>")
        editfieldstab.Append("</td>")
        editfieldstab.Append("<td class='tablecolumn'  style='width: 50%;' >")
        editfieldstab.Append("<div id='editfieldstabr1c2'>" & BuildTextBox("street", 1, False) & "</div>")
        editfieldstab.Append("</td>")
        editfieldstab.Append("</tr >")
        editfieldstab.Append("<tr>")

        editfieldstab.Append("<td class='tablecolumn'  style='width: 50%;' >")
        editfieldstab.Append("<div id='editfieldstabr2c1'>city</div>")
        editfieldstab.Append("</td>")
        editfieldstab.Append("<td class='tablecolumn'  style='width: 50%;' >")
        editfieldstab.Append("<div id='editfieldstabr2c2'>" & BuildTextBox("city", 2, False) & "</div>")
        editfieldstab.Append("</td>")
        editfieldstab.Append("</tr >")
        editfieldstab.Append("<tr>")

        editfieldstab.Append("<td class='tablecolumn'  style='width: 50%;' >")
        editfieldstab.Append("<div id='editfieldstabr3c1'>country</div>")
        editfieldstab.Append("</td>")
        editfieldstab.Append("<td class='tablecolumn'  style='width: 50%;' >")
        editfieldstab.Append("<div id='editfieldstabr3c2'>" & BuildTextBox("country", 3, False) & "</div>")
        editfieldstab.Append("</td>")
        editfieldstab.Append("</tr >")

        editfieldstab.Append("<tr>")
        editfieldstab.Append("<td class='tablecolumn'  style='width: 50%;' >")
        editfieldstab.Append("<div id='editfieldstabr4c1'>latitude</div>")
        editfieldstab.Append("</td>")
        editfieldstab.Append("<td class='tablecolumn'  style='width: 50%;' >")
        editfieldstab.Append("<div id='editfieldstabr4c2'>" & BuildTextBox("latitude", 4, False) & "</div>")
        editfieldstab.Append("</td>")
        editfieldstab.Append("</tr >")

        editfieldstab.Append("<tr>")
        editfieldstab.Append("<td class='tablecolumn'  style='width: 50%;' >")
        editfieldstab.Append("<div id='editfieldstabr5c1'>longitude</div>")
        editfieldstab.Append("</td>")
        editfieldstab.Append("<td class='tablecolumn'  style='width: 50%;' >")
        editfieldstab.Append("<div id='editfieldstabr5c2'>" & BuildTextBox("longitude", 5, False) & "</div>")
        editfieldstab.Append("</td>")
        editfieldstab.Append("</tr >")

        editfieldstab.Append("</table>")

        editfieldstab.Append(BuildButton("coordinates", "coordinates", True))




        editfieldstab.Append(BuildButton("save", "save", True))



        Return editfieldstab.ToString

    End Function


   

    Public Shared Function BuildTextBox(ByVal Name As String, ByVal row As Integer, Optional ByVal Rebuilding As Boolean = False, Optional ByVal Text As String = "") As String

        Dim index As Integer = 0 '' rowindex


        'Try
        '    index = StripIndexFromString(Name, 5)
        'Catch

        'End Try

        Dim tb As New clsJQuery.jqTextBox(Name, "", Text, pagename, 20, False)
        Dim TextBox As String = ""

        tb.id = "brc_tb" & Name
        tb.defaultText = filleditproperties(index, row)
        TextBox = tb.Build



        Return TextBox
    End Function

    


    Public Shared Function BuildButton(ByVal Name As String, ByVal buttontext As String, ByVal submitform As Boolean, Optional ByVal Rebuilding As Boolean = False) As String
        Dim Content As String = ""
        Dim Button As String
        Dim b As New clsJQuery.jqButton(Name, "", pagename, submitform)
        '' Dim strippedname As String = Mid(Name, 1, Name.Length - 2)


        Select Case Name
            Case "save"

                b.submitForm = submitform
        End Select

        b.id = "brc_b" & Name
        b.label = buttontext

        Button = b.Build


        Return Button
    End Function

   

    Public Shared Function BuildCheckbox(ByVal Name As String, Optional ByVal Text As String = "", Optional ByVal Rebuilding As Boolean = False) As String

        Dim index As Integer = -1 '' rowindex

        Try
            index = StripIndexFromString(Name, 5)
        Catch

        End Try


        Dim c As New clsJQuery.jqCheckBox(Name, Text, pagename, True, True)

        c.id = "brc_cb" & Name       '' brc = buirenradarconfig checkbox

        Dim CheckBox As String = ""


        Dim xmlfile1 As XDocument = Files.LocationsXdoc

        '' if hsref > 0 then device can be deleted and all other buttons accept add can be enabled 


        Dim query1 = (From el In xmlfile1...<location>
        Where (el.<location_index>.Value = index)
        Select el).FirstOrDefault()

        If query1.<location_monitor>.Value = "" Then

            query1.<location_monitor>.Value = "False"
        End If


        c.checked = query1.<location_monitor>.Value
        CheckBox = c.Build



        Return CheckBox

    End Function



    Public Shared Function filleditproperties(ByVal index As Integer, ByVal row As Integer) As String

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
        Where (el.<location_index>.Value = index)
        Select el).FirstOrDefault()

        Select Case row

            Case 1       ''<location_name>




                Return query1.<location_street>.Value


            Case 2

                Return query1.<location_city>.Value

            Case 3

                Return query1.<location_country>.Value

            Case 4  ''<device_latitude>


                Return query1.<location_longitude>.Value

            Case 5 ''<device_longitude>

                Return query1.<location_lattitude>.Value


        End Select



    End Function






















    ''' <summary>
    ''' 'zero based to get right elemnt in right cell
    ''' </summary>
    ''' <param name="row"></param>
    ''' <param name="cell"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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




                Return query1.<location_street>.Value & " " & query1.<location_city>.Value & " " & query1.<location_city>.Value & " " & query1.<location_country>.Value



            Case 2  ''<device_latitude>


                Return query1.<location_longitude>.Value

            Case 3 ''<device_longitude>

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

   





End Class
