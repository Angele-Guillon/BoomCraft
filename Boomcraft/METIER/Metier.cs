using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boomcraft.METIER
{
	public class Metier
	{
		public string Get_JoueurRessource(int iId)
		{
			//  TODO : Exploite la classe joueur et renvoie sous la forme d'un JSON les nom des différentes ressources ainsi que la quantité.

			return "'ressources': { 'wood': 100, 'food': 100, 'gold': 100, 'rock': 100 }";
		}
	}
}
