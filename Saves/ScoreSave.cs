using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TkoUtilities.SavingManager;

namespace inflatedpufferfish.Saves;

public class ScoreSave : ISaveableEntity
{
    public int TotalScore { get; set; }

    public ScoreSave(int totalScore)
    {
        TotalScore = totalScore;
    }
}
