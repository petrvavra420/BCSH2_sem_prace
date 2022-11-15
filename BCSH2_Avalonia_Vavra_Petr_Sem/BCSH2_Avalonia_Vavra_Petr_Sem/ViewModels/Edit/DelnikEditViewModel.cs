using BCSH2_Avalonia_Vavra_Petr_Sem.Models.Enums;
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
using BCSH2_Avalonia_Vavra_Petr_Sem.Interfaces;
using System.Diagnostics;

namespace BCSH2_Avalonia_Vavra_Petr_Sem.ViewModels.Edit
{
    public class DelnikEditViewModel : ViewModelBase
    {
        private string delnikName;
        private ShiftsEnum shiftSelectedItem;
        private Stroj strojSelectedItem;
        private Mistr mistrSelectedItem;
        private LiteDatabase db;
        private int idDelnik;
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

        public DelnikEditViewModel(LiteDatabase database, ICollectionModels polozka)
        {
            db = database;
            DelnikShiftItems = new ObservableCollection<ShiftsEnum>();
            DelnikStrojItems = new ObservableCollection<Stroj>();
            DelnikMistrItems = new ObservableCollection<Mistr>();
            Delnik delnik = (Delnik)polozka;
            idDelnik = delnik.DelnikId;
            System.Diagnostics.Debug.WriteLine("ID DELNIKA: " + idDelnik);

            //nastav dělníkovi jmé podle vybrané položky
            DelnikName = delnik.Name;
            DelnikShiftSelectedItem = delnik.Shift;

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
            //nastav defaultní stroj podle stroj ID
            DelnikStrojSelectedItem = (Stroj)DelnikStrojItems.Where(x => x.StrojId == delnik.StrojId).FirstOrDefault();

            var colMistr = db.GetCollection<Mistr>("Mistr");
            IEnumerable<Mistr> resultsMistr = colMistr.FindAll();

            foreach (var result in resultsMistr)
            {
                DelnikMistrItems.Add(result);
                System.Diagnostics.Debug.WriteLine(result.ToString());
            }
            DelnikMistrSelectedItem = (Mistr)DelnikMistrItems.Where(x => x.MistrId == delnik.MistrId).FirstOrDefault();

            
            

            var okEnabled = this.WhenAnyValue(
                    x => x.DelnikName, x => x.DelnikShiftSelectedItem, x => x.DelnikMistrSelectedItem, x => x.DelnikStrojSelectedItem,
                    (name, shift, mistr, stroj) =>
                    !string.IsNullOrWhiteSpace(name) &&
                    shift != null &&
                    mistr != null &&
                    stroj != null
                );

            DelnikOk = ReactiveCommand.Create(() =>
                new Delnik
                {
                    DelnikId = delnik.DelnikId,
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
