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
using System.Windows.Forms;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using System.Data.Entity.Infrastructure;

namespace LTTQ_DoAn.ViewModel
{
    public class VictimViewModel : BaseViewModel
    {
        public ICommand ViewCommand { get; }
        public ICommand ViewHealthRecordCommand { get; }
        public ICommand ChangeCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        private List<BENHNHAN> victims;
        private BENHNHAN selectedItem = null;

        QUANLYBENHVIENEntities _db;
        public List<BENHNHAN> Victims { get => victims; set
            {
                victims = value;
                OnPropertyChanged(nameof(Victims));
            }
        }
        public BENHNHAN SelectedItem { get => selectedItem; set 
            { 
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private void Load()
        {
            _db = new QUANLYBENHVIENEntities();
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
            ViewHealthRecordCommand = new ViewModelCommand(ExecuteViewHealthRecordCommand, CanExecuteViewHealthRecordCommand);
        }

        private bool CanExecuteAddCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteAddCommand(object? obj)
        {
            //MessageBox.Show(this.selectedItem.SUB_ID.ToString());
            AddVictim wd = new AddVictim();
            wd.Closed += AddVictim_Closed;
            //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
            // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
        private void AddVictim_Closed(object sender, EventArgs e)
        {
            /*
            if (BaseViewModel.global_db.BENHNHAN.Count() > 0)
            {
                _db = BaseViewModel.global_db;
                // Xử lý sau khi cửa sổ AddVictim đã đóng
                // Điều này có thể là nơi bạn thực hiện các hành động sau khi cửa sổ đã đóng
                Victims = BaseViewModel.global_db.BENHNHAN.ToList();
            }*/
            Load();
        }
        //tham số 1 điều kiện để xóa lịch khám
        private bool CanExecuteDeleteCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteDeleteCommand(object? obj)
        {
            try
            {
                int Id = SelectedItem.MABENHNHAN;
                var deleteMember = _db.BENHNHAN.Where(m => m.MABENHNHAN == Id).Single();
                _db.BENHNHAN.DeleteObject(deleteMember);
                _db.SaveChanges();

                new MessageBoxCustom(
                    "Thông báo", 
                    "Đã xóa bệnh nhân: \nMã bệnh nhân: " +
                        SelectedItem.SUB_ID.ToString() + "\nHọ Tên: " +
                        SelectedItem.HOTEN.ToString(),
                    MessageType.Success,
                    MessageButtons.OK)
                    .ShowDialog();

                /*
                MessageBox.Show("Đã xóa bệnh nhân: \nMã bệnh nhân: " + 
                 SelectedItem.SUB_ID.ToString() + "\nHọ Tên: " + 
                 SelectedItem.HOTEN.ToString());
                */
                Load();
            }
            catch (DbUpdateException e)
            {
                //MessageBox.Show("Bạn cần xóa lịch khám của bệnh nhân này trước!");
                new MessageBoxCustom(
                    "Thông báo", 
                    "Bạn cần xóa lịch khám của bệnh nhân này trước!",
                    MessageType.Error, 
                    MessageButtons.OK
                    )
                    .ShowDialog();

            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message + "\nLỗi: " + e.GetType().ToString());
                new MessageBoxCustom(
                    "Thông báo",
                    e.Message + "\nLỗi: " + e.GetType().ToString(),
                    MessageType.Error,
                    MessageButtons.OK
                    )
                    .ShowDialog();
            }

        }

        private bool CanExecuteViewCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteViewCommand(object? obj)
        {
            ViewVictim wd = new ViewVictim();
            if (SelectedItem != null)
            {
                wd.DataContext = new ViewVictimViewModel(SelectedItem);
                Application.Current.MainWindow = wd;
                wd.ShowDialog();
            }
            //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
            // vi dụ nút cancel ở trong AddAppointmentViewModel.cs

        }
        private bool CanExecuteChangeCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteChangeCommand(object? obj)
        {
            ChangeVictim wd = new ChangeVictim();
            wd.Closed += ChangeVictim_Closed;
            if (SelectedItem != null)
            {
                wd.DataContext = new ChangeVictimViewModel(SelectedItem);
                //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
                // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
                Application.Current.MainWindow = wd;
                wd.ShowDialog();
            }
        }

        private void ChangeVictim_Closed(object sender, EventArgs e)
        {
            Load();
        }

        private bool CanExecuteViewHealthRecordCommand(object? obj)
        {
            return true;
        }

        private void ExecuteViewHealthRecordCommand(object? obj)
        {
            
            HealthRecordAndPrescription wd = new HealthRecordAndPrescription();
            if (SelectedItem != null)
            {
                wd.DataContext = new HealthRecordAndPrescriptionViewModel(SelectedItem, wd);
                //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
                // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
                Application.Current.MainWindow = wd;
                wd.ShowDialog();
            }
        }

      
    }
}