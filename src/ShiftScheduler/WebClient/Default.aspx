<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Default.aspx.cs" Inherits="WebClient._Default" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxDocking" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxNavBar" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSplitter" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxScheduler.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxScheduler" tagprefix="dxwschs" %>
<%@ Register assembly="DevExpress.XtraScheduler.v14.1.Core, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraScheduler" tagprefix="cc1" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    

                    


                
            
    <dx:ASPxSplitter ID="ContentSplitter" runat="server" Width="100%" Height="100%">
                <ClientSideEvents PaneResized="function(s, e) { 
            if(window.ASPxGridView1 && e.pane.name == 'ContentCenter')
                ASPxGridView1.SetHeight(e.pane.GetClientHeight());
            }" />
                <Styles>
            <Pane>
                <Paddings Padding="0px" />
                <Border BorderWidth="0px" />
            </Pane>
        </Styles>
        <Panes>
            <dx:SplitterPane Name="ContentLeft" ShowCollapseBackwardButton="True" Size="18%">
                <PaneStyle CssClass="leftPane">
<Paddings Padding="1px"></Paddings>
                </PaneStyle>
                <PaneStyle>
                    <Paddings Padding="1px" />
                </PaneStyle>
                <ContentCollection>
                    <dx:SplitterContentControl>
                        <div class="leftPanel">
                            <%-- DXCOMMENT: Configure the left panel's ASPxNavBar --%>
<dx:ASPxNavBar ID="ASPxNavBar1" runat="server" AutoCollapse="True"  AllowSelectItem="true" ClientInstanceName ="ASPxClientNavBar1" EnableAnimation="True" Width="100%">
    <ClientSideEvents ItemClick="function(s, e) {	

        //ASPxClientNavBar1.SetSelectedItem(e.item);
        if(ASPxClientScheduler1.GetGroupType() != ASPxSchedulerGroupType.Date)
        {
            ASPxClientScheduler1.SetGroupType(ASPxSchedulerGroupType.Date);
            
        }
      
       aspxSchedulerResNavCmd('ctl00_ctl00_ASPxSplitter1_Content_MainContent_ContentSplitter_ASPxScheduler1_resourceNavigatorBlock_ctl00', 'NAVSPECRES', e.item.index);      
     //  alert(ASPxClientScheduler1.GetSelectedResource()); 
        
        
}" />
    <Groups>
        <dx:NavBarGroup Text="Group 1">
            <Items>
                <dx:NavBarItem Text="Item 1"></dx:NavBarItem>
                <dx:NavBarItem Text="Item 2"></dx:NavBarItem>
                <dx:NavBarItem Text="Item 3"></dx:NavBarItem>
            </Items>
        </dx:NavBarGroup>
    </Groups>
    <Paddings Padding="0px" />
    <Border BorderWidth="0px" />
</dx:ASPxNavBar>
                        
                        
                        <dx:ASPxButton runat="server" Text="Show All" ID="ASPxButton1" AutoPostBack="False" EnableClientSideAPI="True">
                            <ClientSideEvents Click="function(s, e) {
    ASPxClientNavBar1.SetSelectedItem(null);
	if(ASPxClientScheduler1.GetGroupType() != ASPxSchedulerGroupType.None)
        ASPxClientScheduler1.SetGroupType(ASPxSchedulerGroupType.None);
	else
		aspxSchedulerResNavCmd('ctl00_ctl00_ASPxSplitter1_Content_MainContent_ContentSplitter_ASPxScheduler1_resourceNavigatorBlock_ctl00', 'NAVSPECRES', '');
    
}" />
                            </dx:ASPxButton>
                        
                        </div>
                        <br />
                        <br />
                        <br />
                        <dx:ASPxRoundPanel ID="ASPxRoundPanel1"   Height ="100%" runat="server"  HeaderText="Scheduler" ShowCollapseButton="true" Width="100%">
                            <PanelCollection>
                                <dx:PanelContent  >
                                    <div>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="From"></dx:ASPxLabel>   
                                    <dx:ASPxDateEdit ID="ASPxDateEditFrom" runat="server" ClientInstanceName="ASPxDateEditFrom" EnableClientSideAPI="True" OnCalendarCustomDisabledDate="ASPxDateEditFrom_CalendarCustomDisabledDate">
                                        <CalendarProperties FirstDayOfWeek="Monday">
                                        </CalendarProperties>
                                        <ClientSideEvents DateChanged="function(s, e) {
		
		var dateFrom = ASPxDateEditFrom.GetDate();
             //alert(dateFrom);
		if(dateFrom != null)
		{	
 			dateFrom.setDate(dateFrom.getDate() + 7);            
		}
        ASPxDateEditTo.SetMinDate(dateFrom);

}"  />
                                        </dx:ASPxDateEdit>

                                    </div>
                                <div>
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="To"></dx:ASPxLabel>   
                                    <dx:ASPxDateEdit ID="ASPxDateEditTo" runat="server" ClientInstanceName="ASPxDateEditTo" OnCalendarCustomDisabledDate="ASPxDateEditTo_CalendarCustomDisabledDate">
                                        <CalendarProperties FirstDayOfWeek="Monday">
                                        </CalendarProperties>
                                        <ClientSideEvents DateChanged="function(s, e) {
		
		var dateTo = ASPxDateEditTo.GetDate();
             //alert(dateFrom);
		if(dateTo != null)
		{	
 			dateTo.setDate(dateTo.getDate() - 7);            
		}
        ASPxDateEditFrom.SetMaxDate(dateTo);

}"  />
                                    </dx:ASPxDateEdit>
                                </div>  
                                    <br />
                                <dx:ASPxButton runat="server" Text="Calculate" HorizontalAlign="Center" EnableClientSideAPI="True" AutoPostBack="False" ClientInstanceName="ASPxButton1">
                                    <ClientSideEvents CheckedChanged="function(s, e) {

}" Click="function(s, e) {
s.SetChecked(true);
ASPxClientScheduler1.PerformCallback('CALCULATESCHED');	

}" />
                                    </dx:ASPxButton>                            
                                </dx:PanelContent>
                                
                             </PanelCollection>
                        </dx:ASPxRoundPanel>
                            
                        
                    </dx:SplitterContentControl>
                 
                    
                    
                            
                   
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane Name="ContentCenter" ScrollBars="Auto">
                <PaneStyle CssClass="contentPane"></PaneStyle>
                <Separator Visible="True">
                    <SeparatorStyle>
                        <Border BorderWidth="1px" />
                        <BorderTop BorderWidth="0px" />
                    </SeparatorStyle>
                </Separator>
                <ContentCollection>
                    <dx:SplitterContentControl>

                        <!-- Here goes page code -->
                         

    <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" ActiveViewType="Month" ClientIDMode="AutoID" Start="2015-06-01" OptionsBehavior-ShowViewNavigator="False" OptionsBehavior-ShowViewSelector="False" AppointmentDataSourceID="appointmentDataSource" OnPopupMenuShowing="ASPxScheduler1_PopupMenuShowing" GroupType="Date" ClientInstanceName="ASPxClientScheduler1" OnBeforeExecuteCallbackCommand="ASPxScheduler1_BeforeExecuteCallbackCommand" OnCustomCallback="ASPxScheduler1_CustomCallback">
<Views>
<DayView><TimeRulers>
<cc1:TimeRuler></cc1:TimeRuler>
</TimeRulers>
</DayView>

<WorkWeekView><TimeRulers>
<cc1:TimeRuler></cc1:TimeRuler>
</TimeRulers>
</WorkWeekView>
    <MonthView ShowMoreButtons="False" WeekCount="4" NavigationButtonVisibility="Never" ResourcesPerPage="1">
        <AppointmentDisplayOptions EndTimeVisibility="Never" StartTimeVisibility="Never" StatusDisplayType="Bounds" />
    </MonthView>
</Views>
        <ClientSideEvents EndCallback="function(s, e) {
            if(ASPxClientNavBar1.GetSelectedItem() != null)
            {
                var selectedResource = parseInt(s.GetSelectedResource()) - 1;
                //alert(ASPxClientNavBar1.GetSelectedItem().index + ' ' +  selectedResource);
                if(ASPxClientNavBar1.GetSelectedItem().index != selectedResource)
                    aspxSchedulerResNavCmd('ctl00_ctl00_ASPxSplitter1_Content_MainContent_ContentSplitter_ASPxScheduler1_resourceNavigatorBlock_ctl00', 'NAVSPECRES', ASPxClientNavBar1.GetSelectedItem().index);
                    
            }
			
			if(ASPxButton1.GetChecked())
			{
				var refreshAction = new ASPxClientSchedulerRefreshAction();
				ASPxButton1.SetChecked(false);
				s.Refresh(refreshAction.None);
			}
}" />
     <Templates>
            
            <VerticalResourceHeaderTemplate>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <dx:ASPxImage runat="server" ImageUrl='<%# "Default.aspx?Image=" + Container.Resource.Id %>'
                                AlternateText='<%# Container.Resource.Caption %>'>
                            </dx:ASPxImage>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%# Container.Resource.Caption %>
                        </td>
                    </tr>
                </table>
            </VerticalResourceHeaderTemplate>
        </Templates>
     
    <OptionsCustomization AllowAppointmentEdit="None" AllowInplaceEditor="None" />

<OptionsBehavior ShowViewSelector="False" ShowViewNavigator="False"></OptionsBehavior>

    <OptionsToolTips ShowAppointmentDragToolTip="False" ShowAppointmentToolTip="False" ShowSelectionToolTip="False" />
    <OptionsView FirstDayOfWeek="Monday" NavigationButtons-Visibility="Never" />

</dxwschs:ASPxScheduler>
                        
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            
        </Panes>
    </dx:ASPxSplitter>


<%-- DXCOMMENT: Configure your datasource for ASPxGridView --%>
<asp:ObjectDataSource ID="appointmentDataSource" runat="server" SelectMethod="SelectMethodHandler" TypeName="WebClient.App_Code.CustomEventDataSource" OnObjectCreated="appointmentsDataSource_ObjectCreated"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetEmployeesList" TypeName="WebClient._Default"></asp:ObjectDataSource>
</asp:Content>