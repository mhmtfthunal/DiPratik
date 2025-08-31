using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiPratik.Models;
public class Ogretmen : IOgretmen
{
    public string FirstName { get; }
    public string LastName { get; }

    public Ogretmen(string firstName, string lastName)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
    }

    public string GetInfo() => $"{FirstName} {LastName}";
}
