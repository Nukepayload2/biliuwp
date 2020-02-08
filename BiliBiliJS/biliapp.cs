﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI.Popups;

/// <summary>
/// 用这个跟B站Web交互
/// </summary>
namespace BiliBiliJS
{
    [AllowForWeb]
    public sealed class biliapp
    {
        public void Alert(string message)
        {
            MessageDialog md;

            md = new MessageDialog(message);

            _ = md.ShowAsync();
        }
        public event EventHandler<string> ValidateLoginEvent;
        public void ValidateLogin(string data)
        {
            if (ValidateLoginEvent != null)
            {
                ValidateLoginEvent(this, data);
            }
        }
        public event EventHandler<string> CloseBrowserEvent;
        public void CloseBrowser()
        {
            if (CloseBrowserEvent != null)
            {
                CloseBrowserEvent(this,"");
            }
        }
    }
}
