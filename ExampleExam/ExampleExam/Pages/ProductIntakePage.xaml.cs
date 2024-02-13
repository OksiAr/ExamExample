using ExampleExam.Components;
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

namespace ExampleExam.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductIntakePage.xaml
    /// </summary>
    public partial class ProductIntakePage : Page
    {
        Intake newIntake;
        public ProductIntakePage()
        {
            InitializeComponent();
            newIntake = App.db.Intake.Add(new Intake()
            {
                DateIntake = DateTime.Now
            });
            App.db.SaveChanges();
            ProductCb.ItemsSource = App.db.Product.ToList();
            ProductCb.DisplayMemberPath = "Name";
            Refresh();
        }
        private void Refresh()
        {
            ProductIntakeList.ItemsSource = App.db.ProductIntake.Where(x => x.IntakeId == newIntake.Id).ToList();
        }
        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            var selProduct = ProductCb.SelectedItem as Product;
            var productIntake = App.db.ProductIntake.FirstOrDefault(x => x.IntakeId == newIntake.Id && x.SerialNumber == selProduct.SerialNumber); 
            if( productIntake != null)
            {
                productIntake.Count++;
            }
            else
            {
                App.db.ProductIntake.Add(new ProductIntake()
                {
                    SerialNumber = selProduct.SerialNumber,
                    IntakeId = newIntake.Id,
                    Count = 1
                });  
            }
            App.db.SaveChanges();
            Refresh();
        }
    }
}
