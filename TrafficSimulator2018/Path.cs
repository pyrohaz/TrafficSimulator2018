/*
 * Created by SharpDevelop.
 * User: harri
 * Date: 12/02/2017
 * Time: 20:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace TrafficSimulator2018
{
	/// <summary>
	/// A Path is the "road" between two nodes. It has a defined speed limit. The length of the node is calculated
	/// from the distance between the two nodes.
	/// </summary>
	public class Path
	{
		
		static int next_route_ID = 0;
		
		int path_ID = next_route_ID++;
		Node [] nodes = new Node[2];
		double speed_limit = 0;
		double distance_between_nodes = 0;
		
		/// <summary>
		/// Sets the start and end points and speed limit of the path upon initialisation.
		/// </summary>
		/// <param name="node1"></param>
		/// <param name="node2"></param>
		/// <param name="speed_limit"></param>
		public Path(Node node1, Node node2, double speed_limit) {
			nodes[0] = node1;
			nodes[1] = node2;
			this.speed_limit = speed_limit;
			distance_between_nodes = Math.Sqrt(Math.Pow(nodes[0].GetX() - nodes[1].GetX(), 2) + Math.Pow(nodes[0].GetY() - nodes[1].GetY(), 2));
		}
		
		/// <summary>
		/// Returns an array of Nodes that contains the start and end points of the path.
		/// The size of the returned Node array will always be 2.
		/// </summary>
		/// <returns></returns>
		public Node [] GetNodes() {
			return nodes;
		}
		
		/// <summary>
		/// Returns an int that represents the ID of the path.
		/// </summary>
		/// <returns></returns>
		public int GetID() {
			return path_ID;
		}
		
		/// <summary>
		/// Returns a double representing the speed limit of the path.
		/// </summary>
		/// <returns></returns>
		public double GetSpeedLimit() {
			return speed_limit;
		}
		
		// Sets the speed limit of the path.
		public void SetSpeedLimit(double speed_limit) {
			this.speed_limit = speed_limit;
		}
		
		/// <summary>
		/// Returns an int representing the ID of the Path.
		/// </summary>
		/// <returns></returns>
		public int GetPathID() {
			return path_ID;
		}
		
		/// <summary>
		/// Returns a double representing the distance between the two end Nodes of the Path.
		/// </summary>
		/// <returns></returns>
		public double GetDistance() {
			return distance_between_nodes;
		}	
		
		/// <summary>
		/// Returns a double representing the length of time (in s) that it should take to travel
		/// from one end of this Path to the other.
		/// </summary>
		/// <returns></returns>
		public double GetTime() {
			return GetDistance()/speed_limit;
		}
		
		/// <summary>
		/// Returns a string that describes the Path.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return "Path " + path_ID + ":\nNode 1: " + nodes[0].GetID() + "\nNode 2: " + nodes[1].GetID() + "\nSpeed limit: " + GetSpeedLimit() +
				"\nDistance between nodes: " + distance_between_nodes + "\nTime between nodes (s): " + GetTime() + "\n\n";
		}
	}
}
