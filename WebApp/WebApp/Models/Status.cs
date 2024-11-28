using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public enum Status
    {
        NotStarted,    
        InProgress,    
        Waiting,       
        Deferred,      
        Abandoned,     
        Completed      
    }
}