using Avalonia.Controls;
using BCSH2_Avalonia_Vavra_Petr_Sem.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace BCSH2_Avalonia_Vavra_Petr_Sem.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase content;
        public ViewModelBase Content { 
            get=> content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }
        public string Greeting => "Welcome to Avalonia!";

        public MainWindowViewModel()
        {
            Content = MainPage = new ControlPanelViewModel(this);
        }
        public ControlPanelViewModel MainPage { get; }

        public void ZavodAddItem() {
            var vm = new ZavodAddViewModel();
            Observable.Merge(vm.Ok,vm.Cancel.Select(_ => (Zavod)null))
                .Take(1)
                .Subscribe(model =>
                    {
                        if (model != null)
                        {
                            MainPage.VlozZaznam(model);
                        }
                        Content = MainPage;
                    }
                );
            Content = vm;
        }
    }
}
