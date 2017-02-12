/*
 * Created by SharpDevelop.
 * User: harri
 * Date: 12/02/2017
 * Time: 20:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace TrafficSimulator2018
{
	/// <summary>
	/// Description of Route.
	/// </summary>
	public class Route
	{
		
		Node [] nodes = new Node[2];
		Path path;
		
		// Sets the start and end points for the node and defines the Path
		public Route(Node node1, Node node2, Path path)
		{
			nodes[0] = node1;
			nodes[1] = node2;
			this.path = path;
		}
		
		// Returns an array that contains the start and end points of the path
		public Node [] getNodes() {
			return nodes;
		}
		
		// Returns the Path object for this Route
		public Path getPath() {
			return path;
		}
	}
}
