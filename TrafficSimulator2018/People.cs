/*
 * Created by SharpDevelop.
 * User: richa
 * Date: 12/02/2017
 * Time: 23:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TrafficSimulator2018
{
	/// <summary>
	/// Contains references to all of the different Person objects that need to be rendered.
	/// </summary>
	public static class People
	{
		
		static List<Person> people = new List<Person>();
		
		// Sets up all People objects
		static People() {
			people.Add(new Person("Barry", new PseudoNode(Map.GetPathWithNodes(0,2), 15), new PseudoNode(Map.GetNode(7))));
		}
		
		/// <summary>
		/// Returns a List of all Person objects.
		/// </summary>
		/// <returns></returns>
		public static List<Person> GetPeople() {
			return people;
		}
		
		/// <summary>
		/// Adds a Person object the List of all Person objects.
		/// </summary>
		/// <param name="person"></param>
		public static void AddPerson(Person person) {
			people.Add(person);
		}
		
		/// <summary>
		/// Returns a Person with the given ID. If no Person with that ID exists, then the
		/// the result is null.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Person GetPerson(int id) {
			// Search through all people, looking for the given ID.
			foreach (Person person in people) {
				if (person.GetID() == id)
					return person;
			}
			return null; // Return null if the person is not found
		}
	
		/// <summary>
		/// Returns an int representing the number of People currently stored.
		/// </summary>
		/// <returns></returns>
		public static int GetNumberOfPeople() {
			return people.Count;
		}
		
	}
}
