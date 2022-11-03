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
using System.Threading.Tasks;

namespace BCSH2_Avalonia_Vavra_Petr_Sem.ViewModels
{
    public class MistrAddViewModel : ViewModelBase
    {
        private string mistrName;
        private ShiftsEnum mistrShift;
        private Linka mistrLinka;
        private ObservableCollection<ShiftsEnum> MistrShiftItems { get; set; }
        private ObservableCollection<Linka> MistrLinkaItems { get; set; }
        private LiteDatabase db;
        public ReactiveCommand<Unit, Mistr> MistrOk { get; }
        public ReactiveCommand<Unit, Unit> MistrCancel { get; }

        public Linka MistrLinkaSelectedItem {
            get => mistrLinka;
            set => this.RaiseAndSetIfChanged(ref mistrLinka, value);
        }

        public ShiftsEnum MistrShiftSelectedItem
        {
            get => mistrShift;
            set => this.RaiseAndSetIfChanged(ref mistrShift, value);
        }
        public string MistrName
        {
            get => mistrName;
            set => this.RaiseAndSetIfChanged(ref mistrName, value);
        }

        public MistrAddViewModel(LiteDatabase database)
        {
            db = database;
            MistrShiftItems = new ObservableCollection<ShiftsEnum>();
            MistrLinkaItems = new ObservableCollection<Linka>();

            foreach (ShiftsEnum item in ShiftsEnum.GetValues(typeof(ShiftsEnum)))
            {
                MistrShiftItems.Add(item);
            }

            var colLinka = db.GetCollection<Linka>("Linka");
            IEnumerable<Linka> resultsLinka = colLinka.FindAll();

            foreach (var result in resultsLinka)
            {
                MistrLinkaItems.Add(result);
                System.Diagnostics.Debug.WriteLine(result.ToString());
            }

            var okEnabled = this.WhenAnyValue(
                x => x.MistrName, x => x.MistrShiftSelectedItem, x => x.MistrLinkaSelectedItem,
                (name,shift,linka) =>
                !string.IsNullOrWhiteSpace(name) &&
                shift != null &&
                linka != null
                );

            MistrOk = ReactiveCommand.Create(() =>
                new Mistr
                {
                    Name = MistrName,
                    Shift = MistrShiftSelectedItem,
                    LinkaId = MistrLinkaSelectedItem.LinkaId
                },okEnabled
            );

            MistrCancel = ReactiveCommand.Create(() => { });
        }
    }
}
