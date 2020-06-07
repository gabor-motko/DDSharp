using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using DDSharp;

namespace DDSViewer
{
    public partial class ViewerForm : Form
    {
        public ViewerForm()
        {
            InitializeComponent();
        }

        private void Log(string s)
        {
            Console.WriteLine(s);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Loading " + filePathText.Text);
            using (BinaryReader reader = new BinaryReader(File.Open(filePathText.Text, FileMode.Open)))
            {
                Console.WriteLine("Signature {0:x}", reader.ReadUInt32());
                Console.WriteLine("Header length {0}", reader.ReadUInt32());
                Console.WriteLine("Flags {0}", Convert.ToString(reader.ReadUInt32(), 2));
                Console.WriteLine("H: {0} W: {1}", reader.ReadUInt32(), reader.ReadUInt32());
                Console.WriteLine("Pitch: {0}", reader.ReadUInt32());
                Console.WriteLine("Depth: {0}", reader.ReadUInt32());
                Console.WriteLine("Mipmaps: {0}", reader.ReadUInt32());
                reader.BaseStream.Seek(44, SeekOrigin.Current);
                Console.WriteLine("PF Size: {0}", reader.ReadUInt32());
                Console.WriteLine("PF Flags: {0}", Convert.ToString(reader.ReadUInt32(), 2));
                Console.WriteLine("PF FourCC: {0}", Encoding.ASCII.GetString(reader.ReadBytes(4)));
                Console.WriteLine("PF BitPerPixel: {0}", reader.ReadUInt32());
                Console.WriteLine("PF Mask R: {0:x}", reader.ReadUInt32());
                Console.WriteLine("PF Mask G: {0:x}", reader.ReadUInt32());
                Console.WriteLine("PF Mask B: {0:x}", reader.ReadUInt32());
                Console.WriteLine("PF Mask A: {0:x}", reader.ReadUInt32());
            }
        }

        private void loadDdsButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Loading " + filePathText.Text);
            try
            {
                DDS image = DDS.LoadFromFile(filePathText.Text, Log);
                Console.WriteLine(image.ToString());
                imageBox.Image = image.GetBitmap();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
