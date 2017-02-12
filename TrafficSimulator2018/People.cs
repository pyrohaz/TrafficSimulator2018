/*
 * Created by SharpDevelop.
 * User: richa
 * Date: 12/02/2017
 * Time: 23:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

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
		}
		
		public static List<People> GetPeople() {
			return people;
		}
		
		public static void addPerson(Person person) {
			people.add(person);
		}
		
	}
}
