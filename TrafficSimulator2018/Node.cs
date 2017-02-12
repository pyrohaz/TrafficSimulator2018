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
		public Node()
		{
		}
		
		//Sets
		public void setID(int ID){
			id = ID;
		}	
		
		public void setVisitor(int V){
			visitors = V;
		}
		
		public void setX(double X){
			x = X;
		}
		
		public void setY(double Y){
			y = Y;
		}
		
		//Gets
		public int getID(){
			return id;
		}
		
		public int getVisitors(){
			return visitors;
		}
		
		public double getX(){
			return x;
		}
		
		public double getY(){
			return y;
		}
		
		double x, y;
		int visitors, id;
	}
}
