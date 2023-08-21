using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartMessage : SubjectData
{
    public string Message { get; set; }

    public static StartMessage Create(string msg)
    {
        StartMessage startMessage = new StartMessage();
        startMessage.Message = msg;
        return startMessage;
    }
}
