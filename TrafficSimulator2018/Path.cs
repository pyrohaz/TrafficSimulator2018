/*
 * Created by SharpDevelop.
 * User: harri
 * Date: 12/02/2017
 * Time: 20:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace TrafficSimulator2018
{
	/// <summary>
	/// Description of Path.
	/// </summary>
	public class Path
	{
		
		static int next_route_ID = 0;
		
		int route_ID = next_route_ID++;
		double speed_limit = 0;
		Node [] nodes = new Node[2];
		
		// Sets the start and end points and speed limit of the path.
		public Path(Node node1, Node node2, double speed_limit) {
			nodes[0] = node1;
			nodes[1] = node2;
			this.speed_limit = speed_limit;
		}
		
		// Returns an array that contains the start and end points of the path.
		public Node [] GetNodes() {
			return nodes;
		}
		
		// Returns the ID of the path.
		public int GetID() {
			return route_ID;
		}
		
		// Returns a double representing the speed limit of the path
		public double GetSpeedLimit()
		{
			return speed_limit;
		}
		
		// Sets the speed limit of the path
		public void SetSpeedLimit(double speed_limit) {
			this.speed_limit = speed_limit;
		}
		
	}
}
