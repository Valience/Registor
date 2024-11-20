using System.Collections.Generic;
using System.Linq;
using Registor.Model;

namespace Registor.Helpers;

public static class CryptoModuleHelper
{
    public static List<CryptoModule> RemoveNullModules(List<CryptoModule> modules)
    {
        return modules.Where(m => m != null).ToList();
    }
}
