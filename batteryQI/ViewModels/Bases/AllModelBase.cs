using batteryQI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace batteryQI.ViewModels.Bases
{
    // 모든 뷰 모델의 베이스 뷰 모델을 만들어 관리
    internal partial class AllModelBase : ObservableObject
    {
        protected Manager _manager;
        protected Manufacture _maufactureList;
        protected Battery _battery;
        protected DBlink DBConnection;
        public Manager Manager
        {
            get => _manager;
            set => SetProperty(ref _manager, value);
        }
        public Battery Battery
        {
            get => _battery;
            set => SetProperty(ref _battery, value);
        }
        public Manufacture ManufactureList
        {
            get => _maufactureList;
            set => SetProperty(ref _maufactureList, value);
        }
    }
}
