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
	public class Route {

		// Once the route has been calculated, this will contain the Nodes for the Route in the order that they occur.
		List<Node> node_route;
		PseudoNode source_node, end_node;
		
		// Once the route has been calculated, this will contain the Paths for the Route in the order that they occur.
		// The Nodes in each Path will not necessarily be in the correct order.
		List<Path> path_route;
		List<Direction> path_directions;
		
		/// <summary>
		/// Initialises a Eoute by giving a start and end Node, and calculates the quickest Route between them.
		/// </summary>
		/// <param name="source_node"></param>
		/// <param name="end_node"></param>
		public Route(Node source_node, Node end_node) : this (new PseudoNode(source_node), new PseudoNode(end_node)) {}
		
		/// <summary>
		/// Initialises a Route by giving a start Node and end PseudoNode, and calculates the quickest Route between
		/// them.
		/// </summary>
		/// <param name="source_node"></param>
		/// <param name="end_node"></param>
		public Route(Node source_node, PseudoNode end_node) : this (new PseudoNode(source_node), end_node) {}
		
		/// <summary>
		/// Initialises a Route by giving a start PseudoNode and end Node, and calculates the quickest Route between
		/// them.
		/// </summary>
		/// <param name="source_node"></param>
		/// <param name="end_node"></param>
		public Route(PseudoNode source_node, Node end_node) : this (source_node, new PseudoNode(end_node)) {}
		
		/// <summary>
		/// Initialises a Route by giving a start and end PseudoNode, and calculates the quickest Route between them.
		/// </summary>
		/// <param name="_source_node"></param>
		/// <param name="_end_node"></param>
		public Route(PseudoNode _source_node, PseudoNode _end_node) {
			
			// Storing the source and destination nodes
			source_node = new PseudoNode(_source_node);
			end_node = new PseudoNode(_end_node);
			
			Node [] source_nodes = source_node.GetPath().GetNodes();
			Node [] end_nodes = end_node.GetPath().GetNodes();
			bool [] end_nodes_mapped = {false, false};
			
			List<NodeAndTime> nodes_and_times = new List<NodeAndTime>();
			List<Node> nodes = Map.GetNodes();
			
			// Setting the initial times to get to each node from the source node
			foreach(Node node in nodes) {
				if (node == source_nodes[0] || node == source_nodes[1]) {
					nodes_and_times.Add(new NodeAndTime(node, source_node.GetPath().GetTimeToNodeFrom(node, source_node)));
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
				
				// If all nodes have been mapped, then the analysis_node was not set in the previous code snippet.
				// Hence we need not run the rest of the loop and we can finish the while loop. This is a fail safe
				// to ensure that the code does not crash. In ordinary circumstances, this scenario should be
				// handled below, at the bottom of the while loop.
				if (analysis_node == null) {
					complete = true;
					continue;
				}
				
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
				
				// Check if both Nodes either side of the PseudoNode have been analysed.
				if (analysis_node.GetNode() == end_nodes[0]) {
					end_nodes_mapped[0] = true;
				} else if (analysis_node.GetNode() == end_nodes[1]) {
					end_nodes_mapped[1] = true;
				}
				
				// Completing the loop if both of the end Nodes have been mapped
				if (end_nodes_mapped[0] && end_nodes_mapped[1]) {
					complete = true;
				}
			}
			
			// Getting the time that it will take to get to the PseudoNode via both Nodes either side of the containing Route
			NodeAndTime [] end_node_and_times = {GetNodeAndTime(end_nodes[0], nodes_and_times), GetNodeAndTime(end_nodes[1], nodes_and_times)};
			double time_from_end_node_1 = end_node_and_times[0].GetTime() + end_node.GetPath().GetTimeToNodeFrom(end_node_and_times[0].GetNode(), end_node);
			double time_from_end_node_2 = end_node_and_times[1].GetTime() + end_node.GetPath().GetTimeToNodeFrom(end_node_and_times[1].GetNode(), end_node);
			
			// Selecting to go through the Node that will give the shortest time, and converting these to a list of Nodes.
			if (time_from_end_node_1 <= time_from_end_node_2) {
				node_route = end_node_and_times[0].ConvertToListOfNodes();
				path_route = end_node_and_times[0].ConvertToListOfPaths();
			} else {
				node_route = end_node_and_times[1].ConvertToListOfNodes();
				path_route = end_node_and_times[1].ConvertToListOfPaths();
			}
			
			// Adding start and end Paths on as they may not currently be there
			if (path_route.Count == 0) {
				path_route.Add(source_node.GetPath());
			}
			if (path_route[0] != source_node.GetPath()) {
				path_route.Insert(0, source_node.GetPath());
			}
			if (path_route[path_route.Count-1] != end_node.GetPath()) {
				path_route.Add(end_node.GetPath());
			}
			
			// Determine which directions the Paths should be travelled in
			DetermineDirections();
			
			// TODO: Remove when comfortable
			Debug.WriteLine("Path Route: ");
			foreach (Path path in path_route) {
				Debug.WriteLine(path.GetNodes()[0].GetID() + ", " + path.GetNodes()[1].GetID());
			}
			
		}
		
		/// <summary>
		/// This method returns the PseudoNode that is the destination node for the Route.
		/// </summary>
		/// <returns></returns>
		public PseudoNode GetDestinationNode() {
			return end_node;
		}
		
		/// <summary>
		/// This method returns a List of Nodes representing the Route, in the order that the Nodes will be travelled.
		/// </summary>
		/// <returns></returns>
		public List<Node> GetNodeRoute() {
			return node_route;
		}
		
		/// <summary>
		/// This method returns a List of Paths representing the Route, in the order that the Paths will be travelled.
		/// Note that the Nodes for each Path will not necessarily be in the correct order.
		/// </summary>
		/// <returns></returns>
		public List<Path> GetPathRoute() {
			return path_route;
		}
		
		/// <summary>
		/// This method finds the next Path on the Route after the given Path. If the given Path is not on the Route,
		/// or if the given Path was the final Path in the Route, this method will return null.
		/// </summary>
		/// <param name="current_path"></param>
		/// <returns></returns>
		public Path GetNextPath(Path current_path) {
			for (int i = 0; i < path_route.Count-1; i++) {
				if (path_route[i] == current_path) {
					return path_route[i+1];
				}
			}
			return null;
		}
		
		/// <summary>
		/// This method finds the next Path on the Route by the current position of the PseudoNode. If the PseudoNode is
		/// not on the Route, or if it lies on the last Path in the Route, this method will return null.
		/// </summary>
		/// <param name="current_position"></param>
		/// <returns></returns>
		public Path GetNextPath(PseudoNode current_position) {
			return GetNextPath(current_position.GetPath());
		}
		
		/// <summary>
		/// This method returns a Direction that specifies whether the given Path should be travelled forwards or
		/// backwards (i.e. from Path.GetNodes()[0] to Path.GetNodes()[1] or vice versa). If the Path is not in the
		/// Route, this method will return Direction.FORWARDS.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public Direction GetDirection(Path path) {
			for (int i = 0; i < path_route.Count; i++) {
				if (path == path_route[i]) {
					return path_directions[i];
				}
			}
			return Direction.FORWARDS;
		}
		
		/// <summary>
		/// This method returns a Direction that specifies whether the Path that the PseudoNode is on should be travelled
		/// forwards or backwards (i.e. from Path.GetNodes()[0] to Path.GetNodes()[1] or vice versa). If the PseudoNode
		/// does not sit on a Path in this Route, this method will return Direction.FORWARDS.
		/// </summary>
		/// <param name="pseudo_node"></param>
		/// <returns></returns>
		public Direction GetDirection(PseudoNode pseudo_node) {
			return GetDirection(pseudo_node.GetPath());
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
		/// This sets the directions of travel for each of the Paths in the path_route array. The result is stored
		/// in the path_directions array.
		/// </summary>
		void DetermineDirections() {

			// Initialise the size of the new Path directions to be the same as the size as the
			// path_route
			path_directions = new List<Direction>(path_route.Count);
			
			// If the route only contains one path
			if (path_route.Count == 1) {
				if (source_node.GetDistanceAlongPath() < end_node.GetDistanceAlongPath()) {
					path_directions.Add(Direction.FORWARDS);
				} else {
					path_directions.Add(Direction.BACKWARDS);
				}
				// Do not allow processing of the rest of this function
				return;
			}
			
			// Loop through each Path (except the last) and find the correct direction
			for (int i = 0; i < path_route.Count-1; i++) {
				
				// Get nodes that exist on the current Path
				Node [] current_nodes = path_route[i].GetNodes();
				
				// Get nodes that exist on the next Path
				Node [] next_nodes = path_route[i+1].GetNodes();
				
				if (current_nodes[0] == next_nodes[0] || current_nodes[0] == next_nodes[1]) {
					path_directions.Add(Direction.BACKWARDS);
				} else {
					path_directions.Add(Direction.FORWARDS);
				}
			}
			
			// Find the direction of the last Path
			Node [] last_path_nodes = path_route[path_route.Count-1].GetNodes();
			Node [] penultimate_path_node = path_route[path_route.Count-2].GetNodes();
			
			if (last_path_nodes[0] == penultimate_path_node[0] || last_path_nodes[0] == penultimate_path_node[1]) {
				path_directions.Add(Direction.FORWARDS);
			} else {
				path_directions.Add(Direction.BACKWARDS);
			}
			
		}
				
		/// <summary>
		/// Returns a string that represents the Route.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			string route = "Route: [start]";
			foreach (Node node in node_route) {
				route += ", " + node.GetID();
			}
			route += ", [end]";
			return route;
		}
		
	}
	
	class NodeAndTime {
		Node node; // The Node
		double time; // The cumulative (but shortest) time up to this Node
		
		// This bool will turn true when Routes to all adjacent Nodes have been analysed
		bool adjacent_nodes_mapped = false;
		
		// A List of NodeAndTime objects that contain the shortest calculated route to the given Node
		List<NodeAndTime> shortest_route_to_node = new List<NodeAndTime>();
		
		// These variables contain a List of Nodes or Paths that defines the fastest Route to the Node.
		// These should only be utilised once the shortest path algorithm has finished.
		List<Node> node_route;
		List<Path> path_route;
		
		/// <summary>
		/// Initialises a Node with whether it is the source Node or not, and sets the time to the Node
		/// accordingly. Source nodes will have a time set to 0 (as it takes no time to get from source
		/// to source). All other Nodes will be initialised to the maximum value.
		/// </summary>
		/// <param name="node"></param>
		/// <param name="source_node"></param>
		public NodeAndTime(Node node, bool source_node) {
			this.node = node;
			time = source_node ? 0 : Double.MaxValue;
		}
		
		/// <summary>
		/// Initialises a Node and gives the length of time that it takes to get to the Node. This can be
		/// useful if the starting position of the Route is a PseudoNode.
		/// </summary>
		/// <param name="node"></param>
		/// <param name="time"></param>
		public NodeAndTime(Node node, double time) {
			this.node = node;
			this.time = time;
		}
		
		/// <summary>
		/// This method returns the Node.
		/// </summary>
		/// <returns></returns>
		public Node GetNode() {
			return node;
		}
		
		/// <summary>
		/// This method sets the minimum time to get to the Node.
		/// </summary>
		/// <param name="time"></param>
		public void SetTime(double time) {
			this.time = time;
		}
		
		/// <summary>
		/// This method returns a double that contains the minimum time to get the Node.
		/// </summary>
		/// <returns></returns>
		public double GetTime() {
			return time;
		}
		
		/// <summary>
		/// This method returns the current shortest route to the Node via a List of NodeAndTimes in order.
		/// </summary>
		/// <returns></returns>
		public List<NodeAndTime> GetShortestRouteToNodeForCalc() {
			return shortest_route_to_node;
		}
		
		/// <summary>
		/// This method sets the shortest route to the Node via a List of NodeAndTimes.
		/// </summary>
		/// <param name="shortest_route_to_node"></param>
		public void SetShortestRouteToNode(List<NodeAndTime> shortest_route_to_node) {
			this.shortest_route_to_node = shortest_route_to_node;
		}
		
		/// <summary>
		/// This method sets whether all adjacent Nodes have been analysed.
		/// </summary>
		/// <param name="adjacent_nodes_mapped"></param>
		public void SetAdjacentNodesMapped(bool adjacent_nodes_mapped) {
			this.adjacent_nodes_mapped = adjacent_nodes_mapped;
		}
		
		/// <summary>
		/// This method whether all adjacent Nodes have been analysed.
		/// </summary>
		/// <returns></returns>
		public bool AdjacentNodesMapped() {
			return adjacent_nodes_mapped;
		}
		
		/// <summary>
		/// This method converts the shortest route in a List of NodeAndTimes to a List of Nodes in
		/// order.
		/// </summary>
		/// <returns></returns>
		public List<Node> ConvertToListOfNodes() {
			node_route = new List<Node>(shortest_route_to_node.Count + 1);
			foreach (NodeAndTime node_and_time in shortest_route_to_node) {
				node_route.Add(node_and_time.GetNode());
			}
			node_route.Add(node);
			return node_route;
		}
		
		/// <summary>
		/// This method converts the shortest route in a List of NodeAndTimes to a List of Paths in
		/// order. Note that the Nodes in the Paths may not be in the correct order.
		/// </summary>
		/// <returns></returns>
		public List<Path> ConvertToListOfPaths() {
			if (node_route == null)
				ConvertToListOfNodes();
			
			path_route = new List<Path>(node_route.Count-1);
			
			for (int i = 0; i < node_route.Count-1; i++) {
				path_route.Add(Map.GetPathWithNodes(node_route[i], node_route[i+1]));
			}
			
			return path_route;
		}
		
		public void PrintShortestPath() {
			Debug.Write("Route to node " + node.GetID() + ": ");
			
			if (shortest_route_to_node.Count == 0) {
				Debug.WriteLine("No route yet calculated.");
				return;
			}
			
			for (int i = 0; i < shortest_route_to_node.Count-1; i++) {
				Debug.Write(shortest_route_to_node[i].GetNode().GetID() + ", ");
			}
			Debug.Write(shortest_route_to_node[shortest_route_to_node.Count-1].GetNode().GetID() + "\n");
		}
		
		/// <summary>
		/// This method returns a string that represents the NodeAndTime.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return "Node " + node.GetID() + "\nTime: " + time + "\n";
		}
		
	}
	
}