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
    public partial class WristWatchViewModel
    {
        #region Properties
        private TextBox _tbWristWatchName;
        private TextBox _tbWristWatchDescription;
        private TextBox _tbWristWatchPrice;

        public TextBox TbWristWatchName
        {
            get { return _tbWristWatchName; }
            set { _tbWristWatchName = value; }
        }
        public TextBox TbWristWatchDescription
        {
            get { return _tbWristWatchDescription; }
            set { _tbWristWatchDescription = value; }
        }

        public TextBox TbWristWatchPrice
        {
            get { return _tbWristWatchPrice; }
            set { _tbWristWatchPrice = value; }
        }
        public int WristWatchId { get; set; }

        public ObservableCollection<Item> WristWatchList { get; set; } = new ObservableCollection<Item>();

        #endregion

        #region Private Members

        private readonly CrudOperationsTypedDataSet _crudService;

        #endregion

        #region Constructor

        public WristWatchViewModel()
        {
            _tbWristWatchName = new TextBox();
            _tbWristWatchDescription = new TextBox();
            _tbWristWatchPrice= new TextBox();
            _crudService = new CrudOperationsTypedDataSet();
            LoadItems();
        }

        #endregion

        #region Load Data
        public void LoadItems()
        {
            if (WristWatchList != null)
            {
                WristWatchList.Clear();
                _crudService.GetItemsByType(WristWatchList, "Watch");
            }
        }

        #endregion

        #region Initialize Input
        public void InitializeUserInput(TextBox tbWristWatchName, TextBox tbWristWatchDescription, TextBox tbWristWatchPrice)
        {
            _tbWristWatchName = tbWristWatchName;
            _tbWristWatchDescription = tbWristWatchDescription;
            _tbWristWatchPrice = tbWristWatchPrice; 
        }
        #endregion

        #region Helpers
        public void ClearUserInput()
        {
            _tbWristWatchName.Text = string.Empty;
            _tbWristWatchDescription.Text = string.Empty;
            _tbWristWatchPrice.Text = string.Empty;
        }

        public void RefreshView()
        {
            ClearUserInput();
            LoadItems();
            WristWatchId = -1;
        }
        #endregion

        #region Relay Commands

        public void SelectWristWatch(int id)
        {
            WristWatchId = id;

            var selectedWristWatch = WristWatchList.FirstOrDefault(c => c.ItemId == id);
            if (selectedWristWatch != null)
            {
                _tbWristWatchName.Text = selectedWristWatch.ItemName;
                _tbWristWatchDescription.Text = selectedWristWatch.ItemDescription;
                _tbWristWatchPrice.Text = selectedWristWatch.ItemPrice.ToString("F2");

            }
        }

        public async Task AddWristWatch()
        {
            var WristWatchName = _tbWristWatchName.Text;
            var WristWatchDescription = _tbWristWatchDescription.Text;
            var WristWatchPrice = _tbWristWatchPrice.Text;

            if (string.IsNullOrWhiteSpace(WristWatchName))
            {
                MessageBox.Show("The Watch Name field cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(WristWatchDescription))
            {
                MessageBox.Show("The Watch Description field cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(WristWatchPrice))
            {
                MessageBox.Show("The Watch Price field cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(WristWatchPrice, out decimal price))
            {
                MessageBox.Show("Invalid price. Please enter a valid decimal value.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                const string placeholderImagePath = "/Assets/Images/Watches/placeholder-wrist.png";
                const string defaultType = "Watch";

                await Task.Run(() => _crudService.InsertItem(WristWatchName, WristWatchDescription, price, placeholderImagePath, defaultType));
                RefreshView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public async Task EditWristWatch()
        {
            if (WristWatchId <= 0)
            {
                MessageBox.Show("Please select an item to edit before proceeding.");
                return;
            }

            var WristWatchName = _tbWristWatchName.Text;
            var WristWatchDescription = _tbWristWatchDescription.Text;
            var WristWatchPrice = _tbWristWatchPrice.Text;

            if (!string.IsNullOrWhiteSpace(WristWatchName))
            {
                if (decimal.TryParse(WristWatchPrice, out decimal price))
                {
                    _crudService.EditItem(WristWatchId, WristWatchName, WristWatchDescription, price);
                    RefreshView();
                }
                else
                {
                    await Task.FromException(new Exception("Invalid price. Please enter a valid decimal value."));
                }
            }
            else
            {
                await Task.FromException(new Exception("WristWatch name cannot be empty."));
            }
        }



        public async Task DeleteWristWatch()
        {
            if (WristWatchId > 0)
            {
                try
                {
                    await Task.Run(() => _crudService.DeleteItem(WristWatchId));
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



        public async Task<string> GetWristWatchNameById()
        {
            if (WristWatchId > 0)
            {
                return _crudService.GetItemNameById(WristWatchId);
            }

            await Task.FromException(new Exception("Invalid WristWatch ID."));
            return string.Empty;
        }

        #endregion
    }
}
