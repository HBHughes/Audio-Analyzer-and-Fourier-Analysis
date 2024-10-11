using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
       public static UInt16[] ByteCombination(byte[] fileBytes) //for increased bytes/sample
       {
            int arraysize = DataLength(fileBytes);
            int j = 0;
            UInt16[] uint16_array = new UInt16[arraysize];
            for (int i = DataStartByte(fileBytes); i < fileBytes.Length-1; i++)
            {
                UInt16 k = BitConverter.ToUInt16(fileBytes, i);
                uint16_array[j] = k;
                j++;
            }
            return uint16_array;
       }
       public static int DataStartByte(byte[] fileBytes)
       { 
          return 20+SubChunk1Size(fileBytes)+8;
       }
       public static int GetSampleRate(byte[] fileBytes)
        {
            return fileBytes[16] + fileBytes[17] + fileBytes[18] + fileBytes[19];
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
       public static int BlockAlign(byte[] fileBytes)
       {
            int BlockAlign = fileBytes[32]+fileBytes[33];
            return BlockAlign;
       }
       public static int AudioChannelCountWAV(byte[] fileBytes) //Byte 22 and 23
        {
            int ChannelCount = fileBytes[22] + fileBytes[23];
            return ChannelCount;
        }
        public static int SampleRateWAV(byte[] fileBytes)
        {
            int SampleRate = fileBytes[24]+fileBytes[25]+fileBytes[26]+fileBytes[27];
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
