using System.Collections.Generic;
using System.Xml.Serialization;

namespace Battleship
{
    public class OnePlayerState
    {
        public string PlayerName { get; set; }
        public bool Win { get; set; }
        //time
        public int Round { get; set; }

        public int ShipsShunken { get; set; }

        public OnePlayerState() { }

        public OnePlayerState(string playerName, bool win, int round, int shipsShunken)
        {
            this.PlayerName = playerName;
            this.Win = win;
            this.Round = round;
            this.ShipsShunken = shipsShunken;
        }

        public void Save(string FileName, List<OnePlayerState> state)
        {
            using (var writer = new System.IO.StreamWriter(FileName))
            {
                var serializer = new XmlSerializer(state.GetType());
                serializer.Serialize(writer, state);
                writer.Flush();
            }
        }

        public static List<OnePlayerState> Load(string FileName)
        {
            using (var stream = System.IO.File.OpenRead(FileName))
            {
                var serializer = new XmlSerializer(typeof(List<OnePlayerState>));
                return serializer.Deserialize(stream) as List<OnePlayerState>;
            }
        }
    }
}
