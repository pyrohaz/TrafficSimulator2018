/*
 * Created by SharpDevelop.
 * User: harri
 * Date: 12/02/2017
 * Time: 19:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrafficSimulator2018
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
		}
		void PanelPaint(object sender, PaintEventArgs e)
		{
			base.OnPaint(e);
			using(Graphics g = e.Graphics){
				var p = new Pen(Color.Black, 3);
				
				int x, y;
				for(y = 0; y<panel.Size.Height; y++){
					for(x = 0; x<panel.Size.Width; x++){
						g.DrawRectangle(p, new Rectangle(x,y,x,y));
					}
				}
			}
		}
	}
}
