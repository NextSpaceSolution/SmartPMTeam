﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models
{
    public partial class AccountModel
    {
        public string userId { get; set; }

        public string username { get; set; }

        public object jobResponsible { get; set; }

        public string userEditBy { get; set; }

        public DateTime? userEditDate { get; set; }

        public object groupId { get; set; }

        public string status { get; set; }

        public string groupName { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }
    }
    public partial class AccountModles
    {
 
        public List<AccountModel> AccountModel { get; set; }
    }

}
