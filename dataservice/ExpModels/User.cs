using System;
using System.Collections.Generic;

namespace dataservice.ExpModels
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? EmpId { get; set; }
        public byte[] Salt { get; set; }
    }
}
