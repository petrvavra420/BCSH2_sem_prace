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

namespace BCSH2_Avalonia_Vavra_Petr_Sem.ViewModels
{
    public class StrojAddViewModel : ViewModelBase
    {
        private string strojName;
        private int strojManufacturingSpeed;
        private Linka strojLinkaSelectedItem;
        public ObservableCollection<Linka> StrojLinkaItems { get; set; }
        private LiteDatabase db;
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

        public StrojAddViewModel(LiteDatabase database)
        {
            db = database;
            StrojLinkaItems = new ObservableCollection<Linka>();
            var colLinka = db.GetCollection<Linka>("Linka");
            IEnumerable<Linka> resultsLinka = colLinka.FindAll();

            foreach (var result in resultsLinka)
            {
                StrojLinkaItems.Add(result);
                System.Diagnostics.Debug.WriteLine(result.ToString());
            }

            var okEnabled = this.WhenAnyValue(
                    x => x.StrojName, x => x.StrojManufacturingSpeed, x => x.StrojLinkaSelectedItem,
                    (name,speed,selectedItem) => 
                    !string.IsNullOrWhiteSpace(name) &&
                    speed > 0 &&
                    selectedItem != null
                );

            StrojOk = ReactiveCommand.Create(() => new Stroj
            {
                Name = StrojName,
                ManufacturingSpeed = StrojManufacturingSpeed,
                LinkaId = StrojLinkaSelectedItem.LinkaId
            },okEnabled);

            StrojCancel = ReactiveCommand.Create(() => { });

        }

        public ReactiveCommand<Unit, Stroj> StrojOk { get; }
        public ReactiveCommand<Unit, Unit> StrojCancel { get; }


    }
}
