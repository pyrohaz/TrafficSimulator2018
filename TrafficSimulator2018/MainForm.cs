﻿/*
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
using System.Threading;

namespace TrafficSimulator2018
{
	public partial class MainForm : Form
	{
		const int PANEL_EDGE = 50;
		MapRenderer maprndr;
		Thread render;
		
		public MainForm()
		{
			InitializeComponent();
			
			base.DoubleBuffered = true;
			maprndr = new MapRenderer();
			maprndr.SetPanelRange(PANEL_EDGE, panel.Size.Width-PANEL_EDGE, PANEL_EDGE, panel.Size.Height-PANEL_EDGE);
			maprndr.CalculateNodeData();
			
			render = new Thread(new ThreadStart(this.RenderThread));
			render.Start();
			while(!render.IsAlive){}
		}
		void PanelPaint(object sender, PaintEventArgs e)
		{
			maprndr.Render(e.Graphics);
		}
		
		void RenderThread(){
			while(true){
				//maprndr.Render();
				Debug.WriteLine("Inv");
				panel.Invalidate();
				Thread.Sleep(50);
			}
		}
	}
}
