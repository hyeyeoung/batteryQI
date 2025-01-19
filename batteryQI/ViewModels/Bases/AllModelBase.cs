using batteryQI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace batteryQI.ViewModels.Bases
{
    // 모든 뷰 모델의 베이스 뷰 모델을 만들어 관리
    internal partial class AllModelBase : ObservableObject
    {
        protected Manager _manager;
        protected Manufacture _manufactureList;
        protected Battery _battery;
        protected DBlink DBConnection;
        public Manager Manager
        {
            get => _manager;
            set { _manager = value; }
        }
        public Battery Battery
        {
            get => _battery;
            set { _battery = value; }
        }
        public Manufacture ManufactureList
        {
            get => _manufactureList;
            set
            {
                _manufactureList = value;
                //OnPropertyChanged("ManufactureList");
            }
        }

        //// View Property Binding 방법 2
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged(string name)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null)
        //    {
        //        handler(this, new PropertyChangedEventArgs(name));
        //    }
        //}
    }
}
