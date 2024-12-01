using Assignment2_MooreJoshua.Models;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment2_MooreJoshua.Services
{
    public static class DataServices
    {
        public static string GetConnectionString()
        {
            return Properties.Settings.Default.HarryRosenWatchesDB;
        }

        public static async Task HandleException(Exception ex)
        {
            string msg = ex.Message;
            MessageBox.Show(msg);
            await Task.CompletedTask;
        }

        public static async Task GetItemsAsync(ObservableCollection<Item> items, string type = null)
        {
            try
            {
                string query = "SELECT ItemId, ItemName, ItemType, ItemDescription, ItemImageSource, ItemPrice FROM Items";

                if (!string.IsNullOrEmpty(type))
                {
                    query += " WHERE ItemType = @ItemType";
                }

                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        if (!string.IsNullOrEmpty(type))
                        {
                            cmd.Parameters.AddWithValue("@ItemType", type);
                        }

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            // Clear existing items
                            items.Clear();

                            while (await reader.ReadAsync())
                            {
                                var item = new Item
                                {
                                    ItemId = reader.GetInt32(0),
                                    ItemName = reader.GetString(1),
                                    ItemType = reader.GetString(2),
                                    ItemDescription = reader.GetString(3),
                                    ItemImageSource = reader.GetString(4),
                                    ItemPrice = reader.GetDecimal(5)
                                };

                                // Add item on UI thread
                                Application.Current.Dispatcher.Invoke(() => items.Add(item));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
    }

}

