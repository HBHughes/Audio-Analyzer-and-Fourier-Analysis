using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Additionals
{
    internal class FileServices
    {
       
    }
    internal class UserInput
    {

        private static bool NonNullUserTextInput(string? Input)
        {
            if (!String.IsNullOrEmpty(Input))
            {
                return true;
            }
            return false;
        }
        public static bool StartIsReady(string? filePath, string fileExt)
        {
            if (NonNullUserTextInput(filePath) && (ValidUserFileInput(filePath,fileExt))) 
            {
                return true;
            }
            return false;
        }
        private static bool ValidUserFileInput(string? filePath, string fileExt)
        {
                return (File.Exists(filePath) && Path.GetExtension(filePath) == fileExt);
        }
    }
}
