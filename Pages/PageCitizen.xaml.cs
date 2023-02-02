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
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.IO;


namespace Realestate.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageCitizen.xaml
    /// </summary>
    public partial class PageCitizen : Page
    {
        public PageCitizen()
        {
            InitializeComponent();
            DtgCitizen.ItemsSource = CommunalPaymentsEntities.GetContext().Citizen.ToList();
        }

        private void MenuAddCitizen_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageAddCitezen(null));
        }

        private void MenuSortClear_Click(object sender, RoutedEventArgs e)
        {
            DtgCitizen.ItemsSource = CommunalPaymentsEntities.GetContext().Citizen.ToList();
        }

        private void MenuEditCitizen_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageAddCitezen((Citizen)DtgCitizen.SelectedItem));
        }

        private void MenuDelCitizen_Click(object sender, RoutedEventArgs e)
        {
            var CitizenForRemoving = DtgCitizen.SelectedItems.Cast<Citizen>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {CitizenForRemoving.Count()} записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CommunalPaymentsEntities.GetContext().Citizen.RemoveRange(CitizenForRemoving);
                    CommunalPaymentsEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DtgCitizen.ItemsSource = CommunalPaymentsEntities.GetContext().Citizen.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void SearchCitizen_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DtgCitizen.ItemsSource != null)
            {
                DtgCitizen.ItemsSource = CommunalPaymentsEntities.GetContext().Citizen.Where(x => x.FIO.ToLower().Contains(SearchCitizen_Name.Text.ToLower())).ToList();
            }
            if (SearchCitizen_Name.Text.Count() == 0) DtgCitizen.ItemsSource = CommunalPaymentsEntities.GetContext().Citizen.ToList();
        }

        private void MenuSortCitizen_Click(object sender, RoutedEventArgs e)
        {
            DtgCitizen.ItemsSource = CommunalPaymentsEntities.GetContext().Citizen.OrderBy(x => x.FIO).ToList();
        }

        private void MenuSortCitizen2_Click(object sender, RoutedEventArgs e)
        {
            DtgCitizen.ItemsSource = CommunalPaymentsEntities.GetContext().Citizen.OrderByDescending(x => x.FIO).ToList();
        }


        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            var excelApp = new Excel.Application();
            Excel.Workbook wb = excelApp.Workbooks.Open($"{Directory.GetCurrentDirectory()}\\Шаблон.xlsx");
            Excel.Worksheet worksheet = excelApp.Worksheets.Item[1];
            worksheet.Cells[3, 2] = DateTime.Now.ToString();
            int IndexRows = 1;

            worksheet.Cells[5, 1][IndexRows] = "№";
            worksheet.Cells[5, 2][IndexRows] = "ФИО собственника";
            worksheet.Cells[5, 3][IndexRows] = "Паспорт";
            worksheet.Cells[5, 4][IndexRows] = "Телефон";
            Excel.Range headerRange = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[5][1]];
            headerRange.Font.Bold = true;


            var printItems = DtgCitizen.Items;
            foreach (Citizen item in printItems)
            {
                worksheet.Cells[5, 1][IndexRows + 1] = IndexRows;
                worksheet.Cells[5, 2][IndexRows + 1] = item.FIO;
                worksheet.Cells[5, 3][IndexRows + 1] = item.Passport.ToString();
                worksheet.Cells[5, 4][IndexRows + 1] = item.Telephone.ToString();
                IndexRows++;
                worksheet.Columns.AutoFit();
                worksheet.Rows.AutoFit();
            }
            worksheet.Cells[IndexRows + 6, 3] = "Подпись";
            worksheet.Cells[IndexRows + 6, 4] = "Кулаков А.И.";
            excelApp.Visible = true;
        }
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageMain());
        }
    }
}
