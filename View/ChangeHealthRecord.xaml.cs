﻿using System;
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
using System.Windows.Shapes;

namespace LTTQ_DoAn.View
{
    /// <summary>
    /// Interaction logic for ChangeHealthRecord.xaml
    /// </summary>
    public partial class ChangeHealthRecord : Window
    {
        public ChangeHealthRecord()
        {
            InitializeComponent();
        }

        private void bt_minimize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Tính toán số lượng dòng và cập nhật chiều cao của TextBox
            int lineCount = conclude_textbox.LineCount;
            double lineHeight = conclude_textbox.FontSize;
            double newHeight = lineCount * lineHeight;

            // Đặt chiều cao mới cho TextBox
            conclude_textbox.Height = newHeight;
        }
    }
}
