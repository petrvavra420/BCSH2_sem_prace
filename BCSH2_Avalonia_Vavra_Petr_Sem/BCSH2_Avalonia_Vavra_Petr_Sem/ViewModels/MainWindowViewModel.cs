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

        public void ZavodAddItem()
        {
            var vm = new ZavodAddViewModel();
            //Reactive pøíkazy se dají odposlouchávat a produkují hodnotu kdykoliv jsou spuštìny
            //Merge spojí oba pøíkazy do jednoho streamu, ty musí být stejného typu a proto se musí Cancel pøetypovat
            //Take(1) vezme první hodnotu v proudu který bude buï OK nebo CANCEL
            //Subscribe naslouchá hodnotì proudu a pokud není NULL(což bude pokud se zmáèkne cancel) zavolá metodu VlozZaznam
            //VlozZaznam má v parametru ICollectionModels a rozpozná o jaký typ se jedná a vloží ho do kolekce
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
