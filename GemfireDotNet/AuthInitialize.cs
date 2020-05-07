using System.Collections.Generic;

namespace Apache 
{
    namespace Geode
    {
        namespace DotNetCore
        {
            public interface IAuthInitialize
            {
                Dictionary<string, string> GetCredentials();
                void Close();
            }
        }
    }
}