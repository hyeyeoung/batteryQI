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

namespace batteryQI.ViewModels
{
    // 관리자 페이지
    internal partial class ManagerViewModel : ObservableObject
    {
        private string _manufacName = "";
        private IDictionary<string, string> _manufacDict = new Dictionary<string, string>();
        public IDictionary<string, string> ManufacDict
        {
            get => _manufacDict;
        }
        public string ManufacName
        {
            get => _manufacName;
            set => SetProperty(ref _manufacName, value);
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
            _manager = Manager.Instance();
            _newWorkAmount = _manager.WorkAmount;
            getManafactureNameID();
        }


        private void getManafactureNameID() // DB에서 제조사 리스트 가져오기
        {
            // DB에서 가져와서 리스트 초기화하기, ID는 안 가져오고 Name만 추가
            List<Dictionary<string, object>> ManufactureList_Raw = DBConnection.Select("SELECT * FROM manufacture;"); // 데이터 가져오기
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
                _manufacDict.Add(Name, ID);
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
                    if(ManufacName != "")
                    {
                        if (MessageBox.Show($"추가하시려는 제조사가 다음이 맞습니까?\r\n{ManufacName}", "Yes-No", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            DBConnection.Insert($"INSERT INTO manufacture (manufacId, manufacName) VALUES(0, '{ManufacName}');");
                            _manufacDict.Clear();
                            getManafactureNameID();
                            MessageBox.Show($"새로운 제조사가 추가되었습니다.\r\n{ManufacName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("제조사를 입력해주세요", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
