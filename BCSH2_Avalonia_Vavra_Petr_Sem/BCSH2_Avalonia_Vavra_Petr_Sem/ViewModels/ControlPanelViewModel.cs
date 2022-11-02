using BCSH2_Avalonia_Vavra_Petr_Sem.Models;
using BCSH2_Avalonia_Vavra_Petr_Sem.Models.Enums;
using LiteDB;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;

namespace BCSH2_Avalonia_Vavra_Petr_Sem.ViewModels
{
    public class ControlPanelViewModel : ViewModelBase
    {
        Object _selectedItemList;
        public Object ListSelectedItem { 
            get => _selectedItemList;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedItemList, value);
                OnListSelectionChange();
            }
        }

        private void OnListSelectionChange()
        {
            //Items.Add(_selectedItemList);
        }

        private string _selectedEntityString = "test";
        public string SelectedEntityString
        {
            get
            {
                return _selectedEntityString;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedEntityString, value);
                _selectedEntityString = value;
            }
        }
        public string Greeting => "Evidence";
        private int _selectedEntity;
        public int SelectedEntity
        {
            get { return _selectedEntity; }
            set
            {
                _selectedEntity = value;
                ComboBoxSelectionChanged();
            }
        }
        LiteDatabase db = new LiteDatabase(@"C:\Temp\Databasetest.db");


        public ControlPanelViewModel()
        {
            Items = new ObservableCollection<Object>();

            PridatZaznamCommand = ReactiveCommand.Create(PridejZaznam);

            //defaultnì zobrazí dìlníky
            Items.Clear();
            var colDelnik = db.GetCollection<Delnik>("Delnik");
            IEnumerable<Delnik> resultsDelnik = colDelnik.FindAll();

            foreach (var result in resultsDelnik)
            {
                Items.Add(result.ToString());
            }
        }
        public ObservableCollection<Object> Items { get; }

        public ReactiveCommand<Unit, Unit> PridatZaznamCommand { get; }

        void ComboBoxSelectionChanged()
        {

            switch (_selectedEntity)
            {
                case 0:
                    SelectedEntityString = "Dìlníci";
                    Items.Clear();
                    var colDelnik = db.GetCollection<Delnik>("Delnik");
                    IEnumerable<Delnik> resultsDelnik = colDelnik.FindAll();
                    //Delnik delnikNew = new Delnik("Jméno dìlníka",ShiftsEnum.SHIFT_A,1,1);
                    //colDelnik.Insert(delnikNew);
                    //colDelnik.EnsureIndex(x => x.DelnikId);
                    foreach (var result in resultsDelnik)
                    {
                        Items.Add(result.ToString());
                    }
                    break;
                case 1:
                    SelectedEntityString = "Linky";
                    Items.Clear();
                    var colLinka = db.GetCollection<Linka>("Linka");
                    //Linka linkaNew = new Linka("Linka 1", ProductsEnum.PRODUCT_4, 1);
                    //colLinka.Insert(linkaNew);
                    //colLinka.EnsureIndex(x => x.LinkaId);
                    IEnumerable<Linka> resultsLinka = colLinka.FindAll();

                    foreach (var result in resultsLinka)
                    {
                        Items.Add(result.ToString());
                    }
                    break;
                case 2:
                    SelectedEntityString = "Mistøi";

                    Items.Clear();
                    var colMistr = db.GetCollection<Mistr>("Mistr");
                    IEnumerable<Mistr> resultsMistr = colMistr.FindAll();
                    //Mistr mistrNew = new Mistr("Jméno Mistra", ShiftsEnum.SHIFT_A, 1);
                    //colMistr.Insert(mistrNew);
                    //colMistr.EnsureIndex(x => x.MistrId);
                    foreach (var result in resultsMistr)
                    {
                        Items.Add(result.ToString());
                    }
                    break;
                case 3:
                    SelectedEntityString = "Stroje";
                    Items.Clear();
                    var colStroj = db.GetCollection<Stroj>("Stroj");
                    IEnumerable<Stroj> resultsStroj = colStroj.FindAll();
                    //Stroj strojNew = new Stroj("Stroj 1", 120, 1);
                    //colStroj.Insert(strojNew);
                    //colStroj.EnsureIndex(x => x.StrojId);

                    foreach (var result in resultsStroj)
                    {
                        Items.Add(result.ToString());
                    }
                    break;
                case 4:
                    SelectedEntityString = "Závody";
                    Items.Clear();
                    var colZavod = db.GetCollection<Zavod>("Zavod");
                    //Zavod zavodNew = new Zavod("Zavod", new Adress("Zeme1", "Mesto1", "Ulice1", 157, 58945));
                    //colZavod.Insert(zavodNew);
                    //colZavod.EnsureIndex(x => x.ZavodId);
                    IEnumerable<Zavod> resultsZavod = colZavod.FindAll();

                    foreach (var result in resultsZavod)
                    {
                        Items.Add(result.ToString());
                    }
                    break;


                default:
                    break;
            }
        }
        void PridejZaznam()
        {
            Console.WriteLine("COMMAND EXECUTED");
            SelectedEntity = 420;
            Console.WriteLine("COMMAND EXECUTED");
            Items.Add("Pridano1");
        }

    }
}
