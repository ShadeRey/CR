using System;
using System.Data;
using Avalonia.Collections;
using CR.Models;
using MySqlConnector;
using ReactiveUI;

namespace CR.ViewModels;

public class CooperationViewModel: ViewModelBase
{
    public CooperationViewModel()
    {
        Cooperation = GetCooperationFromDB();
    }
    
    private static readonly string _connectionString =
        //"server=10.10.1.24;user=user_01;password=user01pro;database=pro1_23";
        "Server=localhost;Database=UP;User Id=root;Password=sharaga228;";

    private AvaloniaList<CooperationModel> _cooperation;

    public AvaloniaList<CooperationModel> Cooperation
    {
        get => _cooperation;
        set => this.RaiseAndSetIfChanged(ref _cooperation, value);
    }

    private AvaloniaList<CooperationModel> _cooperationPreSearch;

    public AvaloniaList<CooperationModel> CooperationPreSearch
    {
        get => _cooperationPreSearch;
        set => this.RaiseAndSetIfChanged(ref _cooperationPreSearch, value);
    }
    

    private AvaloniaList<CooperationModel> GetCooperationFromDB()
    {
        AvaloniaList<CooperationModel> cooperations = new AvaloniaList<CooperationModel>();
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                string selectAllCooperations = """
                                               SELECT Cooperation.ID, Form, StartDate, Teacher.TeacherFullName, Parent.ParentFullName 
                                               FROM Cooperation 
                                                   join Teacher on Cooperation.Teacher = Teacher.ID 
                                                   join Parent on Cooperation.Parent = Parent.ID
                                               """;
                MySqlCommand cmd = new MySqlCommand(selectAllCooperations, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CooperationModel cooperationItem = new CooperationModel();
                    if (!reader.IsDBNull(reader.GetOrdinal("ID")))
                    {
                        cooperationItem.ID = reader.GetInt32("ID");
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("TeacherFullName")))
                    {
                        cooperationItem.Teacher = reader.GetString("TeacherFullName");
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("ParentFullName")))
                    {
                        cooperationItem.Parent = reader.GetString("ParentFullName");
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