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
        }
        // 클릭 이벤트 등록
        [RelayCommand]
        private void LinkDB()
        {
            DBlink x = new();
            x.Connect(); // 링크
        }

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

    }
}
