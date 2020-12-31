/*
    Thanks to the original source code: https://github.com/balatj/Guitar-Tuner
*/
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DinoBassControl
{
    public class Sound
    {
        BufferedWaveProvider bufferedWaveProvider = null;
        bool isGettingData = true;
        bool canJump = true;

        public int SelectInputDevice()
        {
            return 0;
        }

        public void StartDetect(int inputDevice)
        {
            WaveInEvent waveIn = new WaveInEvent();

            waveIn.DeviceNumber = inputDevice;
            waveIn.WaveFormat = new WaveFormat(44100, 1);
            waveIn.DataAvailable += WaveIn_DataAvailable;

            bufferedWaveProvider = new BufferedWaveProvider(waveIn.WaveFormat);

            // begin record
            waveIn.StartRecording();

            IWaveProvider stream = new Wave16ToFloatProvider(bufferedWaveProvider);
            Pitch pitch = new Pitch(stream);

            byte[] buffer = new byte[8192];
            int bytesRead;

            do
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);

                float freq = pitch.Get(buffer);
                int freq_int = (int)freq;

                if (freq_int != 0)
                {

                    if (freq_int == 75)
                    {
                        if (canJump)
                        {
                            canJump = false;
                            SendKeys.Send(" ");
                            Console.WriteLine("JUMP");
                        }
                    }

                    if (freq_int == 98)
                    {
                        if (!canJump)
                        {
                            canJump = true;
                        }
                    }

                    if (freq_int == 83) {
                        isGettingData = false;
                        break;
                    }

                }

            } while (bytesRead != 0 && isGettingData);

            // stop recording
            waveIn.StopRecording();
            waveIn.Dispose();
        }

        void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (bufferedWaveProvider != null)
            {
                bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
                bufferedWaveProvider.DiscardOnBufferOverflow = true;
            }
        }
    }
}
