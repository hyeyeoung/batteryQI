using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunityToolkit.Mvvm.ComponentModel;

namespace batteryQI.Models
{
    // 싱글톤 패턴
    internal class Manager : ObservableObject
    {
        private string _managerID; // 관리자 ID
        private string _managerPW; // 관리자 PW
        private int _workAmount; // 관리자에게 할당된 작업량
        static Manager manager; // singleton

        private Manager() { } // singleton을 위반하는 상황 방지를 위해 private 수식
        public static Manager Instance()
        {
            if (manager == null)
            {
                manager = new Manager(); // Manager 객체 생성
            }
            return manager;
        }
        public string ManagerID
        {
            get { return _managerID; }
            set
            {
                SetProperty(ref _managerID, value);
            }
        }
        public string ManagerPW
        {
            get { return _managerPW; }
            set
            {
                SetProperty(ref _managerPW, value);
            }
        }

        public int WorkAmount
        {
            get { return _workAmount; }
            set
            {
                SetProperty(ref _workAmount, value);
            }
        }

        public int ReturnTotalWorkAmount()
        {
            return WorkAmount;
        }

        public int ReturnCompletedAmount()
        {
            return 0; // 임시
        }

        public void EditWorkAmount(int newAmount)
        {
            WorkAmount = newAmount;
        }

        public void InspectBattery()
        {

        }

        public void AddDBBetteryCompany(string newCompany)
        {

        }
    }
}
