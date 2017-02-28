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
	public static class Map {
		
		static List<Node> nodes = new List<Node>();
		static List<Path> paths = new List<Path>();
		static string directory = "..\\..\\data\\maps\\test-map\\";
		
		// Setting up the environment
		static Map() {
			
			// TODO: Handle bad map better
			if (!ReadNodes(directory + "nodes.txt")) {
				Debug.WriteLine("Problem loading map. Exiting.");
				System.Windows.Forms.Application.Exit();
				return;
			}
			
			// Setting up routes
			paths.Add(new Path(GetNode("0"), GetNode(1), 7));
			paths.Add(new Path(GetNode("0"), GetNode(2), 11));
			paths.Add(new Path(GetNode("1"), GetNode(2), 3));
			paths.Add(new Path(GetNode("1"), GetNode(3), 10));
			paths.Add(new Path(GetNode("1"), GetNode(7), 20));
			paths.Add(new Path(GetNode("2"), GetNode(3), 10));
			paths.Add(new Path(GetNode("2"), GetNode(5), 16));
			paths.Add(new Path(GetNode("3"), GetNode(4), 10));
			paths.Add(new Path(GetNode("3"), GetNode(6), 16));
			paths.Add(new Path(GetNode("4"), GetNode(5), 5));
			paths.Add(new Path(GetNode("4"), GetNode(6), 5));
			paths.Add(new Path(GetNode("6"), GetNode(7), 3));
		}
		
		/// <summary>
		/// Returns a List object that contains all of the routes in the map.
		/// </summary>
		/// <returns></returns>
		public static List<Path> GetPaths() {
			return paths;
		}
		
		/// <summary>
		/// Returns a List object that contains all of the routes in the map.
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
			
			// Finding the Node by ID
			foreach (Node node in nodes) {
				if (node.GetID() == id) {
					return node;
				}
			}
			
			// If the ID cannot be found, inform user and return null
			Debug.WriteLine("Node with ID " + id + " cannot be found.");
			return null;
		}
		
		/// <summary>
		/// Returns the Node with the given name. If no node exists with that name, a null pointer is returned.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static Node GetNode(string name) {
			
			// Finding the Node by name
			foreach (Node node in nodes) {
				if (node.GetName().Equals(name)) {
					return node;
				}
			}
			
			// If the Node with that name cannot be found, return null
			return null;
		}
		
		/// <summary>
		/// Returns all of the Paths that are connected to the specified node. This returns
		/// null if the Node does not exist.
		/// </summary>
		/// <param name="node_ID"></param>
		/// <returns></returns>
		public static List<Path> GetPathsFromNode(int node_ID) {
			Node node = GetNode(node_ID);
			return GetPathsFromNode(node);
		}
		
		/// <summary>
		/// Returns all of the Paths that are connected to the specified Node. This returns
		/// null if the Node does not exist.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static List<Path> GetPathsFromNode(Node node) {
			
			// Returns null if the node does not exist.
			if (node == null)
				return null;
			
			List<Path> attached_paths = new List<Path>();
			
			// Loops through all paths to see if the node is a part of that path
			foreach(Path path in paths) {
				Node [] attached_nodes = path.GetNodes();
				
				foreach (Node attached_node in attached_nodes) {
					if (attached_node == node) {
						attached_paths.Add(path);
					}
				}
			}

			return attached_paths;
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
		
		/// <summary>
		/// Reads in the Nodes from a given text file.
		/// </summary>
		/// <param name="filepath"></param>
		/// <returns></returns>
		static bool ReadNodes(string filepath) {
			if (System.IO.File.Exists(filepath)) {
				System.IO.StreamReader reader = new System.IO.StreamReader(filepath);
				
				int line_number = 1;
				
				do {
					// Read input line
					string line = reader.ReadLine();
					
					// Ignore all comments
					if (line.StartsWith("#", StringComparison.CurrentCulture)) {
						continue;
					}
					
					// Split line into different components
					string [] line_components = line.Split(' ');
					
					try {
						switch (line_components.Length) {
						case 2:
							nodes.Add(new Node(Convert.ToUInt32(line_components[0]), Convert.ToUInt32(line_components[1])));
							break;
						case 3:
							nodes.Add(new Node(Convert.ToUInt32(line_components[0]), Convert.ToUInt32(line_components[1]), line_components[2]));
							break;
						}
					} catch (FormatException e) {
						Debug.WriteLine("Problem with line " + line_number);
						return false;
					} catch (OverflowException e) {
						Debug.WriteLine("Problem with line " + line_number);
						return false;
					}
					
					line_number++;
					
				} while (reader.Peek() != -1);
				
				return true;
			} else {
				return false;
			}
		}
		
	}
}
