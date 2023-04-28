using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Executable : File
{
    public AttackType AttackType;
    public Executable(string name, File parentFile, AttackType attackType) : base(name, parentFile)
    {
        AttackType = attackType;
    }
}
