﻿#pragma checksum "..\..\..\Dotations\ManageDotationsWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CB601CE317168DB27D94E764521A1051"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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


namespace MyDentApplication {
    
    
    /// <summary>
    /// ManageDotationsWindow
    /// </summary>
    public partial class ManageDotationsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\Dotations\ManageDotationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyDentApplication.ManageDotationsWindow Window;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Dotations\ManageDotationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Dotations\ManageDotationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgDotations;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Dotations\ManageDotationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteDotation;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Dotations\ManageDotationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEditDotation;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Dotations\ManageDotationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddDotation;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Dotations\ManageDotationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpDotations;
        
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
            System.Uri resourceLocater = new System.Uri("/MyDentApplication;component/dotations/managedotationswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Dotations\ManageDotationsWindow.xaml"
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
            this.Window = ((MyDentApplication.ManageDotationsWindow)(target));
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.dgDotations = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.btnDeleteDotation = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\Dotations\ManageDotationsWindow.xaml"
            this.btnDeleteDotation.Click += new System.Windows.RoutedEventHandler(this.btnDeleteDotation_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnEditDotation = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\Dotations\ManageDotationsWindow.xaml"
            this.btnEditDotation.Click += new System.Windows.RoutedEventHandler(this.btnEditDotation_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnAddDotation = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\Dotations\ManageDotationsWindow.xaml"
            this.btnAddDotation.Click += new System.Windows.RoutedEventHandler(this.btnAddDotation_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.dpDotations = ((System.Windows.Controls.DatePicker)(target));
            
            #line 47 "..\..\..\Dotations\ManageDotationsWindow.xaml"
            this.dpDotations.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.dpDotations_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

