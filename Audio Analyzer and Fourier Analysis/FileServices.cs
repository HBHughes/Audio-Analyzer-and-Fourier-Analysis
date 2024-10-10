using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Additionals
{
    internal class FileServices
    {
        public static bool ValidUserFileInput(string filePath, string fileExt)
        {
            return (File.Exists(filePath) && Path.GetExtension(filePath) == fileExt);
        }
    }
    internal class UserInput
    {

        private bool NonNullUserTextInput(string? Input)
        {
            if (!String.IsNullOrEmpty(Input))
            {
                return true;
            }
            return false;
        }
    }
}
