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
            //Reactive p��kazy se daj� odposlouch�vat a produkuj� hodnotu kdykoliv jsou spu�t�ny
            //Merge spoj� oba p��kazy do jednoho streamu, ty mus� b�t stejn�ho typu a proto se mus� Cancel p�etypovat
            //Take(1) vezme prvn� hodnotu v proudu kter� bude bu� OK nebo CANCEL
            //Subscribe naslouch� hodnot� proudu a pokud nen� NULL(co� bude pokud se zm��kne cancel) zavol� metodu VlozZaznam
            //VlozZaznam m� v parametru ICollectionModels a rozpozn� o jak� typ se jedn� a vlo�� ho do kolekce
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
