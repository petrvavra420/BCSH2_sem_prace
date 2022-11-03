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
    public class DelnikAddViewModel : ViewModelBase
    {
        private string delnikName;
        private ShiftsEnum shiftSelectedItem;
        private Stroj strojSelectedItem;
        private Mistr mistrSelectedItem;
        private LiteDatabase db;
        public ObservableCollection<ShiftsEnum> DelnikShiftItems { get; set; }
        public ObservableCollection<Stroj> DelnikStrojItems { get; set; }
        public ObservableCollection<Mistr> DelnikMistrItems { get; set; }
        public ReactiveCommand<Unit, Delnik> DelnikOk { get; }
        public ReactiveCommand<Unit, Unit> DelnikCancel { get; }

        public Mistr DelnikMistrSelectedItem
        {
            get => mistrSelectedItem;
            set => this.RaiseAndSetIfChanged(ref mistrSelectedItem, value);
        }
        public Stroj DelnikStrojSelectedItem
        {
            get => strojSelectedItem;
            set => this.RaiseAndSetIfChanged(ref strojSelectedItem, value);
        }
        public ShiftsEnum DelnikShiftSelectedItem
        {
            get => shiftSelectedItem;
            set => this.RaiseAndSetIfChanged(ref shiftSelectedItem, value);
        }
        public string DelnikName
        {
            get => delnikName;
            set => this.RaiseAndSetIfChanged(ref delnikName, value);
        }

        public DelnikAddViewModel(LiteDatabase database)
        {
            db = database;
            DelnikShiftItems = new ObservableCollection<ShiftsEnum>();
            DelnikStrojItems = new ObservableCollection<Stroj>();
            DelnikMistrItems = new ObservableCollection<Mistr>();

            foreach (ShiftsEnum item in ShiftsEnum.GetValues(typeof(ShiftsEnum)))
            {
                DelnikShiftItems.Add(item);
            }

            var colStroj = db.GetCollection<Stroj>("Stroj");
            IEnumerable<Stroj> resultsLinka = colStroj.FindAll();

            foreach (var result in resultsLinka)
            {
                DelnikStrojItems.Add(result);
                System.Diagnostics.Debug.WriteLine(result.ToString());
            }

            var colMistr = db.GetCollection<Mistr>("Mistr");
            IEnumerable<Mistr> resultsMistr = colMistr.FindAll();

            foreach (var result in resultsMistr)
            {
                DelnikMistrItems.Add(result);
                System.Diagnostics.Debug.WriteLine(result.ToString());
            }

            var okEnabled = this.WhenAnyValue(
                    x => x.DelnikName, x => x.DelnikShiftSelectedItem, x => x.DelnikMistrSelectedItem, x => x.DelnikStrojSelectedItem,
                    (name,shift,mistr,stroj) =>
                    !string.IsNullOrWhiteSpace(name) &&
                    shift != null &&
                    mistr != null &&
                    stroj != null 
                );

            DelnikOk = ReactiveCommand.Create(() =>
                new Delnik
                {
                    Name = DelnikName,
                    Shift = DelnikShiftSelectedItem,
                    StrojId = DelnikStrojSelectedItem.StrojId,
                    MistrId = DelnikMistrSelectedItem.MistrId

                }, okEnabled
            );
            DelnikCancel = ReactiveCommand.Create(() => { });
        }
    }
}
