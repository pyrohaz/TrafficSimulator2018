/*
 * Created by SharpDevelop.
 * User: harri
 * Date: 12/02/2017
 * Time: 22:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;

namespace TrafficSimulator2018
{
	/// <summary>
	/// Description of MapRenderer.
	/// </summary>
	public class MapRenderer
	{
		//Panel drawing variables
		const int NODE_RADIUS = 8;
		
		Graphics panelgfx;
		Font font;
		
		int xleft, xright, ytop, ybottom;
		double xmin, xmax, ymin, ymax;
		
		public MapRenderer()
		{
			font = new Font("Calibri", 12);
		}
		
		public void Render(){
			DrawPaths();
			DrawNodes();
		}
		
		public void SetPanelRange(int XLeft, int XRight, int YTop, int YBottom){
			xleft = XLeft;
			xright = XRight;
			ytop = YTop;
			ybottom = YBottom;
		}
		
		public void CalculateNodeData(){
			
			//Find smallest and largest for scales
			xmin = double.MaxValue;
			xmax = double.MinValue;
			ymin = double.MaxValue;
			ymax = double.MinValue;
			for(int n = 0; n<Map.getNodes().Count; n++){
				if(Map.getNodes()[n].getX() < xmin) xmin = Map.getNodes()[n].getX();
				else if(Map.getNodes()[n].getX() > xmax) xmax = Map.getNodes()[n].getX();
				
				if(Map.getNodes()[n].getY() < ymin) ymin = Map.getNodes()[n].getY();
				else if(Map.getNodes()[n].getY() > ymax) ymax = Map.getNodes()[n].getY();
			}
		}
		
		public void SetGFX(Graphics Panelgfx){
			panelgfx = Panelgfx;
		}
		
		void DrawNodes(){
			//Plot all nodes on panel
			Brush b = new SolidBrush(Color.Black);
			panelgfx.Clear(Color.White);
			
			for(int n = 0; n<Map.getNodes().Count; n++){
				double nxl, nyu;
				
				nxl = xleft + (Map.getNodes()[n].getX() - xmin)*(double)(xright-xleft)/(xmax-xmin) - NODE_RADIUS/2;
				nyu = ytop + (Map.getNodes()[n].getY() - ymin)*(double)(ybottom-ytop)/(ymax-ymin) - NODE_RADIUS/2;
				
				//Debug.WriteLine(nxl + " " + nyu);
			
				panelgfx.FillEllipse(b, new RectangleF((float)nxl, (float)nyu, NODE_RADIUS, NODE_RADIUS));
				panelgfx.DrawString(Map.getNodes()[n].getID().ToString(), font, new SolidBrush(Color.Blue), (float)nxl+NODE_RADIUS/2, (float)nyu+NODE_RADIUS/2);
			}
		}
		
		void DrawPaths(){
			for(int n = 0; n<Map.getRoutes().Count; n++){
				double nxl, nyu;
				double nx1, nx2, ny1, ny2;
				
				nodex = 
				
				nxl = xleft + (Map.getNodes()[n].getX() - xmin)*(double)(xright-xleft)/(xmax-xmin) - NODE_RADIUS/2;
				nyu = ytop + (Map.getNodes()[n].getY() - ymin)*(double)(ybottom-ytop)/(ymax-ymin) - NODE_RADIUS/2;
			}
		}
	}
}
