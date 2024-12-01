using Assignment2_MooreJoshua.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment2_MooreJoshua.Services
{
    public class CrudOperationsTypedDataSet
    {
        #region Private Members

        private HarryRosenWatchesDBDataSetTableAdapters.ItemsTableAdapter _adapter;
        private HarryRosenWatchesDBDataSet.ItemsDataTable _tbItems;
        #endregion

        #region Constructor

        public CrudOperationsTypedDataSet()
        {
            _adapter = new HarryRosenWatchesDBDataSetTableAdapters.ItemsTableAdapter();
            _tbItems = new HarryRosenWatchesDBDataSet.ItemsDataTable();
        }
        #endregion

        #region Methods Get, Add, Edit, Delete Items
        public void GetAllItems(ObservableCollection<Item> Items)
        {
            try
            {
                _tbItems = _adapter.GetItems();

                if (_tbItems != null)
                {
                    foreach (var row in _tbItems)
                    {
                        var Item = new Item
                        {
                            ItemId = row.ItemId,
                            ItemName = row.ItemName,
                            ItemDescription = row.ItemDescription,
                            ItemPrice = row.ItemPrice,
                        };

                        Items.Add(Item);
                    }
                }
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }

        public string GetItemNameById(int id)
        {
            var row = _tbItems.FindByItemId(id);

            if (row != null)
            {
                return row.ItemName;

            }
            return String.Empty;
        }

        public void InsertItem(string name, string description, decimal price, string imageSource, string type)
        {
            if (IsParameterEmpty(name))
            {
                MessageBox.Show("Enter a name for the Item!");
                return;
            }

            try
            {
                _adapter.Insert(name, description, price, type, imageSource); // Ensure the order matches the SQL query
                MessageBox.Show($"Item {name} added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        public void GetItemsByType(ObservableCollection<Item> items, string itemType)
        {
            try
            {
                // Fetch data from the database using the new query
                _tbItems = _adapter.GetItemsByType(itemType);

                if (_tbItems != null)
                {
                    foreach (var row in _tbItems)
                    {
                        var item = new Item
                        {
                            ItemId = row.ItemId,
                            ItemName = row.ItemName,
                            ItemDescription = row.ItemDescription,
                            ItemPrice = row.ItemPrice,
                            ItemType = row.ItemType,
                            ItemImageSource = row.ItemImageSource
                        };

                        items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }


        public void EditItem(int id, string name, string description, decimal price)
        {
            if (IsParameterEmpty(name))
            { 
                MessageBox.Show("Enter a name for Item!");
                return;
            }

            if (IsParameterEmpty(description))
            {
                MessageBox.Show("Enter a description for Item!");
                return;
            }

            if (price <= 0.0m)
            {
                MessageBox.Show("Enter a valid price for Item!");
                return;
            }

            try
            {
                var row = _tbItems.FindByItemId(id);
                if (row != null)
                {
                    row.ItemName = name;  
                    row.ItemDescription = description; 
                    row.ItemPrice = price;  
                    _adapter.Update(_tbItems); 
                    MessageBox.Show($"Item {name} updated successfully!");
                }
                else
                {
                    MessageBox.Show("Item not found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                throw;
            }
        }


        private bool IsParameterEmpty(string parameter)
        {
            return string.IsNullOrWhiteSpace(parameter);
        }

        public void DeleteItem(int id)
        {
            if (id <= 0)
            {
                MessageBox.Show("Please select a valid Item to delete!");
                return;
            }

            if (id > 0 && id <= 12)
            {
                MessageBox.Show("You cannot delete this item as it is protected!");
                return;
            }

            try
            {
                var name = GetItemNameById(id);

                if (!string.IsNullOrEmpty(name))
                {
                    _adapter.Delete(id, name);
                    MessageBox.Show($"Item {name} deleted successfully!");
                }
                else
                {
                    MessageBox.Show("The Item does not exist.");
                }
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }


        public void GetItemByName(string name)
        {
            _tbItems = _adapter.GetItemByName(name);
            if (_tbItems != null && _tbItems.Count > 0)
            {
                var row = _tbItems[0];

                MessageBox.Show($"You selected {row.ItemName} for deletion!");
            }
        }

        #endregion

    }
}
