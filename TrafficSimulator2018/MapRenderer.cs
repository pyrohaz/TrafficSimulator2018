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
			for(int n = 0; n<Map.GetNodes().Count; n++){
				if(Map.GetNodes()[n].GetX() < xmin) xmin = Map.GetNodes()[n].GetX();
				else if(Map.GetNodes()[n].GetX() > xmax) xmax = Map.GetNodes()[n].GetX();
				
				if(Map.GetNodes()[n].GetY() < ymin) ymin = Map.GetNodes()[n].GetY();
				else if(Map.GetNodes()[n].GetY() > ymax) ymax = Map.GetNodes()[n].GetY();
			}
		}
		
		public void SetGFX(Graphics Panelgfx){
			panelgfx = Panelgfx;
		}
		
		void DrawNodes(){
			//Plot all nodes on panel
			Brush b = new SolidBrush(Color.Black);
			panelgfx.Clear(Color.White);
			
			for(int n = 0; n<Map.GetNodes().Count; n++){
				double nxl, nyu;
				
				nxl = xleft + (Map.GetNodes()[n].GetX() - xmin)*(double)(xright-xleft)/(xmax-xmin) - NODE_RADIUS/2;
				nyu = ytop + (Map.GetNodes()[n].GetY() - ymin)*(double)(ybottom-ytop)/(ymax-ymin) - NODE_RADIUS/2;
				
				//Debug.WriteLine(nxl + " " + nyu);
				
				panelgfx.FillEllipse(b, new RectangleF((float)nxl, (float)nyu, NODE_RADIUS, NODE_RADIUS));
				panelgfx.DrawString(Map.GetNodes()[n].GetID().ToString(), font, new SolidBrush(Color.Blue), (float)nxl+NODE_RADIUS/2, (float)nyu+NODE_RADIUS/2);
			}
		}
		
		void DrawPaths(){
			for(int n = 0; n<Map.GetPaths().Count; n++){
				double nxl1, nxl2, nyu1, nyu2;
				double nx1, nx2, ny1, ny2;
				
				if(Map.GetPaths()[n].GetNodes()[0] != null &&  Map.GetPaths()[n].GetNodes()[1] != null){
					nx1 = Map.GetPaths()[n].GetNodes()[0].GetX();
					ny1 = Map.GetPaths()[n].GetNodes()[0].GetY();
					nx2 = Map.GetPaths()[n].GetNodes()[1].GetX();
					ny2 = Map.GetPaths()[n].GetNodes()[1].GetY();
					
					nxl1 = xleft + (nx1 - xmin)*(double)(xright-xleft)/(xmax-xmin);
					nyu1 = ytop + (ny1 - ymin)*(double)(ybottom-ytop)/(ymax-ymin);
					
					nxl2 = xleft + (nx2 - xmin)*(double)(xright-xleft)/(xmax-xmin);
					nyu2 = ytop + (ny2 - ymin)*(double)(ybottom-ytop)/(ymax-ymin);
					
					panelgfx.DrawLine(new Pen(Color.DarkGreen), (int)nxl1, (int)nyu1, (int)nxl2, (int)nyu2);
				}
			}
		}
	}
}
