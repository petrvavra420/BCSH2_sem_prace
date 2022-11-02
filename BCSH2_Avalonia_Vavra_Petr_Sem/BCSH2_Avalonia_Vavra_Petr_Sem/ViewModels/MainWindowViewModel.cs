using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCSH2_Avalonia_Vavra_Petr_Sem.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public MainWindowViewModel()
        {
            WindowContent = new ControlPanelViewModel();
        }
        public ControlPanelViewModel WindowContent { get; }
    }
}
