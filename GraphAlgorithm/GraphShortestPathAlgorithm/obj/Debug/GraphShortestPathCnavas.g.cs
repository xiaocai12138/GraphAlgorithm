﻿#pragma checksum "..\..\GraphShortestPathCnavas.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "89241A95BAC2714E85E9EBEDD3EDFD49A197DBB941E1263C4ED1BFC960C7AEBD"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using GraphBaseFramewark;
using GraphShortestPathAlgorithm;
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


namespace GraphShortestPathAlgorithm {
    
    
    /// <summary>
    /// GraphShortestPathCnavas
    /// </summary>
    public partial class GraphShortestPathCnavas : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\GraphShortestPathCnavas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stackPanelFather;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\GraphShortestPathCnavas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal GraphBaseFramewark.GraphCanvas ucGraphCanvas;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\GraphShortestPathCnavas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreateRelNode;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\GraphShortestPathCnavas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnIterationLocation;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\GraphShortestPathCnavas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbNodeCount;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\GraphShortestPathCnavas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbRelCount;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\GraphShortestPathCnavas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbIterationCount;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\GraphShortestPathCnavas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbShowIterationIndex;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\GraphShortestPathCnavas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cbShowRelNode;
        
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
            System.Uri resourceLocater = new System.Uri("/GraphShortestPathAlgorithm;component/graphshortestpathcnavas.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\GraphShortestPathCnavas.xaml"
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
            this.stackPanelFather = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.ucGraphCanvas = ((GraphBaseFramewark.GraphCanvas)(target));
            return;
            case 3:
            this.btnCreateRelNode = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\GraphShortestPathCnavas.xaml"
            this.btnCreateRelNode.Click += new System.Windows.RoutedEventHandler(this.btnCreateRelNode_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnIterationLocation = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\GraphShortestPathCnavas.xaml"
            this.btnIterationLocation.Click += new System.Windows.RoutedEventHandler(this.btnIterationLocation_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbNodeCount = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.tbRelCount = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.tbIterationCount = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.tbShowIterationIndex = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.cbShowRelNode = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
