using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models;
public class User
{
    public string Name { get; }
    public string AthleteID { get; }

    public User(string name)
    {
        this.Name = name;
    }

    
}
