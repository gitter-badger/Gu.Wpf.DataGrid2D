﻿namespace Gu.Wpf.DataGrid2D.Demo
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Gu.Wpf.DataGrid2D.Demo.Annotations;

    public class Vm : INotifyPropertyChanged
    {
        private object _selectedItem;

        public Vm()
        {
            Data2D = new[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            ColumnHeaders = new[] { "Col1", "Col2" };
            JaggedRows = new int[3][];
            JaggedRows[0] = new[] { 1, 2 };
            JaggedRows[1] = new[] { 3, 4 };
            JaggedRows[2] = new[] { 5, 6 };

            int count = 1;
            ListOfListsOfItems = new List<List<ItemVm>>();
            RowVms = new List<RowVm>();
            AllRowsItems = new List<ItemVm>();
            for (int i = 0; i < 3; i++)
            {
                var row = new List<ItemVm>();
                ListOfListsOfItems.Add(row);

                var rowVm = new RowVm("Row" + i);
                RowVms.Add(rowVm);
                for (int j = 0; j < 2; j++)
                {
                    row.Add(new ItemVm(count));
                    var itemVm = new ItemVm(count);
                    rowVm.Add(itemVm);
                    AllRowsItems.Add(itemVm);
                    count++;
                }
            }

            ColumnItemHeaders = Enumerable.Range(0, 2)
                                          .Select(x => new ItemVm(x))
                                          .ToArray();
            RowHeaders = Enumerable.Range(0, 3).Select(x => "Row" + x).ToArray();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string[] ColumnHeaders { get; private set; }
        
        public string[] RowHeaders { get; private set; }
       
        public ItemVm[] ColumnItemHeaders { get; private set; }
        
        public int[][] JaggedRows { get; private set; }

        public List<List<ItemVm>> ListOfListsOfItems { get; private set; }

        public List<ItemVm> AllRowsItems { get; private set; }
        
        public List<RowVm> RowVms { get; private set; } 

        public int[,] Data2D { get; private set; }

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (Equals(value, _selectedItem))
                {
                    return;
                }
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
