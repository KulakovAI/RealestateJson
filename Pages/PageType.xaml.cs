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
using Realestate.Classes;
using Type = Realestate.Classes.Type;

namespace Realestate.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageType.xaml
    /// </summary>
    public partial class PageType : Page
    {
        public PageType()
        {
            InitializeComponent();
            DTGtype.ItemsSource = CommunalPaymentsEntities.GetContext().Type.ToList();
        }

        private void SearсhType_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DTGtype.ItemsSource != null)
            {
                DTGtype.ItemsSource = CommunalPaymentsEntities.GetContext().Type.Where(x => x.Type1.ToLower().Contains(SearсhType.Text.ToLower())).ToList();
            }
            if (SearсhType.Text.Count() == 0) DTGtype.ItemsSource = CommunalPaymentsEntities.GetContext().Type.ToList();
        }

        private void MenuEditType_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageAddType((Type)DTGtype.SelectedItem));
        }

        private void MenuAddType_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageAddType(null));
        }

        private void MenuDelType_Click(object sender, RoutedEventArgs e)
        {
            var typeForRemoving = DTGtype.SelectedItems.Cast<Type>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {typeForRemoving.Count()} записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CommunalPaymentsEntities.GetContext().Type.RemoveRange(typeForRemoving);
                    CommunalPaymentsEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DTGtype.ItemsSource = CommunalPaymentsEntities.GetContext().Type.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void MenuSortNameType1_Click(object sender, RoutedEventArgs e)
        {
            DTGtype.ItemsSource = CommunalPaymentsEntities.GetContext().Type.OrderBy(x => x.Type1).ToList();
        }

        private void MenuSortNameType2_Click(object sender, RoutedEventArgs e)
        {
            DTGtype.ItemsSource = CommunalPaymentsEntities.GetContext().Type.OrderByDescending(x => x.Type1).ToList();
        }
        private void MenuSortClear_Click(object sender, RoutedEventArgs e)
        {
            DTGtype.ItemsSource = CommunalPaymentsEntities.GetContext().Type.ToList();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageMain());
        }
    }
}