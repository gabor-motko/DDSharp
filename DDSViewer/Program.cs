using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDSViewer
{
    class Program
    {
        private static ViewerForm _form;
        [STAThread]

        static void Main(string[] args)
        {
            _form = new ViewerForm();
            System.Windows.Forms.Application.Run(_form);
        }
    }
}
