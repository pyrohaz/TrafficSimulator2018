/*
 * Created by SharpDevelop.
 * User: Richard
 * Date: 24/02/2017
 * Time: 14:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace TrafficSimulator2018
{
	/// <summary>
	/// The Direction describes the direction in which a Path should be travelled
	/// down. Directions.FORWARDS implies that the direction of travel should be
	/// from Path.GetNodes()[0] to Path.GetNodes()[1], whereas backwards implies
	/// the opposite.
	/// </summary>
	public enum Direction {
		FORWARDS,
		BACKWARDS
	}
}
