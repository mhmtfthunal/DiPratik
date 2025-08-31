using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiPratik.Models;
public class ClassRoom
{
    // DI ile dışarıdan verilecek bağımlılık
    public IOgretmen Teacher { get; }

    // Constructor Injection
    public ClassRoom(IOgretmen teacher)
    {
        Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
    }

    public string GetTeacherInfo() => Teacher.GetInfo();
}
