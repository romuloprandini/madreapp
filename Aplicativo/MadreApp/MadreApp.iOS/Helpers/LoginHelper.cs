using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Firebase.Auth;
using MadreApp.Interfaces;
using System.Diagnostics;

namespace MadreApp.iOS.Helpers
{
    public class LoginHelper : ILogin
    {
        public void VerifyPhoneNumber(string phoneNumber)
        {
            PhoneAuthProvider.DefaultInstance.VerifyPhoneNumber(phoneNumber, OnVerificationCompleted);
        }

        public void OnVerificationCompleted(string verificationId, NSError error)
        {
            Debug.WriteLine(verificationId);
            if (error != null && error.Description != null)
            {
                Debug.WriteLine(error.Description);
            }
        }
    }
}