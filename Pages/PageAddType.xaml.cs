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
    /// Логика взаимодействия для PageAddType.xaml
    /// </summary>
    public partial class PageAddType : Page
    {
        private Type _currentType = new Type();
        public PageAddType(Type selectedtype)
        {
            InitializeComponent();
            if (selectedtype != null)
            {
                _currentType = selectedtype;
                TitletType.Text = "Изменение типа недвижимости";
                BtnAddType.Content = "Изменить";
            }
            DataContext = _currentType;
        }

        private void BtnAddType_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentType.Type1)) error.AppendLine("Укажите название");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (_currentType.IDtype == 0)
            {
                CommunalPaymentsEntities.GetContext().Type.Add(_currentType);
                try
                {
                    CommunalPaymentsEntities.GetContext().SaveChanges();
                    Classes.ClassFrame.frmObj.Navigate(new PageType());
                    MessageBox.Show("Новый тип недвижимости успешно добавлен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    CommunalPaymentsEntities.GetContext().SaveChanges();
                    Classes.ClassFrame.frmObj.Navigate(new PageType());
                    MessageBox.Show("Тип недвижимости успешно изменён!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
    }

        private void BtnCancelType_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageType());
        }
    }
}
