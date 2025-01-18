using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using batteryQI.Models;
using batteryQI.Views;
using System.Windows.Controls;

namespace batteryQI.ViewModels.Bases
{
    internal partial class ViewModelBases : AllModelBase
    {
        public ViewModelBases()
        {
            // singltoon 구조이기 때문에 기존에 생성된 객체들을 참조
            _manager = Manager.Instance();
            _maufactureList = Manufacture.Instance();

            DBConnection = DBlink.Instance();
            DBConnection.Connect();

            // View화면을 구성하는 요소들 초기화
            InitializeManufactureList();
        }
        private void InitializeManufactureList() // 제조사 목록 초기화
        {
            // DB가 제대로 연결되어 있고 PassBox가 안 비어져 있으면 수행
            if (DBConnection.ConnectOk())
            {
                #region 제조사 리스트 객체 데이터 DB에서 가져오기. 현재는 DB가 비어있어 임의의 리터럴로 초기화.
                // 제조사 리스트 객체 데이터 가져오기
                //List<Dictionary<string, object>> mfList = DBConnection.Select($"SELECT manufacId, manufacName FROM manufacture;");

                // Manufacture 객체 속성에 데이터 초기화. 
                //foreach (var row in mfList)
                //{
                //    // manufacId와 manufacName 추출 및 변환
                //    if (row.ContainsKey("manufacId") && row.ContainsKey("manufacName"))
                //    {
                //        ManufactureList.ManufacId.Add(Convert.ToInt32(row["manufacId"]));
                //        ManufactureList.ManufacName.Add(row["manufacName"].ToString());
                //    }
                //}
                #endregion
                ManufactureList.ManufacId = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
                ManufactureList.ManufacName = new List<string>
                    { "에너자이저", "듀라셀", "삼성", "LG", "SK", "파나소닉", "소니" };

                // 데이터 변경을 알리기 위해 속성 갱신 알림.
                OnPropertyChanged(nameof(ManufactureList));
            }
        }

        // 클릭 이벤트 등록
        [RelayCommand]
        private void LinkDB()
        {
            DBlink x = new();
            x.Connect(); // 링크
        }

        #region ManagerView 커멘드
        [RelayCommand]
        private void NewWorkAmountSet(object obj) // 새로운 작업량 할당 커멘드
        {
            if (obj is TextBox newWorkAmount)
            {
                if (int.TryParse(newWorkAmount.Text, out int _newWorkAmount))
                {
                    // local상 설정된 작업량 업데이트
                    int _defaultWorkAmount = _manager.ReturnTotalWorkAmount(); // 기존 작업량
                    _manager.EditWorkAmount(_newWorkAmount); // 새 작업량 반환
                    MessageBox.Show($"새로운 작업량이 설정되었습니다\r\n기존 작업량: {_defaultWorkAmount} 새 작업량: {_newWorkAmount}", 
                        "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // DB에 작업량 업데이트

                    // 텍스트 초기화
                    newWorkAmount.Text = "";
                }
                else
                {
                    MessageBox.Show("새로 설정할 작업량을 확인해 주세요", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        [RelayCommand]
        private void AddNewManufacture(object obj) // 새로운 제조사 추가 커멘드
        {
            ManufactureList.ManufacId.Add(8); // 임시 확인용 데이터 추가
            ManufactureList.ManufacName.Add("도쿄 일렉트론");
            OnPropertyChanged(nameof(ManufactureList));
            // 이건 또 추가된게 ListBox에 적용되지 않음(view에 미출력). 원인이 뭘까?
        }
        #endregion
    }
}
