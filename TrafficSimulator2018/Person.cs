﻿/*
 * Created by SharpDevelop.
 * User: harri
 * Date: 12/02/2017
 * Time: 20:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace TrafficSimulator2018
{
	/// <summary>
	/// A Person is an object that navigates between the Nodes, along the Paths. They will be initialised
	/// at a Node, and have an desired end Node. Their Route will then be calculated so as to travel towards
	/// their desired location.
	/// </summary>
	public class Person
	{
		// Initialising ID. The next_id variable is incremented every time a new instance of Person is created.
		// Hence then each new Person gets a new ID.
		static int next_id = 0;
		int id = next_id++;
		
		string name;
		Path current_path; // TODO: implement the current path
		Node last_node_passed;
		double distance_along_path = 0;
		Route route;
		PseudoNode position;
		
		/// <summary>
		/// Creates a person with a start and end node. This will give the person a
		/// default name.
		/// </summary>
		/// <param name="start_node"></param>
		/// <param name="end_node"></param>
		public Person(Node start_node, Node end_node) : this("Steve", start_node, end_node) {}
		
		/// <summary>
		/// Creates a person with a start and end Node and a name.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="start_node"></param>
		/// <param name="end_node"></param>
		public Person(string name, Node start_node, Node end_node) : this("Steve", new PseudoNode(start_node), new PseudoNode(end_node)) {}
		
		/// <summary>
		/// Creates a Person with a start and end PseudoNode and a name.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="start_node"></param>
		/// <param name="end_node"></param>
		public Person (string name, PseudoNode start_node, PseudoNode end_node) {
			this.name = name;
			route = new Route(start_node, end_node);
			List<Node> node_route = route.GetNodeRoute();
			position = start_node;
		}
		
		/// <summary>
		/// Returns the Path that the user is currently on.
		/// </summary>
		/// <returns></returns>
		public Path GetCurrentPath() {
			return current_path;
		}
		
		/// <summary>
		/// Returns a double representing the distance along the path from the first node
		/// in the path array to the second (note that it's not in the direction of travel).
		/// </summary>
		/// <returns></returns>
		public double GetDistanceAlongPath() {
			return distance_along_path;
		}
		
		/// <summary>
		/// Returns a double representing the x coordinate of the Person's position.
		/// </summary>
		/// <returns></returns>
		public double GetX() {
			return position.GetPosition()[0];
		}
		
		/// <summary>
		/// Returns a double representing the y coordinate of the Person's position.
		/// </summary>
		/// <returns></returns>
		public double GetY() {
			return position.GetPosition()[1];
		}
		
		/// <summary>
		/// Returns a double [] of size 2, representing the x and y coordinates of the Person's
		/// position respectively.
		/// </summary>
		/// <returns></returns>
		public double [] GetPosition() {
			return position.GetPosition();
		}
		
		/// <summary>
		/// Returns a string representing the name of the Person.
		/// </summary>
		/// <returns></returns>
		public string GetName() {
			return name;
		}
		
		/// <summary>
		/// Returns an int representing the ID of the person.
		/// </summary>
		/// <returns></returns>
		public int GetID() {
			return id;
		}
		
		/// <summary>
		/// Returns the Route that the person is navigating.
		/// </summary>
		/// <returns></returns>
		public Route GetRoute() {
			return route;
		}
		
		/// <summary>
		/// This method will update the position of the Person. The number of seconds since the
		/// last update is required in order to calculate the Person's new position. If the
		/// Person reaches the next part of their Path, their current_path and last_node_passed
		/// values will be updated. If the Person reaches the end of their route, the Person will
		/// be deleted from the List in Persons.
		/// </summary>
		/// <param name="seconds_since_last_update"></param>
		public void update(double seconds_since_last_update) {
			// TODO: implement the update method
		}
		
		/// <summary>
		/// Returns a string that describes the Person.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return "Person " + id + ":\nName: " + name + "\n";
			//return "Person " + id + ":\nName: " + name + "\nCurrent Path: " + current_path.GetID() + "\nDistance along path: " + distance_along_path + "\n";
		}
		
	}
}
