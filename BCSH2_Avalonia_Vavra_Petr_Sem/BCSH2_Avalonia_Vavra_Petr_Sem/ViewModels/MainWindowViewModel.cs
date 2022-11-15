using Avalonia.Controls;
using BCSH2_Avalonia_Vavra_Petr_Sem.Interfaces;
using BCSH2_Avalonia_Vavra_Petr_Sem.Models;
using BCSH2_Avalonia_Vavra_Petr_Sem.ViewModels.Edit;
using LiteDB;
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
        LiteDatabase db;
        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }
        public string Greeting => "Welcome to Avalonia!";

        public MainWindowViewModel()
        {
            db = new LiteDatabase(@"..\..\..\Database\Databasetest.db");
            Content = MainPage = new ControlPanelViewModel(this, db);
        }
        public ControlPanelViewModel MainPage { get; }


        //metody pro pøidávání záznamù
        public void ZavodAddItem()
        {
            var vm = new ZavodAddViewModel();
            //Reactive pøíkazy se dají odposlouchávat a produkují hodnotu kdykoliv jsou spuštìny
            //Merge spojí oba pøíkazy do jednoho streamu, ty musí být stejného typu a proto se musí Cancel pøetypovat
            //Take(1) vezme první hodnotu v proudu který bude buï OK nebo CANCEL
            //Subscribe naslouchá hodnotì proudu a pokud není NULL(což bude pokud se zmáèkne cancel) zavolá metodu VlozZaznam
            //VlozZaznam má v parametru ICollectionModels a rozpozná o jaký typ se jedná a vloží ho do kolekce
            Observable.Merge(vm.Ok, vm.Cancel.Select(_ => (Zavod)null))
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

        public void LinkaAddItem()
        {
            var vm = new LinkaAddViewModel(db);
            Observable.Merge(vm.LinkaOk, vm.LinkaCancel.Select(_ => (Linka)null))
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

        public void StrojAddItem()
        {
            var vm = new StrojAddViewModel(db);
            Observable.Merge(vm.StrojOk, vm.StrojCancel.Select(_ => (Stroj)null))
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

        public void MistrAddItem()
        {
            var vm = new MistrAddViewModel(db);
            Observable.Merge(vm.MistrOk, vm.MistrCancel.Select(_ => (Mistr)null))
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

        public void DelnikAddItem()
        {
            var vm = new DelnikAddViewModel(db);
            Observable.Merge(vm.DelnikOk, vm.DelnikCancel.Select(_ => (Delnik)null))
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

        //metody pro editaci záznamù
        internal void DelnikEditItem(ICollectionModels polozka)
        {
            var vm = new DelnikEditViewModel(db, polozka);
            Observable.Merge(vm.DelnikOk, vm.DelnikCancel.Select(_ => (Delnik)null))
                .Take(1)
                .Subscribe(model =>
                {
                    if (model != null)
                    {
                        MainPage.EditujZaznam(model);
                    }
                    Content = MainPage;
                }
                );
            Content = vm;
        }

        internal void LinkaEditItem(ICollectionModels polozka)
        {
            throw new NotImplementedException();
        }

        internal void MistrEditItem(ICollectionModels polozka)
        {
            var vm = new MistrEditViewModel(db,polozka);
            Observable.Merge(vm.MistrOk, vm.MistrCancel.Select(_ => (Mistr)null))
                .Take(1)
                .Subscribe(model =>
                {
                    if (model != null)
                    {
                        MainPage.EditujZaznam(model);
                    }
                    Content = MainPage;
                }
                );
            Content = vm;
        }

        internal void StrojEditItem(ICollectionModels polozka)
        {
            var vm = new StrojEditViewModel(db,polozka);
            Observable.Merge(vm.StrojOk, vm.StrojCancel.Select(_ => (Stroj)null))
                .Take(1)
                .Subscribe(model =>
                {
                    if (model != null)
                    {
                        MainPage.EditujZaznam(model);
                    }
                    Content = MainPage;
                }
                );
            Content = vm;
        }

        internal void ZavodEditItem(ICollectionModels polozka)
        {
            var vm = new ZavodEditViewModel(db, polozka);
            Observable.Merge(vm.Ok, vm.Cancel.Select(_ => (Zavod)null))
                 .Take(1)
                 .Subscribe(model =>
                 {
                     if (model != null)
                     {
                         MainPage.EditujZaznam(model);
                     }
                     Content = MainPage;
                 }
                 );
            Content = vm;
        }
    }
}
