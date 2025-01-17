using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using batteryQI.Models;
using batteryQI.ViewModels.Bases;
using CommunityToolkit.Mvvm.Input;

namespace batteryQI.ViewModels
{
    internal partial class MainWindowViewModel : ViewModelBases
    {
        public MainWindowViewModel()
        {

        }

        // 클릭 이벤트 등록
        [RelayCommand]
        private void LinkDB()
        {
            DBlink x = new();
            x.Connect(); // 링크
        }

    }
}
