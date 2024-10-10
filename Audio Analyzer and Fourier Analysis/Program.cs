// See https://aka.ms/new-console-template for more information
using System.IO;
using Additionals;
Console.WriteLine("Hello, World!");
/* Goal: 1 Intake Waveform Audio File(WAV)-> 2 Get Composite Waveform-> 3 Graph Composite Waveform-> 4 Using Fourier Analysis (FFT) to seperate into individual Waveforms - Attempt to Use Most ASM possible
    Resources: https://en.wikipedia.org/wiki/Fourier_analysis , https://en.wikipedia.org/wiki/Pulse-code_modulation , 
    https://en.wikipedia.org/wiki/WAV , https://www.nti-audio.com/en/support/know-how/fast-fourier-transform-fft ,
    https://datafireball.com/2016/08/29/wav-deepdive-into-file-format/ , http://soundfile.sapp.org/doc/WaveFormat/
    Fourier Analysis Notes:
    1) A complex signal can be represented as a sum of simpler sine and cosine waves at different frequencies,
    with the Fourier transform revealing the strength of each frequency component.
    2) A signal is sampled over a period of time and divided into its frequency components. 
    These components are single sinusoidal oscillations at distinct frequencies each with their own amplitude and phase. (NTI-audio)
    3) Need Sampling frequency, notated sf measured in (KHz) and a number of samples (always 2^Z), notated BL for Block Length, Z representing Integers
    4) Nyquist frequency N = sf / 2 -> Theoretical Maximum Frequency that can be determined.
    5) Measurement Duration D = BL / sf (time in seconds/milliseconds)
    6) Frequency resolution; df = fs / BL ( frequency spacing between two measurement results.) returns Hz
    7) The sampling frequency must be at least double the highest frequency of the signal 2fmax >= sf
    8) I'll create my own transform function to decrease reliance on external libraries
    9) Input .wav should be roughly periodic
    Audio Analyzer Notes:
    1) 
*/

string rootPath = "C:\\Users\\HHughes\\Documents\\Coding\\file_example_WAV_1MG.wav";
string validExtension = ".wav";
bool validPath = (FileServices.ValidUserFileInput(rootPath, validExtension));
Console.ReadLine();