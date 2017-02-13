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
	/// Description of Route.
	/// </summary>
	public class Route
	{
		
		public Route(Node start_node, Node end_node) {
			
			List<Node> nodes = Map.GetNodes();
			List<NodeAndTime> nodesAndTimes = new List<NodeAndTime>(nodes.Count);
			
			for (int i = 0; i < nodes.Count; i++) {
				nodesAndTimes.Add(new NodeAndTime(nodes[i]));
			}
			
		}
		
	}
	
	struct NodeAndTime {
		Node node;
		double time;
		
		public NodeAndTime(Node node) {
			this.node = node;
			time = Double.MaxValue;
		}
		
	}
	
}