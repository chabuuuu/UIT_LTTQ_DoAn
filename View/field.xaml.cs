﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace LTTQ_DoAn.View
{
    /// <summary>
    /// Interaction logic for field.xaml
    /// </summary>
    public partial class field : UserControl
    {
        //List<string> fields = new List<string>();
        public field()
        {
            InitializeComponent();  
            /*
            ObservableCollection<Field> fields = new ObservableCollection<Field>();
            fields.Add(new Field { Name = "temp 1" });
            fields.Add(new Field { Name = "temp 1" });
            fields.Add(new Field { Name = "temp 1" });
            fields.Add(new Field { Name = "temp 1" });
            //fields.Add(new Field { Name = "temp 1" });
            FieldList.ItemsSource = fields;
            */
            
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        /*
        public class Field
        {
            public string Name { get; set; }
        }
        */

    }
}
