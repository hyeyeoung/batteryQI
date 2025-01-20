﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using batteryQI.Models;
using batteryQI.Views;
using System.Windows.Controls;
using System.ComponentModel;

namespace batteryQI.ViewModels.Bases
{
    public partial class ViewModelBases : ObservableObject
    {
        private Manager _manager = Manager.Instance();
        public ViewModelBases()
        {
        }

        // 클릭 이벤트 등록
        [RelayCommand]
        private void LinkDB()
        {
            DBlink x = DBlink.Instance();
            x.Connect(); // 링크
        }
    }
}
