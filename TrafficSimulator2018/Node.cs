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
	/// A Node is a junction between different Paths. It contains an x and y position
	/// on the map.
	/// </summary>
	public class Node
	{
		
		double x = 0.0, y = 0.0;
		int visitors = 0, id = -1;
		
		/// <summary>
		/// Default constructor sets a node with default values.
		/// </summary>
		public Node() {}
		
		/// <summary>
		/// Constructor that allows the ID and coordinates to be set on initialisation.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="X"></param>
		/// <param name="Y"></param>
		public Node(int ID, double X, double Y) {
			id = ID;
			x = X;
			y = Y;
		}
		
		/// <summary>
		/// This method should be called if a Person passes through the Node. This will increment
		/// the visitor counter, that counts how many Persons have passed through the node.
		/// </summary>
		/// <returns></returns>
		public int IncrementVisitors() {
			return ++visitors;
		}
		
		/// <summary>
		/// Sets the ID of the node.
		/// </summary>
		/// <param name="ID"></param>
		public void SetID(int ID) {
			id = ID;
		}	
		
		/// <summary>
		/// Sets the x coordinate of the node on the Map. If the x coordinate was never set, this
		/// will be 0.0.
		/// </summary>
		/// <param name="X"></param>
		public void SetX(double X) {
			x = X;
		}
		
		/// <summary>
		/// Sets the y coordinate of the node on the Map. If the x coordinate was never set, this
		/// will be 0.0.
		/// </summary>
		/// <param name="Y"></param>
		public void SetY(double Y) {
			y = Y;
		}
		
		/// <summary>
		/// Returns an int representing the ID of the Node. If the ID has never been set, this will
		/// return -1.
		/// </summary>
		/// <returns></returns>
		public int GetID() {
			return id;
		}
		
		/// <summary>
		/// Returns an int representing the number of Persons that have passed through this node.
		/// </summary>
		/// <returns></returns>
		public int GetVisitors() {
			return visitors;
		}
		
		/// <summary>
		/// Returns a double representing the x coordinate of the node on the Map.
		/// </summary>
		/// <returns></returns>
		public double GetX() {
			return x;
		}
		
		/// <summary>
		/// Returns a double representing the y coordinate of the node on the Map.
		/// </summary>
		/// <returns></returns>
		public double GetY(){
			return y;
		}
		
		/// <summary>
		/// Returns some text that describes the node.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return "Node " + id + ":\nx: " + x + "\ny: " + y + "Visitor count: " + visitors + "\n\n";
		}
		
	}
}
