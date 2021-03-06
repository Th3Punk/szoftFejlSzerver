﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace kliens
{
	public partial class Form1 : Form
	{
		private IPHostEntry ipHostInfo = null;
		private IPAddress ipAddress = null;
		private IPEndPoint remoteEP = null;
		private Socket sender = null;
		//Socket sender = null;

		public Form1 ()
		{
			InitializeComponent ();
			//StartClient();
		}

		public void StartClient ()
		{
			//byte[] bytes = new byte[1024];

			// Connect to a remote device.
			try
			{
				// Establish the remote endpoint for the socket.
				// This example uses port 11000 on the local computer.
				ipHostInfo = Dns.Resolve (Dns.GetHostName ());
				ipAddress = ipHostInfo.AddressList [0];
				remoteEP = new IPEndPoint (ipAddress, 11000);

				// Create a TCP/IP  socket.
				sender = new Socket (AddressFamily.InterNetwork,
					SocketType.Stream, ProtocolType.Tcp);

				// Connect the socket to the remote endpoint. Catch any errors.
				try
				{
					sender.Connect (remoteEP);

					MessageBox.Show ("Socket connected to " + sender.RemoteEndPoint.ToString ());
					//Console.WriteLine("Socket connected to {0}",
					//    sender.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					//byte[] msg = Encoding.ASCII.GetBytes(@"D:\WoW\World of Warcraft\Screenshots\WoWScrnShot_050515_132459.jpg<EOF>");
					String path = fileLocationTextBox.Text + "<EOF>";
					byte [] msg = Encoding.ASCII.GetBytes (@path);

					// Send the data through the socket.
					int bytesSent = sender.Send (msg);

					// Receive the response from the remote device.
					//int bytesRec = sender.Receive(bytes);

					Image newImage;
					byte [] bytes = new Byte [1920 * 1080];

					int bytesRec = sender.Receive (bytes, 0, bytes.Length, 0);
					Array.Resize (ref bytes, bytesRec);
					MemoryStream mem = new MemoryStream (bytes, 0, bytes.Length);
					newImage = System.Drawing.Image.FromStream (mem, false);
					// newImage.Save ((@"C:\Image.jpeg"), ImageFormat.Jpeg);

					string imagePath = AppDomain.CurrentDomain.BaseDirectory;
					string imageName = imagePath + "Image.jpeg";
					newImage.Save (imageName, ImageFormat.Jpeg);

					// pictureBox1.ImageLocation = (@"C:\Image.jpeg");

					// Picture picture = new Picture (@"C:\Image.jpeg");
					Picture picture = new Picture (imageName);
					picture.ShowDialog ();

					//MessageBox.Show("Echoed test = " + Encoding.ASCII.GetString(bytes, 0, bytesRec));
					//Console.WriteLine("Echoed test = {0}",
					//    Encoding.ASCII.GetString(bytes, 0, bytesRec));

					// Release the socket.
					sender.Shutdown (SocketShutdown.Both);
					sender.Close ();

				}
				catch (ArgumentNullException ane)
				{
					MessageBox.Show ("ArgumentNullException : " + ane.ToString ());
					//Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
				}
				catch (SocketException se)
				{
					MessageBox.Show ("SocketException : " + se.ToString ());
					//Console.WriteLine("SocketException : {0}", se.ToString());
				}
				catch (Exception e)
				{
					MessageBox.Show ("Unexpected exception : " + e.ToString ());
					//Console.WriteLine("Unexpected exception : {0}", e.ToString());
				}

			}
			catch (Exception e)
			{
				MessageBox.Show (e.ToString ());
				//Console.WriteLine(e.ToString());
			}
		}

		private void okButton_Click (object sender, EventArgs e)
		{
			StartClient ();

		}

	}
}

