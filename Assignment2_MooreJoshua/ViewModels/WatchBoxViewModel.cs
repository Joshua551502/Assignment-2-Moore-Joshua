using Assignment2_MooreJoshua.Models;
using Assignment2_MooreJoshua.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Assignment2_MooreJoshua.ViewModels
{
    public partial class WatchBoxViewModel
    {
        #region Properties
        private TextBox _tbWatchBoxName;
        private TextBox _tbWatchBoxDescription;
        private TextBox _tbWatchBoxPrice;

        public TextBox TbWatchBoxName
        {
            get { return _tbWatchBoxName; }
            set { _tbWatchBoxName = value; }
        }
        public TextBox TbWatchBoxDescription
        {
            get { return _tbWatchBoxDescription; }
            set { _tbWatchBoxDescription = value; }
        }

        public TextBox TbWatchBoxPrice
        {
            get { return _tbWatchBoxPrice; }
            set { _tbWatchBoxPrice = value; }
        }
        public int WatchBoxId { get; set; }

        public ObservableCollection<Item> WatchBoxList { get; set; } = new ObservableCollection<Item>();

        #endregion

        #region Private Members

        private readonly CrudOperationsTypedDataSet _crudService;

        #endregion

        #region Constructor

        public WatchBoxViewModel()
        {
            _tbWatchBoxName = new TextBox();
            _tbWatchBoxDescription = new TextBox();
            _tbWatchBoxPrice = new TextBox();
            _crudService = new CrudOperationsTypedDataSet();
            LoadItems();
        }

        #endregion

        #region Load Data
        public void LoadItems()
        {
            if (WatchBoxList != null)
            {
                WatchBoxList.Clear();
                _crudService.GetItemsByType(WatchBoxList, "Watch Box");
            }
        }

        #endregion

        #region Initialize Input
        public void InitializeUserInput(TextBox tbWatchBoxName, TextBox tbWatchBoxDescription, TextBox tbWatchBoxPrice)
        {
            _tbWatchBoxName = tbWatchBoxName;
            _tbWatchBoxDescription = tbWatchBoxDescription;
            _tbWatchBoxPrice = tbWatchBoxPrice;
        }
        #endregion

        #region Helpers
        public void ClearUserInput()
        {
            _tbWatchBoxName.Text = string.Empty;
            _tbWatchBoxDescription.Text = string.Empty;
            _tbWatchBoxPrice.Text = string.Empty;
        }

        public void RefreshView()
        {
            ClearUserInput();
            LoadItems();
            WatchBoxId = -1;
        }
        #endregion

        #region Relay Commands

        public void SelectWatchBox(int id)
        {
            WatchBoxId = id;

            var selectedWatchBox = WatchBoxList.FirstOrDefault(c => c.ItemId == id);
            if (selectedWatchBox != null)
            {
                _tbWatchBoxName.Text = selectedWatchBox.ItemName;
                _tbWatchBoxDescription.Text = selectedWatchBox.ItemDescription;
                _tbWatchBoxPrice.Text = selectedWatchBox.ItemPrice.ToString("F2");

            }
        }

        public async Task AddWatchBox()
        {
            var WatchBoxName = _tbWatchBoxName.Text;
            var WatchBoxDescription = _tbWatchBoxDescription.Text;
            var WatchBoxPrice = _tbWatchBoxPrice.Text;

            if (string.IsNullOrWhiteSpace(WatchBoxName))
            {
                MessageBox.Show("The Watch Name field cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(WatchBoxDescription))
            {
                MessageBox.Show("The Watch Description field cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(WatchBoxPrice))
            {
                MessageBox.Show("The Watch Price field cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(WatchBoxPrice, out decimal price))
            {
                MessageBox.Show("Invalid price. Please enter a valid decimal value.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                const string placeholderImagePath = "/Assets/Images/Watches/placeholder-box.png"; // Corrected: This is the image path
                const string defaultType = "Watch Box"; // Corrected: This is the item type

                await Task.Run(() => _crudService.InsertItem(WatchBoxName, WatchBoxDescription, price, placeholderImagePath, defaultType));
                RefreshView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public async Task EditWatchBox()
        {
            if (WatchBoxId <= 0)
            {
                MessageBox.Show("Please select an item to edit before proceeding.");
                return;
            }

            var WatchBoxName = _tbWatchBoxName.Text;
            var WatchBoxDescription = _tbWatchBoxDescription.Text;
            var WatchBoxPrice = _tbWatchBoxPrice.Text;

            if (!string.IsNullOrWhiteSpace(WatchBoxName))
            {
                if (decimal.TryParse(WatchBoxPrice, out decimal price))
                {
                    _crudService.EditItem(WatchBoxId, WatchBoxName, WatchBoxDescription, price);
                    RefreshView();
                }
                else
                {
                    await Task.FromException(new Exception("Invalid price. Please enter a valid decimal value."));
                }
            }
            else
            {
                await Task.FromException(new Exception("WatchBox name cannot be empty."));
            }
        }



        public async Task DeleteWatchBox()
        {
            if (WatchBoxId > 0)
            {
                try
                {
                    await Task.Run(() => _crudService.DeleteItem(WatchBoxId));
                    RefreshView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while deleting the item: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete before proceeding.");
            }
        }



        public async Task<string> GetWatchBoxNameById()
        {
            if (WatchBoxId > 0)
            {
                return _crudService.GetItemNameById(WatchBoxId);
            }

            await Task.FromException(new Exception("Invalid WatchBox ID."));
            return string.Empty;
        }

        #endregion
    }
}
