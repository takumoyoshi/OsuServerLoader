﻿#pragma checksum "C:\Users\klonerovsky\VS projects\OsuServerLoader\OsuServerLoader\Pages\AccountsDialogContent.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "16416077C02F472FCDFA8A60BE15650C95636AF39E97D558115D6A2817C85E6B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OkayuLoader.Pages
{
    partial class AccountsDialogContent : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Pages\AccountsDialogContent.xaml line 12
                {
                    this.TextBoxAccountTag = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.TextBox)this.TextBoxAccountTag).SelectionChanged += this.TextBoxAccountTagHandler;
                }
                break;
            case 3: // Pages\AccountsDialogContent.xaml line 16
                {
                    this.TextBoxAccountName = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.TextBox)this.TextBoxAccountName).SelectionChanged += this.TextBoxAccountNameHandler;
                }
                break;
            case 4: // Pages\AccountsDialogContent.xaml line 20
                {
                    this.PasswordBoxAccountPassword = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.PasswordBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.PasswordBox)this.PasswordBoxAccountPassword).PasswordChanged += this.PasswordBoxAccountPasswordHandler;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

