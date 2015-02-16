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

namespace DynamicGrid
{
    /// <summary>
    /// Interaction logic for DynamicGridItem.xaml
    /// </summary>
    public partial class DynamicGridItem : UserControl
    {
        public DynamicGridItem()
        {
            InitializeComponent();
        }

        public DynamicGridItem(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public event EventHandler<MergeEventArgs> Merge;

        public event EventHandler Split;

        public int X { get; set; }

        public int Y { get; set; }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            OnMerge(true);
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            OnMerge(false);
        }

        private void OnMerge(bool isLeft)
        {
            var m = Merge;
            if (m != null)
            {
                m(this, new MergeEventArgs(isLeft));
            }
        }

        private void Split_Click(object sender, RoutedEventArgs e)
        {
            var split = Split;
            if (split != null)
            {
                split(this, EventArgs.Empty);
            }
        }
    }
}
