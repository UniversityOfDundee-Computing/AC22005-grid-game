using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    class MusicPlayer
    {

        private string FileName;
        private string Name;
        private IntPtr Handle;
        private StringBuilder Buffer;

        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

        public MusicPlayer(string f, IntPtr handle)
        {
            this.FileName = f;
            this.Name = Path.GetFileName(FileName);
            this.Handle = handle;

            mciSendString("open \"" + FileName + "\" alias \"" + Name + "\"", Buffer, 0, IntPtr.Zero);
            Play();
        }

        public void Play()
        {
            mciSendString("seek \"" + Name + "\" to start", Buffer, 0, IntPtr.Zero);
            mciSendString("play \"" + Name + "\" notify", Buffer, 0, this.Handle);
        }
    }
}
