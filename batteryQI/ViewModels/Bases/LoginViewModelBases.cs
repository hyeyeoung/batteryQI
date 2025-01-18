using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using batteryQI.Models;
using batteryQI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace batteryQI.ViewModels.Bases
{
    internal partial class LoginViewModelBases : AllModelBase
    {
        public LoginViewModelBases()
        {
            // Manager 객체 생성(관리자)
            _manager = Manager.Instance();
            // Manufacture 객체 생성(제조사 리스트)
            _maufactureList = Manufacture.Instance();
            // 로그인 창 열면서 DB 연결
            DBConnection = DBlink.Instance();
            DBConnection.Connect();
        }

        [RelayCommand]
        private void Login(object obj) // 기존 로그인 함수에 workAmount 초기화 추가
        {
            // DB가 제대로 연결되어 있고 PassBox가 안 비어져 있으면 수행
            if (DBConnection.ConnectOk() && obj is PasswordBox pw)
            {
                // 관리자 객체 데이터 가져오기
                List<Dictionary<string, object>> login = DBConnection.Select($"SELECT managerId, managerPw, workAmount FROM manager WHERE managerId='{Manager.ManagerID}';");

                // 제조사 리스트 객체 데이터 가져오기
                List<Dictionary<string, object>> mfList = DBConnection.Select($"SELECT manufacId, manufacName FROM manufacture;");

                if (login.Count != 0 && (pw.Password == login[0]["managerPw"].ToString()))
                {
                    // Manager 객체 속성에 데이터 초기화. 
                    Manager.ManagerPW = pw.Password;
                    Manager.WorkAmount = Convert.ToInt32(login[0]["workAmount"]);

                    // 로그인 완료 메시지
                    MessageBox.Show("로그인 완료", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    var mainWindow = new MainWindow();
                    mainWindow.Show();

                    // 현재 창 닫기
                    Application.Current.Windows[0]?.Close();
                }
                else
                {
                    MessageBox.Show("아이디 및 비밀번호를 확인해 주세요", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
