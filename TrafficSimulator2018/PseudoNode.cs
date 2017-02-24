/*
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
	/// A PseudoNode is a "fake Node" that lies on a Path. It can be used to represent the positions of
	/// objects in the map, but is not itself a Node - it does not act as a junction between Nodes. It
	/// can be used as the start and end point of a Route.
	/// </summary>
	public class PseudoNode {
		
		Path path; // The path the the PseudoNode sits on
		double distance_along_path = 0;
		
		/// <summary>
		/// Sets up a PseudoNode with a given Path, and a distance along the Path from the first Node in the
		/// Path.GetNodes() array, to the second. If the distance is larger than the length of the Path, or
		/// if the distance is negative, the distance along the path will be set to 0.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="distance_along_path"></param>
		public PseudoNode(Path path, double distance_along_path) {
			SetPath(path, distance_along_path);
		}
		
		/// <summary>
		/// Sets up a PseudoNode as the same position as a Node in the Map. The initial Path will be set to
		/// be the first Path in the Map.GetPathsFromNode(node) method.
		/// </summary>
		/// <param name="node"></param>
		public PseudoNode(Node node) {
			path = Map.GetPathsFromNode(node)[0];
			distance_along_path = (path.GetNodes()[0] == node) ? 0 : path.GetDistance();
		}
		
		/// <summary>
		/// Sets the Path for the PseudoNode, and the distance along the Path from the first Node in the
		/// Path.GetNodes() array to the second. If the distance is greater than the lenght of the Path,
		/// or the distance is less than 0, the distance will be set to 0.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="distance_along_path"></param>
		public void SetPath(Path path, double distance_along_path) {
			this.path = path;
			SetDistanceAlongPath(distance_along_path);
		}
		
		/// <summary>
		/// Sets the distance along the current Path from the first Node in the Path.GetNodes() array to the
		/// second. If the distance is greater than the length of the Path or distance is less than 0, the
		/// distance will be set to 0.
		/// </summary>
		/// <param name="distance"></param>
		void SetDistanceAlongPath(double distance) {
			if (distance > path.GetDistance() || distance < 0) {
				distance_along_path = 0;
			} else {
				distance_along_path = distance;
			}
		}

		/// <summary>
		/// Returns the Path that the PseudoNode is on.
		/// </summary>
		/// <returns></returns>
		public Path GetPath() {
			return path;
		}
		
		/// <summary>
		/// Returns the distance along the Path from the first Node to the second Node in the Path.GetNodes()
		/// method.
		/// </summary>
		/// <returns></returns>
		public double GetDistanceAlongPath() {
			return distance_along_path;
		}
		
		/// <summary>
		/// Returns a double [] of size 2 that contains the x and y coordinates of the PseudoNode.
		/// </summary>
		/// <returns></returns>
		public double [] GetPosition() {
			double [] node1_position = path.GetNodes()[0].GetPosition();
			double [] node2_position = path.GetNodes()[1].GetPosition();
			
			double angle = Math.Atan2(node2_position[1] - node1_position[1], node2_position[0] - node1_position[0]);
			return new double[] {node1_position[0] + (distance_along_path * Math.Cos(angle)), node1_position[1] + (distance_along_path * Math.Sin(angle))};
		}
		
	}
}
