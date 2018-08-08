using SQLite;

namespace TimeStamp.Resources.Model
{
    public class Tag
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string TagName { get; set; }

        public override string ToString()
        {
            return string.Format("[Tag: ID={0}, TagName={1}]", ID, TagName);
        }
    }
}