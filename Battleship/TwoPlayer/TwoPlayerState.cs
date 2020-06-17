using System.Collections.Generic;
using System.Xml.Serialization;

namespace Battleship.TwoPlayer
{
    public class TwoPlayerState
    {
        public string Winner { get; set; }
        public string Loser { get; set; }
        public int Round { get; set; }

        public int ShipsShunken { get; set; }
        //time

        public TwoPlayerState(string winner, string loser, int round, int shipsShunken)
        {
            this.Winner = winner;
            this.Loser = loser;
            this.Round = round;
            this.ShipsShunken = shipsShunken;
        }

        public TwoPlayerState() { }

        public void Save(string FileName, List<TwoPlayerState> state)
        {
            using (var writer = new System.IO.StreamWriter(FileName))
            {
                var serializer = new XmlSerializer(state.GetType());
                serializer.Serialize(writer, state);
                writer.Flush();
            }
        }

        public static List<TwoPlayerState> Load(string FileName)
        {
            using (var stream = System.IO.File.OpenRead(FileName))
            {
                var serializer = new XmlSerializer(typeof(List<TwoPlayerState>));
                return serializer.Deserialize(stream) as List<TwoPlayerState>;
            }
        }

    }
}
