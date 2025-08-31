using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiPratik.Models;
public interface IOgretmen
{
    string FirstName { get; }
    string LastName { get; }
    string GetInfo();
}
