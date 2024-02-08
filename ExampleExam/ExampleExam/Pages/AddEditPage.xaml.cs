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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        Product product;
        bool isAdd = false;
        public AddEditPage(Product _product)
        {
            InitializeComponent();
            product = _product;
            this.DataContext = product;
            UnitCb.ItemsSource = App.db.Unit.ToList();
            UnitCb.DisplayMemberPath = "Name";
            
            if(product.SerialNumber == 0)
            {
                SerialNumberTb.IsEnabled = true;
                CountTb.IsEnabled = true;
                isAdd = true;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var selUnit = UnitCb.SelectedItem as Unit;
            product.UnitId = selUnit.Id;
            if (isAdd == true)
            {
                App.db.Product.Add(product);
            }
            App.db.SaveChanges();
            MessageBox.Show("Сохранено!");
        }
    }
}
