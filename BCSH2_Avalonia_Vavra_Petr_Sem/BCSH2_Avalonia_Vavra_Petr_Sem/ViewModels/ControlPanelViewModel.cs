using BCSH2_Avalonia_Vavra_Petr_Sem.Interfaces;
using BCSH2_Avalonia_Vavra_Petr_Sem.Models;
using BCSH2_Avalonia_Vavra_Petr_Sem.Models.Enums;
using LiteDB;
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
        //V�choz� hodnota
        private string _selectedEntityString = "D�ln�ci";
        private string searchComboBoxSelectedItem;
        LiteDatabase db;
        public string Greeting => "N�stroje";
        public ObservableCollection<ICollectionModels> Items { get; set; }
        public ObservableCollection<string> SearchComboBoxItems { get; set; }
        private MainWindowViewModel MainViewModel { get; set; }

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

            PridatZaznamCommand = ReactiveCommand.Create(PridejZaznam);
            OdebratZaznamCommand = ReactiveCommand.Create(OdeberZaznam);

            //defaultn� zobraz� d�ln�ky
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
                    SelectedEntityString = "D�ln�ci";
                    VymazSeznam();
                    var colDelnik = db.GetCollection<Delnik>("Delnik");
                    IEnumerable<Delnik> resultsDelnik = colDelnik.FindAll();
                    //Delnik delnikNew = new Delnik("Jm�no d�ln�ka",ShiftsEnum.SHIFT_A,1,1);
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
                    SelectedEntityString = "Mist�i";
                    var colMistr = db.GetCollection<Mistr>("Mistr");
                    IEnumerable<Mistr> resultsMistr = colMistr.FindAll();
                    //Mistr mistrNew = new Mistr("Jm�no Mistra", ShiftsEnum.SHIFT_A, 1);
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
                    SelectedEntityString = "Z�vody";
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

        private void ChangeSearchComboBoxItemsBasedOnEntity(EntityEnum entityType)
        {
            SearchComboBoxItems.Clear();
            switch (entityType)
            {
                case EntityEnum.DELNIK:
                    SearchComboBoxItems.Add("V�e");
                    SearchComboBoxItems.Add("D�ln�k ID");
                    SearchComboBoxItems.Add("Jm�no");
                    SearchComboBoxItems.Add("Sm�na");
                    SearchComboBoxItems.Add("Stroj ID");
                    SearchComboBoxItems.Add("Mistr ID");
                    break;
                case EntityEnum.LINKA:
                    SearchComboBoxItems.Add("V�e");
                    SearchComboBoxItems.Add("Linka ID");
                    SearchComboBoxItems.Add("Jm�no");
                    SearchComboBoxItems.Add("Produkt");
                    SearchComboBoxItems.Add("Z�vod ID");
                    break;
                case EntityEnum.MISTR:
                    SearchComboBoxItems.Add("V�e");
                    SearchComboBoxItems.Add("Jm�no");
                    SearchComboBoxItems.Add("Mistr ID");
                    SearchComboBoxItems.Add("Sm�na");
                    SearchComboBoxItems.Add("Linka ID");
                    break;
                case EntityEnum.STROJ:
                    SearchComboBoxItems.Add("V�e");
                    SearchComboBoxItems.Add("Stroj ID");
                    SearchComboBoxItems.Add("N�zev");
                    SearchComboBoxItems.Add("Rychlost");
                    SearchComboBoxItems.Add("Linka ID");
                    break;
                case EntityEnum.ZAVOD:
                    SearchComboBoxItems.Add("V�e");
                    SearchComboBoxItems.Add("Z�vod ID");
                    SearchComboBoxItems.Add("N�zev");
                    SearchComboBoxItems.Add("Adresa");
                    break;
                default:
                    break;
            }
            SearchComboBoxSelectedItem = "V�e";

        }

        private void OdeberZaznam()
        {
            //Vezme aktu�ln� vybranou polo�ku v ListBoxu, rozpozn� jej� typ a sma�e ji z DB a ListBoxu
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
                System.Diagnostics.Debug.WriteLine("Z�znam smaz�n");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Chyba - z�znam nemohl b�t smaz�n");
            }
        }

        //M�n� obsah window
        void PridejZaznam()
        {
            //podle toho jak� je vybran� entita v ComboBoxu zavol� metodu Add - ta zm�n� view
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

        //Vkl�d� z�znam do DB a kolekce
        public void VlozZaznam(ICollectionModels polozka)
        {
            //v parametru p�evezme polo�ku, rozpozn� jej� typ a podle toho p�id� do ur�it� tabulky v datab�zi
            //pozd�ji v ka�d� t��d� vytvo�it metodu get type aby se dal pou��t switch
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
            //vyhled�v�n� podle v�eho
            if (!string.IsNullOrWhiteSpace(SearchBoxText))
            {
                ObservableCollection<ICollectionModels> itemsSearched = new ObservableCollection<ICollectionModels>();
                IEnumerable<ICollectionModels> itemsFound = Items.Where(x => x.ToString().ToLower().Contains(SearchBoxText.ToLower()));
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
            else
            {
                ComboBoxSelectionChanged();
            }
        }




    }
}
