using System;
using System.Threading.Tasks;

namespace MadreApp.Interfaces
{
    public delegate void VerifyPhoneNumberSuccess(string SmsCode, string provider);
    public delegate void VerifyPhoneNumberFail(string message);

    public interface ILogin
    {
        event EventHandler OnVerifyPhoneNumberCodeSent;
        event EventHandler OnVerifyPhoneNumberSucess;
        event VerifyPhoneNumberFail OnVerifyPhoneNumberFail;

        bool IsLogged { get; }
        MadreApp.Models.User LoggedUser { get; }

        void VerifyPhoneNumber(String phoneNumber);

        void AuthenticatePhoneNumber(string codeSMS);
        Task<bool> LoginWithEmailAndPassword(string email, string password);
        Task<bool> RegisterWithEmailAndPassword(string email, string password, string name, string photoUrl);
        Task<bool> UpdateProfile(string name, string photoUrl);
        Task<bool> UpdatePassword(string email, string currentPassword, string newPassword);
        void Logout();
        
        void Close();
    }
}
