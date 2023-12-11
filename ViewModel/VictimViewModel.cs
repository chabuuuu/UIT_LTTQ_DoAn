﻿using LTTQ_DoAn.Model;
using LTTQ_DoAn.View;
using LTTQ_DoAn.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LTTQ_DoAn.ViewModel
{
    public class VictimViewModel: BaseViewModel
    {
        public ICommand ViewCommand { get; }
        public ICommand ChangeCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        private List<BENHNHAN> victims;
        private BENHNHAN selectedItem = null;

        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();

        public List<BENHNHAN> Victims { get => victims; set => victims = value; }
        public BENHNHAN SelectedItem { get => selectedItem; set 
            { 
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            } 
        }

        private void Load()
        {
            Victims = _db.BENHNHAN.ToList();
            //System.Windows.MessageBox.Show("Done");
        }

        public VictimViewModel()
        {
            Load();
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            ViewCommand = new ViewModelCommand(ExecuteViewCommand, CanExecuteViewCommand);
            ChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
        }

        private bool CanExecuteAddCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteAddCommand(object? obj)
        {
            //MessageBox.Show(this.selectedItem.SUB_ID.ToString());
            AddVictim wd = new AddVictim(this.selectedItem.MABENHNHAN);
            //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
            // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }

        //tham số 1 điều kiện để xóa lịch khám
        private bool CanExecuteDeleteCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteDeleteCommand(object? obj)
        {

        }

        private bool CanExecuteViewCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteViewCommand(object? obj)
        {
            ViewVictim wd = new ViewVictim();
            //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
            // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
        private bool CanExecuteChangeCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteChangeCommand(object? obj)
        {
            ChangeVictim wd = new ChangeVictim();
            //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
            // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
    }
}
