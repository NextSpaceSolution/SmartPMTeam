using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models
{
    public partial class AccountModel
    {
        public string userId { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public string userCreateBy { get; set; }
        public DateTime? userCreateDate { get; set; }

        public string userEditBy { get; set; }

        public DateTime? userEditDate { get; set; }

        public string jobResponsible { get; set; }

        public string status { get; set; }

        public string userTel { get; set; }

        public string lineId { get; set; }


    }
    public partial class AccountModles
    {
 
        public List<AccountModel> AccountModel { get; set; }
    }

}
