using System.Windows.Forms;

namespace Acmil.Common
{
    class BufferedListBox : ListBox
    {
        public BufferedListBox()
        {
            this.DoubleBuffered = true;
        }
    }
}
