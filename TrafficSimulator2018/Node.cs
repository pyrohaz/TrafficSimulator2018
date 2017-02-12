/*
 * Created by SharpDevelop.
 * User: harri
 * Date: 12/02/2017
 * Time: 19:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace TrafficSimulator2018
{
	/// <summary>
	/// Description of Node.
	/// </summary>
	public class Node
	{
		
		double x, y;
		int visitors, id;
		
		public Node()
		{
			id = -1;
			visitors = 0;
			x = 0.0;
			y = 0.0;
		}
		
		public Node(int ID, double X, double Y){
			id = ID;
			x = X;
			y = Y;
		}
		
		//Sets
		public void SetID(int ID){
			id = ID;
		}	
		
		public void SetVisitor(int V){
			visitors = V;
		}
		
		public void SetX(double X){
			x = X;
		}
		
		public void SetY(double Y){
			y = Y;
		}
		
		//Gets
		public int GetID(){
			return id;
		}
		
		public int GetVisitors(){
			return visitors;
		}
		
		public double GetX(){
			return x;
		}
		
		public double GetY(){
			return y;
		}
		
	}
}
