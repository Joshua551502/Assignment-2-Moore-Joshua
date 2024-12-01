using Assignment2_MooreJoshua.Models;
using Assignment2_MooreJoshua.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Assignment2_MooreJoshua.UserControls
{
    public partial class PocketWatch : UserControl
    {
        private readonly PocketWatchViewModel ViewModel = new PocketWatchViewModel();

        public PocketWatch()
        {
            InitializeComponent();
            ViewModel.InitializeUserInput(tbWatchName, tbWatchDescription, tbWatchPrice);
            DataContext = ViewModel;
        }

        private async void AddItem_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.AddPocketWatch();
        }

        private async void EditItem_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.EditPocketWatch();
        }

        private void RefreshItem_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RefreshView();
        }

        private async void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.DeletePocketWatch();
        }

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var selectedItem = (Item)button.DataContext;
            ViewModel.SelectPocketWatch(selectedItem.ItemId);
        }
    }
}
