using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Contact
{
    public class AddContactDTO
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }

    public class ContactDTO:AddContactDTO
    {
        public int Id { get; set; }
    }
}
