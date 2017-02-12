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
	/// Description of Map.
	/// </summary>
	public static class Map
	{
		
		static List<Node> nodes = new List<Node>();
		static List<Route> routes = new List<Route>();
		
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
		}
		
		// Returns a List<Route> object that contains all of the routes in the map.
		public static List<Route> getRoutes()
		{
			return routes;
		}
		
		public static List<Node> getNodes() {
			return nodes;
		}
		
		// Returns the node with the corresponding id. If no node exists with that ID, a message is displayed
		// and a null pointer is returned.
		public static Node getNode(int id)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes[0].getID() == id)
				{
					return nodes[0];
				}
			}
			
			// If the ID cannot be found
			Debug.WriteLine("Node with ID " + id + " cannot be found.");
			return null;
		}
		
		// Returns all of the routes that are connected to the specified node. This returns
		// null if the node does not exit.
		public static List<Route> getRoutesFromNode(int nodeID)
		{
			Node node = getNode(nodeID);
			return getRoutesFromNode(node);
		}
		
		// Returns all of the routes that are connected to the specified node. This returns
		// null if the node does not exit.
		public static List<Route> getRoutesFromNode(Node node)
		{
			
			// Returns null if the node does not exist.
			if (node == null)
				return null;
			
			List<Route> attachedRoutes = new List<Route>();
			
			loop: for (int i = 0; i < routes.Count; i++) {
				Node [] attachedNodes = routes[0].getNodes();
				
				for (int j = 0; i < attachedNodes.Length; i++) {
					if (attachedNodes[j] == node)
					{
						attachedRoutes.Add(routes[i]);
					}
				}
			}
			return attachedRoutes;
		}
		
	}
}
