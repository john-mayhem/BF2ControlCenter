﻿using System;
using System.Windows.Forms;

class PageControl : TabControl
{
    /// <summary>
    /// Represents a Tab Control without displaying the Tabs at the top
    /// </summary>
    /// <see cref="http://stackoverflow.com/questions/4405613/winforms-tab-control-question"/>
    /// <param name="m"></param>
    protected override void WndProc(ref Message m)
    {
        // Hide tabs by trapping the TCM_ADJUSTRECT message
        if (m.Msg == 0x1328 && !DesignMode)
            m.Result = (IntPtr)1;
        else
            base.WndProc(ref m);
    }
}
