using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Models
{
    public interface ILoggedInUserModel
    {
        string Token { get; set; }
        string Id { get; set; }
        string Firstname { get; set; }
        string LastName { get; set; }
        string EmailAddress { get; set; }
        DateTime CreatedDate { get; set; }
        void ResetUserModel();
    }
}
