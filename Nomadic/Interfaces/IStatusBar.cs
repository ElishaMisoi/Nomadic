using System;
using System.Collections.Generic;
using System.Text;

namespace Nomadic.Interfaces
{
    public interface IStatusBar
    {
        void ChangeStatusBarColorToBlack();

        void ChangeStatusBarColorToWhite();

        void HideStatusBar();
    }
}
