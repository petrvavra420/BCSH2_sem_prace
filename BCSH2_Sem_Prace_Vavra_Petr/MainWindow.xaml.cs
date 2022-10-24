using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BCSH2_Sem_Prace_Vavra_Petr
{
    //- obsahuje entity: závod - linky - stroje - mistři - dělníci
    //- každý mistr odpovídá za jednoho nebo více dělníků kteří pracují u strojů, které patří pod
    //- linky a každá linka je umístěna na závodě
    //- v aplikaci je možné editovat, vytvářet, zobrazovat a mazat veškeré entity
    //- v GUI aplikace bude možné rozkliknout veškeré entity a zobrazit jejich hodnoty
    //- v aplikaci bude možnost vyhledávat podle názvu entit a jejich ID
    //- pro ukládání dat bude použita embedded knihovna LiteDB(https://www.litedb.org/)
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //test
            InitializeComponent();
            DatabaseTools dt = new DatabaseTools();
            LiteDatabase db = dt.connectToDatabase(@"C:\Temp\DatabaseTest.db");
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EntitiesList.Items.Add(textboxName.Text);
        }
    }
}
