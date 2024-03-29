﻿#pragma checksum "..\..\GraphNQueensCanvas.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F807C8A1B96956F5CCC9B7BC51086447D4FF57F59319033807FB2A3DECAFBB1F"
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
using GraphNQueensAlgorithm;
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


namespace GraphNQueensAlgorithm {
    
    
    /// <summary>
    /// GraphNQueensCanvas
    /// </summary>
    public partial class GraphNQueensCanvas : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\GraphNQueensCanvas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stackPanelFather;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\GraphNQueensCanvas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal GraphBaseFramewark.GraphCanvas ucGraphCanvas;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\GraphNQueensCanvas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbQueensNodeCount;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\GraphNQueensCanvas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreateRelNode;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\GraphNQueensCanvas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnReCallBack;
        
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
            System.Uri resourceLocater = new System.Uri("/GraphNQueensAlgorithm;component/graphnqueenscanvas.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\GraphNQueensCanvas.xaml"
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
            this.tbQueensNodeCount = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.btnCreateRelNode = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\GraphNQueensCanvas.xaml"
            this.btnCreateRelNode.Click += new System.Windows.RoutedEventHandler(this.btnCreateRelNode_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnReCallBack = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\GraphNQueensCanvas.xaml"
            this.btnReCallBack.Click += new System.Windows.RoutedEventHandler(this.btnReCallBack_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

