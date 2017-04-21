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
using System.Windows.Shapes;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.IO;
using System.Data;
using Word = Microsoft.Office.Interop.Word;

namespace avto
{

    public partial class OtdelKadrov : Window
    {
        private readonly string TemplateFileName = @"E:\УЧЁБА\hunter\avto\Uvol.docx";
        //string connectionString;
        SqlDataAdapter adapter;
        DataTable phoness;
        public OtdelKadrov()
        {
            InitializeComponent();

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string sql = "SELECT dbo.Proizvoditel.Name_proizv, dbo.sklad.nazvanie_tovara_sklad, dbo.Tovar.kol_vo, dbo.Tovar.cena FROM  dbo.Proizvoditel INNER JOIN"
            //+"dbo.Tovar ON dbo.Proizvoditel.ID_proizvod = dbo.Tovar.Proizvoditel_ID INNER JOIN dbo.tovarskld ON dbo.Tovar.ID_Tovarpr = dbo.tovarskld.kod_tovara_id INNER "
            //+"JOIN dbo.Tovar_na_skladakh ON dbo.tovarskld.id_nomer_tovara_na_sklade = dbo.Tovar_na_skladakh.nomer_tovara_na_sklade CROSS JOIN dbo.sklad";dbo.Sotrudnik.Imya, dbo.Sotrudnik.familiya, dbo.Sotrudnik.otchestvo,


            string sql = "SELECT * FROM dbo.Sotrudnik";
            DataTable phones = new DataTable();
            SqlConnection connection = null;
            try
            {
                ConnectionClass ConCheck = new ConnectionClass();
                RegistryKey DataBase_Connection = Registry.CurrentConfig;
                RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
                connection = new SqlConnection(ConCheck.ConnectString);
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                              
                connection.Open();
                adapter.Fill(phones);
                dataGrid.ItemsSource = phones.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            switch (MessageBox.Show("Завершить работу прилоджения?", "Выход", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:

                    break;
            }
        }



        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {

            UpdateDB();

        }


        private void button3_Click(object sender, RoutedEventArgs e)
        {
            
            if (dataGrid3.SelectedItems.Count == 0) return;
            var Name = ((DataRowView)dataGrid3.SelectedItems[0]).Row["Imya"].ToString();
            var fam = ((DataRowView)dataGrid3.SelectedItems[0]).Row["familiya"].ToString();
            var oth = ((DataRowView)dataGrid3.SelectedItems[0]).Row["otchestvo"].ToString();
            var dolj = ((DataRowView)dataGrid3.SelectedItems[0]).Row["Doljnost"].ToString();
            var data = ((DataRowView)dataGrid3.SelectedItems[0]).Row["Data_Utverjdeniya"].ToString();
            var osn = ((DataRowView)dataGrid3.SelectedItems[0]).Row["Osnovanie"].ToString();
            var Otd = ((DataRowView)dataGrid3.SelectedItems[0]).Row["Otdel"].ToString();
            var wordapp = new Word.Application();
            wordapp.Visible = false;
            try
            {
                var worddoc = wordapp.Documents.Open(TemplateFileName);
                RuplucuWordStub("{Name}", Name, worddoc);
                RuplucuWordStub("{fam}", fam, worddoc);
                RuplucuWordStub("{oth}", oth, worddoc);
                RuplucuWordStub("{dolj}", dolj, worddoc);
                RuplucuWordStub("{data}", data, worddoc);
                RuplucuWordStub("{osn}", osn, worddoc);
                RuplucuWordStub("{Otd}", Otd, worddoc);
                wordapp.Visible = true;
                worddoc.SaveAs2(@"E:\УЧЁБА\hunter\avto\Uvol11.docx");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void RuplucuWordStub(string StubTORupluce, string text, Word.Document worddoc)
        {
            var runge = worddoc.Content;
            runge.Find.ClearFormatting();
            runge.Find.Execute(FindText: StubTORupluce, ReplaceWith: text);
        }



        private void button5_Click(object sender, RoutedEventArgs e)
        {

            if (dataGrid3.SelectedItems.Count == 0) return;
            var IDPR = ((DataRowView)dataGrid3.SelectedItems[0]).Row["ID_PRU"].ToString();
            //MessageBox.Show("Удалить:" + IDPR);
            switch (MessageBox.Show("Вы действительно хотите удалить данные?", "Удаление", MessageBoxButton.YesNo))
            { 
                case MessageBoxResult.Yes:

            ConnectionClass ConCheck = new ConnectionClass();
            RegistryKey DataBase_Connection = Registry.CurrentConfig;
            RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
            ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
               Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
               Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
               Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
            SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
            connection.Open();
            SqlCommand command = new SqlCommand("dbo.dell_Prikaz_ob_uvolnenii", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID_PRU", IDPR);
            command.ExecuteNonQuery();
                    UpdateDB();
                    connection.Close();
                    break;
                case MessageBoxResult.No:
                    break;           
            }

        }
        
    
        

        private void UpdateDB()
        {
            string sql = "SELECT         "
                + "* FROM dbo.Sotrudnik INNER JOIN dbo.Prikaz_ob_uvolnenii  ON dbo.Sotrudnik.ID_STR = dbo.Prikaz_ob_uvolnenii.STR_ID  ";
            phoness = new DataTable();
            string sql2 = "select  Sotrudnik.ID_STR, CONCAT(Imya,' ',familiya,' ',otchestvo) as 'sotr' from Sotrudnik ";

            DataTable tip_avto = new DataTable();
            SqlConnection connection = null;
            try
            {
                ConnectionClass ConCheck = new ConnectionClass();
                RegistryKey DataBase_Connection = Registry.CurrentConfig;
                RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));


                connection = new SqlConnection(ConCheck.ConnectString);
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                SqlCommand command1 = new SqlCommand(sql2, connection);
                SqlDataAdapter adapter1 = new SqlDataAdapter(command1);

                adapter1.Fill(tip_avto);
                comboBoxkorl.SelectedValuePath = "ID_STR";
                comboBoxkorl.DisplayMemberPath = "sotr" /*+ "familiya"+ "otchestvo"*/;
                comboBoxkorl.ItemsSource = tip_avto.DefaultView;
                adapter1.Update(tip_avto);

                connection.Open();
                adapter.Fill(phoness);
                dataGrid3.ItemsSource = phoness.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            switch (Utvrbox.Text == "" || Osnovaniebox.Text == "" )
            {
                case (false):

                    ConnectionClass ConCheck = new ConnectionClass();
                    RegistryKey DataBase_Connection = Registry.CurrentConfig;
                    RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                    ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                       Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                       Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                       Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
                    SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                    connection.Open();
                    SqlCommand command = new SqlCommand("dbo.add_Prikaz_ob_uvolnenii", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Data_Utverjdeniya", Utvrbox.Text);
                    command.Parameters.AddWithValue("@Osnovanie", Osnovaniebox.Text);
                    command.Parameters.AddWithValue("@STR_ID", comboBoxkorl.SelectedValue.ToString());
                   

                    command.ExecuteNonQuery();
                    UpdateDB();

                    connection.Close();
                    break;

                case (true):
                    MessageBox.Show("Заполните все пустые поля");
                    break;
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            //otdelkadrow1 = new OtdelKadrov();

            this.Close();
            MainWindow sda = new MainWindow();
            sda.Visibility = Visibility.Visible;
            //UpdateLayout();

        }

        private void DataZay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }
    }
}
