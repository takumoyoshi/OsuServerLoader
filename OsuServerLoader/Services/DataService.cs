using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;

namespace OkayuLoader.Services
{
    public class Account
    {
        public Int64 id { get; set; }
        public string tag { get; set; }
        public string name { get; set; }
        public string password { get; set; }
    }

    internal class DataService
    {
        public void CreateDataFile()
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDataFile = System.IO.Path.Combine(userFolderPath, ".OsuServerLoader\\AccountsBase.db");

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
            string pathDataFile = System.IO.Path.Combine(userFolderPath, ".OsuServerLoader\\AccountsBase.db");

            using (var connection = new SqliteConnection("Data Source=" + pathDataFile))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO accounts (id, tag, name, password) VALUES (@id, @tag, @name, @password);";
                command.Parameters.AddWithValue("id", account.id);
                command.Parameters.AddWithValue("tag", account.tag.Replace(" ", "_"));
                command.Parameters.AddWithValue("name", account.name);
                command.Parameters.AddWithValue("password", account.password);
                command.ExecuteNonQuery();
            }
        }

        public List<Account> LoadAccounts()
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDataFile = System.IO.Path.Combine(userFolderPath, ".OsuServerLoader\\AccountsBase.db");
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
                            account.tag = reader.GetValue(1).ToString();
                            account.name = reader.GetValue(2).ToString();
                            account.password = reader.GetValue(3).ToString();
                            accounts.Add(new Account {
                                id = account.id,
                                tag = account.tag,
                                name = account.name,
                                password = account.password
                            });
                        }
                    }
                }
            }
            return accounts;
        }

        public void DeleteRow(string tag)
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDataFile = System.IO.Path.Combine(userFolderPath, ".OsuServerLoader\\AccountsBase.db");

            using (var connection = new SqliteConnection("Data Source=" + pathDataFile))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "DELETE FROM accounts WHERE tag = @tag";
                command.Parameters.AddWithValue("tag", tag);
                command.ExecuteNonQuery();
            }
        }

        public Account GetAccount(string tag)
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDataFile = System.IO.Path.Combine(userFolderPath, ".OsuServerLoader\\AccountsBase.db");
            Account account = new Account();

            using (var connection = new SqliteConnection("Data Source=" + pathDataFile))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM accounts WHERE tag = @tag LIMIT 1";
                command.Parameters.AddWithValue("tag", tag);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            account.id = 0;
                            account.tag = tag;
                            account.name = reader.GetString(2);
                            account.password = reader.GetString(3);
                        }
                    }
                }
            }
            return account;
        }
    }
}
