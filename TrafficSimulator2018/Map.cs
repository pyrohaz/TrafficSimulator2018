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
	/// The Map class contains all of the Nodes and Paths that make up the "map".
	/// The Nodes and Paths are set up on first use of the class and should then
	/// be accessed statically.
	/// </summary>
	public static class Map
	{
		
		static List<Node> nodes = new List<Node>();
		static List<Path> paths = new List<Path>();
		
		// Setting up the environment
		static Map() {
			
			// Setting up nodes
			nodes.Add(new Node(0, 10, 10));
			nodes.Add(new Node(1, 15, 55));
			nodes.Add(new Node(2, 40, 15));
			nodes.Add(new Node(3, 50, 80));
			nodes.Add(new Node(4, 75, 50));
			nodes.Add(new Node(5, 85, 10));
			nodes.Add(new Node(6, 95, 90));
			nodes.Add(new Node(7, 100, 100));
			
			// Setting up routes
			paths.Add(new Path(GetNode(0), GetNode(1), 0.7));
			paths.Add(new Path(GetNode(0), GetNode(2), 1.0));
			paths.Add(new Path(GetNode(1), GetNode(2), 0.3));
			paths.Add(new Path(GetNode(1), GetNode(3), 1.0));
			paths.Add(new Path(GetNode(1), GetNode(7), 2.0));
			paths.Add(new Path(GetNode(2), GetNode(3), 1.0));
			paths.Add(new Path(GetNode(2), GetNode(5), 1.6));
			paths.Add(new Path(GetNode(3), GetNode(4), 1.0));
			paths.Add(new Path(GetNode(3), GetNode(6), 1.6));
			paths.Add(new Path(GetNode(4), GetNode(5), 0.5));
			paths.Add(new Path(GetNode(4), GetNode(6), 0.5));
			paths.Add(new Path(GetNode(6), GetNode(7), 0.3));
		}
		
		/// <summary>
		/// Returns a List<Path> object that contains all of the routes in the map.
		/// </summary>
		/// <returns></returns>
		public static List<Path> GetPaths() {
			return paths;
		}
		
		/// <summary>
		/// Returns a List<Node> object that contains all of the routes in the map.
		/// </summary>
		/// <returns></returns>
		public static List<Node> GetNodes() {
			return nodes;
		}
		
		/// <summary>
		/// Returns the Node with the corresponding id. If no Node exists with that ID, a message is displayed
		/// and a null pointer is returned.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Node GetNode(int id) {
			for (int i = 0; i < nodes.Count; i++) {
				if (nodes[i].GetID() == id) {
					return nodes[i];
				}
			}
			
			// If the ID cannot be found
			Debug.WriteLine("Node with ID " + id + " cannot be found.");
			return null;
		}
		
		/// <summary>
		/// Returns all of the Paths that are connected to the specified node. This returns
		/// null if the Node does not exist.
		/// </summary>
		/// <param name="node_ID"></param>
		/// <returns></returns>
		public static List<Path> GetPathsFromNode(int node_ID)
		{
			Node node = GetNode(node_ID);
			return GetPathsFromNode(node);
		}
		
		/// <summary>
		/// Returns all of the Paths that are connected to the specified Node. This returns
		/// null if the Node does not exist.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static List<Path> GetPathsFromNode(Node node)
		{
			
			// Returns null if the node does not exist.
			if (node == null)
				return null;
			
			List<Path> attachedRoutes = new List<Path>();
			
			// Loops through all paths to see if the node is a part of that path
			for (int i = 0; i < paths.Count; i++) {
				Node [] attachedNodes = paths[0].GetNodes();
				
				for (int j = 0; i < attachedNodes.Length; i++) {
					if (attachedNodes[j] == node) {
						attachedRoutes.Add(paths[i]);
					}
				}
			}
			return attachedRoutes;
		}
		
		/// <summary>
		/// This method returns a List of all Nodes that are adjacent (i.e. at the other end of
		/// a single Path) to a given Node by its ID.
		/// If no Node exists with that ID, the List will be returned null.
		/// </summary>
		/// <param name="node_ID"></param>
		/// <returns></returns>
		public static List<Node> GetNodesAdjacentToNode(int node_ID) {
			Node node = GetNode(node_ID);
			return GetNodesAdjacentToNode(node);
		}
		
		/// <summary>
		/// This method returns a List of all Nodes that are adjacent (i.e. at the other end of
		/// a single Path) to the given Node.
		/// If the given Node is null, the List will be returned as null.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static List<Node> GetNodesAdjacentToNode(Node node) {
			
			// Return null if the given node is null.
			if (node == null)
				return null;
			
			List<Path> paths_from_node = GetPathsFromNode(node);
			List<Node> adjacent_nodes = new List<Node>();
			
			// Loops through all paths adjacent to a node
			foreach (Path path in paths_from_node) {
				Node [] path_nodes = path.GetNodes();
				
				// Each path always has two nodes. As we already know that these paths are adjacent,
				// we only need to check if the first node is the given "origin" node. If it is the origin
				// node, then the other node on the path must be the adjacent node and vice versa.
				// This adjacent node is added to the List.
				if (path_nodes[0] != node) {
					adjacent_nodes.Add(path_nodes[0]);
				} else {
					adjacent_nodes.Add(path_nodes[1]);
				}
			}
			
			return adjacent_nodes;
		}
		
		/// <summary>
		/// This method returns the Path that joins the two Nodes with the given IDs. If no such Path exists,
		/// or one or more of the Nodes do not exist, this method returns null.
		/// </summary>
		/// <param name="node1_id"></param>
		/// <param name="node2_id"></param>
		/// <returns></returns>
		public static Path GetPathWithNodes(int node1_id, int node2_id) {
			Node node1 = GetNode(node1_id);
			Node node2 = GetNode(node2_id);
			
			// If either of the Nodes are null, return null
			if (node1 == null || node2 == null)
				return null;
			
			return GetPathWithNodes(node1, node2);
		}
		
		/// <summary>
		/// This method returns the Path that joins the two given Nodes. If no such Path exists, this method
		/// will return null.
		/// </summary>
		/// <param name="node1"></param>
		/// <param name="node2"></param>
		/// <returns></returns>
		public static Path GetPathWithNodes(Node node1, Node node2) {
			
			// Checking that the same node is not given twice
			if (node1 == node2)
				return null;
			
			// Gets all of the paths from the node1 and tries to find the path that also contains node2
			List<Path> paths_from_node = GetPathsFromNode(node1);
			foreach(Path path in paths_from_node) {
				Node [] path_nodes = path.GetNodes();
				if (path_nodes[0] == node2 || path_nodes[1] == node2) {
					return path;
				}
			}
			return null; // If no path exists between node1 and node2, return null
		}
		
	}
}
