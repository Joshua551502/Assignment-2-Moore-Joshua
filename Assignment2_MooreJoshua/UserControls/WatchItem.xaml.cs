using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment2_MooreJoshua.UserControls
{
    /// <summary>
    /// Interaction logic for WatchItem.xaml
    /// </summary>
    public partial class WatchItem : UserControl
    {
        public WatchItem()
        {
            InitializeComponent();
        }

        public string ItemImageSource
        {
            get { return (string)GetValue(ItemImageSourceProperty); }
            set { SetValue(ItemImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemImageSourceProperty =
            DependencyProperty.Register("ItemImageSource", typeof(string), typeof(WatchItem), new PropertyMetadata(null));

        public string ItemName
        {
            get { return (string)GetValue(ItemNameProperty); }
            set { SetValue(ItemNameProperty, value); }
        }
        public static readonly DependencyProperty ItemNameProperty =
            DependencyProperty.Register("ItemName", typeof(string), typeof(WatchItem), new PropertyMetadata(null));

        public string ItemDescription
        {
            get { return (string)GetValue(ItemDescriptionProperty); }
            set { SetValue(ItemDescriptionProperty, value); }
        }


        public static readonly DependencyProperty ItemDescriptionProperty =
            DependencyProperty.Register("ItemDescription", typeof(string), typeof(WatchItem), new PropertyMetadata(null));


        public string ItemPrice
        {
            get { return (string)GetValue(ItemPriceProperty); }
            set { SetValue(ItemPriceProperty, value); }
        }

        public static readonly DependencyProperty ItemPriceProperty =
            DependencyProperty.Register("ItemPrice", typeof(string), typeof(WatchItem), new PropertyMetadata(null));


    }
}
