﻿#pragma checksum "..\..\Avtorizaciya.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6DC759A98B1574B46A79E42E01FA34F1"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using avto;


namespace avto {
    
    
    /// <summary>
    /// Avtorizaciya
    /// </summary>
    public partial class Avtorizaciya : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\Avtorizaciya.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FamKlient_textbox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Avtorizaciya.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox Pass_box;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\Avtorizaciya.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox ConfPass_Box;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\Avtorizaciya.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameClient_textbox;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\Avtorizaciya.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Otch_klient_Textbox;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Avtorizaciya.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Tel_Value;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\Avtorizaciya.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button2;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\Avtorizaciya.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Login_text;
        
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
            System.Uri resourceLocater = new System.Uri("/avto;component/avtorizaciya.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Avtorizaciya.xaml"
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
            this.FamKlient_textbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.Pass_box = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 3:
            this.ConfPass_Box = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 30 "..\..\Avtorizaciya.xaml"
            this.ConfPass_Box.LostFocus += new System.Windows.RoutedEventHandler(this.Pass_box_LostFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.NameClient_textbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 31 "..\..\Avtorizaciya.xaml"
            this.NameClient_textbox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NameClient_textbox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Otch_klient_Textbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.Tel_Value = ((System.Windows.Controls.TextBox)(target));
            
            #line 33 "..\..\Avtorizaciya.xaml"
            this.Tel_Value.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.Tel_Value_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 7:
            this.button2 = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\Avtorizaciya.xaml"
            this.button2.Click += new System.Windows.RoutedEventHandler(this.button2_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Login_text = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

