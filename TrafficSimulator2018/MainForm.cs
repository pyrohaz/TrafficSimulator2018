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
	public partial class MainForm : Form
	{
		const int PANEL_EDGE = 10;
		
		List<Node> nodes;
		double xscale, yscale;
		int xleft, xright;
		int ytop, ybottom;
		
		public MainForm()
		{
			InitializeComponent();
			
			xleft = ytop = PANEL_EDGE;
			xright = panel.Size.Width-PANEL_EDGE;
			ybottom = panel.Size.Height-PANEL_EDGE;
			
			nodes = new List<Node>();
			
			nodes.Add(Node(0, 10.0, 10.0));
			nodes.Add(Node(1, 20.0, 10.0));
			nodes.Add(Node(2, 20.0, 20.0));
			nodes.Add(Node(3, 30.0, 30.0));
			nodes.Add(Node(4, 10.0, 30.0));
			nodes.Add(Node(5, 40.0, 40.0));
			nodes.Add(Node(6, 60.0, 70.0));
			nodes.Add(Node(7, 30.0, 70.0));
			
			//Find smallest and largest for scales
			double xmin = double.MinValue, xmax = double.MaxValue;
			double ymin = double.MinValue, ymax = double.MaxValue;
			for(int n = 0; n<nodes.Size(); n++){
				if(nodes[n].getX() < xmin) xmin = nodes[n].getX();
				else if(nodes[n].getX() > xmax) xmax = nodes[n].getX();
				
				if(nodes[n].getY() < ymin) ymin = nodes[n].getY();
				else if(nodes[n].getY() > ymax) ymax = nodes[n].getY();
			}
			
			//Scale factors: panelx/(xmax-xmin)
			xscale = (double)(xright-xleft)/(xmax-xmin);
			yscale = (double)(ytop-ybottom)/(ymax-ymin);
			
			//Plot all nodes on panel
			Pen p = new Pen(Color.Black);
			for(int n = 0; n<nodes.Size(); n++){
				
			}
		}
	}
}
