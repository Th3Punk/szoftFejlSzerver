using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace kliens
{
    public partial class Picture : Form
    {
        public Picture(String path)
        {
            InitializeComponent();
            // pictureBox1.ImageLocation = (@"C:\Image.jpeg");
			// Since I have one partition only...

			pictureBox1.ImageLocation = path;
        }
    }
}
