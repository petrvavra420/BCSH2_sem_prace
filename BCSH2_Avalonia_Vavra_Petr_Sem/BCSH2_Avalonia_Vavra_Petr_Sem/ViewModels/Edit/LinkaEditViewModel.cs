using BCSH2_Avalonia_Vavra_Petr_Sem.Interfaces;
using BCSH2_Avalonia_Vavra_Petr_Sem.Models;
using LiteDB;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Avalonia_Vavra_Petr_Sem.ViewModels.Edit
{
    public class LinkaEditViewModel : ViewModelBase
    {
        //Kolekce na které se bindují data ComboBoxů
        public ObservableCollection<ProductsEnum> LinkaAddItems { get; set; }
        public ObservableCollection<Zavod> LinkaZavodItems { get; set; }

        private int idLinka;

        //parametry pro vytvoření nové linky
        private string linkaName;
        private ProductsEnum linkaProductSelectedItem;
        private Zavod linkaZavodSelectedItem;
        public ProductsEnum LinkaProductSelectedItem
        {
            get => linkaProductSelectedItem;
            set => this.RaiseAndSetIfChanged(ref linkaProductSelectedItem, value);
        }
        public Zavod LinkaZavodSelectedItem
        {
            get => linkaZavodSelectedItem;
            set => this.RaiseAndSetIfChanged(ref linkaZavodSelectedItem, value);
        }

        public string LinkaName
        {
            get => linkaName;
            set => this.RaiseAndSetIfChanged(ref linkaName, value);
        }

        public ReactiveCommand<Unit, Linka> LinkaOk { get; }
        public ReactiveCommand<Unit, Unit> LinkaCancel { get; }

        public LinkaEditViewModel(LiteDatabase db, ICollectionModels polozka)
        {
            Linka linka = (Linka)polozka;
            idLinka = linka.LinkaId;

           

            //inicializuje kolekce pro ComboBoxy s produkty a závody
            LinkaAddItems = new ObservableCollection<ProductsEnum>();
            foreach (ProductsEnum item in ProductsEnum.GetValues(typeof(ProductsEnum)))
            {
                LinkaAddItems.Add(item);
            }
            LinkaZavodItems = new ObservableCollection<Zavod>();
            var colZavod = db.GetCollection<Zavod>("Zavod");
            IEnumerable<Zavod> resultsLinka = colZavod.FindAll();

            foreach (var result in resultsLinka)
            {
                LinkaZavodItems.Add(result);
                System.Diagnostics.Debug.WriteLine(result.ToString());
            }

            //nastaví výchozí hodnoty dle vybrané položky
            LinkaName = linka.Name;
            LinkaProductSelectedItem = linka.Product;
            LinkaZavodSelectedItem = LinkaZavodItems.Where(x => x.ZavodId == linka.ZavodId).FirstOrDefault();

            //kontrola jestli mají všechny pole zadané data, předává se jako druhý parametr příkazu LinkaOk(true/false)
            var okEnabled = this.WhenAnyValue(
                   x => x.LinkaName, x => x.LinkaProductSelectedItem, x => x.LinkaZavodSelectedItem,
                   (name, product, zavod) =>
                   !string.IsNullOrWhiteSpace(name) &&
                   product != null &&
                   zavod != null
                );

            //vytvoří novou linku a pošle ji jako event který přijme metoda LinkaAddItem() v MainWindowViewModel.cs
            LinkaOk = ReactiveCommand.Create(() => new Linka
            {
                LinkaId = idLinka,
                Name = LinkaName,
                Product = LinkaProductSelectedItem,
                ZavodId = LinkaZavodSelectedItem.ZavodId
            }, okEnabled);
            LinkaCancel = ReactiveCommand.Create(() => { });

        }
    }
}
