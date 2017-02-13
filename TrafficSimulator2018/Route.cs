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
		List<NodeAndTime> nodesAndTimes = new List<NodeAndTime>();
		
		/// <summary>
		/// Initialises a route by giving a start and end node.
		/// </summary>
		/// <param name="source_node"></param>
		/// <param name="end_node"></param>
		public Route(Node source_node, Node end_node) {
			
			this.source_node = source_node;
			this.end_node = end_node;
			
			List<Node> nodes = Map.GetNodes();
			
			// Setting the initial lengths (times) to get to each node from the source node
			foreach (Node node in nodes) {
				if (node == source_node) {
					nodesAndTimes.Add(new NodeAndTime(node, true));
				} else {
					nodesAndTimes.Add(new NodeAndTime(node, false));
				}
			}
			
			// Sort to ensure that the source Node is at the top
			// Get the routes to all of the adjacent nodes. Store route taken to get there.
			// Set that route such that it has already been mapped.
			// Sort the nodesAndTimesArray
			// Pick the next Node that hasn't been mapped...
			
			bool complete = false;
			
			while (!complete) {
				
				// Sort to ensure that the source Node is at the top
				Sort(nodesAndTimes);
				
				// Finding the next node that has not been mapped
				NodeAndTime analysis_node = null;
				foreach (NodeAndTime nodeAndTime in nodesAndTimes) {
					if (!nodeAndTime.AdjacentNodesMapped()) {
						analysis_node = nodeAndTime;
						break;
					}
				}
				
				// If all nodes have been mapped, then the analysis_node was not set.
				// Hence we need not run the rest of the loop and we can finish the while loop.
				if (analysis_node == null) {
					complete = true;
				} else {
					// Get adjacent nodes
					List<Node> adj_nodes_temp = Map.GetNodesAdjacentToNode(analysis_node.GetNode());
					
					Debug.WriteLine("Nodes adjacent to Node " + analysis_node.GetNode().GetID() + ":");
					
					foreach(Node node in adj_nodes_temp) {
						
						// Calculate time coming from this node
						double new_time = Map.GetPathWithNodes(analysis_node.GetNode(), node).GetTime() + analysis_node.GetTime();
						
						// If the time lower than it would be taking the previous route here, change that NodeAndTime object to come through this way instead
						NodeAndTime previous_node_and_time = GetNodeAndTime(node);
						if (new_time < previous_node_and_time.GetTime()) {
							List<NodeAndTime> new_shortest_route = analysis_node.GetShortestRouteToNode();
							new_shortest_route.Add(analysis_node);
							previous_node_and_time.SetShortestRouteToNode(new_shortest_route);
							previous_node_and_time.SetTime(new_time);
						}
					}
					analysis_node.SetAdjacentNodesMapped(true);
				}
				
			}
			
			Debug.WriteLine("Optimum route from " + source_node.GetID() + " to " + end_node.GetID() + ":");
			List<NodeAndTime> route = GetNodeAndTime(end_node).GetShortestRouteToNode();
			foreach (NodeAndTime nodeAndTime in route) {
				Debug.Write(nodeAndTime.GetNode().GetID() + ", ");
			}
			Debug.WriteLine(GetNodeAndTime(end_node).GetNode().GetID());
			
			Debug.WriteLine("Time = " + GetNodeAndTime(end_node).GetTime());
			
		}
		
		/// <summary>
		/// Sorts the gives List of NodesAndTimes by the time to get the the Node.
		/// </summary>
		/// <param name="nodesAndTimes"></param>
		void Sort(List<NodeAndTime> nodesAndTimes) {
			nodesAndTimes.Sort(delegate(NodeAndTime n1, NodeAndTime n2) {
					return n1.GetTime().CompareTo(n2.GetTime());
			});
		}
		
		/// <summary>
		/// This method returns the associated NodeAndTime object for a given Node. If the Node
		/// does not exist within the nodesAndTimes List, then this returns null.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		NodeAndTime GetNodeAndTime(Node node) {
			foreach(NodeAndTime nodeAndTime in nodesAndTimes) {
				if (nodeAndTime.GetNode() == node) {
					return nodeAndTime;
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
		
		public List<NodeAndTime> GetShortestRouteToNode() {
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
		
		public override string ToString() {
			return "Node " + node.GetID() + "\nTime: " + time + "\n";
		}
		
	}
	
}