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
	public class Node {
		
		static int nextID = 0;
		
		protected int id = nextID++;
		protected double x = 0.0, y = 0.0;
		protected int visitors = 0;
		protected string name = "node";
		
		/// <summary>
		/// Default constructor sets a node with default values.
		/// </summary>
		public Node() {}
		
		/// <summary>
		/// Contructor that allows the coordinates of the node to be set on initialisation.
		/// </summary>
		/// <param name="X"></param>
		/// <param name="Y"></param>
		public Node(double X, double Y) {
			x = X;
			y = Y;
		}
		
		/// <summary>
		/// Constructor that allows the name and coordinates to be set on initialisation. The name
		/// can be used as an identifier later in the program.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="X"></param>
		/// <param name="Y"></param>
		public Node(double X, double Y, string name) : this(X, Y) {
			this.name = name;
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
		/// Returns a string representing the name of the Node. If the name has never been set,
		/// this will return "node".
		/// </summary>
		/// <returns></returns>
		public string GetName() {
			return name;
		}
		
		/// <summary>
		/// Sets the name of the Node.
		/// </summary>
		/// <param name="name"></param>
		public void SetName(string name) {
			this.name = name;
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
		/// Returns a double [] of size 2 that represents the x and y coordinates (respectively) on
		/// the Map.
		/// </summary>
		/// <returns></returns>
		public double [] GetPosition() {
			return new double[] {x, y};
		}
		
		/// <summary>
		/// Returns some text that describes the node.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return "Node " + id + ":\nx: " + x + "\ny: " + y + "\nVisitor count: " + visitors + "\n";
		}
		
	}
}
