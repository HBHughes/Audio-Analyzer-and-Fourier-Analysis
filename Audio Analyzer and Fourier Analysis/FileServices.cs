using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Additionals
{
    internal class WAVServices
    {

       public static int SubChunk1Size(byte[] fileBytes) 
       {
            int SubChunkSize = 0;
            for (int i = 16; i < 20; i++)
            {
                SubChunkSize = SubChunkSize + fileBytes[i];
            }
            return SubChunkSize;
       }
       public static int BitsPerSample(byte[] fileBytes)
       {
            return fileBytes[34] + fileBytes[35];
       }
       public static Int16[] DataByteCombination(byte[] fileBytes) //for increased bytes/sample
       {
            int arraysize = DataLength(fileBytes);
            int j = 0;
            Int16[] int16_array = new Int16[arraysize];
            for (int i = DataStartByte(fileBytes); i < fileBytes.Length-1; i++)
            {
                Int16 k = BitConverter.ToInt16(fileBytes, i);
                int16_array[j] = k;
                j++;
            }
            return int16_array;
       }
       public static int DataStartByte(byte[] fileBytes)
       { 
          return 20+SubChunk1Size(fileBytes)+8;
       }
       public static int SubChunk2Size(byte[] fileBytes)
       {
            int SubChunk2StartByte = SubChunk1Size(fileBytes) + 20 + 4;
            int SubChunk2=0;
            for (int i=0;i<4;i++)
            {
               SubChunk2 = SubChunk2 + fileBytes[SubChunk2StartByte + i];
            }
            return SubChunk2;
       }
       public static byte[] DataTruncate(byte[] fileBytes)
       {
            byte[] Data = new byte[fileBytes.Length - DataStartByte(fileBytes) - 1];
            int j = 0;
            for (int i = DataStartByte(fileBytes); i < fileBytes.Length - 1; i++)
            {
                byte k = fileBytes[i];
                Data[j] = k;
                j++;
            }
            return Data;
        }
       public static Int16 BlockAlign(byte[] fileBytes)
       {
            Int16 BlockAlign = BitConverter.ToInt16(fileBytes, 32);
            return BlockAlign;
       }
       public static int AudioChannelCountWAV(byte[] fileBytes) //Byte 22 and 23
       {
            int ChannelCount = fileBytes[22] + fileBytes[23];
            return ChannelCount;
       }
        public static Int16 ChannelCount(byte[] fileBytes)
        {
            Int16 ChannelCount = BitConverter.ToInt16(fileBytes, 22);
            return ChannelCount;
        }
        public static float DataTimeLength(byte[] Data, byte[] fileBytes)
        {
            float time = Data.Length / GetSampleRate(fileBytes);
            time = time / ChannelCount(fileBytes);
            time = time / (8/BlockAlign(fileBytes));
            return time;
        }
       public static float DataTimeLength(Int16[] Data, byte[] fileBytes) //forgot to account for channels
       {
            float time = Data.Length / GetSampleRate(fileBytes);
            time = time / ChannelCount(fileBytes);
            time = time / (8/BlockAlign(fileBytes));
            return time;
       }
        public static Int32 GetSampleRate(byte[] fileBytes)
        {
            Int32 SampleRate = BitConverter.ToInt32(fileBytes, 24);
            return SampleRate;
        }
        public static int DataLength(byte[] fileBytes)
        {
            int DataLength = 0;
            DataLength=fileBytes.Length - DataStartByte(fileBytes);
            return DataLength;
        }
    }
    internal class UserInput
    {
        public static bool StartIsReady(string? filePath, string fileExt)
        {
            if (!String.IsNullOrEmpty(filePath)) 
            {
                if ((ValidUserFileInput(filePath, fileExt))){
                    return true; }
            }
            return false;
        }
        private static bool ValidUserFileInput(string filePath, string fileExt)
        {
                return (File.Exists(WinFilePathToValidPath(filePath)) && Path.GetExtension(WinFilePathToValidPath(filePath)) == fileExt);
        }
        public static string WinFilePathToValidPath(string filePath)
        {
            if (!String.IsNullOrEmpty(filePath))
            {
                string cleanedText = Regex.Replace(filePath, "[\"']", string.Empty);
                string returnText = Regex.Replace(cleanedText, @"\\", @"\\"); // could probably combine, not feeling regex today
                return returnText;
            }
            else { return "Null String"; }
        }
    }
}
