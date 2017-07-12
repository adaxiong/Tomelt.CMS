using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.UI.Navigation;

namespace Tomelt.Users.ViewModels
{
    public class UserSearch: DatagridPagerParameters
    {
        public string UserNameOrEmali { get; set; }
        public string SortBy { get; set; }

        public string Staus { get; set; }
    }
}