﻿using ICSharpCode.SharpZipLib.BZip2;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Net;
using System.Reflection;
using System.Xml.Linq;

namespace SARS.Modules
{
    public static class SQLite
    {
        public static string _databaseLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\SARS.db3";
        public static void Setup()
        {
            if (!File.Exists(_databaseLocation))
            {
                // This is the query which will create a new table in our database file with three columns. An auto increment column called "ID", and two NVARCHAR type columns with the names "Key" and "Value"
                string createTableQuery = @"CREATE TABLE IF NOT EXISTS [Avatars] (
                          [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                          [AvatarId] NVARCHAR(50) NOT NULL,
                          [AvatarData] TEXT NOT NULL)";
                string createTable2Query = @"CREATE TABLE IF NOT EXISTS [Worlds] (
                          [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                          [WorldId] NVARCHAR(50) NOT NULL,
                          [WorldData] TEXT NOT NULL)";
                SQLiteConnection.CreateFile(_databaseLocation);      
                using (SQLiteConnection con = new SQLiteConnection($"data source='{_databaseLocation}'"))
                {
                    using (SQLiteCommand com = new SQLiteCommand(con))
                    {
                        con.Open();                           
                        com.CommandText = createTableQuery;    
                        com.ExecuteNonQuery();
                        com.CommandText = createTable2Query;
                        com.ExecuteNonQuery();
                        con.Close();     
                    }
                }
            }
        }

        public static void CreateIndex() {

            string sqlCommand = "CREATE INDEX idx_avatarId ON Avatars (AvatarId);";
            using (SQLiteConnection con = new SQLiteConnection($"data source='{_databaseLocation}'"))
            {
                using (SQLiteCommand com = new SQLiteCommand(con))
                {
                    con.Open();
                    com.CommandText = sqlCommand;
                    com.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public static int CountAvatars()
        {
            using (SQLiteConnection con = new SQLiteConnection($"data source='{_databaseLocation}'"))
            {
                using (SQLiteCommand com = new SQLiteCommand(con))
                {
                    con.Open();
                    com.CommandText = $"SELECT COUNT(*) FROM [Avatars];";
                    int RowCount = Convert.ToInt32(com.ExecuteScalar());
                    con.Close();
                    return RowCount;
                }
            }
        }

        public static string ReadDataAvatar(string avatarId)
        {
            using (SQLiteConnection con = new SQLiteConnection($"data source='{_databaseLocation}'"))
            {
                using (SQLiteCommand com = new SQLiteCommand(con))
                {
                    con.Open();         

                    com.CommandText = $"Select * FROM [Avatars] where AvatarId = '{avatarId}'";     

                    using (SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader["AvatarData"].ToString();   
                        }
                    }
                    con.Close();  
                }
                return null;
            }
        }

        public static string ReadDataWorld(string worldId)
        {
            using (SQLiteConnection con = new SQLiteConnection($"data source='{_databaseLocation}'"))
            {
                using (SQLiteCommand com = new SQLiteCommand(con))
                {
                    con.Open();

                    com.CommandText = $"Select * FROM [Worlds] where WorldId = '{worldId}'";

                    using (SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader["WorldData"].ToString();
                        }
                    }
                    con.Close();
                }
                return null;
            }
        }

        public static void WriteDataAvatar(string avatarId, string jsonData)
        {
            using (var conn = new SQLiteConnection($"data source='{_databaseLocation}'"))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    try
                    {
                        cmd.CommandText = $"INSERT INTO [Avatars] (AvatarId, AvatarData) VALUES (@avatarId,@avatarData)";
                        cmd.Parameters.Add("@avatarId", DbType.String).Value = avatarId;
                        cmd.Parameters.Add("@avatarData", DbType.String).Value = jsonData;

                        cmd.ExecuteNonQuery();
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());

                    }
                    conn.Close();
                }
            }
        }

        public static void WriteDataWorld(string worldId, string jsonData)
        {
            using (var conn = new SQLiteConnection($"data source='{_databaseLocation}'"))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    try
                    {
                        cmd.CommandText = $"INSERT INTO [Worlds] (WorldId, WorldData) VALUES (@worldId,@worldData)";
                        cmd.Parameters.Add("@worldId", DbType.String).Value = worldId;
                        cmd.Parameters.Add("@worldData", DbType.String).Value = jsonData;

                        cmd.ExecuteNonQuery();
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());

                    }
                    conn.Close();
                }
            }
        }
    }
}