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
            // singltoon 구조이기 때문에 기존에 생성된(Login) 객체들을 참조
            Manager = Manager.Instance();
            ManufactureList = Manufacture.Instance();
            DBConnection = DBlink.Instance();
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
            MessageBox.Show($"새로운 제조사가 추가되었습니다.\r\n제조사 이름: {ManufactureList.ManufacName[^1]}\r\n제조사 ID: {ManufactureList.ManufacId[^1]}",
                        "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            OnPropertyChanged(nameof(ManufactureList));
            // 이건 또 추가된게 ListBox에 적용되지 않음(view에 미출력). 원인이 뭘까?
        }
        #endregion
    }
}
