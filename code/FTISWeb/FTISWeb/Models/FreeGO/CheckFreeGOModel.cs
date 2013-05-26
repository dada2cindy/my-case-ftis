using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTISWeb.Models
{
    public interface ICheckFreeGO
    {
        bool ShowFreeGOMsg { get; set; }
        string FreeGOColumnName { get; set; }
    }
}
