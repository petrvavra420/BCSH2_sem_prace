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
    public class StrojEditViewModel : ViewModelBase
    {
        private string strojName;
        private int strojManufacturingSpeed;
        private Linka strojLinkaSelectedItem;
        public ObservableCollection<Linka> StrojLinkaItems { get; set; }
        private LiteDatabase db;
        private int idStroj;
        public Linka StrojLinkaSelectedItem
        {
            get => strojLinkaSelectedItem;
            set => this.RaiseAndSetIfChanged(ref strojLinkaSelectedItem, value);
        }
        public int StrojManufacturingSpeed
        {
            get => strojManufacturingSpeed;
            set => this.RaiseAndSetIfChanged(ref strojManufacturingSpeed, value);
        }
        public string StrojName
        {
            get => strojName;
            set => this.RaiseAndSetIfChanged(ref strojName, value);
        }

        public StrojEditViewModel(LiteDatabase database, ICollectionModels polozka)
        {
            db = database;
            Stroj stroj = (Stroj)polozka;
            idStroj = stroj.StrojId;
            StrojLinkaItems = new ObservableCollection<Linka>();
            var colLinka = db.GetCollection<Linka>("Linka");
            IEnumerable<Linka> resultsLinka = colLinka.FindAll();


            foreach (var result in resultsLinka)
            {
                StrojLinkaItems.Add(result);
                if (result.LinkaId == stroj.LinkaId)
                {
                    StrojLinkaSelectedItem = result;
                }
            }
            //nastaví hodnoty podle vybrané položky
            StrojName = stroj.Name;
            StrojManufacturingSpeed = stroj.ManufacturingSpeed;


            var okEnabled = this.WhenAnyValue(
                                x => x.StrojName, x => x.StrojManufacturingSpeed, x => x.StrojLinkaSelectedItem,
                                (name, speed, selectedItem) =>
                                !string.IsNullOrWhiteSpace(name) &&
                                speed > 0 &&
                                selectedItem != null
                            );

            StrojOk = ReactiveCommand.Create(() => new Stroj
            {
                StrojId = idStroj,
                Name = StrojName,
                ManufacturingSpeed = StrojManufacturingSpeed,
                LinkaId = StrojLinkaSelectedItem.LinkaId
            }, okEnabled);

            StrojCancel = ReactiveCommand.Create(() => { });

        }

        public ReactiveCommand<Unit, Stroj> StrojOk { get; }
        public ReactiveCommand<Unit, Unit> StrojCancel { get; }
    }
}
