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
using System.Diagnostics;

namespace TrafficSimulator2018
{
	public class MapRenderer
	{
		//Panel drawing variables
		const int NODE_RADIUS = 8;
		const int PERSON_RADIUS = 20;
		
		Font font;
		
		int xleft, xright, ytop, ybottom;
		double xmin, xmax, ymin, ymax;
		
		public MapRenderer()
		{
			font = new Font("Calibri", 12);
			CalculateNodeParameters();
		}
		
		//Render all parts of the panel
		public void Render(Graphics g){
			DrawPaths(g);
			DrawNodes(g);
			DrawPeople(g);
		}
		
		//Set internal panel range parameters (minimum left, right, top and bottom used within panel)
		public void SetPanelRange(int XLeft, int XRight, int YTop, int YBottom){
			xleft = XLeft;
			xright = XRight;
			ytop = YTop;
			ybottom = YBottom;
		}
		
		//Find minimum and maximum node positions - required for fitting all data within the panel
		public void CalculateNodeParameters(){
			
			//Find smallest and largest x and y values
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
		
		//Draw all nodes to the panel - scaled. Includes node ID
		void DrawNodes(Graphics panelgfx){
			//Plot all nodes on panel
			Brush b = new SolidBrush(Color.Black);
			
			for(int n = 0; n<Map.GetNodes().Count; n++){
				double nxl, nyu;
				
				nxl = xleft + (Map.GetNodes()[n].GetX() - xmin)*(double)(xright-xleft)/(xmax-xmin) - NODE_RADIUS/2;
				nyu = ytop + (Map.GetNodes()[n].GetY() - ymin)*(double)(ybottom-ytop)/(ymax-ymin) - NODE_RADIUS/2;
				
				panelgfx.FillEllipse(b, new RectangleF((float)nxl, (float)nyu, NODE_RADIUS, NODE_RADIUS));
				panelgfx.DrawString(Map.GetNodes()[n].GetID().ToString(), font, new SolidBrush(Color.Blue), (float)nxl+NODE_RADIUS/2, (float)nyu+NODE_RADIUS/2);
			}
		}
		
		//Draw all paths between nodes with the path length
		void DrawPaths(Graphics panelgfx){
			for(int n = 0; n<Map.GetPaths().Count; n++){
				double nxl1, nxl2, nyu1, nyu2;
				double nx1, nx2, ny1, ny2;
				double tx, ty;
				
				if(Map.GetPaths()[n].GetNodes()[0] != null &&  Map.GetPaths()[n].GetNodes()[1] != null){
					nx1 = Map.GetPaths()[n].GetNodes()[0].GetX();
					ny1 = Map.GetPaths()[n].GetNodes()[0].GetY();
					nx2 = Map.GetPaths()[n].GetNodes()[1].GetX();
					ny2 = Map.GetPaths()[n].GetNodes()[1].GetY();
					
					nxl1 = xleft + (nx1 - xmin)*(double)(xright-xleft)/(xmax-xmin);
					nyu1 = ytop + (ny1 - ymin)*(double)(ybottom-ytop)/(ymax-ymin);
					
					nxl2 = xleft + (nx2 - xmin)*(double)(xright-xleft)/(xmax-xmin);
					nyu2 = ytop + (ny2 - ymin)*(double)(ybottom-ytop)/(ymax-ymin);
					
					tx = (nxl1+nxl2)/2;
					ty = (nyu1+nyu2)/2;
					
//					dist = Map.GetPaths()[n].GetDistance();
					double time = Map.GetPaths()[n].GetTime();
					
					panelgfx.DrawLine(new Pen(Color.Aquamarine), (int)nxl1, (int)nyu1, (int)nxl2, (int)nyu2);
//					panelgfx.DrawString(dist.ToString("N1"), font, new SolidBrush(Color.DarkRed), (float)tx, (float)ty);
					panelgfx.DrawString(time.ToString("N1")+"s", font, new SolidBrush(Color.DarkRed), (float)tx, (float)ty);
				}
			}
		}
		
		void DrawPeople(Graphics panelgfx){
			double px, py, pxs, pys;
			for(int n = 0; n<People.GetNumberOfPeople(); n++){
				//Grab each persons position and draw it
				Person person = People.GetPeople()[n];
				
				px = person.GetX();
				py = person.GetY();
				
				//Scale person position to screen
				pxs = xleft + (px - xmin)*(double)(xright-xleft)/(xmax-xmin) - NODE_RADIUS/2;
				pys = ytop + (py - ymin)*(double)(ybottom-ytop)/(ymax-ymin) - NODE_RADIUS/2;
				
				//Draw person
				panelgfx.FillEllipse(new SolidBrush(Color.DodgerBlue), new RectangleF((float)pxs, (float)pys, (float)NODE_RADIUS, (float)NODE_RADIUS));
			}
		}
	}
}
