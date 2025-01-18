using CommunityToolkit.Mvvm.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace batteryQI.Models
{
    // 제조사 목록 클래스
    internal class Manufacture : ObservableObject
    {
        private string _manufacId; // 제조사 ID
        private string _manufacName; // 제조사 이름

        public string ManufacId
        {
            get { return _manufacId; }
            set
            {
                SetProperty(ref _manufacId, value);
            }
        }

        public string ManufacName
        {
            get { return _manufacName; }
            set
            {
                SetProperty(ref _manufacName, value);
            }
        }
    }
}
