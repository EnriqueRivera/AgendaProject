﻿#pragma checksum "..\..\..\Banks\ManageBanksWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1324B25433FAB686F6FC026EB8821A49"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
    /// ManageBanksWindow
    /// </summary>
    public partial class ManageBanksWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 5 "..\..\..\Banks\ManageBanksWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyDentApplication.ManageBanksWindow Window;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\Banks\ManageBanksWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\Banks\ManageBanksWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgBanks;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Banks\ManageBanksWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteBank;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Banks\ManageBanksWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEditBank;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Banks\ManageBanksWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddBank;
        
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
            System.Uri resourceLocater = new System.Uri("/MyDentApplication;component/banks/managebankswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Banks\ManageBanksWindow.xaml"
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
            this.Window = ((MyDentApplication.ManageBanksWindow)(target));
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.dgBanks = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.btnDeleteBank = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\Banks\ManageBanksWindow.xaml"
            this.btnDeleteBank.Click += new System.Windows.RoutedEventHandler(this.btnDeleteBank_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnEditBank = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\Banks\ManageBanksWindow.xaml"
            this.btnEditBank.Click += new System.Windows.RoutedEventHandler(this.btnEditBank_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnAddBank = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\Banks\ManageBanksWindow.xaml"
            this.btnAddBank.Click += new System.Windows.RoutedEventHandler(this.btnAddBank_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

