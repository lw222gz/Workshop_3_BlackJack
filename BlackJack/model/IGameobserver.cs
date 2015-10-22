using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    interface IGameobserver
    {
        void Playerhasacard();
        //Hämta från player,se om dom fått kort, listener i player.
        //Skicka till userinterface att man har fått ett nytt kort, och uppdatera handen, pausa programmet snabbt.
        //Paus ska skrivas i MOdel / controller , ej vyn
        //Paus ska hända när kort dras, oavsätt spelare eller dealer.
    }
}
