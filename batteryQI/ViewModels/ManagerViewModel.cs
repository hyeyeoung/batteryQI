using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using System.Data.Common;
using batteryQI.Models;
using System.Windows.Forms;
using Mysqlx.Crud;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace batteryQI.ViewModels
{
    // 관리자 페이지
    internal partial class ManagerViewModel : ObservableObject
    {
        // 제조사 관련 필드와 프로퍼티.
        private string _newManufacName = "";
        private ObservableCollection<Manufacture> _manufacList;
        private List<bool> _manufacUsed;

        public ObservableCollection<Manufacture> ManufacList
        {
            get => _manufacList;
        }
        public string NewManufacName
        {
            get => _newManufacName;
            set => SetProperty(ref _newManufacName, value);
        }
        public List<bool> ManufacUsed
        {
            get => _manufacUsed;
            set => SetProperty(ref _manufacUsed, value);
        }

        DBlink DBConnection;
        Manager _manager;
        int _newWorkAmount; // 변경할 작업량. 기존 코드는 텍스트를 입력하기만해도 _manager.WorkAmount 변경, 의도치 않은 값 변경 위험
        public Manager Manager
        {
            get => _manager;
            set => SetProperty(ref _manager, value);
        }
        public int NewWorkAmount
        {
            get => _newWorkAmount;
            set =>SetProperty(ref _newWorkAmount, value);
        }
        public ManagerViewModel()
        {
            DBConnection = DBlink.Instance(); // DB객체 연결

            _manufacList = new ObservableCollection<Manufacture>();
            _manufacUsed = ManufactureActive.Instance();
            _manager = Manager.Instance();
            _newWorkAmount = _manager.WorkAmount;
            getManafactureNameID();
        }


        private void getManafactureNameID() // DB에서 제조사 리스트 가져오기
        {
            // DB에서 가져와서 리스트 초기화하기, ID는 안 가져오고 Name만 추가
            List<Dictionary<string, object>> ManufactureList_Raw = DBConnection.Select("SELECT * FROM manufacture;"); // 데이터 가져오기
            // 가져온 제조사 리스트 사용 여부 초기화. 전체가 true로 덮어쓰기되는 상황 방지
            while(ManufacUsed.Count < ManufactureList_Raw.Count)
                ManufacUsed.Add(true);
            
            for (int i = 0; i < ManufactureList_Raw.Count; i++)
            {
                string Name = "";
                string ID = "";
                foreach (KeyValuePair<string, object> items in ManufactureList_Raw[i])
                {
                    // 제조사 이름 key, 제조사 id value
                    if (items.Key == "manufacName")
                    {
                        Name = items.Value.ToString();
                    }
                    else if (items.Key == "manufacId")
                    {
                        ID = items.Value.ToString();
                    }
                }
                _manufacList.Add(new Manufacture(Name, ID, ManufacUsed[i]));
            }
        }

        [RelayCommand]
        private void ManufactInsert()
        {
            // 제조사 인풋
            try
            {
                if (DBConnection.ConnectOk())
                {
                    if(NewManufacName != "")
                    {
                        // 비활성화 해놨던 제조사 리스트에 추가하려는 제조사가 이미 있었던 경우를 분기 처리
                        if (_manufacList.Any(Manufacture => Manufacture.Name == NewManufacName))
                        {
                            if (MessageBox.Show($"비활성화 되었던 제조사 목록에 입력하신 제조사가 존재합니다." +
                                $"\r\n{NewManufacName} 제조사를 재활성화 하시겠습니까?", "Yes-No", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                _manufacList.FirstOrDefault(Manufacture => Manufacture.Name == NewManufacName).Active = true;
                                MessageBox.Show($"비활성화 되었던 제조사가 활성화되었습니다.\r\n{NewManufacName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else if (MessageBox.Show($"추가하려는 제조사가 다음이 맞습니까?\r\n{NewManufacName}", "Yes-No", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            DBConnection.Insert($"INSERT INTO manufacture (manufacId, manufacName) VALUES(0, '{NewManufacName}');");
                            _manufacList.Clear();
                            getManafactureNameID();
                            MessageBox.Show($"새로운 제조사가 추가되었습니다.\r\n{NewManufacName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("제조사를 입력해주세요", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("DB 연결 오류", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("입력 오류", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        [RelayCommand]
        private void ManufacDeActiveReActive(Manufacture selectedManufacture)
        {
            // 제조사 비활성화
            try
            {
                if (selectedManufacture != null)
                {
                    string selectedName = selectedManufacture.Name;
                    string selectedID = selectedManufacture.Id;
                    bool? Active = _manufacList.FirstOrDefault(Manufacture => Manufacture.Name == selectedName).Active;

                    if (Active == true)
                    {
                        if (MessageBox.Show($"해당 제조사를 비활성화 하시겠습니까?\r\n제조사명: {selectedName} - ID: {selectedID}",
                            "Yes-No", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            _manufacList.FirstOrDefault(Manufacture => Manufacture.Name == selectedName).Active = false;
                            MessageBox.Show($"기존의 제조사가 비활성화 되었습니다.\r\n{selectedName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        if (MessageBox.Show($"해당 제조사를 재활성화 하시겠습니까?\r\n제조사명: {selectedName} - ID: {selectedID}",
                            "Yes-No", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            _manufacList.FirstOrDefault(Manufacture => Manufacture.Name == selectedName).Active = true;
                            MessageBox.Show($"기존의 제조사가 재활성화 되었습니다.\r\n{selectedName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("비활성화/재활성화할 제조사를 선택해주세요", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("입력 오류", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        [RelayCommand]
        private void SaveButton_Click()
        {
            // 월 검사 할당량 수정 이벤트
            if (DBConnection.ConnectOk())
            {
                int _defaultWorkAmount = _manager.WorkAmount; // 기존 작업량

                if (MessageBox.Show($"월 검사 할당량을 수정하시겠습니까?\r\n기존 작업량: {_defaultWorkAmount}, 새 작업량: {_newWorkAmount}",
                    "Yes-No", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _manager.WorkAmount = _newWorkAmount; // 새 작업량 할당
                    // 새 작업량 DB 업데이트
                    DBConnection.Update($"UPDATE manager SET workAmount={_manager.WorkAmount} WHERE managerId='{_manager.ManagerID}';");

                    MessageBox.Show($"월 검사 할당량이 {_newWorkAmount}로 수정되었습니다.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("DB 연결 오류", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
