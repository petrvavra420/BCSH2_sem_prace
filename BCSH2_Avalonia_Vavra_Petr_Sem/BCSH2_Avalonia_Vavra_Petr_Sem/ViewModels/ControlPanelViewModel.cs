using BCSH2_Avalonia_Vavra_Petr_Sem.Interfaces;
using BCSH2_Avalonia_Vavra_Petr_Sem.Models;
using BCSH2_Avalonia_Vavra_Petr_Sem.Models.Enums;
using DynamicData;
using LiteDB;
using Microsoft.CodeAnalysis;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;

namespace BCSH2_Avalonia_Vavra_Petr_Sem.ViewModels
{
    public class ControlPanelViewModel : ViewModelBase
    {
        private string searchBoxText;
        ICollectionModels _selectedItemList;
        private int _selectedEntity;
        //Výchozí hodnota
        private string _selectedEntityString = "Dìlníci";
        private string searchComboBoxSelectedItem;
        LiteDatabase db;
        public string Greeting => "Nástroje";
        private bool deleteButtonEnable;
        public ObservableCollection<ICollectionModels> Items { get; set; }
        public ObservableCollection<string> SearchComboBoxItems { get; set; }
        private MainWindowViewModel MainViewModel { get; set; }

        public bool DeleteButtonEnable
        {
            get => deleteButtonEnable;
            set => this.RaiseAndSetIfChanged(ref deleteButtonEnable, value);
        }

        public string SearchComboBoxSelectedItem
        {
            get => searchComboBoxSelectedItem;
            set
            {
                this.RaiseAndSetIfChanged(ref searchComboBoxSelectedItem, value);
                OnSearchComboBoxChanged();
            }
        }

        private void OnSearchComboBoxChanged()
        {
        }

        public string SearchBoxText
        {
            get => searchBoxText;
            set
            {
                this.RaiseAndSetIfChanged(ref searchBoxText, value);
                OnSearchInputChange();
            }
        }

        public ICollectionModels ListSelectedItem
        {
            get => _selectedItemList;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedItemList, value);
                OnListSelectionChange();
                if (value != null)
                {
                    DeleteButtonEnable = true;
                }
                else
                {
                    DeleteButtonEnable = false;
                }
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
            SearchComboBoxItems = new ObservableCollection<string>();
            ChangeSearchComboBoxItemsBasedOnEntity(EntityEnum.DELNIK);
            DeleteButtonEnable = false;

            PridatZaznamCommand = ReactiveCommand.Create(PridejZaznam);
            OdebratZaznamCommand = ReactiveCommand.Create(OdeberZaznam);
            EditovatZaznamCommand = ReactiveCommand.Create(EditujZaznam);

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
        public ReactiveCommand<Unit, Unit> EditovatZaznamCommand { get; }

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
                    ChangeSearchComboBoxItemsBasedOnEntity(EntityEnum.DELNIK);
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
                    ChangeSearchComboBoxItemsBasedOnEntity(EntityEnum.LINKA);
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
                    ChangeSearchComboBoxItemsBasedOnEntity(EntityEnum.MISTR);
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
                    ChangeSearchComboBoxItemsBasedOnEntity(EntityEnum.STROJ);
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
                    ChangeSearchComboBoxItemsBasedOnEntity(EntityEnum.ZAVOD);
                    break;


                default:
                    break;
            }
        }

        void UpdateData()
        {

            switch (_selectedEntity)
            {
                case 0:
                    SelectedEntityString = "Dìlníci";
                    VymazSeznam();
                    var colDelnik = db.GetCollection<Delnik>("Delnik");
                    IEnumerable<Delnik> resultsDelnik = colDelnik.FindAll();
                    foreach (var result in resultsDelnik)
                    {
                        Items.Add(result);
                    }
                    break;
                case 1:
                    Items.Clear();
                    SelectedEntityString = "Linky";
                    var colLinka = db.GetCollection<Linka>("Linka");
                    IEnumerable<Linka> resultsLinka = colLinka.FindAll();
                    foreach (var result in resultsLinka)
                    {
                        Items.Add(result);
                    }
                    break;
                case 2:
                    Items.Clear();
                    SelectedEntityString = "Mistøi";
                    var colMistr = db.GetCollection<Mistr>("Mistr");
                    IEnumerable<Mistr> resultsMistr = colMistr.FindAll();

                    foreach (var result in resultsMistr)
                    {
                        Items.Add(result);
                    }
                    break;
                case 3:
                    Items.Clear();
                    SelectedEntityString = "Stroje";
                    var colStroj = db.GetCollection<Stroj>("Stroj");
                    IEnumerable<Stroj> resultsStroj = colStroj.FindAll();
                    foreach (var result in resultsStroj)
                    {
                        Items.Add(result);
                    }
                    break;
                case 4:
                    Items.Clear();
                    SelectedEntityString = "Závody";
                    var colZavod = db.GetCollection<Zavod>("Zavod");
                    IEnumerable<Zavod> resultsZavod = colZavod.FindAll();
                    foreach (var result in resultsZavod)
                    {
                        Items.Add(result);
                    }
                    break;
                default:
                    break;
            }
        }

        private void ChangeSearchComboBoxItemsBasedOnEntity(EntityEnum entityType)
        {
            SearchComboBoxItems.Clear();
            switch (entityType)
            {
                case EntityEnum.DELNIK:
                    SearchComboBoxItems.Add("Vše");
                    SearchComboBoxItems.Add("Dìlník ID");
                    SearchComboBoxItems.Add("Jméno");
                    SearchComboBoxItems.Add("Smìna");
                    SearchComboBoxItems.Add("Stroj ID");
                    SearchComboBoxItems.Add("Mistr ID");
                    break;
                case EntityEnum.LINKA:
                    SearchComboBoxItems.Add("Vše");
                    SearchComboBoxItems.Add("Linka ID");
                    SearchComboBoxItems.Add("Jméno");
                    SearchComboBoxItems.Add("Produkt");
                    SearchComboBoxItems.Add("Závod ID");
                    break;
                case EntityEnum.MISTR:
                    SearchComboBoxItems.Add("Vše");
                    SearchComboBoxItems.Add("Jméno");
                    SearchComboBoxItems.Add("Mistr ID");
                    SearchComboBoxItems.Add("Smìna");
                    SearchComboBoxItems.Add("Linka ID");
                    break;
                case EntityEnum.STROJ:
                    SearchComboBoxItems.Add("Vše");
                    SearchComboBoxItems.Add("Stroj ID");
                    SearchComboBoxItems.Add("Název");
                    SearchComboBoxItems.Add("Rychlost");
                    SearchComboBoxItems.Add("Linka ID");
                    break;
                case EntityEnum.ZAVOD:
                    SearchComboBoxItems.Add("Vše");
                    SearchComboBoxItems.Add("Závod ID");
                    SearchComboBoxItems.Add("Název");
                    SearchComboBoxItems.Add("Adresa");
                    break;
                default:
                    break;
            }
            SearchComboBoxSelectedItem = "Vše";

        }
        private void EditujZaznam()
        {
            System.Diagnostics.Debug.WriteLine("edituj!");
            //System.Diagnostics.Debug.WriteLine(ListSelectedItem.ToString());
            ICollectionModels polozka = ListSelectedItem;
            switch (_selectedEntity)
            {
                case 0:
                    MainViewModel.DelnikEditItem(polozka);
                    break;
                case 1:
                    MainViewModel.LinkaEditItem(polozka);
                    break;
                case 2:
                    MainViewModel.MistrEditItem(polozka);
                    break;
                case 3:
                    MainViewModel.StrojEditItem(polozka);
                    break;
                case 4:
                    MainViewModel.ZavodEditItem(polozka);
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
                var colStroj = db.GetCollection<Stroj>("Stroj");
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
            else
            {
                System.Diagnostics.Debug.WriteLine("Chyba - záznam nemohl být smazán");
            }
        }

        //Mìní obsah window
        void PridejZaznam()
        {
            //podle toho jaká je vybraná entita v ComboBoxu zavolá metodu Add - ta zmìní view
            Console.WriteLine("COMMAND EXECUTED");
            switch (_selectedEntity)
            {
                case 0:
                    MainViewModel.DelnikAddItem();
                    break;
                case 1:
                    MainViewModel.LinkaAddItem();
                    break;
                case 2:
                    MainViewModel.MistrAddItem();
                    break;
                case 3:
                    MainViewModel.StrojAddItem();
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

        //Vkládá záznam do DB a kolekce
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
                System.Diagnostics.Debug.WriteLine(polozka.ToString());
                var colStroj = db.GetCollection<Stroj>("Stroj");
                colStroj.Insert((Stroj)polozka);
                colStroj.EnsureIndex(x => x.StrojId);
                Items.Add(polozka);
            }
            else if (polozka is Mistr)
            {
                System.Diagnostics.Debug.WriteLine(polozka.ToString());
                var colMistr = db.GetCollection<Mistr>("Mistr");
                colMistr.Insert((Mistr)polozka);
                colMistr.EnsureIndex(x => x.MistrId);
                Items.Add(polozka);
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
                System.Diagnostics.Debug.WriteLine(polozka.ToString());
                var colDelnik = db.GetCollection<Delnik>("Delnik");
                colDelnik.Insert((Delnik)polozka);
                colDelnik.EnsureIndex(x => x.DelnikId);
                Items.Add(polozka);
            }

        }

        private void OnSearchInputChange()
        {
            System.Diagnostics.Debug.WriteLine("Typed: " + SearchBoxText);
            if (!string.IsNullOrWhiteSpace(SearchBoxText))
            {
                switch (_selectedEntity)
                {
                    case 0:
                        DelnikSearch();
                        break;
                    case 1:
                        LinkaSearch();
                        break;
                    case 2:
                        MistrSearch();
                        break;
                    case 3:
                        StrojSearch();
                        break;
                    case 4:
                        ZavodSearch();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                UpdateData();
            }
        }

        //Metody pro vyhledávání - každá metoda si vytvoøí pomocnou pøetypovanou kolekci a vloží do ní všechny záznamy
        //Potom zjistí obsah ComboBoxu a podle toho do itemsFound vloží data
        //Smaže data z hlavní kolekce Items a vloží do ní nalezené záznamy
        private void ZavodSearch()
        {
            UpdateData();
            ObservableCollection<Zavod> itemsSearched = new ObservableCollection<Zavod>();
            ObservableCollection<Zavod> itemsCopy = new ObservableCollection<Zavod>();

            for (int i = 0; i < Items.Count; i++)
            {
                itemsCopy.Add((Zavod)Items[i]);
            }
            IEnumerable<Zavod> itemsFound = itemsCopy.Where(x => x.ToString().ToLower().Contains(SearchBoxText.ToLower())); ;

            switch (SearchComboBoxSelectedItem)
            {
                case "Závod ID":
                    itemsFound = itemsCopy.Where(x => x.ZavodId.ToString().ToLower().Contains(SearchBoxText.ToLower()));
                    break;
                case "Název":
                    itemsFound = itemsCopy.Where(x => x.Name.ToString().ToLower().Contains(SearchBoxText.ToLower()));
                    break;
                case "Adresa":
                    itemsFound = itemsCopy.Where(x => x.Adress.ToString().Contains(SearchBoxText));
                    break;
                default:
                    break;
            }
            itemsSearched.Clear();
            foreach (var item in itemsFound)
            {
                itemsSearched.Add(item);
            }
            Items.Clear();
            for (int i = 0; i < itemsSearched.Count; i++)
            {
                Items.Add(itemsSearched[i]);
            }
        }

        private void StrojSearch()
        {
            UpdateData();
            ObservableCollection<Stroj> itemsSearched = new ObservableCollection<Stroj>();
            ObservableCollection<Stroj> itemsCopy = new ObservableCollection<Stroj>();

            for (int i = 0; i < Items.Count; i++)
            {
                itemsCopy.Add((Stroj)Items[i]);
            }
            IEnumerable<Stroj> itemsFound = itemsCopy.Where(x => x.ToString().ToLower().Contains(SearchBoxText.ToLower())); ;

            switch (SearchComboBoxSelectedItem)
            {
                case "Stroj ID":
                    itemsFound = itemsCopy.Where(x => x.StrojId.ToString().ToLower().Contains(SearchBoxText.ToLower()));
                    break;
                case "Název":
                    itemsFound = itemsCopy.Where(x => x.Name.ToString().ToLower().Contains(SearchBoxText.ToLower()));
                    break;
                case "Rychlost":
                    itemsFound = itemsCopy.Where(x => x.ManufacturingSpeed.ToString().Contains(SearchBoxText));
                    break;
                case "Linka ID":
                    itemsFound = itemsCopy.Where(x => x.LinkaId.ToString().Contains(SearchBoxText));
                    break;
                default:
                    break;
            }
            itemsSearched.Clear();
            foreach (var item in itemsFound)
            {
                itemsSearched.Add(item);
            }
            Items.Clear();
            for (int i = 0; i < itemsSearched.Count; i++)
            {
                Items.Add(itemsSearched[i]);
            }
        }

        private void MistrSearch()
        {
            UpdateData();
            ObservableCollection<Mistr> itemsSearched = new ObservableCollection<Mistr>();
            ObservableCollection<Mistr> itemsCopy = new ObservableCollection<Mistr>();

            for (int i = 0; i < Items.Count; i++)
            {
                itemsCopy.Add((Mistr)Items[i]);
            }
            IEnumerable<Mistr> itemsFound = itemsCopy.Where(x => x.ToString().ToLower().Contains(SearchBoxText.ToLower())); ;

            switch (SearchComboBoxSelectedItem)
            {
                case "Mistr ID":
                    itemsFound = itemsCopy.Where(x => x.MistrId.ToString().ToLower().Contains(SearchBoxText.ToLower()));
                    break;
                case "Jméno":
                    itemsFound = itemsCopy.Where(x => x.Name.ToString().ToLower().Contains(SearchBoxText.ToLower()));
                    break;
                case "Smìna":
                    itemsFound = itemsCopy.Where(x => x.Shift.ToString().Contains(SearchBoxText));
                    break;
                case "Linka ID":
                    itemsFound = itemsCopy.Where(x => x.LinkaId.ToString().Contains(SearchBoxText));
                    break;
                default:
                    break;
            }
            itemsSearched.Clear();
            foreach (var item in itemsFound)
            {
                itemsSearched.Add(item);
            }
            Items.Clear();
            for (int i = 0; i < itemsSearched.Count; i++)
            {
                Items.Add(itemsSearched[i]);
            }
        }

        private void LinkaSearch()
        {
            UpdateData();
            ObservableCollection<Linka> itemsSearched = new ObservableCollection<Linka>();
            ObservableCollection<Linka> itemsCopy = new ObservableCollection<Linka>();

            for (int i = 0; i < Items.Count; i++)
            {
                itemsCopy.Add((Linka)Items[i]);
            }
            IEnumerable<Linka> itemsFound = itemsCopy.Where(x => x.ToString().ToLower().Contains(SearchBoxText.ToLower())); ;

            switch (SearchComboBoxSelectedItem)
            {
                case "Linka ID":
                    itemsFound = itemsCopy.Where(x => x.LinkaId.ToString().ToLower().Contains(SearchBoxText.ToLower()));
                    break;
                case "Jméno":
                    itemsFound = itemsCopy.Where(x => x.Name.ToString().ToLower().Contains(SearchBoxText.ToLower()));
                    break;
                case "Produkt":
                    itemsFound = itemsCopy.Where(x => x.Product.ToString().Contains(SearchBoxText));
                    break;
                case "Závod ID":
                    itemsFound = itemsCopy.Where(x => x.ZavodId.ToString().Contains(SearchBoxText));
                    break;
                default:
                    break;
            }
            itemsSearched.Clear();
            foreach (var item in itemsFound)
            {
                itemsSearched.Add(item);
            }
            Items.Clear();
            for (int i = 0; i < itemsSearched.Count; i++)
            {
                Items.Add(itemsSearched[i]);
            }
        }

        private void DelnikSearch()
        {
            UpdateData();
            ObservableCollection<Delnik> itemsSearched = new ObservableCollection<Delnik>();
            ObservableCollection<Delnik> itemsCopy = new ObservableCollection<Delnik>();

            for (int i = 0; i < Items.Count; i++)
            {
                itemsCopy.Add((Delnik)Items[i]);
            }
            //Hledání podle všeho
            IEnumerable<Delnik> itemsFound = itemsCopy.Where(x => x.ToString().ToLower().Contains(SearchBoxText.ToLower())); ;

            switch (SearchComboBoxSelectedItem)
            {

                case "Jméno":
                    itemsFound = itemsCopy.Where(x => x.Name.ToString().ToLower().Contains(SearchBoxText.ToLower()));
                    break;
                case "Dìlník ID":
                    itemsFound = itemsCopy.Where(x => x.DelnikId.ToString().Contains(SearchBoxText));
                    break;
                case "Smìna":
                    itemsFound = itemsCopy.Where(x => x.Shift.ToString().Contains(SearchBoxText));
                    break;
                case "Stroj ID":
                    itemsFound = itemsCopy.Where(x => x.StrojId.ToString().Contains(SearchBoxText));
                    break;
                case "Mistr ID":
                    itemsFound = itemsCopy.Where(x => x.MistrId.ToString().Contains(SearchBoxText));
                    break;

                default:
                    break;
            }
            itemsSearched.Clear();
            foreach (var item in itemsFound)
            {
                itemsSearched.Add(item);
            }
            Items.Clear();
            for (int i = 0; i < itemsSearched.Count; i++)
            {
                Items.Add(itemsSearched[i]);
            }
        }

        internal void EditujZaznam(ICollectionModels polozka)
        {
            if (polozka is Zavod)
            {
                System.Diagnostics.Debug.WriteLine(polozka.ToString());
                var colZavod = db.GetCollection<Zavod>("Zavod");
                colZavod.Update((Zavod)polozka);
                UpdateData();
            }
            else if (polozka is Stroj)
            {
                System.Diagnostics.Debug.WriteLine(polozka.ToString());
                var colStroj = db.GetCollection<Stroj>("Stroj");
                colStroj.Update((Stroj)polozka);
                UpdateData();
            }
            else if (polozka is Mistr)
            {
                System.Diagnostics.Debug.WriteLine(polozka.ToString());
                var colMistr = db.GetCollection<Mistr>("Mistr");
                colMistr.Insert((Mistr)polozka);
                colMistr.EnsureIndex(x => x.MistrId);
                Items.Add(polozka);
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
                System.Diagnostics.Debug.WriteLine(polozka.ToString() + "Aktualizace");
                var colDelnik = db.GetCollection<Delnik>("Delnik");
                colDelnik.Update((Delnik)polozka);
                UpdateData();
            }
        }
    }
}
