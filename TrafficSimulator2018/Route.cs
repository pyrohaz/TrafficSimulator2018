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
using System.Diagnostics;

namespace TrafficSimulator2018
{
	/// <summary>
	/// A Route is contains the Nodes and Paths required to go from a start Node to
	/// an end Node via the shortest possible path.
	/// </summary>
	public class Route
	{
		// TODO: Remove these unnecessary variables.
		Node source_node, end_node;
		List<Node> node_route;
		List<Path> path_route;
		
		/// <summary>
		/// Initialises a route by giving a start and end node.
		/// </summary>
		/// <param name="source_node"></param>
		/// <param name="end_node"></param>
		public Route(Node source_node, Node end_node) {
			
			List<NodeAndTime> nodes_and_times = new List<NodeAndTime>();
			
			this.source_node = source_node;
			this.end_node = end_node;
			
			List<Node> nodes = Map.GetNodes();
			
			// Setting the initial lengths (times) to get to each node from the source node
			foreach (Node node in nodes) {
				if (node == source_node) {
					nodes_and_times.Add(new NodeAndTime(node, true));
				} else {
					nodes_and_times.Add(new NodeAndTime(node, false));
				}
			}
	
						
			// Implementing Dijkstra's algorithm:
			
			bool complete = false;
			
			while (!complete) {
				
				// Sort to ensure that the nearest (by time) nodes to the source node are at the top of the List
				Sort(nodes_and_times);
				
				// Finding the next node that has not been mapped
				NodeAndTime analysis_node = null;
				foreach (NodeAndTime node_and_time in nodes_and_times) {
					if (!node_and_time.AdjacentNodesMapped()) {
						analysis_node = node_and_time;
						break;
					}
				}
				
				// If all nodes have been mapped, then the analysis_node was not set.
				// Hence we need not run the rest of the loop and we can finish the while loop.
				if (analysis_node == null || analysis_node.GetNode() == end_node) {
					complete = true;
				} else {
					Debug.WriteLine("Analysing node " + analysis_node.GetNode().GetID());
					// Get adjacent nodes
					List<Node> adj_nodes_temp = Map.GetNodesAdjacentToNode(analysis_node.GetNode());
					
					foreach(Node node in adj_nodes_temp) {
						
						// Calculate time coming from this node
						double new_time = Map.GetPathWithNodes(analysis_node.GetNode(), node).GetTime() + analysis_node.GetTime();
						
						// If the time lower than it would be taking the previous route here, change that NodeAndTime object to come through this way instead
						NodeAndTime previous_node_and_time = GetNodeAndTime(node, nodes_and_times);
						if (new_time < previous_node_and_time.GetTime()) {
							List<NodeAndTime> new_shortest_route = new List<NodeAndTime>(analysis_node.GetShortestRouteToNodeForCalc());
							new_shortest_route.Add(analysis_node);
							previous_node_and_time.SetShortestRouteToNode(new_shortest_route);
							previous_node_and_time.SetTime(new_time);
						}
					}
					analysis_node.SetAdjacentNodesMapped(true);
				}
			}
			
			// Converting the array of NodeAndTime object to just Nodes.
			NodeAndTime end_node_and_time = GetNodeAndTime(end_node, nodes_and_times);
			node_route = end_node_and_time.ConvertToListOfNodes();
			path_route = end_node_and_time.ConvertToListOfPaths();
			
			// TODO: Remove print lines
			Debug.WriteLine("Optimum route from " + source_node.GetID() + " to " + end_node.GetID() + ":");
			foreach (Node node in node_route) {
				Debug.WriteLine(node.GetID());
			}
			
			Debug.WriteLine("Time = " + GetNodeAndTime(end_node, nodes_and_times).GetTime());
			
		}
		
		/// <summary>
		/// Sorts the gives List of NodesAndTimes by the time to get the the Node. The object passed by reference
		/// to this function is changed itself, but this function also returns a reference to that object.
		/// </summary>
		/// <param name="nodes_and_times_to_sort"></param>
		List<NodeAndTime> Sort(List<NodeAndTime> nodes_and_times_to_sort) {
			nodes_and_times_to_sort.Sort(delegate(NodeAndTime n1, NodeAndTime n2) {
					return n1.GetTime().CompareTo(n2.GetTime());
			});
			return nodes_and_times_to_sort;
		}
		
		/// <summary>
		/// This method returns the associated NodeAndTime object for a given Node. If the Node
		/// does not exist within the nodesAndTimes List, then this returns null.
		/// </summary>
		/// <param name="node"></param>
		/// <param name="nodes_and_times"></param>
		/// <returns></returns>
		NodeAndTime GetNodeAndTime(Node node, List<NodeAndTime> nodes_and_times) {
			foreach(NodeAndTime node_and_time in nodes_and_times) {
				if (node_and_time.GetNode() == node) {
					return node_and_time;
				}
			}
			return null;
		}
				
		/// <summary>
		/// Returns a string that represents the Route.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return source_node.GetID() + " to " + end_node.GetID() + "\n";
		}
		
	}
	
	class NodeAndTime {
		Node node;
		double time;
		bool adjacent_nodes_mapped = false;
		
		List<NodeAndTime> shortest_route_to_node = new List<NodeAndTime>();
		List<Node> node_route;
		List<Path> path_route;
		
		public NodeAndTime(Node node, bool source_node) {
			this.node = node;
			time = source_node ? 0 : Double.MaxValue;
		}
		
		public Node GetNode() {
			return node;
		}
		
		public void SetTime(double time) {
			this.time = time;
		}
		
		public double GetTime() {
			return time;
		}
		
		public List<NodeAndTime> GetShortestRouteToNodeForCalc() {
			return shortest_route_to_node;
		}
		
		public void SetShortestRouteToNode(List<NodeAndTime> shortest_route_to_node) {
			this.shortest_route_to_node = shortest_route_to_node;
		}
		
		public void SetAdjacentNodesMapped(bool adjacent_nodes_mapped) {
			this.adjacent_nodes_mapped = adjacent_nodes_mapped;
		}
		
		public bool AdjacentNodesMapped() {
			return adjacent_nodes_mapped;
		}
		
		public List<Node> ConvertToListOfNodes() {
			node_route = new List<Node>(shortest_route_to_node.Count + 1);
			foreach (NodeAndTime node_and_time in shortest_route_to_node) {
				node_route.Add(node_and_time.GetNode());
			}
			node_route.Add(GetNode());
			return node_route;
		}
		
		public List<Path> ConvertToListOfPaths() {
			if (node_route == null)
				ConvertToListOfNodes();
			
			path_route = new List<Path>(node_route.Count-1);
			
			for (int i = 0; i < node_route.Count-1; i++) {
				path_route.Add(Map.GetPathWithNodes(node_route[i], node_route[i+1]));
			}
			
			return path_route;
		}
		
		public override string ToString() {
			return "Node " + node.GetID() + "\nTime: " + time + "\n";
		}
		
	}
	
}