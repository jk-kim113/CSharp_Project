﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DB_Scientia
{
    public struct CharacterInfo
    {
        public string _nickName;
        public int _chracterIndex;
        public int _accountLevel;
        public int _slotIndex;
    }

    class DB_Query
    {
        //Server=127.0.0.1;Port=3306;Database=cardbattle;Uid=root;Pwd=1234;
        string _connectionString;

        public DB_Query(string serverIP, string port, string database, string uID, string Pwd)
        {
            _connectionString = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};", serverIP, port, database, uID, Pwd);
        }

        public bool CheckLogIn(string id, string pw)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string searchQuery = string.Format("SELECT Pw FROM userinfo WHERE ID = '{0}';", id);

                try
                {
                    MySqlCommand command = new MySqlCommand(searchQuery, connection);

                    MySqlDataReader table = command.ExecuteReader();

                    while (table.Read())
                    {
                        if (table["Pw"].ToString().Equals(pw))
                        {
                            table.Close();
                            return true;
                        }
                    }

                    table.Close();
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public long SearchUUID(string id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string searchQuery = string.Format("SELECT UUID FROM userinfo WHERE ID = '{0}';", id);

                try
                {
                    MySqlCommand command = new MySqlCommand(searchQuery, connection);

                    MySqlDataReader table = command.ExecuteReader();

                    while (table.Read())
                    {
                        long uuid = long.Parse(table["UUID"].ToString());
                        table.Close();
                        return uuid;
                    }

                    table.Close();
                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                    return 0;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public long SearchUUIDwithNickName(string nickName)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string searchQuery = string.Format("SELECT UUID FROM characterinfo WHERE NickName = '{0}';", nickName);

                try
                {
                    MySqlCommand command = new MySqlCommand(searchQuery, connection);

                    MySqlDataReader table = command.ExecuteReader();

                    while (table.Read())
                    {
                        long uuid = long.Parse(table["UUID"].ToString());
                        table.Close();
                        return uuid;
                    }

                    table.Close();
                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                    return 0;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool SearchID(string id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string searchQuery = string.Format("SELECT * FROM userinfo WHERE ID = '{0}';", id);

                try
                {
                    MySqlCommand command = new MySqlCommand(searchQuery, connection);

                    MySqlDataReader table = command.ExecuteReader();

                    while (table.Read())
                    {
                        if (table["ID"].ToString().Equals(id))
                        {
                            table.Close();
                            return true;
                        }
                    }

                    table.Close();
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool SearchNickName(string nickname)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string searchQuery = string.Format("SELECT * FROM userinfo WHERE ID = '{0}';", nickname);

                try
                {
                    MySqlCommand command = new MySqlCommand(searchQuery, connection);

                    MySqlDataReader table = command.ExecuteReader();

                    while (table.Read())
                    {
                        if (table["ID"].ToString().Equals(nickname))
                        {
                            table.Close();
                            return true;
                        }
                    }

                    table.Close();
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void SearchCharacterInfo(long uuid, List<CharacterInfo> characInfoList)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string searchQuery = string.Format("SELECT NickName,CharacterIndex,AccountLevel,SlotIndex FROM characterinfo WHERE UUID = '{0}';", uuid);

                try
                {
                    MySqlCommand command = new MySqlCommand(searchQuery, connection);

                    MySqlDataReader table = command.ExecuteReader();
                    
                    while (table.Read())
                    {
                        CharacterInfo characInfo;
                        characInfo._nickName = table["NickName"].ToString();
                        characInfo._chracterIndex = int.Parse(table["CharacterIndex"].ToString());
                        characInfo._accountLevel = int.Parse(table["AccountLevel"].ToString());
                        characInfo._slotIndex = int.Parse(table["SlotIndex"].ToString());

                        characInfoList.Add(characInfo);
                    }

                    table.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public int SearchCharacterIndex(string nickName)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string searchQuery = string.Format("SELECT CharacterIndex FROM characterinfo WHERE NickName = '{0}';", nickName);

                try
                {
                    MySqlCommand command = new MySqlCommand(searchQuery, connection);

                    MySqlDataReader table = command.ExecuteReader();

                    while (table.Read())
                    {
                        return int.Parse(table["CharacterIndex"].ToString());
                    }

                    table.Close();
                    return -1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }

            return -1;
        }

        public void SearchLevelInfo(string nickName, List<int> level)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string searchQuery = string.Format("SELECT AccountLevel,PhysicsLevel,ChemistryLevel,BiologyLevel,AstronomyLevel FROM characterinfo WHERE NickName = '{0}';", nickName);

                try
                {
                    MySqlCommand command = new MySqlCommand(searchQuery, connection);

                    MySqlDataReader table = command.ExecuteReader();

                    while (table.Read())
                    {
                        level.Add(int.Parse(table["AccountLevel"].ToString()));
                        level.Add(int.Parse(table["PhysicsLevel"].ToString()));
                        level.Add(int.Parse(table["ChemistryLevel"].ToString()));
                        level.Add(int.Parse(table["BiologyLevel"].ToString()));
                        level.Add(int.Parse(table["AstronomyLevel"].ToString()));
                    }

                    table.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public int SearchAccountLevel(string nickName)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string searchQuery = string.Format("SELECT AccountLevel FROM characterinfo WHERE NickName = '{0}';", nickName);

                try
                {
                    MySqlCommand command = new MySqlCommand(searchQuery, connection);

                    MySqlDataReader table = command.ExecuteReader();

                    while (table.Read())
                    {
                        return int.Parse(table["AccountLevel"].ToString());
                    }

                    table.Close();
                    return -1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }

                return -1;
            }
        }

        public void SearchExpInfo(string nickName, List<int> exp)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string searchQuery = string.Format("SELECT AccountExp,PhysicsExp,ChemistryExp,BiologyExp,AstronomyExp FROM characterinfo WHERE NickName = '{0}';", nickName);

                try
                {
                    MySqlCommand command = new MySqlCommand(searchQuery, connection);

                    MySqlDataReader table = command.ExecuteReader();

                    while (table.Read())
                    {
                        exp.Add(int.Parse(table["AccountExp"].ToString()));
                        exp.Add(int.Parse(table["PhysicsExp"].ToString()));
                        exp.Add(int.Parse(table["ChemistryExp"].ToString()));
                        exp.Add(int.Parse(table["BiologyExp"].ToString()));
                        exp.Add(int.Parse(table["AstronomyExp"].ToString()));
                    }

                    table.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void SearchCardReleaseInfo(string nickName, List<int> cardRelease)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string searchQuery = string.Format("SELECT CardIndex FROM cardreleaseinfo WHERE NickName = '{0}';", nickName);

                try
                {
                    MySqlCommand command = new MySqlCommand(searchQuery, connection);

                    MySqlDataReader table = command.ExecuteReader();

                    while (table.Read())
                    {
                        cardRelease.Add(int.Parse(table["CardIndex"].ToString()));
                    }

                    table.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void SearchCardRentalInfo(string nickName, List<int> cardRental)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string searchQuery = string.Format("SELECT CardIndex FROM cardrentalinfo WHERE NickName = '{0}';", nickName);

                try
                {
                    MySqlCommand command = new MySqlCommand(searchQuery, connection);

                    MySqlDataReader table = command.ExecuteReader();

                    while (table.Read())
                    {
                        cardRental.Add(int.Parse(table["CardIndex"].ToString()));
                    }

                    table.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void SearchRentalTimeInfo(string nickName, List<float> rentaltime)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string searchQuery = string.Format("SELECT TimeRemaining FROM cardrentalinfo WHERE NickName = '{0}';", nickName);

                try
                {
                    MySqlCommand command = new MySqlCommand(searchQuery, connection);

                    MySqlDataReader table = command.ExecuteReader();

                    while (table.Read())
                    {
                        rentaltime.Add(float.Parse(table["TimeRemaining"].ToString()));
                    }

                    table.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void SearchMyDeckInfo(string nickName, List<int> mydeck)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string searchQuery = string.Format("SELECT CardIndex FROM mycardinfo WHERE NickName = '{0}';", nickName);

                try
                {
                    MySqlCommand command = new MySqlCommand(searchQuery, connection);

                    MySqlDataReader table = command.ExecuteReader();

                    while (table.Read())
                    {
                        mydeck.Add(int.Parse(table["CardIndex"].ToString()));
                    }

                    table.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool InsertUserInfo(string id, string pw)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = string.Format("INSERT INTO userinfo(ID, PW) VALUES ('{0}','{1}');", id, pw);

                try
                {
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine("Insert Success");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Insert Fail");
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool InsertCharacterInfo(long uuid, string nickName, int characIndex, int slot)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = string.Format("INSERT INTO characterinfo(NickName, UUID, CharacterIndex, SlotIndex) VALUES ('{0}',{1},{2},{3});", nickName, uuid, characIndex, slot);

                try
                {
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine("Insert Success");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Insert Fail");
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void InsertCardReleaseInfo(string nickName, int cardIndex)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = string.Format("INSERT INTO cardreleaseinfo(NickName, CardIndex) VALUES ('{0}',{1});", nickName, cardIndex);

                try
                {
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine("Insert Success");
                    }
                    else
                    {
                        Console.WriteLine("Insert Fail");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
