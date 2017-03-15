namespace MusicShopTemplate.Models
{
    public class Traccia
    {
        public string ID { get; set; }
        public string Titolo { get; set; }
        public int NumeroTraccia { get; set; }
        public string AnteprimaUrlTraccia { get; set; }

        public override string ToString ()
        {
            return NumeroTraccia + " - " + Titolo;
        }
    }
}