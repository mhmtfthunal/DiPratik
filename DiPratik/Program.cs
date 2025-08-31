
using DiPratik.Models;

var teacher = new Ogretmen("Ayşe", "Yılmaz");
var classRoom = new ClassRoom(teacher);

Console.WriteLine(classRoom.GetTeacherInfo()); // Ayşe Yılmaz
