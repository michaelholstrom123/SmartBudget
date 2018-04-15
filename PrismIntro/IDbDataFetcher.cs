using System;
using System.Collections.Generic;

namespace PrismIntro
{
    public interface IDbDataFetcher
    {
        List<string> GetData(string command);    
    }
}
