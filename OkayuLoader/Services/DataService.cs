using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.UI.Xaml.Controls;

namespace OkayuLoader.Services
{
    public class Account
    {
        public Int64 id { get; set; }
        public string tag { get; set; }
        public string nickname { get; set; }
        public string password { get; set; }
    }

    internal class DataService
    {
        public void CreateDataFile()
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDataFile = System.IO.Path.Combine(userFolderPath, ".OkayuLoader\\AccountsBase.db");

            var dbFile = File.Create(pathDataFile);
            dbFile.Close();

            using (var connection = new SqliteConnection("Data Source=" + pathDataFile))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "CREATE TABLE accounts(" +
                    "id int," +
                    "tag string," +
                    "name string," +
                    "password string);";
                command.ExecuteNonQuery();
            }
        }

        public void AddRow(Account account)
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDataFile = System.IO.Path.Combine(userFolderPath, ".OkayuLoader\\AccountsBase.db");

            using (var connection = new SqliteConnection("Data Source=" + pathDataFile))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO accounts (id, tag, name, password) VALUES (@id, @tag, @name, @password);";
                command.Parameters.AddWithValue("id", account.id);
                command.Parameters.AddWithValue("tag", account.tag);
                command.Parameters.AddWithValue("name", account.nickname);
                command.Parameters.AddWithValue("password", account.password);
                command.ExecuteNonQuery();
            }
        }

        public List<Account> LoadAccounts()
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDataFile = System.IO.Path.Combine(userFolderPath, ".OkayuLoader\\AccountsBase.db");
            List<Account> accounts = new List<Account>();
            Account account = new Account();

            using (var connection = new SqliteConnection("Data Source=" + pathDataFile))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM accounts";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            account.id = (Int64)reader.GetValue(0);
                            account.tag = (string)reader.GetValue(1);
                            account.nickname = (string)reader.GetValue(2);
                            account.password = reader.GetValue(3).ToString();
                            accounts.Add(new Account {
                                id = account.id,
                                tag = account.tag,
                                nickname = account.nickname,
                                password = account.password
                            });
                        }
                    }
                }
            }
            return accounts;
        }


    }
}
