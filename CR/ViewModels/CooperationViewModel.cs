using System;
using Avalonia.Collections;
using CR.Models;
using MySqlConnector;

namespace CR.ViewModels;

public class CooperationViewModel: ViewModelBase
{
    private static readonly string _connectionString =
        "server=10.10.1.24;user=user_01;password=user01pro;database=pro1_23";

    private AvaloniaList<Cooperation> GetCooperationFromDB()
    {
        AvaloniaList<Cooperation> cooperations = new AvaloniaList<Cooperation>();
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                string selectAllCooperations = "SELECT Cooperation.ID, Form, StartDate, Teacher.FullName, Parent.FullName FROM Cooperation join Teacher on Cooperation.Teacher = Teacher.ID join Parent on Cooperation.Parent = Parent.ID";
                MySqlCommand cmd = new MySqlCommand(selectAllCooperations, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cooperation cooperationItem = new Cooperation();
                    if (!reader.IsDBNull(reader.GetOrdinal("ID")))
                    {
                        cooperationItem.ID = reader.GetInt32("ID");
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("Teacher")))
                    {
                        cooperationItem.Teacher = reader.GetInt32("Teacher");
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("Parent")))
                    {
                        cooperationItem.Parent = reader.GetInt32("Parent");
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("Form")))
                    {
                        cooperationItem.Form = reader.GetString("Form");
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("StartDate")))
                    {
                        cooperationItem.StartDate = reader.GetDateTimeOffset("StartDate");
                    }

                    cooperations.Add(cooperationItem);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Ошибка: " + ex);
            }
            finally
            {
                connection.Close();
            }
        }

        return cooperations;
    }
}