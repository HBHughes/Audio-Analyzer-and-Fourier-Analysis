using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Additionals
{
    public class WAVFileMetaData(string FilePath, byte[] fileBytes)
    {
        public string FilePath = FilePath;
        public int FileSize = fileBytes.Length * 8;
        public int ChunkSize = BitConverter.ToInt16(fileBytes,4);
        public int SubChunk1Size = BitConverter.ToInt32(fileBytes, 16);
        public int AudioFormat = BitConverter.ToInt16(fileBytes, 20);
        public int ChannelCount = BitConverter.ToInt16(fileBytes, 22);
        public int AudioSampleRate = BitConverter.ToInt32(fileBytes, 24);
        public int ByteRate = BitConverter.ToInt32(fileBytes, 28);
        public int BlockAlign = BitConverter.ToInt16(fileBytes, 32);
        public int BitsPerSample = BitConverter.ToInt16(fileBytes, 34);
        public int SubChunk2Size;
        public int DataStartIndex;
        public List<int> DataInitialize(int DataStartIndex)
        {
            List<int> data = new List<int>();
            for (int i = DataStartIndex; i < fileBytes.Length-DataStartIndex/AudioSampleRate; i++)
            { 
                data.Add(fileBytes[i]);
            }
            return data;
        }
        void SubCunk2SizeInit() 
        {
            SubChunk2Size = BitConverter.ToInt32(fileBytes, 24 + this.SubChunk1Size);
        }
        void DataStartIndexInit() //prob a more efficient way to intialize this
        {
            this.DataStartIndex = 28 + this.SubChunk1Size;
        }
    }
    internal class WAVServices
    {
      /*  public static Int16[] DataByteCombination(byte[] fileBytes) //for increased bytes/sample
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
        public static int GetMaxAmplitude(byte[] dataBytes)
        {
            int MaxAmp = 0;
            for (int i = 0; i < dataBytes.Length - 1; i++) 
            {
                if (dataBytes[i] > MaxAmp) 
                { MaxAmp = dataBytes[i]; }
            }
            return MaxAmp;
        }
        public static int GetMaxAmplitude(Int16[] dataBytes)
        {
            int MaxAmp = 0;
            for (int i = 0; i < dataBytes.Length - 1; i++)
            {
                if (dataBytes[i] > MaxAmp)
                { MaxAmp = dataBytes[i]; }
            }
            return MaxAmp;
        }
        public static int GetMinAmplitude(byte[] dataBytes)
        {
            int MinAmp = 0;
            for (int i = 0; i < dataBytes.Length - 1; i++)
            {
                if (dataBytes[i] < MinAmp)
                { MinAmp = dataBytes[i]; }
            }
            return MinAmp;
        }
        public static int GetMinAmplitude(Int16[] dataBytes)
        {
            int MinAmp = 0;
            for (int i = 0; i < dataBytes.Length - 1; i++)
            {
                if (dataBytes[i] < MinAmp)
                { MinAmp = dataBytes[i]; }
            }
            return MinAmp;
        }
        public static int MaxOccurrenceCount(byte[] dataBytes, int MaxValue)
        {
            int count = 0;
            for (int i = 0; i < dataBytes.Length - 1; i++)
            {
                if (dataBytes[i] == MaxValue)
                {
                    count++;
                }
            }
            return count;
        }
        public static int MaxOccurrenceCount(Int16[] dataBytes, int MaxValue)
        {
            int count = 0;
            for (int i = 0;i < dataBytes.Length - 1;i++)
            {
                if (dataBytes[i] ==  MaxValue)
                {
                    count++;
                }
            }
            return count;
        }
        public static int MinOccurrenceCount(Int16[] dataBytes, int MinValue)
        {
            int count = 0;
            for (int i = 0; i < dataBytes.Length - 1; i++)
            {
                if (dataBytes[i] == MinValue)
                {
                    count++;
                }
            }
            return count;
        }
        public static int MinOccurrenceCount(byte[] dataBytes, int MinValue)
        {
            int count = 0;
            for (int i = 0; i < dataBytes.Length - 1; i++)
            {
                if (dataBytes[i] == MinValue)
                {
                    count++;
                }
            }
            return count;
        } */
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
        public static string WinFilePathToValidPath(string? filePath)
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
