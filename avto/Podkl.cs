using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Windows; 

namespace avto
{
    class Podkl
    {
        private string sql;
        public SqlConnection connection = null;
        public DataTable Table = new DataTable();
        public Podkl(string sql)
        {
        
            this.sql = sql;


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
            adapter.Fill(Table);
            adapter.Update(Table);
            if (connection != null)
                connection.Close();
        }

    }
}
