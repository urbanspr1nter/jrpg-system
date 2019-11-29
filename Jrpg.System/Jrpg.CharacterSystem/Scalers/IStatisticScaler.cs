using System.Collections.Generic;
namespace Jrpg.CharacterSystem.Scalers
{
    public interface IStatisticScaler
    {
        Statistic NewStatistic(Dictionary<StatisticType, Statistic> statistic);
        Statistic Copy(Statistic toCopy);
    }
}
