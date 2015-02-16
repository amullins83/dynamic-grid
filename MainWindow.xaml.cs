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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var numRows = grid.RowDefinitions.Count;
            var numCols = grid.ColumnDefinitions.Count;

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    var item = new DynamicGridItem(j, i);
                    item.Merge += HandleMerge;
                    item.Split += HandleSplit;
                    grid.Children.Add(item);
                    Grid.SetRow(item, i);
                    Grid.SetColumn(item, j);
                }
            }
        }

        private void HandleMerge(object sender, MergeEventArgs args) {
            if (args.IsLeftMerge) {
                HandleLeftMerge((DynamicGridItem)sender);
            }
            else
            {
                HandleRightMerge((DynamicGridItem)sender);
            }
        }

        private void HandleLeftMerge(DynamicGridItem item)
        {
            HandleMergeOffset(item, -1);
        }

        private void HandleRightMerge(DynamicGridItem item)
        {
            HandleMergeOffset(item, 1);
        }

        private void HandleMergeOffset(DynamicGridItem item, int offset)
        {
            var otherItem = FindItemByXOffset(item, offset);
            if (otherItem == null)
            {
                return; // Nothing to do
            }

            otherItem.Merge -= HandleMerge;
            otherItem.Split -= HandleSplit;
            Grid.SetColumnSpan(item, Grid.GetColumnSpan(item) + Grid.GetColumnSpan(otherItem));
            grid.Children.Remove(otherItem);

            if (offset < 0) // Reposition item if merging left
            {
                Grid.SetColumn(item, otherItem.X);
                item.X = otherItem.X;
            }
        }

        private DynamicGridItem FindItemByXOffset(DynamicGridItem item, int xOffset)
        {
            var numRows = grid.RowDefinitions.Count;
            var numCols = grid.ColumnDefinitions.Count;

            int x = item.X + xOffset;

            int xSign = xOffset < 0 ? -1 : 1;

            while (x >= 0 && x < numCols)
            {
                var otherItem = FindItem(x, item.Y);
                if (otherItem != null)
                {
                    return otherItem;
                }
                
                x += xSign;
            }

            return null;
        }

        private DynamicGridItem FindItem(int x, int y)
        {
            foreach (var childObject in grid.Children)
            {
                var child = childObject as DynamicGridItem;
                if (Grid.GetColumn(child) == x && Grid.GetRow(child) == y)
                {
                    return child;
                }
            }

            return null;
        }

        private void HandleSplit(object sender, EventArgs e)
        {
            var item = (DynamicGridItem)sender;
            var itemColSpan = Grid.GetColumnSpan(item);
            if (itemColSpan < 2)
            {
                return; // Nothing to do
            }

            var newItem = new DynamicGridItem(item.X, item.Y);
            newItem.Merge += HandleMerge;
            newItem.Split += HandleSplit;
            grid.Children.Add(newItem);
            Grid.SetColumn(newItem, newItem.X);
            Grid.SetRow(newItem, newItem.Y);
            Grid.SetColumn(item, item.X + 1);
            Grid.SetColumnSpan(item, itemColSpan - 1);
            item.X += 1;
        }
    }
}
