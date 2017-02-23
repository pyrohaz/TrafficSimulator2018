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
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

namespace TrafficSimulator2018
{
	public partial class MainForm : Form
	{
		//Amount of pixels from the edge of the panel to draw
		const int PANEL_EDGE = 50;
		
		//Map renderer
		MapRenderer maprndr;
		
		//Refresh timer
		Timer timer = new Timer();
		
		public MainForm()
		{
			InitializeComponent();
			
			//Turn on double buffering for the drawing panel (remove flickering)
			typeof(Panel).InvokeMember("DoubleBuffered", 
	    		BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, 
    			null, panel, new object[] { true });		
			
			
			//Create new map renderer with panel parameters
			maprndr = new MapRenderer();
			maprndr.SetPanelRange(PANEL_EDGE, panel.Size.Width-PANEL_EDGE, PANEL_EDGE, panel.Size.Height-PANEL_EDGE);
			
			//Generate a timer to constantly update the panel
			timer.Interval = 30;	//50ms update rate
			timer.Enabled = true;
			timer.Tick += TimerCallback;
			
		}
		
		//Upon panel paint function, render to panel
		void PanelPaint(object sender, PaintEventArgs e) {
			maprndr.Render(e.Graphics);
		}
		
		//Upon this timer callback, refresh the panel
		void TimerCallback(object sender, EventArgs e){
			panel.Refresh();
			return;
		}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e) {
		}
	}
}
