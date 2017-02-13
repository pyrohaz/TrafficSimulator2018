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

namespace TrafficSimulator2018
{
	/// <summary>
	/// Description of People.
	/// </summary>
	public static class People
	{
		
		static List<Person> people = new List<Person>();
		
		static People()
		{
			people.Add(new Person(Map.GetNode(0), Map.GetNode(7)));
		}
		
		// Returns a List of all Person objects.
		public static List<Person> GetPeople() {
			return people;
		}
		
		// Adds a Person to the List of all Person objects.
		public static void AddPerson(Person person) {
			people.Add(person);
		}
		
		// Returns a string that describes the people objects.
		public static string ToString() {
			return "People:\nContains " + people.Count + " Persons.\n\n";
		}
		
	}
}
