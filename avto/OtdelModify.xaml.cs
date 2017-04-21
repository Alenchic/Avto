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
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Win32;
using Word = Microsoft.Office.Interop.Word;

namespace avto
{
    /// <summary>
    /// Логика взаимодействия для OtdelModify.xaml
    /// </summary>
    public partial class OtdelModify : Window
    {
        public OtdelModify()
        {
            InitializeComponent();
        }
        DataTable avtomobil;
        DataTable Klient;
        DataTable Sklad;
        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAvtomobil();
        }

        private void UpdateAvtomobil()
        {

            string sql = "SELECT         "
               + "* FROM dbo.Automobile INNER JOIN dbo.Komplektacia  ON dbo.Automobile.Komplektacia_ID = dbo.Komplektacia.id_komplektacia "
               + "INNER JOIN dbo.Kategoria_automobilya  ON dbo.Automobile.Kategotia_ID = dbo.Kategoria_automobilya.id_kategoria";

            string sql1 = "SELECT * FROM dbo.Kategoria_automobilya ";
            string sql2 = "SELECT * FROM dbo.Komplektacia ";
            avtomobil = new DataTable();
            DataTable tip_avto = new DataTable();
            DataTable komplect = new DataTable();
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

                SqlCommand command1 = new SqlCommand(sql1, connection);
                SqlDataAdapter adapter1 = new SqlDataAdapter(command1);

                SqlCommand command2 = new SqlCommand(sql2, connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter(command2);

                connection.Open();

                adapter1.Fill(tip_avto);
                comboBox1tip.SelectedValuePath = "id_kategoria";
                comboBox1tip.DisplayMemberPath = "Tip_avto";
                comboBox1tip.ItemsSource = tip_avto.DefaultView;
                adapter1.Update(tip_avto);

                adapter2.Fill(komplect);
                comboBoxkorl.SelectedValuePath = "id_komplektacia";
                comboBoxkorl.DisplayMemberPath = "Tip_Komplektacii";
                comboBoxkorl.ItemsSource = komplect.DefaultView;
                adapter2.Update(komplect);

                adapter.Fill(avtomobil);
                dataGrid2.ItemsSource = avtomobil.DefaultView;
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


        private void UpdateKlient()
        {
            string sql = "SELECT         "
               + "* FROM dbo.Klient INNER JOIN dbo.Automobile  ON dbo.Klient.Auto_ID = dbo.Automobile.id_auto "
               + "INNER JOIN dbo.Pasport  ON dbo.Klient.Pasport_ID = dbo.Pasport.id_pasport";
            Klient = new DataTable();

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
                adapter.Fill(Klient);

                dataGrid1.ItemsSource = Klient.DefaultView;
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

        private void UpdateZay()
        {
            string sql = "SELECT         "
               + "* FROM dbo.Zyavka INNER JOIN dbo.Zapchast  ON dbo.Zyavka.Zapch_ID = dbo.Zapchast.ID_Zapch ";

            Sklad = new DataTable();

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
                adapter.Fill(Sklad);


                dataGridzay.ItemsSource = Sklad.DefaultView;
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

        private void UpdateSklad()
        {
            string sql = "SELECT         "
               + "* FROM dbo.Zapchast INNER JOIN dbo.Sklad  ON dbo.Zapchast.Sklad_ID = dbo.Sklad.ID_Sklad "
            + "INNER JOIN dbo.Postavhic  ON dbo.Zapchast.PS_ID = dbo.Postavhic.ID_PS";
            Sklad = new DataTable();

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
                adapter.Fill(Sklad);

                dataGrid.ItemsSource = Sklad.DefaultView;

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



        private void TabItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            UpdateKlient();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            switch (MessageBox.Show("Завершить работу приложения?", "Выход", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:

                    break;
            }
        }

        private void TabItem_Loaded_2(object sender, RoutedEventArgs e)
        {
            UpdateSklad();
            UpdateZay();
        }

        private void btndelzay_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridzay.SelectedItems.Count == 0) return;
            var IDPR = ((DataRowView)dataGridzay.SelectedItems[0]).Row["ID_Zyavka"].ToString();
            MessageBox.Show("Удалить:" + IDPR);
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
                    SqlCommand command = new SqlCommand("dbo.dell_Zyavka", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Zyavka", IDPR);
                    command.ExecuteNonQuery();
                    UpdateZay();
                    connection.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void btnaddzay_Click(object sender, RoutedEventArgs e)
        {
            switch (tbZaydata.Text == "" || tbZaykolvo.Text == "" || tbZaynam.Text == "")
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
                    SqlCommand command = new SqlCommand("dbo.add_Zyavka", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Data_zayavki", tbZaydata.Text);
                    command.Parameters.AddWithValue("@Kol_stvo", tbZaykolvo.Text);
                    command.Parameters.AddWithValue("@Zapch_ID", tbZaynam.Text);


                    command.ExecuteNonQuery();
                    UpdateZay();

                    connection.Close();
                    break;

                case (true):
                    MessageBox.Show("Заполните все пустые поля");
                    break;
            }
        }

        private void avtoadd_Click(object sender, RoutedEventArgs e)
        {
            switch (avtogos.Text == "" || avtomarka.Text == "" || avtomodel.Text == "" || avtodata.Text == "")
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
                    SqlCommand command = new SqlCommand("dbo.add_Automobile", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Gos_nomer", avtogos.Text);
                    command.Parameters.AddWithValue("@Data_vypuska", avtodata.Text);
                    command.Parameters.AddWithValue("@Marka", avtomarka.Text);
                    command.Parameters.AddWithValue("@Model", avtomodel.Text);
                    command.Parameters.AddWithValue("@Kategotia_ID", comboBox1tip.SelectedValue.ToString());
                    command.Parameters.AddWithValue("@Komplektacia_ID", comboBoxkorl.SelectedValue.ToString());

                    command.ExecuteNonQuery();
                    UpdateAvtomobil();

                    connection.Close();
                    break;

                case (true):

                    MessageBox.Show("Заполните все пустые поля");
                    break;
            }
        }

        private void btnclearzay_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
