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
		/// <param name="nodeID"></param>
		/// <returns></returns>
		public static List<Path> GetPathsFromNode(int nodeID)
		{
			Node node = GetNode(nodeID);
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
		
	}
}
