/*
 * Created by SharpDevelop.
 * User: harri
 * Date: 12/02/2017
 * Time: 20:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace TrafficSimulator2018
{
	/// <summary>
	/// A Route is contains the Nodes and Paths required to go from a start Node to
	/// an end Node via the shortest possible path.
	/// </summary>
	public class Route
	{
		// TODO: Remove these unnecessary variables.
		Node start_node, end_node;
		
		/// <summary>
		/// Initialises a route by giving a start and end node.
		/// </summary>
		/// <param name="start_node"></param>
		/// <param name="end_node"></param>
		public Route(Node start_node, Node end_node) {
			
			this.start_node = start_node;
			this.end_node = end_node;
			
			List<Node> nodes = Map.GetNodes();
			List<NodeAndTime> nodesAndTimes = new List<NodeAndTime>(nodes.Count);
			
			for (int i = 0; i < nodes.Count; i++) {
				nodesAndTimes.Add(new NodeAndTime(nodes[i]));
			}
			
		}
		
		/// <summary>
		/// Returns a string that represents the Route.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return start_node.GetID() + " to " + end_node.GetID() + "\n";
		} 
		
	}
	
	class NodeAndTime {
		Node node;
		double time;
		
		List<Path> shortest_routes_to_nodes = new List<Path>();
		
		public NodeAndTime(Node node) {
			this.node = node;
			time = Double.MaxValue;
		}
		
		public List<Path> GetShortestRoute() {
			return null;
		}
		
	}
	
}