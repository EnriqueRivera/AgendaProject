﻿#pragma checksum "..\..\..\Inventories\ManageInstrumentsWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3906AFBBB4E95E33D862B4EEC249F8A1"
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
    /// ManageInstrumentsWindow
    /// </summary>
    public partial class ManageInstrumentsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyDentApplication.ManageInstrumentsWindow Window;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgInstruments;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteInstrument;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEditInstrument;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddInstrument;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.DateTimeUpDown dtudSelectedMonth;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRefresh;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSignature;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblSelectedMonth;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddEditRevision;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblSignature;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblSignature1;
        
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
            System.Uri resourceLocater = new System.Uri("/MyDentApplication;component/inventories/manageinstrumentswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
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
            this.Window = ((MyDentApplication.ManageInstrumentsWindow)(target));
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.dgInstruments = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.btnDeleteInstrument = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
            this.btnDeleteInstrument.Click += new System.Windows.RoutedEventHandler(this.btnDeleteInstrument_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnEditInstrument = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
            this.btnEditInstrument.Click += new System.Windows.RoutedEventHandler(this.btnEditInstrument_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnAddInstrument = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
            this.btnAddInstrument.Click += new System.Windows.RoutedEventHandler(this.btnAddInstrument_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.dtudSelectedMonth = ((Xceed.Wpf.Toolkit.DateTimeUpDown)(target));
            return;
            case 8:
            this.btnRefresh = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
            this.btnRefresh.Click += new System.Windows.RoutedEventHandler(this.btnRefresh_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnSignature = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
            this.btnSignature.Click += new System.Windows.RoutedEventHandler(this.btnSignature_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.lblSelectedMonth = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.btnAddEditRevision = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\Inventories\ManageInstrumentsWindow.xaml"
            this.btnAddEditRevision.Click += new System.Windows.RoutedEventHandler(this.btnAddEditRevision_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.lblSignature = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            this.lblSignature1 = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

