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
		
		double speed_limit = 0;
		
		public Path()
		{
		}
		
		// Constructor that allows the speed limit to be set upon initialisation
		public Path(double speed_limit)
		{
			this.speed_limit = speed_limit;
		}
		
		// Returns a double representing the speed limit of the path
		public double getSpeedLimit()
		{
			return speed_limit;
		}
		
		// Sets the speed limit of the path
		public void setSpeedLimit(double speed_limit) {
			this.speed_limit = speed_limit;
		}
		
	}
}
