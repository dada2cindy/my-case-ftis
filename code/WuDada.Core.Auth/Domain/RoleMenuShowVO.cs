using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WuDada.Core.Auth.Domain
{
    public class RoleMenuShowVO
    {
        public int Id { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public bool IsAuth { get; set; }
    }
}
