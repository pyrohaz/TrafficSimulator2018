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
using System.Diagnostics;

namespace TrafficSimulator2018
{	
	public partial class MainForm : Form
	{
		const int PANEL_EDGE = 50;
		Graphics g;
		MapRenderer maprndr;
		
		public MainForm()
		{
			InitializeComponent();
			
			maprndr = new MapRenderer();
			
			//Create panel graphics
			g = panel.CreateGraphics();
			maprndr.SetGFX(g);
			maprndr.SetPanelRange(PANEL_EDGE, panel.Size.Width-PANEL_EDGE, PANEL_EDGE, panel.Size.Height-PANEL_EDGE);
			maprndr.CalculateNodeData();
		}
		
		void PanelClick(object sender, EventArgs e)
		{
			maprndr.Render();
		}
	}
}
