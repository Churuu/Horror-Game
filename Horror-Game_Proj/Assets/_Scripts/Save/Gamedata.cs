using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Gamedata{

	/*
	https://www.sitepoint.com/saving-and-loading-player-game-data-in-unity/
	*/

	// PlayerInfo
	float x,y,z;
	float rx,ry,rz;

	//ai info
	float _x,_y,_z;
	float _rx, _ry, _rz;
	
	public Gamedata(float x, float y, float z, float rx, float ry, float rz, float _x, float _y, float _z, float _rx, float _ry, float _rz)
	{
		// Player info
		this.x = x;
		this.y = y;
		this.z = z;

		this.rx = rx;
		this.ry = ry;
		this.rz = rz;

		//AI info
		this._x = _x;
		this._y = _y;
		this._z = _z;

		this._rx = _rx;
		this._ry = _ry;
		this._rz = _rz;

	}
}
