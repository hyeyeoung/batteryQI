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
        private string _manufacName;
        //private IList<string> _manufacList = new List<string>(); // 제조사 리스트 가져오기
        //private IList<string> _manufacIDList = new List<string>(); // 제조사 id 리스트
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
        public Manager Manager
        {
            get => _manager;
            set => SetProperty(ref _manager, value);
        }
        public ManagerViewModel()
        {
            DBConnection = DBlink.Instance(); // DB객체 연결
            _manager = Manager.Instance();
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
                    //Name = items.
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
                    DBConnection.Insert($"INSERT INTO manufacture (manufacId, manufacName) VALUES(0, '{ManufacName}');");
                    _manufacDict.Clear();
                    getManafactureNameID();
                    MessageBox.Show("완료");
                }
            }
            catch
            {
                MessageBox.Show("입력 오류", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        [RelayCommand]
        private void NewWorkAmountSet(object obj) // 새로운 작업량 할당 커멘드
        {
            if (obj is System.Windows.Controls.TextBox newWorkAmount)
            {
                if (int.TryParse(newWorkAmount.Text, out int _newWorkAmount))
                {
                    // local상 설정된 작업량 업데이트
                    int _defaultWorkAmount = _manager.ReturnTotalWorkAmount(); // 기존 작업량
                    _manager.EditWorkAmount(_newWorkAmount); // 새 작업량 반환
                    MessageBox.Show($"새로운 작업량이 설정되었습니다\r\n기존 작업량: {_defaultWorkAmount} 새 작업량: {_newWorkAmount}",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // DB에 작업량 업데이트

                    // 텍스트 초기화
                    newWorkAmount.Text = "";
                }
                else
                {
                    MessageBox.Show("새로 설정할 작업량을 확인해 주세요", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
