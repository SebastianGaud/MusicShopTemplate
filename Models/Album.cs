namespace MusicShopTemplate.Models
{
    public class Album
    {
        public string ID { get; set; }
        public string Titolo { get; set; }
        public string Cover { get; set; }

        public override string ToString ()
        {
            return Titolo;
        }
    }
}