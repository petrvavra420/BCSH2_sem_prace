using BCSH2_Avalonia_Vavra_Petr_Sem.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Avalonia_Vavra_Petr_Sem.ViewModels
{
    public class ZavodAddViewModel : ViewModelBase
    {
        public string zavodName;
        public string zavodStat;
        public string zavodMesto;
        public string zavodUlice;
        public int zavodCisloPopisne;
        public int zavodPSC;

        //vlastnosti jsou modifikované aby reagovaly na změny
        public string ZavodName
        {
            get => zavodName;
            set => this.RaiseAndSetIfChanged(ref zavodName, value);
        }
        public string ZavodStat
        {
            get => zavodStat;
            set => this.RaiseAndSetIfChanged(ref zavodStat, value);
        }
        public string ZavodMesto
        {
            get => zavodMesto;
            set => this.RaiseAndSetIfChanged(ref zavodMesto, value);
        }
        public string ZavodUlice
        {
            get => zavodUlice;
            set => this.RaiseAndSetIfChanged(ref zavodUlice, value);
        }
        public int ZavodCisloPopisne
        {
            get => zavodCisloPopisne;
            set => this.RaiseAndSetIfChanged(ref zavodCisloPopisne, value);
        }
        public int ZavodPSC
        {
            get => zavodPSC;
            set => this.RaiseAndSetIfChanged(ref zavodPSC, value);
        }
        public ZavodAddViewModel()
        {
            ZavodCisloPopisne = 12;
            ZavodPSC = 19016;
            // Kontroluje TextBoxy pomocí lambda výrazu - reprezentuje stream bool hodnot 
            var okEnabled = this.WhenAnyValue(
                    x => x.ZavodName, x => x.ZavodStat, x => x.ZavodMesto, x => x.ZavodUlice, x => x.ZavodCisloPopisne, x => x.ZavodPSC,
                    (name, stat, mesto, ulice, cp, psc) =>
                    !string.IsNullOrWhiteSpace(name) &&
                    !string.IsNullOrWhiteSpace(stat) &&
                    !string.IsNullOrWhiteSpace(mesto) &&
                    !string.IsNullOrWhiteSpace(ulice) &&
                    cp > 0 &&
                    psc > 0
                );
            //Zavod zavod = new Zavod(ZavodName, new Adress(ZavodStat, ZavodMesto, ZavodUlice, ZavodCisloPopisne, ZavodPSC));
            //Ok = ReactiveCommand.Create(() => zavod);
            
            //inicializace příkazů - reaguje na ně listener ve třídě MainWindowViewModel v metodě ZavodAddItem()
            //příkaz OK předává vytvořenou položku Zavod, druhý parametr je bool CanExecute
            Ok = ReactiveCommand.Create(() => new Zavod
            {
                Name = ZavodName,
                Adress = new Adress
                {
                    Country = ZavodStat,
                    City = ZavodMesto,
                    Street = ZavodUlice,
                    HouseNumber = ZavodCisloPopisne,
                    PostalCode = ZavodPSC
                }
            }, okEnabled);
            Cancel = ReactiveCommand.Create(() => { });

        }

        public ReactiveCommand<Unit, Zavod> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
