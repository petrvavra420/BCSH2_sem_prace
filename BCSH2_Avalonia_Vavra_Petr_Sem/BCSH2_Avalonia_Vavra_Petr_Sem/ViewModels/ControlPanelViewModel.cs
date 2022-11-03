using BCSH2_Avalonia_Vavra_Petr_Sem.Interfaces;
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
        ICollectionModels _selectedItemList;
        private int _selectedEntity;
        //Výchozí hodnota
        private string _selectedEntityString = "Dìlníci";
        LiteDatabase db;
        public string Greeting => "Nástroje";
        public ObservableCollection<ICollectionModels> Items { get; set; }
        private MainWindowViewModel MainViewModel { get; set; }
        public ICollectionModels ListSelectedItem
        {
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

        public int SelectedEntity
        {
            get { return _selectedEntity; }
            set
            {
                _selectedEntity = value;
                ComboBoxSelectionChanged();
            }
        }


        public ControlPanelViewModel(MainWindowViewModel mainWindowViewModel, LiteDatabase database)
        {
            db = database;
            MainViewModel = mainWindowViewModel;
            Items = new ObservableCollection<ICollectionModels>();

            PridatZaznamCommand = ReactiveCommand.Create(PridejZaznam);
            OdebratZaznamCommand = ReactiveCommand.Create(OdeberZaznam);

            //defaultnì zobrazí dìlníky
            Items.Clear();
            var colDelnik = db.GetCollection<Delnik>("Delnik");
            IEnumerable<Delnik> resultsDelnik = colDelnik.FindAll();

            foreach (Delnik result in resultsDelnik)
            {
                Items.Add(result);
            }
        }

        

        public ReactiveCommand<Unit, Unit> OdebratZaznamCommand { get; }
        public ReactiveCommand<Unit, Unit> PridatZaznamCommand { get; }

        void ComboBoxSelectionChanged()
        {

            switch (_selectedEntity)
            {
                case 0:
                    SelectedEntityString = "Dìlníci";
                    VymazSeznam();
                    var colDelnik = db.GetCollection<Delnik>("Delnik");
                    IEnumerable<Delnik> resultsDelnik = colDelnik.FindAll();
                    //Delnik delnikNew = new Delnik("Jméno dìlníka",ShiftsEnum.SHIFT_A,1,1);
                    //colDelnik.Insert(delnikNew);
                    //colDelnik.EnsureIndex(x => x.DelnikId);
                    foreach (var result in resultsDelnik)
                    {
                        Items.Add(result);
                        System.Diagnostics.Debug.WriteLine(result.ToString());
                    }
                    break;
                case 1:
                    Items.Clear();
                    SelectedEntityString = "Linky";
                    var colLinka = db.GetCollection<Linka>("Linka");
                    //Linka linkaNew = new Linka("Linka 1", ProductsEnum.PRODUCT_4, 1);
                    //colLinka.Insert(linkaNew);
                    //colLinka.EnsureIndex(x => x.LinkaId);
                    IEnumerable<Linka> resultsLinka = colLinka.FindAll();

                    foreach (var result in resultsLinka)
                    {
                        Items.Add(result);
                        System.Diagnostics.Debug.WriteLine(result.ToString());
                    }
                    break;
                case 2:
                    Items.Clear();
                    SelectedEntityString = "Mistøi";
                    var colMistr = db.GetCollection<Mistr>("Mistr");
                    IEnumerable<Mistr> resultsMistr = colMistr.FindAll();
                    //Mistr mistrNew = new Mistr("Jméno Mistra", ShiftsEnum.SHIFT_A, 1);
                    //colMistr.Insert(mistrNew);
                    //colMistr.EnsureIndex(x => x.MistrId);
                    foreach (var result in resultsMistr)
                    {
                        Items.Add(result);
                        System.Diagnostics.Debug.WriteLine(result.ToString());
                    }
                    break;
                case 3:
                    Items.Clear();
                    SelectedEntityString = "Stroje";
                    var colStroj = db.GetCollection<Stroj>("Stroj");
                    IEnumerable<Stroj> resultsStroj = colStroj.FindAll();
                    //Stroj strojNew = new Stroj("Stroj 1", 120, 1);
                    //colStroj.Insert(strojNew);
                    //colStroj.EnsureIndex(x => x.StrojId);

                    foreach (var result in resultsStroj)
                    {
                        Items.Add(result);
                        System.Diagnostics.Debug.WriteLine(result.ToString());
                    }

                    System.Diagnostics.Debug.WriteLine(Items.Count);
                    break;
                case 4:
                    Items.Clear();
                    SelectedEntityString = "Závody";
                    var colZavod = db.GetCollection<Zavod>("Zavod");
                    //Zavod zavodNew = new Zavod("Zavod", new Adress("Zeme1", "Mesto1", "Ulice1", 157, 58945));
                    //colZavod.Insert(zavodNew);
                    //colZavod.EnsureIndex(x => x.ZavodId);
                    IEnumerable<Zavod> resultsZavod = colZavod.FindAll();

                    foreach (var result in resultsZavod)
                    {
                        Items.Add(result);
                        System.Diagnostics.Debug.WriteLine(result.ToString());
                    }
                    break;


                default:
                    break;
            }
        }


        private void OdeberZaznam()
        {
            //Vezme aktuálnì vybranou položku v ListBoxu, rozpozná její typ a smaže ji z DB a ListBoxu
            bool isDeleted = false;
            ICollectionModels polozka = ListSelectedItem;
            if (polozka is Zavod)
            {
                Zavod z = (Zavod)polozka;
                var colZavod = db.GetCollection<Zavod>("Zavod");
                isDeleted = colZavod.Delete(z.ZavodId);
                Items.Remove(ListSelectedItem);
            }
            else if (polozka is Stroj)
            {
                Stroj s = (Stroj)polozka;
                var colStroj = db.GetCollection<Zavod>("Zavod");
                isDeleted = colStroj.Delete(s.StrojId);
                Items.Remove(ListSelectedItem);
            }
            else if (polozka is Mistr)
            {
                Mistr m = (Mistr)polozka;
                var colMistr = db.GetCollection<Mistr>("Mistr");
                isDeleted = colMistr.Delete(m.MistrId);
                Items.Remove(ListSelectedItem);
            }
            else if (polozka is Linka)
            {
                Linka l = (Linka)polozka;
                var colLinka = db.GetCollection<Linka>("Linka");
                isDeleted = colLinka.Delete(l.LinkaId);
                Items.Remove(ListSelectedItem);
            }
            else if (polozka is Delnik)
            {
                Delnik d = (Delnik)polozka;
                var colDelnik = db.GetCollection<Delnik>("Delnik");
                isDeleted = colDelnik.Delete(d.DelnikId);
                Items.Remove(ListSelectedItem);
            }

            if (isDeleted)
            {
                System.Diagnostics.Debug.WriteLine("Záznam smazán");
            }
            else { 
                System.Diagnostics.Debug.WriteLine("Chyba - záznam nemohl být smazán");
            }
        }

        void PridejZaznam()
        {
            //podle toho jaká je vybraná entita v ComboBoxu zavolá metodu Add - ta zmìní view
            Console.WriteLine("COMMAND EXECUTED");
            switch (_selectedEntity)
            {
                case 0:
                    break;
                case 1:
                    MainViewModel.LinkaAddItem();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    MainViewModel.ZavodAddItem();
                    break;
                default:
                    break;
            }
        }

        //obsolete
        private void VymazSeznam()
        {
            while (Items.Count > 0)
            {
                Items.RemoveAt(0);
            }

        }

        public void VlozZaznam(ICollectionModels polozka)
        {
            //v parametru pøevezme položku, rozpozná její typ a podle toho pøidá do urèité tabulky v databázi
            //pozdìji v každé tøídì vytvoøit metodu get type aby se dal použít switch
            if (polozka is Zavod)
            {
                System.Diagnostics.Debug.WriteLine(polozka.ToString());
                var colZavod = db.GetCollection<Zavod>("Zavod");
                colZavod.Insert((Zavod)polozka);
                colZavod.EnsureIndex(x => x.ZavodId);
                Items.Add(polozka);

            }
            else if (polozka is Stroj)
            {

            }
            else if (polozka is Mistr)
            {

            }
            else if (polozka is Linka)
            {
                System.Diagnostics.Debug.WriteLine(polozka.ToString());
                var colLinka = db.GetCollection<Linka>("Linka");
                colLinka.Insert((Linka)polozka);
                colLinka.EnsureIndex(x => x.LinkaId);
                Items.Add(polozka);
            }
            else if (polozka is Delnik)
            {

            }

        }

    }
}
