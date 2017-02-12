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
	/// Description of Person.
	/// </summary>
	public class Person
	{
		static int next_person_id = 0;
		
		int person_ID = next_person_id++;
		string name;
		Path current_path;
		double distance_along_path = 0;
		Route route;
		
		// Creates a person with a start and end node. This will give the person a
		// default name.
		public Person(Node start_node, Node end_node) : this("Steve", start_node, end_node) {
		}
		
		// Creates a person with a start and end node and a name.
		public Person(string name, Node start_node, Node end_node) {
			this.name = name;
			route = new Route(start_node, end_node);
		}
		
		// Gets the path that the user is currently on.
		public Path GetCurrentPath() {
			return current_path;
		}
		
		// Returns the distance along the path from the first node in the path array
		// to the second (note that it's not in the direction of travel).
		public double GetDistanceAlongPath() {
			return distance_along_path;
		}
		
		// Returns the name of the person.
		public string GetName() {
			return name;
		}
		
		// Returns the ID of the person.
		public int GetPersonID() {
			return next_person_id;
		}
		
		// Returns the route for the person.
		public Route GetRoute() {
			return route;
		}
		
	}
}
