using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Interfaces
{
    public interface ILogin : IDisposable
    {
        Models.User EsblecerLogin(string nick, string password);        
    }
}
