using System.Xml;
using System.Xml.Linq;


namespace Assets.Scripts.Data
{
    public class UserData
    {
        private string path;

        private string name;
        private int rank;

        public UserData(string path)
        {
            this.path = path;
            Load();
        }

        public void Load()
        {
            XElement userdata = XElement.Load(path + "/userdata.xml");
            name = userdata.Element("Name").Value;
            rank = int.Parse(userdata.Element("Rank").Value);

        }

        public void Save()
        {

        }

        public string Name
        {
            get { return name; }
        }

        public int Rank
        {
            get { return rank; }
        }
    }
}
