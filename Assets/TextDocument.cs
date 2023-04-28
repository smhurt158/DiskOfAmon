using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TextDocument:File
{
    public string Contents;

    public TextDocument(string name, File parentFile, string contents) : base(name, parentFile)
    {
        Contents = contents;
    }
}

