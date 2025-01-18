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
        private List<int> _manufacId; // 제조사 ID
        private List<string> _manufacName; // 제조사 이름
        static Manufacture manufactureList; // singleton 탬플릿. 제조사 목록이 여러개 있을 필요 X.
        public List<int> ManufacId
        {
            get { return _manufacId; }
            set
            {
                SetProperty(ref _manufacId, value);
            }
        }
        public List<string> ManufacName
        {
            get { return _manufacName; }
            set
            {
                SetProperty(ref _manufacName, value);
            }
        }

        private Manufacture() { }
        public static Manufacture Instance()
        {
            if (manufactureList == null)
            {
                manufactureList = new Manufacture();
            }
            return manufactureList;
        }

    }
}
