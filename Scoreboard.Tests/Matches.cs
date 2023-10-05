using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Tests
{
    public class Matches
    {
        public static Match MexicoCanada() => new Match(new Team("Mexico"), new Team("Canada"));
        public static Match UruguayItaly() => new Match(new Team("Uruguay"), new Team("Italy"));
    }
}
