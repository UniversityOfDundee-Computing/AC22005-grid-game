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

        private readonly string FileName;
        private readonly string Name;
        private readonly IntPtr Handle;
        private readonly StringBuilder Buffer = new StringBuilder();

        [DllImport("winmm.dll")]
#pragma warning disable IDE1006 // Naming Styles
        static extern Int32 mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
#pragma warning restore IDE1006 // Naming Styles

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
