using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace batteryQI.Models
{
    // 여러 파일에서 어느 제조사가 활성화, 비활성화 되었는지 확인하기 위해 싱글톤 활성화여부 클래스 구현
    internal class ManufactureActive
    {
        private static List<bool> _manufactureActive;
        private ManufactureActive() { }

        public static List<bool> Instance()
        {
            if (_manufactureActive == null)
            {
                _manufactureActive = new List<bool>();
            }
            return _manufactureActive;
        }
    }
    internal class Manufacture : ObservableObject
    {
        // 제조사 관련 필드, 프로퍼티
        private string _name; // 제조사명
        private string _id; // 제조사 id
        private bool _active; // 사용 여부

        public Manufacture(string name, string id, bool active)
        {
            _name = name;
            _id = id;
            _active = active;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }
        public string Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }
        public bool Active
        {
            get { return _active; }
            set
            {
                SetProperty(ref _active, value);
            }
        } 
    }
}
