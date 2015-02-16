#Dynamic WPF Grid

This is a basic example of a way to allow merging/splitting of grid cells in a WPF application.

##Basic idea

First, this doesn't attempt to dynamically change the number of rows or columns in the actual Grid object. Instead, cells can emit Merge and Split events to which the main window can respond by creating or deleting cells and changing their Row, Column, and ColumnSpan attached properties.

##TODO

- Add row merge/split functions
