using System;
using System.Collections.Generic;
using System.Text;

namespace EasyClean.Models
{
    class EndClient
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        public object ImageArray { get; set; }
    }
}
