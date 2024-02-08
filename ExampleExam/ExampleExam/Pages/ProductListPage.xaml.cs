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
    /// Логика взаимодействия для ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        public ProductListPage()
        {
            InitializeComponent();
            //Заполнение ProductList данными из базы
            ProductList.ItemsSource = App.db.Product.Where(x => x.IsDelete != true).ToList();

            // Добавление в список едениц измерения новой сущности(Все) и заполнение UnitFilterCb этими данными 
            List<Unit> unitList = App.db.Unit.ToList();
            unitList.Insert(0, new Unit() { Id = 0, Name = "Все" });
            UnitFilterCb.ItemsSource = unitList;
            UnitFilterCb.DisplayMemberPath = "Name";
        }
        //Создание метода, для отбора записей(сортировка, фильтрация) удовлетворяющих критериям (серийный номер, наименование, ед.измерения) из всего списка продуктов.
        private void Refresh()
        {
            IEnumerable<Product> productList = App.db.Product.Where(x => x.IsDelete != true);
            //Фильтрация по единице измерения
            if(UnitFilterCb.SelectedIndex > 0)
            {
                var selUnit = UnitFilterCb.SelectedItem as Unit;
                productList = productList.Where(x => x.UnitId == selUnit.Id);
            }
            else
            {
                productList = App.db.Product.Where(x => x.IsDelete != true).ToList();
            }
            //Поиск по наименованию и серийному номеру
            if (SearchTb.Text.Length != 0)
            {
                productList = productList.Where(x => x.Name.ToLower().Contains(SearchTb.Text.ToLower()) || x.SerialNumber.ToString().Contains(SearchTb.Text));
            }

            //Обновление списка на странице
            ProductList.ItemsSource = productList;
        }
        //В событии обратоки ComboBox вызываем метод для отбора записей
        private void UnitFilterCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if( MessageBox.Show("Вы дейсвительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var selProduct = (sender as Button).DataContext as Product;
                selProduct.IsDelete = true;
                App.db.SaveChanges();
                Refresh();
            }
            

        }
    }
}
