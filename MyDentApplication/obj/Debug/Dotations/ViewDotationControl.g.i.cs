﻿#pragma checksum "..\..\..\Dotations\ViewDotationControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8536E8D94D18FFB81E5834AC993A7E67"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    /// ViewDotationControl
    /// </summary>
    public partial class ViewDotationControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\Dotations\ViewDotationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyDentApplication.ViewDotationControl UserControl;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\Dotations\ViewDotationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid lrMedicine;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Dotations\ViewDotationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rcColorMedicine;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Dotations\ViewDotationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblDotationTimeCreation;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Dotations\ViewDotationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblDotationAmount;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Dotations\ViewDotationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnViewDotation;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Dotations\ViewDotationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rcPendingDotations;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Dotations\ViewDotationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rcSignedDotations;
        
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
            System.Uri resourceLocater = new System.Uri("/MyDentApplication;component/dotations/viewdotationcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Dotations\ViewDotationControl.xaml"
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
            this.UserControl = ((MyDentApplication.ViewDotationControl)(target));
            return;
            case 2:
            this.lrMedicine = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.rcColorMedicine = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 4:
            this.lblDotationTimeCreation = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.lblDotationAmount = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.btnViewDotation = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\Dotations\ViewDotationControl.xaml"
            this.btnViewDotation.Click += new System.Windows.RoutedEventHandler(this.btnViewDotation_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.rcPendingDotations = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 8:
            this.rcSignedDotations = ((System.Windows.Shapes.Rectangle)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
