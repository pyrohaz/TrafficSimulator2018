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
using System.Threading;

namespace TrafficSimulator2018
{
	/// <summary>
	/// Description of MapRenderer.
	/// </summary>
	public class MapRenderer
	{
		//Panel drawing variables
		const int NODE_RADIUS = 8;
		const int PERSON_RADIUS = 10;
		
		Graphics panelgfx;
		Font font;
		
		int xleft, xright, ytop, ybottom;
		double xmin, xmax, ymin, ymax;
		
		public MapRenderer()
		{
			font = new Font("Calibri", 12);
		}
		
		public void Render(Graphics g){
			//panelgfx.Clear(Color.White);
			panelgfx = g;
			RemovePeople();
			DrawPaths();
			DrawNodes();
			DrawPeople();
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
		
		void DrawNodes(){
			//Plot all nodes on panel
			Brush b = new SolidBrush(Color.Black);
			
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
				double tx, ty;
				double dist;
				
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
					
					dist = Map.GetPaths()[n].GetDistance();
					
					panelgfx.DrawLine(new Pen(Color.Aquamarine), (int)nxl1, (int)nyu1, (int)nxl2, (int)nyu2);
					panelgfx.DrawString(dist.ToString("N1"), font, new SolidBrush(Color.DarkRed), (float)tx, (float)ty);
				}
			}
		}
		
		double [] position = {0,0,0}, poslast = {0,0,0};
		void RemovePeople(){
			for(int n = 0; n<3; n++){
				Path path = Map.GetPaths()[n];
				
				double nsx = path.GetNodes()[0].GetX();
				double nsy = path.GetNodes()[0].GetY();
				double nex = path.GetNodes()[1].GetX();
				double ney = path.GetNodes()[1].GetY();
				double px, py, pxs, pys;
				
				px = nsx + poslast[n]*(nex-nsx);
				py = nsy + poslast[n]*(ney-nsy);
				
				//Scale person position to screen
				pxs = xleft + (px - xmin)*(double)(xright-xleft)/(xmax-xmin) - NODE_RADIUS/2;
				pys = ytop + (py - ymin)*(double)(ybottom-ytop)/(ymax-ymin) - NODE_RADIUS/2;
				
				panelgfx.FillEllipse(new SolidBrush(Color.White), new RectangleF((float)pxs, (float)pys, (float)NODE_RADIUS, (float)NODE_RADIUS));
			}
		}
		
		void DrawPeople(){
			//for(int n = 0; n<People.GetPeople().Count; n++){
			
			for(int n = 0; n<3; n++){
				//Grab each persons position and draw its position
				//Person person = People.GetPeople()[n];
				//Path path = person.GetPath();
				//double position = person.GetPosition();
				
				Path path = Map.GetPaths()[n];
				
				double nsx = path.GetNodes()[0].GetX();
				double nsy = path.GetNodes()[0].GetY();
				double nex = path.GetNodes()[1].GetX();
				double ney = path.GetNodes()[1].GetY();
				
				double px, py, pxs, pys;
				
				poslast[n] = position[n];
				position[n] += 0.2/path.GetDistance();
				
				if(position[n]>1.0) position[n] = 0.0;
				
				px = nsx + position[n]*(nex-nsx);
				py = nsy + position[n]*(ney-nsy);
				
				//Scale person position to screen
				pxs = xleft + (px - xmin)*(double)(xright-xleft)/(xmax-xmin) - NODE_RADIUS/2;
				pys = ytop + (py - ymin)*(double)(ybottom-ytop)/(ymax-ymin) - NODE_RADIUS/2;
				
				panelgfx.FillEllipse(new SolidBrush(Color.Goldenrod), new RectangleF((float)pxs, (float)pys, (float)NODE_RADIUS, (float)NODE_RADIUS));
			}
		}
	}
}
