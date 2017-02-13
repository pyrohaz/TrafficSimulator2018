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
		
		/// <summary>
		/// Returns a List<Person> of all Person objects.
		/// </summary>
		/// <returns></returns>
		public static List<Person> GetPeople() {
			return people;
		}
		
		/// <summary>
		/// Adds a Person object the the List of all Person objects.
		/// </summary>
		/// <param name="person"></param>
		public static void AddPerson(Person person) {
			people.Add(person);
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
