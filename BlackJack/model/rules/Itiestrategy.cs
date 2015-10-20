﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    interface ItieStrategy
    {
        bool CalcWinner(Player a_player, Dealer a_dealer);
    }
}
