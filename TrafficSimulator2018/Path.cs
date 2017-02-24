/*
 * Created by SharpDevelop.
 * User: harri
 * Date: 12/02/2017
 * Time: 20:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;

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
		
		/// <summary>
		/// Sets the speed limit of the path.
		/// </summary>
		/// <param name="speed_limit"></param>
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
			return distance_between_nodes/speed_limit;
		}
		
		/// <summary>
		/// This method returns a double representing the length of time (in seconds) that it would
		/// take to get to a Node from the given distance along the Path.
		/// </summary>
		/// <param name="node"></param>
		/// <param name="distance_along_path"></param>
		/// <returns></returns>
		public double GetTimeToNodeFrom(Node node, double distance_along_path) {
			if (node == nodes[0]) {
				return distance_along_path/speed_limit;
			} else if (node == nodes[1]) {
				return (distance_between_nodes-distance_along_path)/speed_limit;
			} else {
				Debug.WriteLine("Node " + node.GetID() + " is not part of this path.");
				return Double.MaxValue;
			}
		}
		
		/// <summary>
		/// This method returns a double that represents the length of time (in seconds) that it
		/// would take to get from one PseudoNode on the Path to another on the Path. If one of
		/// the nodes is not on this Path, this method will return Double.MaxValue.
		/// </summary>
		/// <param name="destination_node"></param>
		/// <param name="source_node"></param>
		/// <returns></returns>
		public double GetTimeToPseudoNodeFrom(PseudoNode destination_node, PseudoNode source_node) {
			
			// Check both PseudoNodes are on the Path
			if (destination_node.GetPath() != this || source_node.GetPath() != this) {
				return Double.MaxValue;
			}
			
			// Calculate length of time
			double destination_distance = destination_node.GetDistanceAlongPath();
			double source_distance = source_node.GetDistanceAlongPath();
			double time = (destination_distance - source_distance) / speed_limit;
			time = (time > 0) ? time : -time;
			return time;
		}
		
		/// <summary>
		/// This method returns a double representing the length of time (in seconds) that it would
		/// take to get to from a PseudoNode to a Node along the same Path. If the given Node is
		/// not on the same Path, this returns Double.MAX_VALUE as this method contains no support
		/// for routing.
		/// </summary>
		/// <param name="node"></param>
		/// <param name="pseudoNode"></param>
		/// <returns></returns>
		public double GetTimeToNodeFrom(Node node, PseudoNode pseudoNode) {
			if (pseudoNode.GetPath() == this) {
				return GetTimeToNodeFrom(node, pseudoNode.GetDistanceAlongPath());
			} else {
				Debug.WriteLine("The PseudoNode does not lie on this Path.");
				return Double.MaxValue;
			}
		}
		
		/// <summary>
		/// Returns a string that describes the Path.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return "Path " + path_ID + ":\nNode 1: " + nodes[0].GetID() + "\nNode 2: " + nodes[1].GetID() + "\nSpeed limit: " + GetSpeedLimit() +
				"\nDistance between nodes: " + distance_between_nodes + "\nTime between nodes (s): " + GetTime() + "\n";
		}
	}
}
