﻿#pragma checksum "..\..\..\..\Invoices\Total\TotalInvoicesWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "707C5EDCC98D922838248F35F5F30BE1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MyDentApplication;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace MyDentApplication {
    
    
    /// <summary>
    /// TotalInvoicesWindow
    /// </summary>
    public partial class TotalInvoicesWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\..\Invoices\Total\TotalInvoicesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyDentApplication.TotalInvoicesWindow Window;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\Invoices\Total\TotalInvoicesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Invoices\Total\TotalInvoicesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgReceivedInvoices;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Invoices\Total\TotalInvoicesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.DateTimeUpDown dtudSelectedMonth;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\Invoices\Total\TotalInvoicesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblTotalMonth;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Invoices\Total\TotalInvoicesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgOutgoingInvoices;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\Invoices\Total\TotalInvoicesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRefreshInvoices;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\..\Invoices\Total\TotalInvoicesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnExportToPdf;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MyDentApplication;component/invoices/total/totalinvoiceswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Invoices\Total\TotalInvoicesWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Window = ((MyDentApplication.TotalInvoicesWindow)(target));
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.dgReceivedInvoices = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.dtudSelectedMonth = ((Xceed.Wpf.Toolkit.DateTimeUpDown)(target));
            return;
            case 5:
            this.lblTotalMonth = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.dgOutgoingInvoices = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.btnRefreshInvoices = ((System.Windows.Controls.Button)(target));
            
            #line 81 "..\..\..\..\Invoices\Total\TotalInvoicesWindow.xaml"
            this.btnRefreshInvoices.Click += new System.Windows.RoutedEventHandler(this.btnRefreshInvoices_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnExportToPdf = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\..\..\Invoices\Total\TotalInvoicesWindow.xaml"
            this.btnExportToPdf.Click += new System.Windows.RoutedEventHandler(this.btnExportToPdf_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

