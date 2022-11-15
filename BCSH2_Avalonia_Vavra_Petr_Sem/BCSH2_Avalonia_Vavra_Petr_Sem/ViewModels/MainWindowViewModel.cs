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


        //metody pro p�id�v�n� z�znam�
        public void ZavodAddItem()
        {
            var vm = new ZavodAddViewModel();
            //Reactive p��kazy se daj� odposlouch�vat a produkuj� hodnotu kdykoliv jsou spu�t�ny
            //Merge spoj� oba p��kazy do jednoho streamu, ty mus� b�t stejn�ho typu a proto se mus� Cancel p�etypovat
            //Take(1) vezme prvn� hodnotu v proudu kter� bude bu� OK nebo CANCEL
            //Subscribe naslouch� hodnot� proudu a pokud nen� NULL(co� bude pokud se zm��kne cancel) zavol� metodu VlozZaznam
            //VlozZaznam m� v parametru ICollectionModels a rozpozn� o jak� typ se jedn� a vlo�� ho do kolekce
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

        //metody pro editaci z�znam�
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
