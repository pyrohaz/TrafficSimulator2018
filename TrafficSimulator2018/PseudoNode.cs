﻿/*
 * Created by SharpDevelop.
 * User: Richard
 * Date: 14/02/2017
 * Time: 10:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace TrafficSimulator2018
{
	/// <summary>
	/// Description of PseudoNode.
	/// </summary>
	public class PseudoNode {
		
		Path path; // The path the the PseudoNode sits on
		double distance_along_path = 0;
		
		public PseudoNode(Path path, double distance_along_path) {
			SetPath(path, distance_along_path);
		}
		
		public PseudoNode(Node node) {
			path = Map.GetPathsFromNode(node)[0];
			distance_along_path = (path.GetNodes()[0] == node) ? 0 : path.GetDistance();
		}
		
		public void SetPath(Path path, double distance_along_path) {
			this.path = path;
			this.distance_along_path = distance_along_path;
		}

		public Path GetPath() {
			return path;
		}
		
		public double GetDistanceAlongPath() {
			return distance_along_path;
		}
		
	}
}
